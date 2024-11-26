using NUnit.Framework;
using SuperMarioExample;
using SuperMarioExample.States;

namespace SuperMarioStateMashineTests.States
{
    [TestFixture]
    public class SuperMarioStateTests
    {
        private SuperMarioState _superMarioState;

        [SetUp]
        public void Setup()
        {
            _superMarioState = new SuperMarioState();
        }

        [Test]
        public void GetStateEnum_Returns_SuperMario()
        {
            // Act
            var result = _superMarioState.GetStateEnum();

            // Assert
            Assert.That(result, Is.EqualTo(SuperMarioStateMashine.State.SuperMario));
        }

        [Test]
        public void HandleMushroom_Returns_SameState()
        {
            // Act
            var result = _superMarioState.HandleMushroom();

            // Assert
            Assert.That(result, Is.InstanceOf<SuperMarioState>());
            Assert.That(result.GetStateEnum(), Is.EqualTo(SuperMarioStateMashine.State.SuperMario));
        }

        [Test]
        public void HandleFeather_Returns_CapeMarioState()
        {
            // Act
            var result = _superMarioState.HandleFeather();

            // Assert
            Assert.That(result, Is.InstanceOf<CapeMarioState>());
            Assert.That(result.GetStateEnum(), Is.EqualTo(SuperMarioStateMashine.State.CapeMario));
        }

        [Test]
        public void HandleFlower_Returns_FireMarioState()
        {
            // Act
            var result = _superMarioState.HandleFlower();

            // Assert
            Assert.That(result, Is.InstanceOf<FireMarioState>());
            Assert.That(result.GetStateEnum(), Is.EqualTo(SuperMarioStateMashine.State.FireMario));
        }
    }
}
