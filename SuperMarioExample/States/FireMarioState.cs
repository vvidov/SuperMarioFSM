namespace SuperMarioExample.States
{
    /// <summary>
    /// Represents the Fire Mario state (after getting a flower)
    /// </summary>
    public class FireMarioState : IMarioState
    {
        public IMarioState HandleMushroom()
        {
            // Fire Mario remains Fire Mario when getting a mushroom
            return this;
        }

        public IMarioState HandleFeather()
        {
            return new CapeMarioState();
        }

        public IMarioState HandleFlower()
        {
            // Fire Mario remains Fire Mario when getting another flower
            return this;
        }

        public SuperMarioStateMashine.State GetStateEnum()
        {
            return SuperMarioStateMashine.State.FireMario;
        }
    }
}
