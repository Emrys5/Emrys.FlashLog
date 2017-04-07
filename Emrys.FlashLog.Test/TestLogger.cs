using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Emrys.FlashLog.Test
{
    [TestClass]
    public class TestLogger
    {
        [TestMethod]
        public void TestWriteLog()
        {
            FlashLogger.Instanse().Register();

            FlashLogger.Debug("TestWriteLog");
        }
    }
}
