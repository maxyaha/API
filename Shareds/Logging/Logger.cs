using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NLog;
using Shareds.Logging.Extensions;
using Shareds.Logging.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Net;
using System.Reflection;
using System.Text;

namespace Shareds.Logging
{
    /// <summary>
    /// 
    /// </summary>
    public class Logger : Interfaces.ILogger
    {
        private readonly NLog.Logger logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="level"></param>
        /// <param name="message"></param>
        /// <param name="parameters"></param>
        private void WriteMessage(LogLevel level, string message, params Object[] parameters)
        {
            if (this.logger is null)
                // Return void, If NLog is null.
                return;
            if (!this.logger.IsEnabled(level))
                // Return void, If NLog level is enabled.
                return;

            LogEventInfo log =
                new LogEventInfo(level, this.logger.Name, message);
            log.Properties["Reference"] = Guid.NewGuid();
            log.Properties["AssemblyVersion"] = CallingAssemblyVersion();
            log.Properties["QueryString"] = null;
            log.Properties["FormData"] = null;

            this.logger.Log(GetType(), log);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="level"></param>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        private void WriteMessage(NLog.LogLevel level, string message, Exception exception)
        {
            if (this.logger is null)
                // Return void, If NLog is null.
                return;
            if (!this.logger.IsEnabled(level))
                // Return void, If NLog level is enabled.
                return;

            LogEventInfo log =
                new LogEventInfo(level, this.logger.Name, message);
            log.Exception = exception;
            log.Properties["Reference"] = Guid.NewGuid();
            log.Properties["AssemblyVersion"] = CallingAssemblyVersion();
            log.Properties["QueryString"] = null;
            log.Properties["FormData"] = null;

            this.logger.Log(GetType(), log);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="level"></param>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        /// <param name="request"></param>
        private void WriteMessage(LogLevel level, string message, Exception exception, HttpRequest request)
        {
            if (this.logger is null)
                // Return void, If NLog is null.
                return;
            if (!this.logger.IsEnabled(level))
                // Return void, If NLog level is enabled.
                return;

            LogEventInfo log =
                new LogEventInfo(level, this.logger.Name, message);
            log.Exception = exception;
            log.Properties["Reference"] = Guid.NewGuid();
            log.Properties["AssemblyVersion"] = CallingAssemblyVersion();
            log.Properties["QueryString"] = request is null
                ? null
                : String.Concat(request.Path, request.QueryString.ToString());
            log.Properties["FormData"] = request is null
                ? null
                : request.Form.ToString();

            this.logger.Log(GetType(), log);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        private bool IsEnabled(LogLevel level)
        {
            if (this.logger is null)
                // Return false, If NLog is null.
                return false;

            return this.logger.IsEnabled(level);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string CallingAssemblyVersion()
        {
            return FileVersionInfo
                .GetVersionInfo(Assembly.GetCallingAssembly().Location)
                .ProductVersion;
        }
#pragma warning disable S1144 // Unused private types or members should be removed
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private string ParameterBuilder(params object[] parameters)
        {
            StringBuilder build = new StringBuilder();

            foreach (var parameter in parameters)
            {
                build.AppendLine(LoggerExtension.Dump(parameter));
            }
            return build.ToString();
        }
#pragma warning restore S1144 // Unused private types or members should be removed


        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        private string ExceptionToString(Exception exception)
        {
            StringBuilder builder = new StringBuilder();

            switch (exception)
            {
                case null:
                    break;
                default:
                    builder.AppendLine(exception.Message);
                    builder.AppendLine("--------------");

                    Exception error = exception;

                    while (error.InnerException != null)
                    {
                        builder.AppendLine(error.InnerException.Message);
                        builder.AppendLine("--------------");
                        error = error.InnerException;
                    }
                    break;
            }

            builder.AppendLine(String.Format("{0}: {1}", exception.TargetSite.Name, exception.Message));
            builder.AppendLine("--------------");
            builder.AppendLine(String.Format("Stack Trace: {0}", exception.StackTrace));

            return builder.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        private string ExceptionToString(WebException exception)
        {
            return exception.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        private string ExceptionToString(DbUpdateException exception)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("DbUpdateException:");

            foreach (EntityEntry entry in exception.Entries)
            {
                string property = entry.GetType().Name;
                string error = exception?.InnerException?.Message;
                builder.AppendFormat("Property: {0} Error: {1}", property, error);
            }
            return builder.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        private string ExceptionToString(ValidationException exception)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("ValidationException:");

            string property = exception.GetType().Name;
            string error = exception?.Message;
            builder.AppendFormat("Property: {0} Error: {1}", property, error);

            return builder.ToString();
        }

        public Logger()
        {
            this.logger = NLog.LogManager.GetLogger(GetType().FullName);
        }

        /// <summary>
        /// Getting started logger.
        /// </summary>
        public bool Initialised { get { return !this.logger.Equals(null); } }
        /// <summary>
        /// Status enable debug.
        /// </summary>
        public bool IsDebugEnabled { get { return IsEnabled(LogLevel.Debug); } }
        /// <summary>
        /// Status enable error.
        /// </summary>
        public bool IsErrorEnabled { get { return IsEnabled(LogLevel.Error); } }
        /// <summary>
        /// Status enable fatal.
        /// </summary>
        public bool IsFatalEnabled { get { return IsEnabled(LogLevel.Fatal); } }
        /// <summary>
        /// Status enable info.
        /// </summary>
        public bool IsInfoEnabled { get { return IsEnabled(LogLevel.Info); } }
        /// <summary>
        /// Status enable trace.
        /// </summary>
        public bool IsTraceEnabled { get { return IsEnabled(LogLevel.Trace); } }
        /// <summary>
        /// Status enable warn.
        /// </summary>
        public bool IsWarnEnabled { get { return IsEnabled(LogLevel.Warn); } }

        /// <summary>
        /// Message sent debug.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="parameters"></param>
        public void Debug(string message, params Object[] parameters)
        {
            WriteMessage(LogLevel.Debug, message, parameters);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public void Debug(string message, Exception exception)
        {
            WriteMessage(LogLevel.Debug, message, exception);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        /// <param name="request"></param>
        public void Debug(string message, Exception exception, HttpRequest request)
        {
            WriteMessage(LogLevel.Debug, message, exception, request);
        }
        /// <summary>
        /// Message sent error.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="parameters"></param>
        public void Error(string message, params Object[] parameters)
        {
            WriteMessage(LogLevel.Error, message, parameters);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public void Error(string message, Exception exception)
        {
            WriteMessage(LogLevel.Error, message, exception);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        /// <param name="request"></param>
        public void Error(string message, Exception exception, HttpRequest request)
        {
            WriteMessage(LogLevel.Error, message, exception, request);
        }
        /// <summary>
        /// Message sent fatal.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="parameters"></param>
        public void Fatal(string message, params Object[] parameters)
        {
            WriteMessage(LogLevel.Fatal, message, parameters);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public void Fatal(string message, Exception exception)
        {
            WriteMessage(LogLevel.Fatal, message, exception);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        /// <param name="request"></param>
        public void Fatal(string message, Exception exception, HttpRequest request)
        {
            WriteMessage(LogLevel.Fatal, message, exception, request);
        }
        /// <summary>
        /// Message sent info.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="parameters"></param>
        public void Info(string message, params Object[] parameters)
        {
            WriteMessage(LogLevel.Info, message, parameters);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public void Info(string message, Exception exception)
        {
            WriteMessage(LogLevel.Info, message, exception);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        /// <param name="request"></param>
        public void Info(string message, Exception exception, HttpRequest request)
        {
            WriteMessage(LogLevel.Info, message, exception, request);
        }
        /// <summary>
        /// Message sent trace.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="parameters"></param>
        public void Trace(string message, params Object[] parameters)
        {
            WriteMessage(LogLevel.Trace, message, parameters);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public void Trace(string message, Exception exception)
        {
            WriteMessage(LogLevel.Trace, message, exception);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        /// <param name="request"></param>
        public void Trace(string message, Exception exception, HttpRequest request)
        {
            WriteMessage(LogLevel.Trace, message, exception, request);
        }
        /// <summary>
        /// Message sent warn.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="parameters"></param>
        public void Warn(string message, params Object[] parameters)
        {
            WriteMessage(LogLevel.Warn, message, parameters);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public void Warn(string message, Exception exception)
        {
            WriteMessage(LogLevel.Warn, message, exception);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        /// <param name="request"></param>
        public void Warn(string message, Exception exception, HttpRequest request)
        {
            WriteMessage(LogLevel.Warn, message, exception, request);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="type"></param>
        public void LogException(Exception exception, ExceptionType type)
        {
            if (type.Equals(ExceptionType.Unhandled))
                Fatal(ExceptionToString(exception), exception);
            else
                Error(ExceptionToString(exception), exception);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="type"></param>
        /// <param name="message"></param>
        public void LogException(Exception exception, ExceptionType type, string message)
        {
            if (type.Equals(ExceptionType.Unhandled))
                Fatal(message, exception);
            else
                Error(message, exception);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="type"></param>
        /// <param name="request"></param>
        public void LogException(Exception exception, ExceptionType type, HttpRequest request)
        {
            if (type.Equals(ExceptionType.Unhandled))
                Fatal(ExceptionToString(exception), exception, request);
            else
                Error(ExceptionToString(exception), exception, request);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="type"></param>
        /// <param name="request"></param>
        /// <param name="message"></param>
        public void LogException(Exception exception, ExceptionType type, HttpRequest request, string message)
        {
            if (type.Equals(ExceptionType.Unhandled))
                Fatal(message, exception, request);
            else
                Error(message, exception, request);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="type"></param>
        /// <param name="request"></param>
        public void LogException(WebException exception, ExceptionType type)
        {
            if (type.Equals(ExceptionType.Unhandled))
                Fatal(ExceptionToString(exception), exception);
            else
                Error(ExceptionToString(exception), exception);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        public void LogException(DbUpdateException exception)
        {
            Error(ExceptionToString(exception), exception);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        public void LogException(ValidationException exception)
        {
            Error(ExceptionToString(exception), exception);
        }
    }
}
