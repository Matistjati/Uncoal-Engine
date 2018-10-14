﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;

namespace Console_game.Tests
{
    public class SampleGameObject : GameObject
    {
        public float total;
        public void TestTimeAccuracy()
        {
            total += TimeDelta;
        }
    }

    [TestClass]
    public class GameObjectTests
    {
        static DateTime start;

        [TestMethod]
        public void GameObjectTestTimeAccuracy()
        {

            // This test is slow as we need to sleep for it to work
            SampleGameObject gameObject = new SampleGameObject();

            MethodInfo methodInfo = ReflectiveHelper<GameObject>.GetMethodInfoFromInstance<SampleGameObject>(
                                                                                       gameObject.TestTimeAccuracy);

            FrameRunner.AddFrameSubscriber(methodInfo, gameObject);
            start = DateTime.Now;
            // Let's avoid getting in an infinite loop, shall we?
            new Thread(() =>
            {
                // Make this value higher for a better test
                Thread.Sleep(17 * 3);
                FrameRunner.Pause();
            }).Start();
            FrameRunner.Run();


            Assert.IsTrue(Math.Round((start - DateTime.Now).TotalSeconds, 3) - Math.Round(gameObject.total, 3) < 0.001);
            Assert.IsTrue(Math.Round((start - DateTime.Now).TotalSeconds, 3) - Math.Round(GameObject.Time, 3) < 0.001);
        }
    }
}
