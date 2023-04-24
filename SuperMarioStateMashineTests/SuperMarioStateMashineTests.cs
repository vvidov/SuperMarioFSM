using NUnit.Framework;

namespace ConsoleApp1.Tests
{
    [TestFixture()]
    public class SuperMarioStateMashineTests
    {
        [Test()]
        public void CreateSMStartStateIsMarioState()
        {
            var StartState = SuperMarioStateMashine.State.Mario;

            var superMarioStateMashine = new SuperMarioStateMashine();
            Assert.AreEqual(StartState, superMarioStateMashine.FSMState);
        }

        [Test()]
        [TestCase(SuperMarioStateMashine.Transition.Mushroom, SuperMarioStateMashine.State.SuperMario)]
        [TestCase(SuperMarioStateMashine.Transition.Feather, SuperMarioStateMashine.State.CapeMario)]
        [TestCase(SuperMarioStateMashine.Transition.Flower, SuperMarioStateMashine.State.FireMario)]
        public void TestStartState(SuperMarioStateMashine.Transition transition, SuperMarioStateMashine.State expectedState)
        {
            var superMarioStateMashine = new SuperMarioStateMashine();
            superMarioStateMashine.GetItem(transition);
            Assert.AreEqual(expectedState, superMarioStateMashine.FSMState);
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
            Assert.AreEqual(expectedState, superMarioStateMashine.FSMState);
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
            Assert.AreEqual(expectedState, superMarioStateMashine.FSMState);
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
            Assert.AreEqual(expectedState, superMarioStateMashine.FSMState);
        }
    }
}