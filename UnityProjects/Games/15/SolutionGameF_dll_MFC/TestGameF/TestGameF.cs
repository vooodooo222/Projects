using Microsoft.VisualStudio.TestTools.UnitTesting;
using BoardF;
using System;

namespace BoardFTest
{       
    [TestClass()]
    public class GameTest
    {
        [TestMethod()]
        public void StartTest()
        {
            Game game = new Game(4);
            game.Start();
            Assert.AreEqual(1, game.GetDigitalAt(0, 0));
            Assert.AreEqual(2, game.GetDigitalAt(1, 0));
            Assert.AreEqual(5, game.GetDigitalAt(0, 1));
            Assert.AreEqual(15, game.GetDigitalAt(2, 3));
            Assert.AreEqual(0, game.GetDigitalAt(3, 3));
        }

        [TestMethod()]
        public void PressAtTest()
        {
            Game game = new Game(4);
            game.Start();
            game.PressAt(2, 3); // нажали 15
            Assert.AreEqual(0, game.GetDigitalAt(2, 3));    // 0 перешел на место 15-ти
            Assert.AreEqual(15, game.GetDigitalAt(3, 3));   // 15 перешла на место 0
            game.PressAt(2, 2); // нажали 11
            Assert.AreEqual(0, game.GetDigitalAt(2, 2));    // 0 перешел на место 11-ти
            Assert.AreEqual(11, game.GetDigitalAt(2, 3));   // 11 перешла на место 0
        }

        [TestMethod()]
        public void GetDigitalAtTest()
        {
            Game game = new Game(4);
            game.Start();
            // проверка с некорректными значениями
            // программа не должна падать
            Assert.AreEqual(0, game.GetDigitalAt(-5, -34));
            Assert.AreEqual(0, game.GetDigitalAt(15, 64));
        }

        [TestMethod()]
        public void SolvedTest()
        {
            Game game = new Game(4);
            game.Start();
            Assert.IsTrue(game.Solved());
            game.PressAt(2, 3); // сдвинули 15 на 0
            Assert.IsFalse(game.Solved());
            game.PressAt(3, 3); // вернули 15 на свое прежнее место
            Assert.IsTrue(game.Solved());
        }
    }
}
