using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using log4net;
using log4net.Config;

namespace AlphaPoint_QA.Utils
{
    public class APLogger
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(APLogger));

        static APLogger()
        {
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            var fileinfo = new FileInfo("log4net.config");

            if (fileinfo.Exists)
                XmlConfigurator.ConfigureAndWatch(logRepository, fileinfo);
        }

        public static ILog GetLog()
        {
            return log;
        }

    }
}
