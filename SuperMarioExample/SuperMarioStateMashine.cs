using SuperMarioExample.States;
using System;

namespace SuperMarioExample
{
    /// <summary>
    /// Implements a Finite State Machine for Super Mario power-up states
    /// </summary>
    public class SuperMarioStateMashine
    {
        private IMarioState _currentState;
        private readonly MarioStateLogger? _logger;

        /// <summary>
        /// Initializes a new instance of the SuperMarioStateMashine
        /// </summary>
        /// <param name="logger">Optional logger for state transitions</param>
        public SuperMarioStateMashine(MarioStateLogger? logger = null)
        {
            _currentState = new MarioState();
            _logger = logger;
        }

        /// <summary>
        /// Gets the current state enum value
        /// </summary>
        public State FSMState => _currentState.GetStateEnum();

        /// <summary>
        /// Represents the possible states Mario can be in
        /// </summary>
        public enum State
        {
            Mario,      // Basic Mario state
            SuperMario, // After getting a mushroom
            FireMario,  // After getting a fire flower
            CapeMario   // After getting a feather
        }

        /// <summary>
        /// Represents the possible power-up transitions
        /// </summary>
        public enum Transition
        {
            Mushroom, // Transforms Mario to Super Mario
            Feather,  // Transforms to Cape Mario
            Flower    // Transforms to Fire Mario
        }

        /// <summary>
        /// Processes a power-up item and transitions to the appropriate state
        /// </summary>
        /// <param name="transition">The power-up transition to process</param>
        /// <returns>The resulting state after the transition</returns>
        public State GetItem(Transition transition)
        {
            var oldState = _currentState.GetStateEnum();
            
            _currentState = transition switch
            {
                Transition.Mushroom => _currentState.HandleMushroom(),
                Transition.Feather => _currentState.HandleFeather(),
                Transition.Flower => _currentState.HandleFlower(),
                _ => throw new ArgumentException($"Invalid transition: {transition}")
            };
            
            _logger?.LogStateTransition(oldState, _currentState.GetStateEnum(), transition);
            return _currentState.GetStateEnum();
        }
    }
}
