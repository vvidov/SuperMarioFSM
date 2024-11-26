using NUnit.Framework;
using SuperMarioExample;
using SuperMarioExample.States;

namespace SuperMarioStateMashineTests.States
{
    [TestFixture]
    public class CapeMarioStateTests
    {
        private CapeMarioState _capeMarioState;

        [SetUp]
        public void Setup()
        {
            _capeMarioState = new CapeMarioState();
        }

        [Test]
        public void GetStateEnum_Returns_CapeMario()
        {
            // Act
            var result = _capeMarioState.GetStateEnum();

            // Assert
            Assert.That(result, Is.EqualTo(SuperMarioStateMashine.State.CapeMario));
        }

        [Test]
        public void HandleMushroom_Returns_CapeMario()
        {
            // Act
            var result = _capeMarioState.HandleMushroom();

            // Assert
            Assert.That(result.GetStateEnum(), Is.EqualTo(SuperMarioStateMashine.State.CapeMario));
        }

        [Test]
        public void HandleFeather_Returns_CapeMario()
        {
            // Act
            var result = _capeMarioState.HandleFeather();

            // Assert
            Assert.That(result.GetStateEnum(), Is.EqualTo(SuperMarioStateMashine.State.CapeMario));
        }

        [Test]
        public void HandleFlower_Returns_FireMario()
        {
            // Act
            var result = _capeMarioState.HandleFlower();

            // Assert
            Assert.That(result.GetStateEnum(), Is.EqualTo(SuperMarioStateMashine.State.FireMario));
        }
    }
}
