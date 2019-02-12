using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using log4net;
using log4net.Config;
using OpenQA.Selenium;
using Xunit.Abstractions;

namespace AlphaPoint_QA.Utils
{
    public class TestBase
    {
        private ILog logger { get; set; }
        public ProgressLogger TestProgressLogger { get; private set; }
        internal Config data { get; set; }
        internal IWebDriver driver { get; set; }

        public TestBase(ITestOutputHelper output)
        {
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            var fileinfo = new FileInfo(Const.LogFileName);

            if (fileinfo.Exists)
                XmlConfigurator.ConfigureAndWatch(logRepository, fileinfo);
            logger = LogManager.GetLogger(this.GetType());


            data = ConfigManager.Instance;
            driver = AlphaPointWebDriver.GetInstanceOfAlphaPointWebDriver();
            TestProgressLogger = new ProgressLogger(output, logger, driver);
        }

    }

    public class ProgressLogger
    {
        TestLog testLog;
        internal ITestOutputHelper Output { get; set; }
        ILog logger;
        IWebDriver driver;

        public ProgressLogger(ITestOutputHelper output, ILog logger, IWebDriver driver)
        {
            this.Output = output;
            this.logger = logger;
            this.driver = driver;
            testLog = new TestLog();
        }

        public void StartTest([CallerMemberName] String methodName = "")
        {
            logger.Info("Test Started:" + methodName);
            testLog.StartTime = GetTime();
        }

        public void EndTest([CallerMemberName] String methodName = "")
        {
            logger.Info("Test End:" + methodName);
            testLog.Endtime = GetTime();
            DumpLog();
        }

        public void Info(string message)
        {
            logger.Info(message);
        }
        public void Error(string message, Exception ex)
        {
            logger.Error(message, ex);
        }

        public void Error(string message)
        {
            logger.Error(message);
        }

        public void TakeScreenshot([CallerMemberName] String methodName = "")
        {
            GenericUtils.GetScreenshot(driver, methodName);
        }

        void DumpLog()
        {

            var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(testLog);
            this.Output.WriteLine(jsonString);

        }

        string GetTime()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm tt");
        }


        public void LogCheckPoint(string value)
        {
            logger.Info("CheckPoint:" + value);
            testLog.AddLog("CheckPoint", value, GetTime());
        }
    }


    public class TestLog
    {
        public string StartTime { get; set; }
        public string Endtime { get; set; }
        public List<LogInfo> EventLog { get; set; }
        public TestLog()
        {
            EventLog = new List<LogInfo>();
        }

        public void AddLog(string eventTitle, string eventDetails, string timeStamp)
        {
            EventLog.Add(new LogInfo(eventTitle, eventDetails, timeStamp));
        }
    }

    public class LogInfo
    {
        public string EventTitle { get; set; }
        public string EventDetails { get; set; }
        public string TimeStamp { get; set; }

        public LogInfo(string eventTitle, string eventDetails, string timeStamp)
        {
            this.EventTitle = EventTitle;
            this.EventDetails = eventDetails;
            this.TimeStamp = timeStamp;
        }

        public LogInfo()
        {

        }

    }
}
