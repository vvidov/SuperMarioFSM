using NUnit.Framework;
using MyStateMachine;
using MyStateMachine.States;

namespace MyStateMachineTests.States
{
    [TestFixture()]
    public class SuperMarioStateTests
    {
        private SuperMarioState _supermarioState;

        [SetUp]
        public void Setup()
        {
            _supermarioState = new SuperMarioState();
        }

        [Test()]
        public void GetStateEnum_Returns_SuperMario()
        {
            // Act
            var result = _supermarioState.GetStateEnum();

            // Assert
            Assert.AreEqual(Mario.State.SuperMario, result);
        }

        [Test()]
        public void HandleMushroom_Returns_SuperMarioState()
        {
            // Act
            var result = _supermarioState.HandleMushroom();

            // Assert
            Assert.IsInstanceOfType(result, typeof(SuperMarioState));
            Assert.AreEqual(Mario.State.SuperMario, result.GetStateEnum());
        }

        [Test()]
        public void HandleFlower_Returns_FireMarioState()
        {
            // Act
            var result = _supermarioState.HandleFlower();

            // Assert
            Assert.IsInstanceOfType(result, typeof(FireMarioState));
            Assert.AreEqual(Mario.State.FireMario, result.GetStateEnum());
        }

        [Test()]
        public void HandleFeather_Returns_CapeMarioState()
        {
            // Act
            var result = _supermarioState.HandleFeather();

            // Assert
            Assert.IsInstanceOfType(result, typeof(CapeMarioState));
            Assert.AreEqual(Mario.State.CapeMario, result.GetStateEnum());
        }


    }
}