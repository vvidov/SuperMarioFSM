using NUnit.Framework;
using SuperMarioExample;
using SuperMarioExample.States;

namespace SuperMarioExampleTests.States
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
            Assert.That(result, Is.EqualTo(Mario.State.SuperMario));
        }

        [Test()]
        public void HandleMushroom_Returns_SuperMarioState()
        {
            // Act
            var result = _supermarioState.HandleMushroom();

            // Assert
            Assert.That(result, Is.TypeOf<SuperMarioState>());
            Assert.That(result.GetStateEnum(), Is.EqualTo(Mario.State.SuperMario));
        }

        [Test()]
        public void HandleFlower_Returns_FireMarioState()
        {
            // Act
            var result = _supermarioState.HandleFlower();

            // Assert
            Assert.That(result, Is.TypeOf<FireMarioState>());
            Assert.That(result.GetStateEnum(), Is.EqualTo(Mario.State.FireMario));
        }

        [Test()]
        public void HandleFeather_Returns_CapeMarioState()
        {
            // Act
            var result = _supermarioState.HandleFeather();

            // Assert
            Assert.That(result, Is.TypeOf<CapeMarioState>());
            Assert.That(result.GetStateEnum(), Is.EqualTo(Mario.State.CapeMario));
        }


    }
}