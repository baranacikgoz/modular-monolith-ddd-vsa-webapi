using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace Common.SourceGenerators;

/// <summary>
/// For testing purposes to see source generation in action.
/// </summary>
[Generator]
public class HelloWorldGenerator : ISourceGenerator
{
    public void Initialize(GeneratorInitializationContext context)
    {
        // Initialization logic if needed
    }

    public void Execute(GeneratorExecutionContext context)
    {
        // Define the source code to be generated
        var source = @"
using System;

namespace SourceGenerated;

public static class HelloWorld
{
    public static void SayHello()
    {
        Console.WriteLine(""Hello, World!"");
    }
}
";
        // Add the source code to the compilation
        context.AddSource("HelloWorldGenerator.g.cs", SourceText.From(source, Encoding.UTF8));
    }
}
