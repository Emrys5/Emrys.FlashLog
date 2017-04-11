# Emrys.FlashLog
.net 用队列超高速写日志

## Start
### 1、在程序开始注册FlashLog
如在MVC中，在Global中的Application_Start注册FlashLog即可。
```
FlashLogger.Instance().Register();
```

### 2、写日志
```
  FlashLogger.Debug("Debug");
  FlashLogger.Debug("Debug", new Exception("testexception"));
  FlashLogger.Info("Info");
  FlashLogger.Fatal("Fatal");
  FlashLogger.Error("Error");
  FlashLogger.Warn("Warn", new Exception("testexception"));
```
## 备注
#### 1、基于log4net
目前写日志基于log4net，也可以扩展使用其他的组件
[log4net](https://github.com/apache/log4net)

#### 2、流程
由于日志的耗时全部是IO上，所以现在是把日志放到队列中，然后让一个线程从队列中获取日志。这个导致的问题就是如果程序突然断电或者程序崩溃了，那么在队列中的还没有写到磁盘上的日志将丢失。

更多详情请至
http://www.cnblogs.com/emrys5/p/flashlog.html
