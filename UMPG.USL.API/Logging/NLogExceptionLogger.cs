using NLog;
using System;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web.Http.ExceptionHandling;

namespace UMPG.USL.API.Logging
{
    public class NLogExceptionLogger : ExceptionLogger
    {
        private static readonly Logger Nlog = LogManager.GetCurrentClassLogger();

        public override void Log(ExceptionLoggerContext context)
        {
            Nlog.LogException(LogLevel.Debug, LogRequest(context), context.Exception);
        }

        private static string LogRequest(ExceptionLoggerContext context)
        {
            StringBuilder messageString = new StringBuilder();
            messageString = BuildLogMessage(messageString, context);
            return messageString.ToString();
        }

        private static StringBuilder BuildLogMessage(StringBuilder messageString, ExceptionLoggerContext context)
        {
            messageString.AppendLine(" ");
            messageString.AppendLine("-------------------------------------------");
            messageString.AppendLine();
            messageString.AppendLine("______New Exception______");
            messageString.AppendLine("Occurred On: " + GetDate() + " ||  At: " + GetTime() + " \n");
            messageString.AppendLine(LogException(context.Exception));
            messageString.AppendLine();
            messageString.AppendLine(" ").Append(HeadersToString(context.Request));
            messageString.AppendLine();
            messageString.AppendLine(" ").Append(PropertiesToString(context.Request));
            messageString.AppendLine();
            messageString.AppendLine(" ").Append(RequestToString(context.Request));
            messageString.AppendLine(" ");
            messageString.AppendLine(" ");
            messageString.AppendLine("______End Exception______");
            messageString.AppendLine("-------------------------------------------");
            messageString.AppendLine(" ");
            messageString.AppendLine(" ");
            return messageString;
        }

        private static string GetDate()
        {
            return DateTime.Now.ToLongDateString();
        }

        private static string GetTime()
        {
            return DateTime.Now.ToLongTimeString();
        }

        private static string PropertiesToString(HttpRequestMessage request)
        {
            var message = new StringBuilder();
            if (request.Properties != null)
            {
                message.AppendLine(" ").Append("Properties on request Object: ").AppendLine(" ");
                foreach (var key in request.Properties)
                {
                    message.AppendLine("Key: " + key.Key.ToString() + " -- Value: " + key.Value.ToString());
                }
            }

            return message.ToString();
        }

        private static string HeadersToString(HttpRequestMessage request)
        {
            var message = new StringBuilder();
            if (request.Headers != null)
            {
                message.Append("Headers: ").AppendLine(" ").Append(request.Headers);
            }
            return message.ToString();
        }

        private static string RequestToString(HttpRequestMessage request)
        {
            var message = new StringBuilder();
            if (request.Method != null)
            {
                message.Append("Request Method: ").AppendLine(" ");
                message.Append(request.Method);
            }

            if (request.RequestUri != null)
            {
                message.Append(" ").Append(request.RequestUri);
            }

            return message.ToString();
        }

        private static string LogException(Exception error)
        {
            Exception realerror = error;
            while (realerror.InnerException != null)
            {
                realerror = realerror.InnerException;
            }
            return realerror.ToString();
        }
    }
}