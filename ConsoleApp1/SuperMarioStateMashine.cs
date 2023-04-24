using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class SuperMarioStateMashine
    {
        private State _State = State.Mario;

        public State FSMState => _State;
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
            Feather,
            Flower
        }

        public State GetItem(Transition transition)
        {
            // next line must not be part of FSM, but it is used to log Mario in Console App
            var oldState = _State;
            _State = (_State, transition) switch 
            {
                (State.Mario, Transition.Mushroom) => State.SuperMario,
                (_, Transition.Feather) => State.CapeMario,
                (_, Transition.Flower) => State.FireMario,
                (_, _) => _State
            };
            // next line must not be part of FSM, but it is used to log Mario in Console App
            Console.WriteLine($"Mario was in '{oldState}' and got '{transition}' and now is in '{_State}'");
            return _State;
        }

    }
}
