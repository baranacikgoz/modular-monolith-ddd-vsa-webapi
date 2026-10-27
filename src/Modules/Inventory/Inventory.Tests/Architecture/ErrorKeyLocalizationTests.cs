using System.Reflection;
using System.Xml.Linq;
using Common.Domain.ResultMonad;
using Common.Tests;
using Inventory.Application.BulkAdjustments;
using Inventory.Domain.StockReservations.Errors;
using Xunit;

namespace Inventory.Tests.Architecture;

/// <summary>
///     An error the API answers with is localized by its key (<c>IStringLocalizer</c> returns the bare key when the resource
///     has none, so a missing entry never fails, it only shows a key to the user). Every key an Inventory error class declares
///     has to have an entry with a text in both resource files.
/// </summary>
public class ErrorKeyLocalizationTests
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

    [Fact]
    public void InventoryErrorClasses_AreFound()
    {
        var sources = DeclaredKeys().Select(declared => declared.Source.Split('.')[0]).Distinct().ToList();

        Assert.Contains(nameof(StockReservationErrors), sources);
        Assert.Contains(nameof(StockBulkAdjustErrors), sources);
        Assert.True(DeclaredKeys().Count() > 20);
    }

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
