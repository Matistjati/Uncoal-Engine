﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Console_game.Tests
{
    [TestClass()]
    public class PointExtensionsTests
    {
        static Random rnd = new Random();

        static Coord min;
        static Coord max;

        [ClassInitialize]
        public static void Start(TestContext t)
        {
            min = new Coord((uint)rnd.Next(), (uint)rnd.Next());
            max = new Coord(
                (uint)rnd.Next((int)min.X + 100, (int)min.X + 1000),
                (uint)rnd.Next((int)min.Y + 100, (int)min.Y + 1000));
        }

        [TestMethod()]
        public void PointExtensionsClampLessThanMin()
        {
            Coord vector = new Coord(
                (uint)rnd.Next((int)min.X - 100, (int)min.X - 10),
                (uint)rnd.Next((int)min.Y - 100, (int)min.Y - 10));

            vector.Clamp(min, max);
            Assert.AreEqual(min.X, vector.X);
            Assert.AreEqual(min.Y, vector.Y);
        }

        [TestMethod()]
        public void PointExtensionsClampGreaterThanMax()
        {
            Coord vector = new Coord(
                (uint)rnd.Next((int)max.X + 10, (int)max.X + 100),
                (uint)rnd.Next((int)max.Y + 10, (int)max.Y + 100));

            vector.Clamp(min, max);
            Assert.AreEqual(max.X, vector.X);
            Assert.AreEqual(max.Y, vector.Y);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void CoordXNegativeException()
        {
            Coord coord = new Coord(-20, 40);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void CoordYNegativeException()
        {
            Coord coord = new Coord(40, -20);
        }
    }
}