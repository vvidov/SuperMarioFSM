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

        public State SMState => _State;
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
            _State = (_State, transition) switch 
            {
                (State.Mario, Transition.Mushroom) => State.SuperMario,
                (_, Transition.Feather) => State.CapeMario,
                (_, Transition.Flower) => State.FireMario,
                (_, _) => _State
            };
            return _State;
        }

    }
}
