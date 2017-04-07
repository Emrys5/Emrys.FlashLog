# Emrys.FlashLog
.net 用队列超高速写日志

## Start
### 1、在程序开始注册FlashLog
如在MVC中，在Global中的Application_Start注册FlashLog即可。
```
FlashLogger.Instanse().Register();
```

### 2、写日志
```
FlashLog.Debug("日志内容");
```
## 备注
#### 1、基于log4net
