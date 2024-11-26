using System;
using System.IO;
using System.Linq;
using System.Text;

namespace StateMachineGenerator
{
    public class CodeGenerator
    {
        private readonly StateMachineDefinition _definition;
        private readonly string _namespace;
        private readonly string _baseClassName;

        public CodeGenerator(StateMachineDefinition definition, string namespaceName, string baseClassName)
        {
            _definition = definition;
            _namespace = namespaceName;
            _baseClassName = baseClassName;
        }

        public void GenerateCode(string outputPath)
        {
            var solutionName = _baseClassName + "StateMachine";
            
            // Create solution structure
            GenerateSolutionStructure(outputPath, solutionName);

            // Read mermaid content for README
            var mermaidContent = File.ReadAllText(_definition.InputPath);
            
            // Generate README
            GenerateReadme(outputPath, solutionName, mermaidContent);

            // Create source and test directories
            var libProjectPath = Path.Combine(outputPath, solutionName);
            var testProjectPath = Path.Combine(outputPath, solutionName + "Tests");
            
            Directory.CreateDirectory(Path.Combine(libProjectPath, "States"));
            Directory.CreateDirectory(Path.Combine(testProjectPath, "States"));

            // Generate source files
            GenerateInterface(libProjectPath);
            foreach (var state in _definition.States)
            {
                GenerateStateClass(libProjectPath, state);
            }
            GenerateStateMachineClass(libProjectPath);

            // Generate test files
            foreach (var state in _definition.States)
            {
                GenerateStateTests(testProjectPath, state);
            }

            // Generate logger class
            GenerateLogger(libProjectPath);
        }

        private void GenerateSolutionStructure(string outputPath, string solutionName)
        {
            var libProjectPath = Path.Combine(outputPath, $"{solutionName}");
            var testProjectPath = Path.Combine(outputPath, $"{solutionName}Tests");
            
            Directory.CreateDirectory(libProjectPath);
            Directory.CreateDirectory(testProjectPath);

            // Generate project GUIDs
            var mainProjectGuid = Guid.NewGuid();
            var testProjectGuid = Guid.NewGuid();

            // Generate solution file
            var solutionContent = $@"
Microsoft Visual Studio Solution File, Format Version 12.00
# Visual Studio Version 17
VisualStudioVersion = 17.0.31903.59
MinimumVisualStudioVersion = 10.0.40219.1
Project(""{{9A19103F-16F7-4668-BE54-9A1E7A4F7556}}"") = ""{solutionName}"", ""{solutionName}\{solutionName}.csproj"", ""{{{mainProjectGuid}}}""
EndProject
Project(""{{9A19103F-16F7-4668-BE54-9A1E7A4F7556}}"") = ""{solutionName}Tests"", ""{solutionName}Tests\{solutionName}Tests.csproj"", ""{{{testProjectGuid}}}""
EndProject
Global
	GlobalSection(SolutionConfigurationPlatforms) = preSolution
		Debug|Any CPU = Debug|Any CPU
		Release|Any CPU = Release|Any CPU
	EndGlobalSection
	GlobalSection(ProjectConfigurationPlatforms) = postSolution
		{{{mainProjectGuid}}}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{{{mainProjectGuid}}}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{{{mainProjectGuid}}}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{{{mainProjectGuid}}}.Release|Any CPU.Build.0 = Release|Any CPU
		{{{testProjectGuid}}}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{{{testProjectGuid}}}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{{{testProjectGuid}}}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{{{testProjectGuid}}}.Release|Any CPU.Build.0 = Release|Any CPU
	EndGlobalSection
EndGlobal";
            File.WriteAllText(Path.Combine(outputPath, $"{solutionName}.sln"), solutionContent);

            // Generate main project file
            var mainProjectContent = @"<Project Sdk=""Microsoft.NET.Sdk"">
  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>
</Project>";
            File.WriteAllText(Path.Combine(libProjectPath, $"{solutionName}.csproj"), mainProjectContent);

            // Generate test project file
            var testProjectContent = $@"<Project Sdk=""Microsoft.NET.Sdk"">
  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
    <IsPackable>false</IsPackable>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include=""Microsoft.NET.Test.Sdk"" Version=""17.8.0"" />
    <PackageReference Include=""NUnit"" Version=""4.0.1"" />
    <PackageReference Include=""NUnit3TestAdapter"" Version=""4.5.0"" />
    <PackageReference Include=""NUnit.Analyzers"" Version=""3.10.0"" />
    <PackageReference Include=""coverlet.collector"" Version=""6.0.0"" />
  </ItemGroup>

  <ItemGroup>
    <ProjectReference Include=""..\{solutionName}\{solutionName}.csproj"" />
  </ItemGroup>
</Project>";
            File.WriteAllText(Path.Combine(testProjectPath, $"{solutionName}Tests.csproj"), testProjectContent);
        }

        private void GenerateInterface(string outputPath)
        {
            var code = $@"namespace {_namespace}.States
{{
    public interface I{_baseClassName}State
    {{
        {string.Join("\n        ", _definition.Events.Select(e => $"I{_baseClassName}State Handle{e}();"))}
        
        {_baseClassName}.State GetStateEnum();
    }}
}}";
            File.WriteAllText(Path.Combine(outputPath, "States", $"I{_baseClassName}State.cs"), code);
        }

        private void GenerateStateClass(string outputPath, string stateName)
        {
            var transitions = _definition.Transitions
                .Where(t => t.FromState == stateName)
                .GroupBy(t => t.Event)
                .ToDictionary(g => g.Key, g => g.First().ToState);

            var stateTransitions = new StringBuilder();
            foreach (var evt in _definition.Events)
            {
                var toState = transitions.GetValueOrDefault(evt, stateName);
                stateTransitions.AppendLine($@"        public I{_baseClassName}State Handle{evt}()
        {{
            return new {toState}State();
        }}
");
            }

            var code = $@"namespace {_namespace}.States
{{
    public class {stateName}State : I{_baseClassName}State
    {{
{stateTransitions}
        public {_baseClassName}.State GetStateEnum()
        {{
            return {_baseClassName}.State.{stateName};
        }}
    }}
}}";
            File.WriteAllText(Path.Combine(outputPath, "States", $"{stateName}State.cs"), code);
        }

        private void GenerateStateMachineClass(string outputPath)
        {
            var code = $@"using {_namespace}.States;
using System;

namespace {_namespace}
{{
    public class {_baseClassName}
    {{
        private I{_baseClassName}State _currentState;
        private readonly MarioStateLogger _logger;

        public {_baseClassName}(MarioStateLogger logger = null)
        {{
            _currentState = new {_definition.InitialState}State();
            _logger = logger;
        }}

        public State FSMState => _currentState.GetStateEnum();

        public enum State
        {{
            {string.Join(",\n            ", _definition.States)}
        }}

        public enum Transition
        {{
            {string.Join(",\n            ", _definition.Events)}
        }}

        public State GetItem(Transition transition)
        {{
            var oldState = _currentState.GetStateEnum();
            
            _currentState = transition switch
            {{
                {string.Join(",\n                ", _definition.Events.Select(e => $"Transition.{e} => _currentState.Handle{e}()"))},
                _ => throw new ArgumentException($""Invalid transition: {{transition}}"")
            }};
            
            _logger?.LogStateTransition(oldState, _currentState.GetStateEnum(), transition);
            return _currentState.GetStateEnum();
        }}
    }}
}}";
            File.WriteAllText(Path.Combine(outputPath, $"{_baseClassName}.cs"), code);
        }

        private void GenerateTests(string outputPath)
        {
            foreach (var state in _definition.States)
            {
                GenerateStateTests(outputPath, state);
            }
        }

        private void GenerateStateTests(string outputPath, string stateName)
        {
            var transitions = _definition.Transitions
                .Where(t => t.FromState == stateName)
                .GroupBy(t => t.Event)
                .ToDictionary(g => g.Key, g => g.First().ToState);

            var testMethods = new StringBuilder();
            foreach (var evt in _definition.Events)
            {
                var toState = transitions.GetValueOrDefault(evt, stateName);
                testMethods.AppendLine($@"        [Test()]
        public void Handle{evt}_Returns_{toState}State()
        {{
            // Act
            var result = _{stateName.ToLower()}State.Handle{evt}();

            // Assert
            Assert.That(result, Is.TypeOf<{toState}State>());
            Assert.That(result.GetStateEnum(), Is.EqualTo({_baseClassName}.State.{toState}));
        }}
");
            }

            var code = $@"using NUnit.Framework;
using {_namespace};
using {_namespace}.States;

namespace {_namespace}Tests.States
{{
    [TestFixture()]
    public class {stateName}StateTests
    {{
        private {stateName}State _{stateName.ToLower()}State;

        [SetUp]
        public void Setup()
        {{
            _{stateName.ToLower()}State = new {stateName}State();
        }}

        [Test()]
        public void GetStateEnum_Returns_{stateName}()
        {{
            // Act
            var result = _{stateName.ToLower()}State.GetStateEnum();

            // Assert
            Assert.That(result, Is.EqualTo({_baseClassName}.State.{stateName}));
        }}

{testMethods}
    }}
}}";
            Directory.CreateDirectory(Path.Combine(outputPath, "Tests", "States"));
            File.WriteAllText(Path.Combine(outputPath, "Tests", "States", $"{stateName}StateTests.cs"), code);
        }

        private void GenerateLogger(string outputPath)
        {
            var code = $@"using System;

namespace {_namespace}
{{
    public class MarioStateLogger
    {{
        public void LogStateTransition({_baseClassName}.State oldState, {_baseClassName}.State newState, {_baseClassName}.Transition transition)
        {{
            Console.WriteLine($""State transition: {{oldState}} -> {{newState}} via {{transition}}"");
        }}
    }}
}}";
            File.WriteAllText(Path.Combine(outputPath, "MarioStateLogger.cs"), code);
        }

        private void GenerateReadme(string outputPath, string solutionName, string mermaidContent)
        {
            var readmeContent = $@"# {solutionName}

This is an automatically generated state machine implementation based on a Mermaid.js state diagram.

## Project Structure

- `{solutionName}/` - Main library project containing the state machine implementation
  - `States/` - Contains all state classes
    - `I{_baseClassName}State.cs` - Interface defining state behavior
    - Individual state implementation files
  - `{_baseClassName}.cs` - Main state machine class
  - `{_baseClassName}Logger.cs` - Logger for state transitions

- `{solutionName}Tests/` - Test project
  - `States/` - Contains test classes for each state

## State Machine Overview

The state machine implements the following states:
{string.Join("\n", _definition.States.Select(s => $"- `{s}`"))}

Available transitions:
{string.Join("\n", _definition.Events.Select(e => $"- `{e}`"))}

## Getting Started

1. Open the solution in Visual Studio 2022 or later
2. Build the solution
3. Run the tests to verify the implementation

## Usage Example

```csharp
using {_namespace};
using {_namespace}.States;

// Create a new instance with optional logger
var logger = new {_baseClassName}Logger();
var {_baseClassName.ToLower()} = new {_baseClassName}(logger);

// Get current state
var currentState = {_baseClassName.ToLower()}.FSMState;

// Perform state transitions
{_baseClassName.ToLower()}.GetItem({_baseClassName}.Transition.{_definition.Events.First()});
```

## Testing

The solution includes a comprehensive test suite using NUnit. Each state has its own test class verifying:
- Initial state
- Valid state transitions
- State enum values

Run the tests using Visual Studio's Test Explorer or via command line:
```bash
dotnet test
```

## Generated Code Information

This state machine was automatically generated using the State Machine Generator tool.
- Input: Mermaid.js state diagram
- Namespace: `{_namespace}`
- Base Class: `{_baseClassName}`

## State Diagram

```mermaid
{mermaidContent}
```

## License

This is auto-generated code. Feel free to modify and use it as needed in your project.
";
            File.WriteAllText(Path.Combine(outputPath, "README.md"), readmeContent);
        }
    }
}
