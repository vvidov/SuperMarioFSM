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
            Assert.AreEqual(StartState, superMarioStateMashine.SMState);
        }
        [Test()]
        public void GetMushRoomFromStartState()
        {
            var expectedState = SuperMarioStateMashine.State.SuperMario;

            var superMarioStateMashine = new SuperMarioStateMashine();
            superMarioStateMashine.GetItem(SuperMarioStateMashine.Transition.Mushroom);
            Assert.AreEqual(expectedState, superMarioStateMashine.SMState);
        }
        [Test()]
        public void GetFeatherFromStartState()
        {
            var expectedState = SuperMarioStateMashine.State.CapeMario;

            var superMarioStateMashine = new SuperMarioStateMashine();
            superMarioStateMashine.GetItem(SuperMarioStateMashine.Transition.Feather);
            Assert.AreEqual(expectedState, superMarioStateMashine.SMState);
        }
        [Test()]
        public void GetFlowerFromStartState()
        {
            var expectedState = SuperMarioStateMashine.State.FireMario;

            var superMarioStateMashine = new SuperMarioStateMashine();
            superMarioStateMashine.GetItem(SuperMarioStateMashine.Transition.Flower);
            Assert.AreEqual(expectedState, superMarioStateMashine.SMState);
        }

        [Test()]
        public void GetMushRoomFromSuperMarioState()
        {
            var expectedState = SuperMarioStateMashine.State.SuperMario;

            var superMarioStateMashine = new SuperMarioStateMashine();
            superMarioStateMashine.GetItem(SuperMarioStateMashine.Transition.Mushroom);
            superMarioStateMashine.GetItem(SuperMarioStateMashine.Transition.Mushroom);
            Assert.AreEqual(expectedState, superMarioStateMashine.SMState);
        }
        [Test()]
        public void GetFeatherFromSuperMarioState()
        {
            var expectedState = SuperMarioStateMashine.State.CapeMario;

            var superMarioStateMashine = new SuperMarioStateMashine();
            superMarioStateMashine.GetItem(SuperMarioStateMashine.Transition.Mushroom);
            superMarioStateMashine.GetItem(SuperMarioStateMashine.Transition.Feather);
            Assert.AreEqual(expectedState, superMarioStateMashine.SMState);
        }
        [Test()]
        public void GetFlowerFromStartSuperMarioState()
        {
            var expectedState = SuperMarioStateMashine.State.FireMario;

            var superMarioStateMashine = new SuperMarioStateMashine();
            superMarioStateMashine.GetItem(SuperMarioStateMashine.Transition.Mushroom);
            superMarioStateMashine.GetItem(SuperMarioStateMashine.Transition.Flower);
            Assert.AreEqual(expectedState, superMarioStateMashine.SMState);
        }


        [Test()]
        public void GetMushRoomFromFireMarioState()
        {
            var expectedState = SuperMarioStateMashine.State.FireMario;

            var superMarioStateMashine = new SuperMarioStateMashine();
            superMarioStateMashine.GetItem(SuperMarioStateMashine.Transition.Flower);
            superMarioStateMashine.GetItem(SuperMarioStateMashine.Transition.Mushroom);
            Assert.AreEqual(expectedState, superMarioStateMashine.SMState);
        }
        [Test()]
        public void GetFeatherFromFireMarioState()
        {
            var expectedState = SuperMarioStateMashine.State.CapeMario;

            var superMarioStateMashine = new SuperMarioStateMashine();
            superMarioStateMashine.GetItem(SuperMarioStateMashine.Transition.Flower);
            superMarioStateMashine.GetItem(SuperMarioStateMashine.Transition.Feather);
            Assert.AreEqual(expectedState, superMarioStateMashine.SMState);
        }
        [Test()]
        public void GetFlowerFromStartFireMarioState()
        {
            var expectedState = SuperMarioStateMashine.State.FireMario;

            var superMarioStateMashine = new SuperMarioStateMashine();
            superMarioStateMashine.GetItem(SuperMarioStateMashine.Transition.Flower);
            superMarioStateMashine.GetItem(SuperMarioStateMashine.Transition.Flower);
            Assert.AreEqual(expectedState, superMarioStateMashine.SMState);
        }


        [Test()]
        public void GetMushRoomFromCapeMarioState()
        {
            var expectedState = SuperMarioStateMashine.State.CapeMario;

            var superMarioStateMashine = new SuperMarioStateMashine();
            superMarioStateMashine.GetItem(SuperMarioStateMashine.Transition.Feather);
            superMarioStateMashine.GetItem(SuperMarioStateMashine.Transition.Mushroom);
            Assert.AreEqual(expectedState, superMarioStateMashine.SMState);
        }
        [Test()]
        public void GetFeatherFromCapeMarioState()
        {
            var expectedState = SuperMarioStateMashine.State.CapeMario;

            var superMarioStateMashine = new SuperMarioStateMashine();
            superMarioStateMashine.GetItem(SuperMarioStateMashine.Transition.Feather);
            superMarioStateMashine.GetItem(SuperMarioStateMashine.Transition.Feather);
            Assert.AreEqual(expectedState, superMarioStateMashine.SMState);
        }
        [Test()]
        public void GetFlowerFromStartCapeMarioState()
        {
            var expectedState = SuperMarioStateMashine.State.FireMario;

            var superMarioStateMashine = new SuperMarioStateMashine();
            superMarioStateMashine.GetItem(SuperMarioStateMashine.Transition.Feather);
            superMarioStateMashine.GetItem(SuperMarioStateMashine.Transition.Flower);
            Assert.AreEqual(expectedState, superMarioStateMashine.SMState);
        }

    }
}