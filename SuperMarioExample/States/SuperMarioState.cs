namespace SuperMarioExample.States
{
    /// <summary>
    /// Represents the Super Mario state (after getting a mushroom)
    /// </summary>
    public class SuperMarioState : IMarioState
    {
        public IMarioState HandleMushroom()
        {
            // Super Mario remains Super Mario when getting another mushroom
            return this;
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
            return SuperMarioStateMashine.State.SuperMario;
        }
    }
}
