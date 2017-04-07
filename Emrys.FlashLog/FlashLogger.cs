using log4net;
using log4net.Config;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Emrys.FlashLog
{
    public class FlashLogger
    {
        /// <summary>
        /// 记录消息Queue
        /// </summary>
        private ConcurrentQueue<FlashLogMessage> _que = new ConcurrentQueue<FlashLogMessage>();

        /// <summary>
        /// 信号
        /// </summary>
        private ManualResetEvent _mre = new ManualResetEvent(false);

        /// <summary>
        /// 日志
        /// </summary>
        private ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// 日志
        /// </summary>
        private static FlashLogger _flashLog;

        /// <summary>
        /// 第一次运行初始化配置
        /// </summary>
        static FlashLogger()
        {
            // 设置日志配置文件路径
            XmlConfigurator.Configure(new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log4net.config")));
            _flashLog = new FlashLogger();
        }

        /// <summary>
        /// 实现单例
        /// </summary>
        /// <returns></returns>
        public static FlashLogger Instanse()
        {
            return _flashLog;
        }

        /// <summary>
        /// 另一个线程记录日志，只在程序初始化时调用一次
        /// </summary>
        public void Register()
        {
            Thread t = new Thread(new ThreadStart(WriteLog));
            t.IsBackground = false;
            t.Start();
        }

        /// <summary>
        /// 从队列中写日志至磁盘
        /// </summary>
        private void WriteLog()
        {
            while (true)
            {
                // 等待信号通知
                _mre.WaitOne();

                // 判断是否有内容需要如磁盘
                while (_que.Count > 0)
                {
                    FlashLogMessage msg;
                    if (_que.TryDequeue(out msg)) // 从列队中获取内容，并删除列队中的内容
                    {
                        // 判断日志等级，然后写日志
                        switch (msg.Level)
                        {
                            case FlashLogLevel.Debug:
                                _log.Debug(msg.Message, msg.Exception);
                                break;
                            case FlashLogLevel.Info:
                                _log.Info(msg.Message, msg.Exception);
                                break;
                            case FlashLogLevel.Error:
                                _log.Error(msg.Message, msg.Exception);
                                break;
                            case FlashLogLevel.Warn:
                                _log.Warn(msg.Message, msg.Exception);
                                break;
                            case FlashLogLevel.Fatal:
                                _log.Fatal(msg.Message, msg.Exception);
                                break;
                        }

                    }
                }

                // 重新设置信号
                _mre.Reset();
            }
        }


        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="message">日志文本</param>
        /// <param name="level">等级</param>
        /// <param name="ex">Exception</param>
        public void EnqueueMessage(string message, FlashLogLevel level, Exception ex = null)
        {
            if ((level == FlashLogLevel.Debug && _log.IsDebugEnabled)
             || (level == FlashLogLevel.Error && _log.IsErrorEnabled)
             || (level == FlashLogLevel.Fatal && _log.IsFatalEnabled)
             || (level == FlashLogLevel.Info && _log.IsInfoEnabled)
             || (level == FlashLogLevel.Warn && _log.IsWarnEnabled))
            {
                _que.Enqueue(new FlashLogMessage { Message = message, Level = level, Exception = ex });

                // 通知线程往磁盘中写日志
                _mre.Set();
            }
        }

        public static void Debug(string msg, Exception ex = null)
        {
            Instanse().EnqueueMessage(msg, FlashLogLevel.Debug, ex);
        }

        public static void Error(string msg, Exception ex = null)
        {
            Instanse().EnqueueMessage(msg, FlashLogLevel.Error, ex);
        }

        public static void Fatal(string msg, Exception ex = null)
        {
            Instanse().EnqueueMessage(msg, FlashLogLevel.Fatal, ex);
        }

        public static void Info(string msg, Exception ex = null)
        {
            Instanse().EnqueueMessage(msg, FlashLogLevel.Info, ex);
        }

        public static void Warn(string msg, Exception ex = null)
        {
            Instanse().EnqueueMessage(msg, FlashLogLevel.Warn, ex);
        }

    }

    
  


   
}
