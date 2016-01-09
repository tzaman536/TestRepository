using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;

namespace PhenixTools.Logger
{
    public class SimplexLogger
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(SimplexLogger));

        public static void PrintLogTest()
        {
            logger.Debug("Here is a debug log.");
            logger.Info("... and an Info log.");
            logger.Warn("... and a warning.");
            logger.Error("... and an error.");
            logger.Fatal("... and a fatal error.");
        }

        public static void Info(string message, Exception ex = null)
        {
            if(string.IsNullOrEmpty(message))
                logger.Info(ex.Message);
            else
                logger.Info(message);

            if (ex != null)
            {
                logger.Info(null, ex);
            }
        }

        public static void Debug(string message, Exception ex = null)
        {
            if (string.IsNullOrEmpty(message))
                logger.Debug(ex.Message);
            else
                logger.Debug(message);

            if (ex != null)
            {
                logger.Debug(null, ex);
            }
        }
        public static void Warn(string message, Exception ex = null)
        {
            if (string.IsNullOrEmpty(message))
                logger.Warn(ex.Message);
            else
                logger.Warn(message);

            if (ex != null)
            {
                logger.Warn(null, ex);
            }
        }
        public static void Error(string message, Exception ex = null)
        {
            if (string.IsNullOrEmpty(message))
                logger.Error(ex.Message);
            else
                logger.Error(message);

            if (ex != null)
            {
                logger.Error(null, ex);
            }
        }
        public static void Fatal(string message, Exception ex = null)
        {
            if (string.IsNullOrEmpty(message))
                logger.Fatal(ex.Message);
            else
                logger.Fatal(message);

            if (ex != null)
            {
                logger.Fatal(null, ex);
            }
        }


    }

}
