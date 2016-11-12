using Castle.Core.Logging;
using System;

namespace App.Presentation.Web
{
    internal static class LoggerExtensions
    {
        /// <summary>
        /// Logs the exception.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="exception">The exception.</param>
        public static void LogException(this ILogger logger, Exception exception)
        {
            logger.Fatal(
                string.Format(
                    "Exception[{1}]=\"{2}{0}===== Inner Exception ====={0}{3}{0}===== Stack Trace ====={0}{4}",
                    Environment.NewLine,
                    exception.GetType().Name,
                    exception.Message,
                    exception.InnerException,
                    exception.StackTrace));
        }
    }
}