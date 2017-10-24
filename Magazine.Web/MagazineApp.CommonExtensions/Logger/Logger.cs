using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagazineApp.Logging
{
    public class Logger {
        private static ILog log = LogManager.GetLogger("LOGGER");

        public static ILog Log {
            get { return log; }
        }

        public static void InitLogger() {
            XmlConfigurator.Configure();
        }

        public static void Info(string message) {
            log.Info(message);
        }

        public static void Warn(string message) {
            log.Warn(message);
        }

        public static void Error(string message) {
            log.Error(message);
        }

        public static void Error(string message, Exception ex) {
            log.Error(message,ex);
        }

        public static void Fatal(string message) {
            log.Fatal(message);
        }

        public static void Debug(string message) {
            log.Debug(message);
        }
        public static void Debug(string message,Exception ex) {
            log.Debug(message, ex);
        }

    }
}
