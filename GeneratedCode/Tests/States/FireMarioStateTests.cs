using NUnit.Framework;
using MyStateMachine;
using MyStateMachine.States;

namespace MyStateMachineTests.States
{
    [TestFixture()]
    public class FireMarioStateTests
    {
        private FireMarioState _firemarioState;

        [SetUp]
        public void Setup()
        {
            _firemarioState = new FireMarioState();
        }

        [Test()]
        public void GetStateEnum_Returns_FireMario()
        {
            // Act
            var result = _firemarioState.GetStateEnum();

            // Assert
            Assert.AreEqual(Mario.State.FireMario, result);
        }

        [Test()]
        public void HandleMushroom_Returns_FireMarioState()
        {
            // Act
            var result = _firemarioState.HandleMushroom();

            // Assert
            Assert.IsInstanceOfType(result, typeof(FireMarioState));
            Assert.AreEqual(Mario.State.FireMario, result.GetStateEnum());
        }

        [Test()]
        public void HandleFlower_Returns_FireMarioState()
        {
            // Act
            var result = _firemarioState.HandleFlower();

            // Assert
            Assert.IsInstanceOfType(result, typeof(FireMarioState));
            Assert.AreEqual(Mario.State.FireMario, result.GetStateEnum());
        }

        [Test()]
        public void HandleFeather_Returns_CapeMarioState()
        {
            // Act
            var result = _firemarioState.HandleFeather();

            // Assert
            Assert.IsInstanceOfType(result, typeof(CapeMarioState));
            Assert.AreEqual(Mario.State.CapeMario, result.GetStateEnum());
        }


    }
}