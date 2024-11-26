namespace SuperMarioExample.States
{
    public interface IMarioState
    {
        IMarioState HandleMushroom();
        IMarioState HandleFlower();
        IMarioState HandleFeather();
        
        Mario.State GetStateEnum();
    }
}