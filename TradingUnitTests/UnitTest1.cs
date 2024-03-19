using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Trading.Li;

namespace Trading.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            CollectionAssert.AreEqual("AAPL", ClientDatabase.);
        }
    }
}
