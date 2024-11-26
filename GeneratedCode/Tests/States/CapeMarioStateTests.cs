using NUnit.Framework;
using MyStateMachine;
using MyStateMachine.States;

namespace MyStateMachineTests.States
{
    [TestFixture()]
    public class CapeMarioStateTests
    {
        private CapeMarioState _capemarioState;

        [SetUp]
        public void Setup()
        {
            _capemarioState = new CapeMarioState();
        }

        [Test()]
        public void GetStateEnum_Returns_CapeMario()
        {
            // Act
            var result = _capemarioState.GetStateEnum();

            // Assert
            Assert.AreEqual(Mario.State.CapeMario, result);
        }

        [Test()]
        public void HandleMushroom_Returns_CapeMarioState()
        {
            // Act
            var result = _capemarioState.HandleMushroom();

            // Assert
            Assert.IsInstanceOfType(result, typeof(CapeMarioState));
            Assert.AreEqual(Mario.State.CapeMario, result.GetStateEnum());
        }

        [Test()]
        public void HandleFlower_Returns_FireMarioState()
        {
            // Act
            var result = _capemarioState.HandleFlower();

            // Assert
            Assert.IsInstanceOfType(result, typeof(FireMarioState));
            Assert.AreEqual(Mario.State.FireMario, result.GetStateEnum());
        }

        [Test()]
        public void HandleFeather_Returns_CapeMarioState()
        {
            // Act
            var result = _capemarioState.HandleFeather();

            // Assert
            Assert.IsInstanceOfType(result, typeof(CapeMarioState));
            Assert.AreEqual(Mario.State.CapeMario, result.GetStateEnum());
        }


    }
}