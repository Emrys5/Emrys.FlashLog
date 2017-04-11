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
            FlashLogger.Instance().Register();

            FlashLogger.Debug("TestWriteLog");
            FlashLogger.Debug("TestWriteLog1");
            FlashLogger.Debug("TestWriteLog2");
            FlashLogger.Debug("TestWriteLog3");
            FlashLogger.Debug("TestWriteLog4");
            FlashLogger.Debug("TestWriteLog5");

        }
    }
}
