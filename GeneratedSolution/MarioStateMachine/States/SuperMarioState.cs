namespace SuperMarioExample.States
{
    public class SuperMarioState : IMarioState
    {
        public IMarioState HandleMushroom()
        {
            return new SuperMarioState();
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
            return Mario.State.SuperMario;
        }
    }
}