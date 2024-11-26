namespace SuperMarioExample.States
{
    /// <summary>
    /// Represents the basic Mario state
    /// </summary>
    public class MarioState : IMarioState
    {
        public IMarioState HandleMushroom()
        {
            return new SuperMarioState();
        }

        public IMarioState HandleFeather()
        {
            return new CapeMarioState();
        }

        public IMarioState HandleFlower()
        {
            return new FireMarioState();
        }

        public SuperMarioStateMashine.State GetStateEnum()
        {
            return SuperMarioStateMashine.State.Mario;
        }
    }
}
