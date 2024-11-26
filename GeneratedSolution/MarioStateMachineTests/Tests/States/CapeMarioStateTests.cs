using NUnit.Framework;
using SuperMarioExample;
using SuperMarioExample.States;

namespace SuperMarioExampleTests.States
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
            Assert.That(result, Is.EqualTo(Mario.State.CapeMario));
        }

        [Test()]
        public void HandleMushroom_Returns_CapeMarioState()
        {
            // Act
            var result = _capemarioState.HandleMushroom();

            // Assert
            Assert.That(result, Is.TypeOf<CapeMarioState>());
            Assert.That(result.GetStateEnum(), Is.EqualTo(Mario.State.CapeMario));
        }

        [Test()]
        public void HandleFlower_Returns_FireMarioState()
        {
            // Act
            var result = _capemarioState.HandleFlower();

            // Assert
            Assert.That(result, Is.TypeOf<FireMarioState>());
            Assert.That(result.GetStateEnum(), Is.EqualTo(Mario.State.FireMario));
        }

        [Test()]
        public void HandleFeather_Returns_CapeMarioState()
        {
            // Act
            var result = _capemarioState.HandleFeather();

            // Assert
            Assert.That(result, Is.TypeOf<CapeMarioState>());
            Assert.That(result.GetStateEnum(), Is.EqualTo(Mario.State.CapeMario));
        }


    }
}