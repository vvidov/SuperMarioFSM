using NUnit.Framework;
using SuperMarioExample;
using SuperMarioExample.States;

namespace SuperMarioStateMashineTests.States
{
    [TestFixture]
    public class FireMarioStateTests
    {
        private FireMarioState _fireMarioState;

        [SetUp]
        public void Setup()
        {
            _fireMarioState = new FireMarioState();
        }

        [Test]
        public void GetStateEnum_Returns_FireMario()
        {
            // Act
            var result = _fireMarioState.GetStateEnum();

            // Assert
            Assert.That(result, Is.EqualTo(SuperMarioStateMashine.State.FireMario));
        }

        [Test]
        public void HandleMushroom_Returns_SameState()
        {
            // Act
            var result = _fireMarioState.HandleMushroom();

            // Assert
            Assert.That(result, Is.InstanceOf<FireMarioState>());
            Assert.That(result.GetStateEnum(), Is.EqualTo(SuperMarioStateMashine.State.FireMario));
        }

        [Test]
        public void HandleFeather_Returns_CapeMarioState()
        {
            // Act
            var result = _fireMarioState.HandleFeather();

            // Assert
            Assert.That(result, Is.InstanceOf<CapeMarioState>());
            Assert.That(result.GetStateEnum(), Is.EqualTo(SuperMarioStateMashine.State.CapeMario));
        }

        [Test]
        public void HandleFlower_Returns_SameState()
        {
            // Act
            var result = _fireMarioState.HandleFlower();

            // Assert
            Assert.That(result, Is.InstanceOf<FireMarioState>());
            Assert.That(result.GetStateEnum(), Is.EqualTo(SuperMarioStateMashine.State.FireMario));
        }
    }
}
