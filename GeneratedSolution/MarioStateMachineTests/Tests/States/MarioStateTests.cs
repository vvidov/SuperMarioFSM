using NUnit.Framework;
using SuperMarioExample;
using SuperMarioExample.States;

namespace SuperMarioExampleTests.States
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
            Assert.That(result, Is.EqualTo(Mario.State.Mario));
        }

        [Test()]
        public void HandleMushroom_Returns_SuperMarioState()
        {
            // Act
            var result = _marioState.HandleMushroom();

            // Assert
            Assert.That(result, Is.TypeOf<SuperMarioState>());
            Assert.That(result.GetStateEnum(), Is.EqualTo(Mario.State.SuperMario));
        }

        [Test()]
        public void HandleFlower_Returns_FireMarioState()
        {
            // Act
            var result = _marioState.HandleFlower();

            // Assert
            Assert.That(result, Is.TypeOf<FireMarioState>());
            Assert.That(result.GetStateEnum(), Is.EqualTo(Mario.State.FireMario));
        }

        [Test()]
        public void HandleFeather_Returns_CapeMarioState()
        {
            // Act
            var result = _marioState.HandleFeather();

            // Assert
            Assert.That(result, Is.TypeOf<CapeMarioState>());
            Assert.That(result.GetStateEnum(), Is.EqualTo(Mario.State.CapeMario));
        }


    }
}