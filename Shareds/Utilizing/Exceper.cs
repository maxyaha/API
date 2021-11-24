using System;

namespace Shareds.Utilizing
{
    public static class Exceper
    {
        /// <summary>
        /// Custom exception.
        /// </summary>
        public const string ConcurrencyAggregatedFormat = "Aggregate {0} has been previously modified.";
        /// <summary>
        /// Custom exception.
        /// </summary>
        public const string ConcurrencyAlreadyExistFormat = "The {0} already exist: {1}.";
        /// <summary>
        /// Custom exception.
        /// </summary>
        public const string UnregisteredDomainCommandHandledFormat = "No handler registered {0}.";
        /// <summary>
        /// System exception.
        /// </summary>
        public const string InvalidOperationInitializedFormat = "Initialized empty {0}.";
        /// <summary>
        /// System exception.
        /// </summary>
        public const string NotImplementedRequirementedFormat = "The {0} does not meet business requirements.";
        /// <summary>
        /// System exception.
        /// </summary>
        public const string ArgumentNullTypeFormat = "Don't know any implementation of this type: {0}.";
        /// <summary>
        /// System exception.
        /// </summary>
        public const string ArgumentNullDataFormat = "Don't know any implementation of this data: {0}.";
        /// <summary>
        /// System exception.
        /// </summary>
        public const string ValidationFailed = "EF Validation failed, see inner exception for details.";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="format"></param>
        /// <param name="arg0"></param>
        /// <returns></returns>
        public static string Format(string format, Type type)
        {
            return !IsExceptionFormat(format)
                ? null
                : string.Format(format, type.Name);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="format"></param>
        /// <param name="arg0"></param>
        /// <returns></returns>
        public static string Format(string format, Exception inner)
        {
            return !IsExceptionFormat(format)
                ? null
                : string.Format(format, inner.Message);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="format"></param>
        /// <param name="arg0"></param>
        /// <returns></returns>
        public static string Format(string format, object arg0)
        {
            return !IsExceptionFormat(format)
                ? null
                : string.Format(format, arg0);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="format"></param>
        /// <param name="arg0"></param>
        /// <param name="arg1"></param>
        /// <returns></returns>
        public static string Format(string format, object arg0, object arg1)
        {
            return !IsExceptionFormat(format)
                ? null
                : string.Format(format, arg0, arg1);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="format"></param>
        /// <param name="arg0"></param>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        /// <returns></returns>
        public static string Format(string format, object arg0, object arg1, object arg2)
        {
            return !IsExceptionFormat(format)
                ? null
                : string.Format(format, arg0, arg1, arg2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        private static bool IsExceptionFormat(this string format)
        {
            return  string.IsNullOrWhiteSpace(format) && (format == ConcurrencyAggregatedFormat
                            || format == ConcurrencyAlreadyExistFormat
                            || format == UnregisteredDomainCommandHandledFormat
                            || format == InvalidOperationInitializedFormat
                            || format == NotImplementedRequirementedFormat
                            || format == ArgumentNullTypeFormat
                            || format == ArgumentNullDataFormat);

        }

    }
}
