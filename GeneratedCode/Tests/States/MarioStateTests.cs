using NUnit.Framework;
using MyStateMachine;
using MyStateMachine.States;

namespace MyStateMachineTests.States
{
    [TestFixture()]
    public class MarioStateTests
    {
        private MarioState _marioState;

        [SetUp]
        public void Setup()
        {
            _marioState = new MarioState();
        }

        [Test()]
        public void GetStateEnum_Returns_Mario()
        {
            // Act
            var result = _marioState.GetStateEnum();

            // Assert
            Assert.AreEqual(Mario.State.Mario, result);
        }

        [Test()]
        public void HandleMushroom_Returns_SuperMarioState()
        {
            // Act
            var result = _marioState.HandleMushroom();

            // Assert
            Assert.IsInstanceOfType(result, typeof(SuperMarioState));
            Assert.AreEqual(Mario.State.SuperMario, result.GetStateEnum());
        }

        [Test()]
        public void HandleFlower_Returns_FireMarioState()
        {
            // Act
            var result = _marioState.HandleFlower();

            // Assert
            Assert.IsInstanceOfType(result, typeof(FireMarioState));
            Assert.AreEqual(Mario.State.FireMario, result.GetStateEnum());
        }

        [Test()]
        public void HandleFeather_Returns_CapeMarioState()
        {
            // Act
            var result = _marioState.HandleFeather();

            // Assert
            Assert.IsInstanceOfType(result, typeof(CapeMarioState));
            Assert.AreEqual(Mario.State.CapeMario, result.GetStateEnum());
        }


    }
}