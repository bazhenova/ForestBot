using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ForestProject;

namespace ForestProjectTests
{
    [TestClass]
    public class SetHeroTests
    {
        void GeneralSetHeroTest(Point place)
        {
            var loader = new ForestLoader(new string[] {"11L",
                                                        "101",
                                                        "K01"});
            var map = loader.LoadData().Item1;
            var forest = new Forest(map);
            var man = new Hero(2, "", new Point(0,0));
            man.SetCoordinates(new Point(0, 0));
            forest.SetHero(man, place);
            Assert.AreEqual(man.GetCoordinates().X, place.X);
            Assert.AreEqual(man.GetCoordinates().Y, place.Y);
        }

        [TestMethod]
        public void SetTest1()
        {
            GeneralSetHeroTest(new Point(1, 1));
        }

        [TestMethod]
        public void SetTest2()
        {
            GeneralSetHeroTest(new Point(2, 1));
        }

        [TestMethod]
        public void SetTest3()
        {
            GeneralSetHeroTest(new Point(2, 0));
        }

        [TestMethod]
        public void SetTest4()
        {
            GeneralSetHeroTest(new Point(0, 2));
        }
    }
}
