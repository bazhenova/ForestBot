using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ForestProject;

namespace ForestProjectTests
{
    [TestClass]
    public class MovingTests
    {        
        bool MovingTest(Point delta)
        {
            var loader = new ForestLoader(new string[] {"1K0100",
                                                        "001001",
                                                        "0L1100"});
            var map = loader.LoadData().Item1;
            var forest = new Forest(map);
            var man = new Hero(2, "", new Point(0, 0));
            forest.SetHero(man, new Point(1, 1));
            return forest.TryMove(man, delta);
        }

        [TestMethod]
        public void TestTryMoveToRoad()
        {
            Assert.IsTrue(MovingTest(new Point(-1, 0)));
        }

        [TestMethod]
        public void TestTryMoveToJungle()
        {
            Assert.IsFalse(MovingTest(new Point(1, 0)));
        }

        [TestMethod]
        public void TestTryMoveToLife()
        {
            Assert.IsTrue(MovingTest(new Point(0, 1)));
        }

        [TestMethod]
        public void TestTryMoveToTrap()
        {
            Assert.IsTrue(MovingTest(new Point(0, -1)));
        }

        [TestMethod]
        public void TestChangeCountLife()
        {
            var loader = new ForestLoader(new string[] {"1K0100",
                                                        "001001",
                                                        "0L1100"});
            var map = loader.LoadData().Item1;
            var forest = new Forest(map);
            var man = new Hero(2, "", new Point(0, 0));
            var lastCountLife = man.CountOfLife;
            forest.SetHero(man, new Point(1, 1));
            var result = forest.TryMove(man, new Point(0, 1));
            Assert.IsTrue(forest.Hero.CountOfLife != lastCountLife);
            Assert.AreEqual(forest.Hero.CountOfLife - lastCountLife, 1);
        }
    }
}
