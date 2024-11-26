using NUnit.Framework;
using SuperMarioExample;
using SuperMarioExample.States;

namespace SuperMarioStateMashineTests
{
    [TestFixture()]
    public class SuperMarioStateMashineTests
    {
        private class TestLogger : MarioStateLogger
        {
            public SuperMarioStateMashine.State? LastOldState { get; private set; }
            public SuperMarioStateMashine.State? LastNewState { get; private set; }
            public SuperMarioStateMashine.Transition? LastTransition { get; private set; }

            public override void LogStateTransition(SuperMarioStateMashine.State oldState, 
                                                 SuperMarioStateMashine.State newState, 
                                                 SuperMarioStateMashine.Transition transition)
            {
                LastOldState = oldState;
                LastNewState = newState;
                LastTransition = transition;
            }
        }

        [Test()]
        public void CreateSMStartStateIsMarioState()
        {
            var StartState = SuperMarioStateMashine.State.Mario;

            var superMarioStateMashine = new SuperMarioStateMashine();
            Assert.That(superMarioStateMashine.FSMState, Is.EqualTo(StartState));
        }

        [Test()]
        [TestCase(SuperMarioStateMashine.Transition.Mushroom, SuperMarioStateMashine.State.SuperMario)]
        [TestCase(SuperMarioStateMashine.Transition.Feather, SuperMarioStateMashine.State.CapeMario)]
        [TestCase(SuperMarioStateMashine.Transition.Flower, SuperMarioStateMashine.State.FireMario)]
        public void TestStartState(SuperMarioStateMashine.Transition transition, SuperMarioStateMashine.State expectedState)
        {
            var superMarioStateMashine = new SuperMarioStateMashine();
            superMarioStateMashine.GetItem(transition);
            Assert.That(superMarioStateMashine.FSMState, Is.EqualTo(expectedState));
        }

        [Test()]
        [TestCase(SuperMarioStateMashine.Transition.Mushroom, SuperMarioStateMashine.State.SuperMario)]
        [TestCase(SuperMarioStateMashine.Transition.Feather, SuperMarioStateMashine.State.CapeMario)]
        [TestCase(SuperMarioStateMashine.Transition.Flower, SuperMarioStateMashine.State.FireMario)]
        public void TestSuperMarioState(SuperMarioStateMashine.Transition transition, SuperMarioStateMashine.State expectedState)
        {
            var superMarioStateMashine = new SuperMarioStateMashine();
            superMarioStateMashine.GetItem(SuperMarioStateMashine.Transition.Mushroom);
            superMarioStateMashine.GetItem(transition);
            Assert.That(superMarioStateMashine.FSMState, Is.EqualTo(expectedState));
        }

        [Test()]
        [TestCase(SuperMarioStateMashine.Transition.Mushroom, SuperMarioStateMashine.State.FireMario)]
        [TestCase(SuperMarioStateMashine.Transition.Feather, SuperMarioStateMashine.State.CapeMario)]
        [TestCase(SuperMarioStateMashine.Transition.Flower, SuperMarioStateMashine.State.FireMario)]
        public void TestFireMarioState(SuperMarioStateMashine.Transition transition, SuperMarioStateMashine.State expectedState)
        {
            var superMarioStateMashine = new SuperMarioStateMashine();
            superMarioStateMashine.GetItem(SuperMarioStateMashine.Transition.Flower);
            superMarioStateMashine.GetItem(transition);
            Assert.That(superMarioStateMashine.FSMState, Is.EqualTo(expected: expectedState));
        }

        [Test()]
        [TestCase(SuperMarioStateMashine.Transition.Mushroom, SuperMarioStateMashine.State.CapeMario)]
        [TestCase(SuperMarioStateMashine.Transition.Feather, SuperMarioStateMashine.State.CapeMario)]
        [TestCase(SuperMarioStateMashine.Transition.Flower, SuperMarioStateMashine.State.FireMario)]
        public void TestCapeMarioState(SuperMarioStateMashine.Transition transition, SuperMarioStateMashine.State expectedState)
        {
            var superMarioStateMashine = new SuperMarioStateMashine();
            superMarioStateMashine.GetItem(SuperMarioStateMashine.Transition.Feather);
            superMarioStateMashine.GetItem(transition);
            Assert.That(superMarioStateMashine.FSMState, Is.EqualTo(expectedState));
        }

        [Test()]
        public void TestLogger_RecordsStateTransitions()
        {
            // Arrange
            var logger = new TestLogger();
            var superMarioStateMashine = new SuperMarioStateMashine(logger);

            // Act
            superMarioStateMashine.GetItem(SuperMarioStateMashine.Transition.Mushroom);

            // Assert
            Assert.That(logger.LastOldState, Is.EqualTo(SuperMarioStateMashine.State.Mario));
            Assert.That(logger.LastNewState, Is.EqualTo(SuperMarioStateMashine.State.SuperMario));
            Assert.That(logger.LastTransition, Is.EqualTo(SuperMarioStateMashine.Transition.Mushroom));
        }

        [Test()]
        public void TestLogger_HandlesNullLogger()
        {
            // Arrange
            var superMarioStateMashine = new SuperMarioStateMashine(null);

            // Act & Assert
            Assert.DoesNotThrow(() => superMarioStateMashine.GetItem(SuperMarioStateMashine.Transition.Mushroom));
        }

        [Test()]
        public void TestInvalidTransition_ThrowsArgumentException()
        {
            // Arrange
            var superMarioStateMashine = new SuperMarioStateMashine();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => 
                superMarioStateMashine.GetItem((SuperMarioStateMashine.Transition)999));
        }
    }
}