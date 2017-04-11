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

            FlashLogger.Debug("Debug");
            FlashLogger.Debug("Debug", new Exception("testexception"));
            FlashLogger.Info("Info");
            FlashLogger.Fatal("Fatal");
            FlashLogger.Error("Error");
            FlashLogger.Warn("Warn", new Exception("testexception"));

        }
    }
}
