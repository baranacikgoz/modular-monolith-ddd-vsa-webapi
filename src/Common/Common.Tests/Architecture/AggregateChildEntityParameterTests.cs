using System.Reflection;
using Common.Domain.Aggregates;
using Common.Domain.Entities;
using Common.Domain.StronglyTypedIds;
using Xunit;

namespace Common.Tests.Architecture;

#pragma warning disable CA1515 // Consider making public types internal

/// <summary>
/// An aggregate method takes its child entity, never the child's id (CLAUDE.md §4). A signature like
/// <c>Unmatch(ChildId id)</c> followed by <c>_children.SingleOrDefault(c => c.Id == id)</c> re-implements a
/// lookup the caller has already paid for (it loaded the aggregate with its children to get here) and buries
/// NotFound inside the aggregate instead of the functional pipeline. The caller resolves the child from the
/// loaded aggregate (<c>aggregate.Children.SingleAsResult(c => c.Id == id)</c>) and hands the object in, so
/// the aggregate method only ever sees a child that exists.
/// </summary>
public sealed class AggregateChildEntityParameterTests
{
    /// <summary>
    /// Proves the detector on an in-memory fixture before trusting it against the solution: a child id must be
    /// flagged whether it arrives bare, nullable, inside a collection, through an internal method or a static
    /// factory; the child object itself, the aggregate's own id, a raw Guid and the id of a referenced lookup
    /// entity (single navigation plus foreign key, owned elsewhere) must pass.
    /// </summary>
    [Fact]
    public void Detector_FlagsChildIdParameters_AndIgnoresChildObjectsAndOtherIds()
    {
        var violations = AggregateChildEntityParameterDetector.FindViolations([typeof(FixtureAggregate)]);

        string[] expected =
        [
            nameof(FixtureAggregate.BadFactory),
            nameof(FixtureAggregate.RemoveChild),
            nameof(FixtureAggregate.RemoveChildIfAny),
            nameof(FixtureAggregate.RemoveChildInternal),
            nameof(FixtureAggregate.RemoveChildren),
        ];

        Assert.Equal(expected, violations.Select(v => v.MemberName).Order(StringComparer.Ordinal).ToArray());
        Assert.All(violations, v => Assert.Equal(typeof(FixtureChild), v.ChildType));

        // The unflagged members are real code paths, not decoration: they run, so the fixture stays honest.
        var aggregate = FixtureAggregate.BadFactory(new FixtureChildId(DefaultIdType.NewGuid()));
        Assert.True(aggregate.Is(aggregate.Id));
        Assert.True(aggregate.HasChildWithRawId(aggregate.Children.Single().Id.Value));
        aggregate.AssignLookup(new FixtureLookupId(DefaultIdType.NewGuid()));
        Assert.Equal(aggregate.LookupId, aggregate.Lookup!.Id);
    }

    /// <summary>The real scan: no aggregate in the solution may take one of its own child entities' ids.</summary>
    [Fact]
    public void AggregateMethods_MustTakeChildEntities_NotChildIds()
    {
        var aggregates = SolutionAssemblies.All
            .SelectMany(SolutionAssemblies.LoadableTypes)
            .Where(AggregateChildEntityParameterDetector.IsAggregateRoot)
            .ToList();

        Assert.True(
            aggregates.Count > 0,
            "No AggregateRoot<TId> subclass was discovered in the test run's output directory: assembly discovery "
            + "regressed and the scan is silently checking nothing.");

        var violations = AggregateChildEntityParameterDetector.FindViolations(aggregates);

        Assert.True(
            violations.Count == 0,
            "An aggregate method takes the id of one of its own child entities (CLAUDE.md §4). Take the child "
            + "entity itself: the caller already loaded the aggregate with its children, so it resolves the child "
            + "there (aggregate.Children.SingleAsResult(c => c.Id == id)) inside the pipeline and NotFound stays a "
            + "Result; the aggregate never re-implements a lookup.\n"
            + string.Join('\n', violations));
    }

    private readonly record struct FixtureAggregateId(DefaultIdType Value) : IStronglyTypedId;

    private readonly record struct FixtureChildId(DefaultIdType Value) : IStronglyTypedId;

    private readonly record struct FixtureLookupId(DefaultIdType Value) : IStronglyTypedId;

    private sealed class FixtureChild(FixtureChildId id) : AuditableEntity<FixtureChildId>(id);

    private sealed class FixtureLookup(FixtureLookupId id) : AuditableEntity<FixtureLookupId>(id);

    private sealed class FixtureAggregate() : AggregateRoot<FixtureAggregateId>(new FixtureAggregateId(DefaultIdType.NewGuid()))
    {
        private readonly List<FixtureChild> _children = [];

        public IReadOnlyCollection<FixtureChild> Children => _children.AsReadOnly();

        // A referenced entity owned elsewhere (EF navigation plus foreign key), not a child of this aggregate.
        public FixtureLookupId LookupId { get; private set; }

        public FixtureLookup? Lookup { get; private set; }

        public static FixtureAggregate BadFactory(FixtureChildId firstChildId) // VIOLATION
        {
            var aggregate = new FixtureAggregate();
            aggregate._children.Add(new FixtureChild(firstChildId));
            return aggregate;
        }

        public void RemoveChild(FixtureChildId childId) // VIOLATION
        {
            _children.RemoveAll(c => c.Id == childId);
        }

        internal void RemoveChildInternal(FixtureChildId childId) // VIOLATION
        {
            _children.RemoveAll(c => c.Id == childId);
        }

        public void RemoveChildIfAny(FixtureChildId? childId) // VIOLATION
        {
            _children.RemoveAll(c => c.Id == childId);
        }

        public void RemoveChildren(IReadOnlyCollection<FixtureChildId> childIds) // VIOLATION
        {
            _children.RemoveAll(c => childIds.Contains(c.Id));
        }

        public void RemoveChild(FixtureChild child)
        {
            _children.Remove(child);
        }

        public bool Is(FixtureAggregateId aggregateId)
        {
            return Id == aggregateId;
        }

        public bool HasChildWithRawId(DefaultIdType rawId)
        {
            return _children.Exists(c => c.Id.Value == rawId);
        }

        public void AssignLookup(FixtureLookupId lookupId)
        {
            LookupId = lookupId;
            Lookup = new FixtureLookup(lookupId);
        }
    }
}

/// <summary>One flagged parameter: a non-private method or constructor declared on an aggregate takes the id of one of that aggregate's own child entities.</summary>
internal readonly record struct AggregateChildEntityParameterViolation(
    Type Aggregate,
    string MemberName,
    string ParameterName,
    Type ChildIdType,
    Type ChildType)
{
    public override string ToString()
    {
        return $"{Aggregate.FullName}.{MemberName}({ParameterName}: {ChildIdType.Name}) takes the id of child entity "
               + $"{ChildType.Name}; take a {ChildType.Name} instead.";
    }
}

/// <summary>
/// A child entity of an aggregate is any non-aggregate <see cref="AuditableEntity{TId}"/> that is the element type
/// of one of the aggregate's own collection fields or properties. A single navigation property with a foreign key
/// (<c>Product.ProductTemplate</c>) references an entity owned elsewhere and is not a child: its id is a legitimate
/// parameter. A violation is any
/// non-private method or constructor declared on the aggregate whose parameter carries such a child's id type,
/// bare or wrapped (nullable, by-ref, array, or any generic type argument such as a collection or a tuple).
/// Ids of other aggregates and raw <see cref="DefaultIdType"/> values are never flagged.
/// </summary>
internal static class AggregateChildEntityParameterDetector
{
    public static bool IsAggregateRoot(Type type)
    {
        return type is { IsClass: true, IsAbstract: false } && FindGenericBase(type, typeof(AggregateRoot<>)) is not null;
    }

    public static List<AggregateChildEntityParameterViolation> FindViolations(IEnumerable<Type> aggregates)
    {
        var violations = new List<AggregateChildEntityParameterViolation>();

        foreach (var aggregate in aggregates)
        {
            var childrenByIdType = ChildEntityTypes(aggregate).ToDictionary(IdTypeOf, child => child);
            if (childrenByIdType.Count == 0)
            {
                continue;
            }

            foreach (var member in NonPrivateDeclaredMembers(aggregate))
            {
                foreach (var parameter in member.GetParameters())
                {
                    foreach (var leaf in Leaves(parameter.ParameterType).Distinct())
                    {
                        if (childrenByIdType.TryGetValue(leaf, out var child))
                        {
                            violations.Add(new AggregateChildEntityParameterViolation(
                                aggregate, member.Name, parameter.Name ?? "?", leaf, child));
                        }
                    }
                }
            }
        }

        return violations;
    }

    private static IEnumerable<MethodBase> NonPrivateDeclaredMembers(Type aggregate)
    {
        const BindingFlags declared = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;

        var methods = aggregate.GetMethods(declared)
            .Where(m => !m.IsPrivate && !m.IsSpecialName) // property accessors and operators are not command methods
            .Cast<MethodBase>();

        var constructors = aggregate.GetConstructors(declared & ~BindingFlags.Static)
            .Where(c => !c.IsPrivate)
            .Cast<MethodBase>();

        return methods.Concat(constructors);
    }

    private static IEnumerable<Type> ChildEntityTypes(Type aggregate)
    {
        const BindingFlags declared = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly;
        var memberTypes = new List<Type>();

        // Walk the aggregate's own hierarchy but stop at AggregateRoot<TId>: its DomainEvent list is not a child.
        for (var type = aggregate; type is not null && FindGenericBase(type, typeof(AggregateRoot<>)) != type; type = type.BaseType)
        {
            memberTypes.AddRange(type.GetFields(declared).Select(f => f.FieldType));
            memberTypes.AddRange(type.GetProperties(declared).Select(p => p.PropertyType));
        }

        return memberTypes.SelectMany(CollectionElementTypes).SelectMany(Leaves).Where(IsChildEntity).Distinct();
    }

    /// <summary>The element type of an array or of any <c>IEnumerable&lt;T&gt;</c> (never <c>string</c>); nothing for a scalar member.</summary>
    private static IEnumerable<Type> CollectionElementTypes(Type type)
    {
        if (type.IsArray)
        {
            return [type.GetElementType()!];
        }

        if (type == typeof(string))
        {
            return [];
        }

        return type.GetInterfaces().Append(type)
            .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>))
            .Select(i => i.GetGenericArguments()[0]);
    }

    private static bool IsChildEntity(Type type)
    {
        return type.IsClass
               && FindGenericBase(type, typeof(AuditableEntity<>)) is not null
               && FindGenericBase(type, typeof(AggregateRoot<>)) is null;
    }

    private static Type IdTypeOf(Type child)
    {
        return FindGenericBase(child, typeof(AuditableEntity<>))!.GetGenericArguments()[0];
    }

    /// <summary>Every concrete type a declared type can carry: itself, or what its nullable/by-ref/array/generic-argument wrappers unwrap to.</summary>
    private static IEnumerable<Type> Leaves(Type type)
    {
        if (type.IsByRef || type.IsArray)
        {
            return Leaves(type.GetElementType()!);
        }

        if (Nullable.GetUnderlyingType(type) is { } underlying)
        {
            return Leaves(underlying);
        }

        if (type.IsGenericType && type != typeof(string))
        {
            return type.GetGenericArguments().SelectMany(Leaves);
        }

        return [type];
    }

    private static Type? FindGenericBase(Type type, Type genericDefinition)
    {
        for (var current = type; current is not null; current = current.BaseType)
        {
            if (current.IsGenericType && current.GetGenericTypeDefinition() == genericDefinition)
            {
                return current;
            }
        }

        return null;
    }
}

#pragma warning restore CA1515
