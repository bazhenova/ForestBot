using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ForestProject;
using ForestProject.Locations;

namespace ForestProjectTests
{
    [TestClass]
    public class LoaderTests
    {
        [TestMethod]
        public void LoaderTestJungle()
        {
            var loader = new ForestLoader("11", "00");
            var map = loader.LoadData().Item1;
            Assert.IsTrue(map[0, 0] is Jungle);
            Assert.IsTrue(map[0, 1] is Jungle);
        }

        [TestMethod]
        public void LoaderTestRoad()
        {
            var loader = new ForestLoader(new string[] {"11", "00"});
            var map = loader.LoadData().Item1;
            Assert.IsTrue(map[1, 0] is Road);
            Assert.IsTrue(map[1, 1] is Road);
        }

        [TestMethod]
        public void LoaderTestLife()
        {
            var loader = new ForestLoader(new string[] {"L1", "0K"});
            var map = loader.LoadData().Item1;
            Assert.IsTrue(map[0, 0] is Life);
        }

        [TestMethod]
        public void LoaderTestTrap()
        {
            var loader = new ForestLoader(new string[] {"L1", "0K"});
            var map = loader.LoadData().Item1;
            Assert.IsTrue(map[1, 1] is Trap);
        }

        [TestMethod]
        public void LoaderTestLength()
        {
            var loader = new ForestLoader(new string[] {"100KL1", 
                                                        "001111",
                                                        "K11100",
                                                        "000100"});
            var map = loader.LoadData().Item1;
            Assert.AreEqual(4, map.GetLength(0));
            Assert.AreEqual(6, map.GetLength(1));
        }
    }
}
