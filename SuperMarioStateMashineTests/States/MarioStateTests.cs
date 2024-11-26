using NUnit.Framework;
using SuperMarioExample;
using SuperMarioExample.States;

namespace SuperMarioStateMashineTests.States
{
    [TestFixture]
    public class MarioStateTests
    {
        private MarioState _marioState;

        [SetUp]
        public void Setup()
        {
            _marioState = new MarioState();
        }

        [Test]
        public void GetStateEnum_Returns_Mario()
        {
            // Act
            var result = _marioState.GetStateEnum();

            // Assert
            Assert.That(result, Is.EqualTo(SuperMarioStateMashine.State.Mario));
        }

        [Test]
        public void HandleMushroom_Returns_SuperMario()
        {
            // Act
            var result = _marioState.HandleMushroom();

            // Assert
            Assert.That(result.GetStateEnum(), Is.EqualTo(SuperMarioStateMashine.State.SuperMario));
        }

        [Test]
        public void HandleFeather_Returns_CapeMario()
        {
            // Act
            var result = _marioState.HandleFeather();

            // Assert
            Assert.That(result.GetStateEnum(), Is.EqualTo(SuperMarioStateMashine.State.CapeMario));
        }

        [Test]
        public void HandleFlower_Returns_FireMario()
        {
            // Act
            var result = _marioState.HandleFlower();

            // Assert
            Assert.That(result.GetStateEnum(), Is.EqualTo(SuperMarioStateMashine.State.FireMario));
        }
    }
}
