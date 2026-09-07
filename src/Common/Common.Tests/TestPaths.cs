namespace Common.Tests;

/// <summary>Repository-relative files that tests feed to containers (e.g. the Keycloak realm export).</summary>
public static class TestPaths
{
    public const string RealmRelativePath = "keycloak/realm-modular-monolith.json";

    /// <summary>Absolute path of <c>keycloak/realm-modular-monolith.json</c>, located by walking up from the test binaries.</summary>
    public static string RealmFile { get; } = Locate(RealmRelativePath);

    private static string Locate(string relativePath)
    {
        var directory = new DirectoryInfo(AppContext.BaseDirectory);
        while (directory is not null)
        {
            var candidate = Path.Combine(directory.FullName, relativePath);
            if (File.Exists(candidate))
            {
                return candidate;
            }

            directory = directory.Parent;
        }

        throw new FileNotFoundException(
            $"'{relativePath}' not found in any parent of '{AppContext.BaseDirectory}'. Tests must run from the repository checkout.");
    }
}
