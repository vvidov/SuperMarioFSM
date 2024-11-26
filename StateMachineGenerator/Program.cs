using System;
using System.CommandLine;
using System.IO;
using System.Threading.Tasks;

namespace StateMachineGenerator
{
    class Program
    {
        static async Task<int> Main(string[] args)
        {
            var inputOption = new Option<FileInfo>(
                name: "--input",
                description: "The input Mermaid file path")
            {
                IsRequired = true
            };

            var outputOption = new Option<DirectoryInfo>(
                name: "--output",
                description: "The output directory path")
            {
                IsRequired = true
            };

            var namespaceOption = new Option<string>(
                name: "--namespace",
                description: "The namespace for generated code",
                getDefaultValue: () => "StateMachine");

            var baseClassOption = new Option<string>(
                name: "--baseclass",
                description: "The base class name for the state machine",
                getDefaultValue: () => "StateMachine");

            var rootCommand = new RootCommand("State Machine Generator")
            {
                inputOption,
                outputOption,
                namespaceOption,
                baseClassOption
            };

            rootCommand.SetHandler(async (input, output, ns, baseClass) =>
            {
                try
                {
                    var mermaidContent = await File.ReadAllTextAsync(input.FullName);
                    var parser = new MermaidParser();
                    var definition = parser.ParseMermaid(mermaidContent);

                    var generator = new CodeGenerator(definition, ns, baseClass);
                    generator.GenerateCode(output.FullName);

                    Console.WriteLine("State machine code generated successfully!");
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error: {ex.Message}");
                    Environment.ExitCode = 1;
                }
            }, inputOption, outputOption, namespaceOption, baseClassOption);

            return await rootCommand.InvokeAsync(args);
        }
    }
}
