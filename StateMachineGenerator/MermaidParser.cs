using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace StateMachineGenerator
{
    public class StateTransition
    {
        public required string FromState { get; set; }
        public required string ToState { get; set; }
        public required string Event { get; set; }
    }

    public class StateMachineDefinition
    {
        public HashSet<string> States { get; set; } = new();
        public HashSet<string> Events { get; set; } = new();
        public List<StateTransition> Transitions { get; set; } = new();
        public required string InitialState { get; set; }
        public required string InputPath { get; set; }
    }

    public class MermaidParser
    {
        private static readonly Regex StateTransitionRegex = new(@"^\s*(\w+)\s*-->\s*(\w+)\s*:\s*(\w+)\s*$", RegexOptions.Compiled);
        private static readonly Regex InitialStateRegex = new(@"^\[*\]\s*-->\s*(\w+)\s*$", RegexOptions.Compiled);

        public StateMachineDefinition ParseMermaid(string mermaidContent)
        {
            var definition = new StateMachineDefinition
            {
                InitialState = string.Empty, // Temporary value that will be updated during parsing
                InputPath = string.Empty // Will be set after parsing
            };

            foreach (var line in mermaidContent.Split('\n'))
            {
                var trimmedLine = line.Trim();
                if (string.IsNullOrWhiteSpace(trimmedLine))
                    continue;

                var stateTransitionMatch = StateTransitionRegex.Match(trimmedLine);
                if (stateTransitionMatch.Success)
                {
                    var fromState = stateTransitionMatch.Groups[1].Value;
                    var toState = stateTransitionMatch.Groups[2].Value;
                    var evt = stateTransitionMatch.Groups[3].Value;

                    definition.States.Add(fromState);
                    definition.States.Add(toState);
                    definition.Events.Add(evt);
                    definition.Transitions.Add(new StateTransition { FromState = fromState, ToState = toState, Event = evt });
                    continue;
                }

                var initialStateMatch = InitialStateRegex.Match(trimmedLine);
                if (initialStateMatch.Success)
                {
                    definition.InitialState = initialStateMatch.Groups[1].Value;
                    definition.States.Add(definition.InitialState);
                }
            }

            if (definition.InitialState == string.Empty)
            {
                throw new InvalidOperationException("No initial state found in Mermaid diagram");
            }

            return definition;
        }
    }
}
