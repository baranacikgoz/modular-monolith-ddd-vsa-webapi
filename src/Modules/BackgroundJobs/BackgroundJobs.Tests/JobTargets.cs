namespace BackgroundJobs.Tests;

/// <summary>
/// Dummy expression-tree targets for Hangfire job tests. Hangfire's
/// <c>Job.FromExpression</c> rejects non-public methods ("Only public methods
/// can be invoked in the background"), so these must stay public. They live on
/// a non-test class to avoid xUnit1013 (public method on a test class).
/// </summary>
public static class JobTargets
{
    public static void DummyMethod()
    {
        // Dummy method for expression tree
    }

    public static Task DummyTaskMethod()
    {
        // Dummy method for expression tree
        return Task.CompletedTask;
    }
}
