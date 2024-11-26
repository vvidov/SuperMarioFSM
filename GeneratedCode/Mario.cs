using MyStateMachine.States;
using System;

namespace MyStateMachine
{
    public class Mario
    {
        private IMarioState _currentState;
        private readonly MarioLogger _logger;

        public Mario(MarioLogger logger = null)
        {
            _currentState = new MarioState();
            _logger = logger;
        }

        public State FSMState => _currentState.GetStateEnum();

        public enum State
        {
            Mario,
            SuperMario,
            FireMario,
            CapeMario
        }

        public enum Transition
        {
            Mushroom,
            Flower,
            Feather
        }

        public State GetItem(Transition transition)
        {
            var oldState = _currentState.GetStateEnum();
            
            _currentState = transition switch
            {
                Transition.Mushroom => _currentState.HandleMushroom(),
                Transition.Flower => _currentState.HandleFlower(),
                Transition.Feather => _currentState.HandleFeather()
                _ => throw new ArgumentException($"Invalid transition: {transition}")
            };
            
            _logger?.LogStateTransition(oldState, _currentState.GetStateEnum(), transition);
            return _currentState.GetStateEnum();
        }
    }
}