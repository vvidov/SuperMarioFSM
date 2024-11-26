namespace SuperMarioExample.States
{
    /// <summary>
    /// Represents the Cape Mario state (after getting a feather)
    /// </summary>
    public class CapeMarioState : IMarioState
    {
        public IMarioState HandleMushroom()
        {
            // Cape Mario remains Cape Mario when getting a mushroom
            return this;
        }

        public IMarioState HandleFeather()
        {
            // Cape Mario remains Cape Mario when getting another feather
            return this;
        }

        public IMarioState HandleFlower()
        {
            return new FireMarioState();
        }

        public SuperMarioStateMashine.State GetStateEnum()
        {
            return SuperMarioStateMashine.State.CapeMario;
        }
    }
}
