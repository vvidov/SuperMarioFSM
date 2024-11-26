namespace MyStateMachine.States
{
    public class CapeMarioState : IMarioState
    {
        public IMarioState HandleMushroom()
        {
            return new CapeMarioState();
        }

        public IMarioState HandleFlower()
        {
            return new FireMarioState();
        }

        public IMarioState HandleFeather()
        {
            return new CapeMarioState();
        }


        public Mario.State GetStateEnum()
        {
            return Mario.State.CapeMario;
        }
    }
}