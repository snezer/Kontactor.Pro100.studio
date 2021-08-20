using System;
using Microsoft.Extensions.Logging;

namespace netcoreservice.Service.Extensions
{
    internal static class LoggerExtensions
    {
        private static readonly object[] EmptyArgs = new object[0];

        internal static IDisposable BeginScope(this ILogger logger, string message) =>
            logger.BeginScope(message, EmptyArgs);

        /// <summary>
        /// Отмечает начало логируемой области и возвращает ее экземпляр.
        /// </summary>
        /// <typeparam name="T1">Тип передаваемого параметра</typeparam>
        /// <param name="logger">Экземпляр логера</param>
        /// <param name="message">Логируемое сообщение</param>
        /// <param name="arg1">Передаваемый параметр</param>
        /// <returns>Экземпляр созданной области логирования</returns>
        /// <remarks>При использовании проверить вызов метода на наличие возможности бесконечной рекурсии.</remarks>
        internal static IDisposable BeginScope<T1>(this ILogger logger, string message, T1 arg1) =>
            logger.BeginScope(message, new object[]
            {
                arg1?.ToString()
            });

        internal static IDisposable BeginScope<T1, T2>(this ILogger logger, string message, T1 arg1, T2 arg2) =>
            logger.BeginScope(message, new object[]
            {
                arg1?.ToString(),
                arg2?.ToString()
            });

        internal static IDisposable BeginScope<T1, T2, T3>(this ILogger logger, string message, T1 arg1, T2 arg2, T3 arg3) =>
            logger.BeginScope(message, new object[]
            {
                arg1?.ToString(),
                arg2?.ToString(),
                arg3?.ToString()
            });

        internal static void LogDebug(this ILogger logger, string message)
        {
            if (logger.IsEnabled(LogLevel.Debug))
                logger.LogDebug(message, EmptyArgs);
        }

        internal static void LogDebug<T1>(this ILogger logger, string message, T1 arg1)
        {
            if (logger.IsEnabled(LogLevel.Debug))
                logger.LogDebug(message, new object[]
                {
                    arg1?.ToString()
                });
        }

        internal static void LogDebug<T1, T2>(this ILogger logger, string message, T1 arg1, T2 arg2)
        {
            if (logger.IsEnabled(LogLevel.Debug))
                logger.LogDebug(message, new object[]
                {
                    arg1?.ToString(),
                    arg2?.ToString()
                });
        }

        internal static void LogDebug<T1, T2, T3>(this ILogger logger, string message, T1 arg1, T2 arg2, T3 arg3)
        {
            if (logger.IsEnabled(LogLevel.Debug))
                logger.LogDebug(message, new object[]
                {
                    arg1?.ToString(),
                    arg2?.ToString(),
                    arg3?.ToString()
                });
        }
    }
}