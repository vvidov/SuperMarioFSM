namespace MyStateMachine.States
{
    public class FireMarioState : IMarioState
    {
        public IMarioState HandleMushroom()
        {
            return new FireMarioState();
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
            return Mario.State.FireMario;
        }
    }
}