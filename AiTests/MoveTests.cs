using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ForestProject;
using System.IO;

namespace AiTests
{
    [TestClass]
    public class MoveTests
    {
        void GeneralAiTest(string fileName, Point source, Point target, bool result)
        {
            //var lines = File.ReadAllLines(fileName);
            //var loader = new ForestLoader(lines);
            //var map = loader.LoadData().Item1;
            //var forest = new Forest(map);
            //var hero = new Hero(2, "A", target);
            //forest.SetHero(hero, source);
            //IAi ai = new Ai(hero, forest);
            //forest.AddBot(ai);
            //forest.DoAllMoves();
            //Assert.AreEqual(forest.IsPath, result);
        }

        [TestMethod]
        public void AiTest1()
        {
            GeneralAiTest("test3.txt", new Point(1, 1), new Point(4, 1), false);
        }

        [TestMethod]
        public void AiTest2()
        {
            GeneralAiTest("test2.txt", new Point(1, 1), new Point(0, 2), true);
        }

        [TestMethod]
        public void AiTest3()
        {
            GeneralAiTest("test2.txt", new Point(1, 1), new Point(2, 3), true);
        }

        [TestMethod]
        public void AiTest4()
        {
            GeneralAiTest("test1.txt", new Point(1, 1), new Point(4, 5), true);
        }

        [TestMethod]
        public void AiTest5()
        {
            GeneralAiTest("test4.txt", new Point(1, 1), new Point(0, 3), false);
        }
    }
}
