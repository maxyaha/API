using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Shareds.Logging.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public enum ExceptionType
    {
        /// <summary>
        /// Can handle.
        /// </summary>
        Handled = 0,
        /// <summary>
        /// Can not handle.
        /// </summary>
        Unhandled = 1
    }
    /// <summary>
    /// 
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Getting started logger.
        /// </summary>
        bool Initialised { get; }
        /// <summary>
        /// Status enable debug.
        /// </summary>
        bool IsDebugEnabled { get; }
        /// <summary>
        /// Status enable error.
        /// </summary>
        bool IsErrorEnabled { get; }
        /// <summary>
        /// Status enable fatal.
        /// </summary>
        bool IsFatalEnabled { get; }
        /// <summary>
        /// Status enable info.
        /// </summary>
        bool IsInfoEnabled { get; }
        /// <summary>
        /// Status enable trace.
        /// </summary>
        bool IsTraceEnabled { get; }
        /// <summary>
        /// Status enable warn.
        /// </summary>
        bool IsWarnEnabled { get; }

        /// <summary>
        /// Message sent debug.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="parameters"></param>
        void Debug(string message, params Object[] parameters);
        /// <summary>
        /// Message sent error.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="parameters"></param>
        void Error(string message, params Object[] parameters);
        /// <summary>
        /// Message sent fatal.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="parameters"></param>
        void Fatal(string message, params Object[] parameters);
        /// <summary>
        /// Message sent info.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="parameters"></param>
        void Info(string message, params Object[] parameters);
        /// <summary>
        /// Message sent trace.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="parameters"></param>
        void Trace(string message, params Object[] parameters);
        /// <summary>
        /// Message sent warn.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="parameters"></param>
        void Warn(string message, params Object[] parameters);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="type"></param>
        void LogException(Exception exception, ExceptionType type);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="type"></param>
        /// <param name="message"></param>
        void LogException(Exception exception, ExceptionType type, string message);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="type"></param>
        /// <param name="request"></param>
        void LogException(Exception exception, ExceptionType type, HttpRequest request);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="type"></param>
        /// <param name="request"></param>
        /// <param name="message"></param>
        void LogException(Exception exception, ExceptionType type, HttpRequest request, string message);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="type"></param>
        void LogException(WebException exception, ExceptionType type);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        void LogException(DbUpdateException exception);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        void LogException(ValidationException exception);
    }
}
