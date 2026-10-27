using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Common.Domain.ResultMonad;
using Common.Tests;
using Inventory.Application.BulkAdjustments;
using Inventory.Domain.StockLevels;
using Inventory.Domain.StockReservations;
using Inventory.Domain.StockReservations.Errors;
using Inventory.Domain.WarehouseConnections;
using Xunit;

namespace Inventory.Tests.Architecture;

/// <summary>
///     An error the API answers with is localized by its key (<c>IStringLocalizer</c> returns the bare key when the resource
///     has none, so a missing entry never fails, it only shows a key to the user). Every key an Inventory error class declares
///     has to have an entry with a text in both resource files.
/// </summary>
public partial class ErrorKeyLocalizationTests
{
    private const string ResourceFolder = "src/Common/Common.Application/Localization/Resources";

    private static readonly Assembly[] Assemblies =
    [
        typeof(StockReservationErrors).Assembly,
        typeof(StockBulkAdjustErrors).Assembly
    ];

    public static TheoryData<string> ResourceFiles => ["ResxLocalizer.resx", "ResxLocalizer.en.resx"];

    [Theory]
    [MemberData(nameof(ResourceFiles))]
    public void InventoryErrorClasses_EveryDeclaredKey_HasATextInTheResourceFile(string resourceFile)
    {
        var texts = TextsOf(resourceFile);

        var missing = DeclaredKeys()
            .Where(declared => !texts.TryGetValue(declared.Key, out var text) || string.IsNullOrWhiteSpace(text))
            .Select(declared => $"{declared.Key} ({declared.Source})")
            .Order(StringComparer.Ordinal)
            .ToList();

        Assert.True(missing.Count == 0, $"{resourceFile} has no text for: {string.Join(", ", missing)}");
    }

    // A not-found answer is "<entity name> <NotFound text>": the entity name is a resource key too, and without an entry the
    // client reads the bare key. The names come from the calls that build the error, not from an error class.
    [Theory]
    [MemberData(nameof(ResourceFiles))]
    public void InventorySource_EveryNotFoundEntityName_HasATextInTheResourceFile(string resourceFile)
    {
        var texts = TextsOf(resourceFile);

        var missing = NotFoundEntityNames()
            .Where(name => !texts.TryGetValue(name, out var text) || string.IsNullOrWhiteSpace(text))
            .Order(StringComparer.Ordinal)
            .ToList();

        Assert.True(missing.Count == 0, $"{resourceFile} has no text for the not-found entity names: {string.Join(", ", missing)}");
    }

    [Fact]
    public void InventorySource_NotFoundEntityNames_AreFound()
    {
        var names = NotFoundEntityNames().ToList();

        Assert.Contains(nameof(StockReservation), names);
        Assert.Contains(nameof(StockLevel), names);
        Assert.Contains(nameof(WarehouseConnection), names);
        Assert.Contains("WarehouseReference", names);
    }

    [Fact]
    public void InventoryErrorClasses_AreFound()
    {
        var sources = DeclaredKeys().Select(declared => declared.Source.Split('.')[0]).Distinct().ToList();

        Assert.Contains(nameof(StockReservationErrors), sources);
        Assert.Contains(nameof(StockBulkAdjustErrors), sources);
        Assert.True(DeclaredKeys().Count() > 20);
    }

    /// <summary>
    ///     The entity names Inventory's production code hands to <c>Error.NotFound</c>, directly or through
    ///     <c>SingleAsResultAsync</c> and <c>FirstAsResultAsync</c>, written as a literal or as <c>nameof(Type)</c>.
    /// </summary>
    private static IEnumerable<string> NotFoundEntityNames()
    {
        var root = TestPaths.RepositoryRoot;
        var call = NotFoundCall();

        return Directory
            .EnumerateFiles(Path.Combine(root, "src", "Modules", "Inventory"), "*.cs", SearchOption.AllDirectories)
            .Select(path => Path.GetRelativePath(root, path).Replace(Path.DirectorySeparatorChar, '/'))
            .Where(path => !path.Contains("/obj/", StringComparison.Ordinal)
                           && !path.Contains("/bin/", StringComparison.Ordinal)
                           && !path.Contains("/Migrations/", StringComparison.Ordinal)
                           && !path.Contains("Inventory.Tests/", StringComparison.Ordinal))
            .SelectMany(path => call.Matches(File.ReadAllText(Path.Combine(root, path))))
            .Select(match => match.Groups["literal"].Success ? match.Groups["literal"].Value : match.Groups["type"].Value)
            .Distinct(StringComparer.Ordinal);
    }

    [GeneratedRegex("""(?:NotFound|SingleAsResultAsync|FirstAsResultAsync)\(\s*(?:"(?<literal>[^"]+)"|nameof\((?:[A-Za-z0-9_]+\.)*(?<type>[A-Za-z0-9_]+)\))""")]
    private static partial Regex NotFoundCall();

    private static Dictionary<string, string> TextsOf(string resourceFile)
    {
        var path = Path.Combine(TestPaths.RepositoryRoot, ResourceFolder, resourceFile);

        return XDocument.Load(path).Root!
            .Elements("data")
            .ToDictionary(
                data => (string)data.Attribute("name")!,
                data => (string?)data.Element("value") ?? string.Empty,
                StringComparer.Ordinal);
    }

    /// <summary>
    ///     The keys of every static class named <c>*Errors</c> in the module's Domain and Application assemblies: its
    ///     <see cref="Error" /> fields and factory methods (called with default arguments, the key does not depend on them), the
    ///     parameter name an error carries, and the public string constants, which are failure keys a job row stores.
    /// </summary>
    private static IEnumerable<(string Key, string Source)> DeclaredKeys()
    {
        var holders = Assemblies
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => type is { IsAbstract: true, IsSealed: true } && type.Name.EndsWith("Errors", StringComparison.Ordinal))
            .Order(Comparer<Type>.Create((a, b) => string.CompareOrdinal(a.FullName, b.FullName)));

        foreach (var holder in holders)
        {
            foreach (var field in holder.GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                if (field.FieldType == typeof(Error))
                {
                    foreach (var key in KeysOf((Error)field.GetValue(null)!))
                    {
                        yield return (key, $"{holder.Name}.{field.Name}");
                    }
                }
                else if (field is { IsLiteral: true } && field.FieldType == typeof(string))
                {
                    yield return ((string)field.GetRawConstantValue()!, $"{holder.Name}.{field.Name}");
                }
            }

            foreach (var method in holder.GetMethods(BindingFlags.Public | BindingFlags.Static).Where(m => m.ReturnType == typeof(Error)))
            {
                var arguments = method.GetParameters()
                    .Select(parameter => parameter.ParameterType.IsValueType ? Activator.CreateInstance(parameter.ParameterType) : null)
                    .ToArray();

                foreach (var key in KeysOf((Error)method.Invoke(null, arguments)!))
                {
                    yield return (key, $"{holder.Name}.{method.Name}");
                }
            }
        }
    }

    private static IEnumerable<string> KeysOf(Error error)
    {
        yield return error.Key;

        if (error.ParameterName is not null)
        {
            yield return error.ParameterName;
        }
    }
}
