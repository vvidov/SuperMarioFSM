namespace SuperMarioExample.States
{
    /// <summary>
    /// Interface defining the contract for all Mario states
    /// </summary>
    public interface IMarioState
    {
        /// <summary>
        /// Handles the transition when Mario receives a mushroom
        /// </summary>
        /// <returns>The new state after receiving the mushroom</returns>
        IMarioState HandleMushroom();

        /// <summary>
        /// Handles the transition when Mario receives a feather
        /// </summary>
        /// <returns>The new state after receiving the feather</returns>
        IMarioState HandleFeather();

        /// <summary>
        /// Handles the transition when Mario receives a flower
        /// </summary>
        /// <returns>The new state after receiving the flower</returns>
        IMarioState HandleFlower();

        /// <summary>
        /// Gets the current state enum value
        /// </summary>
        /// <returns>The current state enum value</returns>
        SuperMarioStateMashine.State GetStateEnum();
    }
}
