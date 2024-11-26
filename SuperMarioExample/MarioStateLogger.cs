using System;

namespace SuperMarioExample
{
    public abstract class MarioStateLogger
    {
        public virtual void LogStateTransition(SuperMarioStateMashine.State oldState, 
                                            SuperMarioStateMashine.State newState, 
                                            SuperMarioStateMashine.Transition transition)
        {
            Console.WriteLine($"Mario was in '{oldState}' and got '{transition}' and now is in '{newState}'");
        }
    }

    public class ConsoleMarioStateLogger : MarioStateLogger
    {
    }
}
