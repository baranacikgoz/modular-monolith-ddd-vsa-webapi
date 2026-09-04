using System.Collections;
using System.Reflection;
using Common.Domain.Events;
using Common.Domain.StronglyTypedIds;
using Xunit;

namespace Common.Tests.Architecture;

#pragma warning disable CA1515 // Consider making public types internal

/// <summary>
/// A shipped DomainEvent's payload is its own frozen contract (CLAUDE.md §5): AuditLog
/// deserializes rows back into the event's CLR type forever, by type name. This test enforces
/// the allow-list of property types a DomainEvent (or a nested snapshot type) may carry, so a
/// shared domain type (entity, aggregate root, ValueObject, or domain enum) nested inside an
/// event can never slip in unnoticed and start evolving that event's contract out from under it.
/// A domain enum is never referenced directly; each event nests its own snapshot enum instead.
/// </summary>
public sealed class DomainEventContractTests
{
    private static readonly HashSet<Type> _allowedBclTypes =
    [
        typeof(string), typeof(decimal), typeof(Guid), typeof(DateOnly), typeof(TimeOnly),
        typeof(DateTime), typeof(DateTimeOffset), typeof(TimeSpan), typeof(Uri),
    ];

    [Fact]
    public void DomainEvents_MustOnlyCarryFrozenPayloadTypes()
    {
        var eventTypes = SolutionAssemblies.All
            .SelectMany(GetLoadableTypes)
            .Where(t => t is { IsClass: true, IsAbstract: false } && typeof(DomainEvent).IsAssignableFrom(t))
            .ToList();

        var violations = new List<string>();

        foreach (var eventType in eventTypes)
        {
            Walk(eventType, eventType, [], violations);
        }

        Assert.True(
            violations.Count == 0,
            "DomainEvent payload contract violated (CLAUDE.md §5):\n" + string.Join("\n", violations));
    }

    private static void Walk(Type root, Type type, HashSet<Type> visited, List<string> violations)
    {
        if (!visited.Add(type))
        {
            return;
        }

        foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
        {
            var propertyType = Unwrap(property.PropertyType);

            if (IsAllowed(propertyType))
            {
                continue;
            }

            if (IsNestedInside(propertyType, root))
            {
                Walk(root, propertyType, visited, violations);
                continue;
            }

            violations.Add(
                $"{root.FullName}.{property.Name} carries {propertyType.FullName}. "
                + "Flatten the value onto the event, or nest a dedicated snapshot type (record or enum) inside the event itself.");
        }
    }

    private static Type Unwrap(Type type)
    {
        var underlying = Nullable.GetUnderlyingType(type);
        if (underlying is not null)
        {
            return underlying;
        }

        if (type != typeof(string) && type.IsGenericType && typeof(IEnumerable).IsAssignableFrom(type))
        {
            return Unwrap(type.GetGenericArguments()[0]);
        }

        return type;
    }

    private static bool IsAllowed(Type type)
    {
        return type.IsPrimitive
               || typeof(IStronglyTypedId).IsAssignableFrom(type)
               || _allowedBclTypes.Contains(type);
    }

    private static bool IsNestedInside(Type nestedType, Type root)
    {
        for (var declaring = nestedType.DeclaringType; declaring is not null; declaring = declaring.DeclaringType)
        {
            if (declaring == root)
            {
                return true;
            }
        }

        return false;
    }

    private static IEnumerable<Type> GetLoadableTypes(Assembly assembly)
    {
        try
        {
            return assembly.GetTypes();
        }
        catch (ReflectionTypeLoadException ex)
        {
            return ex.Types.Where(t => t is not null)!;
        }
    }
}

#pragma warning restore CA1515
