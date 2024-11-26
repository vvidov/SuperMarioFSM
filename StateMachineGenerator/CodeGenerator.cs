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
            Directory.CreateDirectory(outputPath);
            Directory.CreateDirectory(Path.Combine(outputPath, "States"));

            GenerateInterface(outputPath);
            foreach (var state in _definition.States)
            {
                GenerateStateClass(outputPath, state);
            }
            GenerateStateMachineClass(outputPath);
            GenerateTests(outputPath);
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
        private readonly {_baseClassName}Logger _logger;

        public {_baseClassName}({_baseClassName}Logger logger = null)
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
                {string.Join(",\n                ", _definition.Events.Select(e => $"Transition.{e} => _currentState.Handle{e}()"))}
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
            Assert.IsInstanceOfType(result, typeof({toState}State));
            Assert.AreEqual({_baseClassName}.State.{toState}, result.GetStateEnum());
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
            Assert.AreEqual({_baseClassName}.State.{stateName}, result);
        }}

{testMethods}
    }}
}}";
            Directory.CreateDirectory(Path.Combine(outputPath, "Tests", "States"));
            File.WriteAllText(Path.Combine(outputPath, "Tests", "States", $"{stateName}StateTests.cs"), code);
        }
    }
}
