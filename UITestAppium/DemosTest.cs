using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;

namespace UITestAppium {
    [TestClass]
    public class DemosTest : AppSession {
        [TestMethod]
        public void TestMethod1() {
            session.FindElementByName("Configura").Click();
            session.FindElementByName("Limpia").Click();
            session.FindElementByName("Trabajos").Click();
            session.FindElementByName("Load").Click();
            //Thread.Sleep(5000);
            session.FindElementByName("Form").Click();
            //Thread.Sleep(5000);
        }

        [ClassInitialize]
        public static void ClassInitialize(TestContext context) {
            Setup(context);
        }
        [ClassCleanup]
        public static void ClassCleanup() {
            TearDown();
        }
    }
}
