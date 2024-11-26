using System;

namespace SuperMarioExample
{
    public class MarioStateLogger
    {
        public void LogStateTransition(Mario.State oldState, Mario.State newState, Mario.Transition transition)
        {
            Console.WriteLine($"State transition: {oldState} -> {newState} via {transition}");
        }
    }
}