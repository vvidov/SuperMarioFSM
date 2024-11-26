using NUnit.Framework;
using SuperMarioExample;
using SuperMarioExample.States;

namespace SuperMarioExampleTests.States
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
            Assert.That(result, Is.EqualTo(Mario.State.FireMario));
        }

        [Test()]
        public void HandleMushroom_Returns_FireMarioState()
        {
            // Act
            var result = _firemarioState.HandleMushroom();

            // Assert
            Assert.That(result, Is.TypeOf<FireMarioState>());
            Assert.That(result.GetStateEnum(), Is.EqualTo(Mario.State.FireMario));
        }

        [Test()]
        public void HandleFlower_Returns_FireMarioState()
        {
            // Act
            var result = _firemarioState.HandleFlower();

            // Assert
            Assert.That(result, Is.TypeOf<FireMarioState>());
            Assert.That(result.GetStateEnum(), Is.EqualTo(Mario.State.FireMario));
        }

        [Test()]
        public void HandleFeather_Returns_CapeMarioState()
        {
            // Act
            var result = _firemarioState.HandleFeather();

            // Assert
            Assert.That(result, Is.TypeOf<CapeMarioState>());
            Assert.That(result.GetStateEnum(), Is.EqualTo(Mario.State.CapeMario));
        }


    }
}