using System;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;

namespace OperationResult
{
    /// <summary>
    /// Вспомогательный класс, для уменьшения бойлерплейт кода.
    /// </summary>
    public static partial class OperationResultHelper
    {
        #region Поля

        /// <summary>
        /// Кэшированный тэг с ошибкой.
        /// </summary>
        private static readonly ErrorTag _errorTag = new ErrorTag();

        /// <summary>
        /// Кэшированный тэг успешного результата.
        /// </summary>
        private static readonly SuccessTag _successTag = new SuccessTag();

        #endregion Поля

        #region Методы (public)

        /// <summary>
        /// Создает успешный результат операции.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SuccessTag Ok()
        {
            return _successTag;
        }

        /// <summary>
        /// Создает успешный результат операции с данными.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SuccessTag<TResult> Ok<TResult>(TResult result)
        {
            return new SuccessTag<TResult>(result);
        }

        /// <summary>
        /// Создает результат операции "Ошибка".
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ErrorTag Error()
        {
            return _errorTag;
        }

        /// <summary>
        /// Создает результат операции "Ошибка" с данными об ошибке.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ErrorTag Error(in ErrorInfo errorInfo)
        {
            return new ErrorTag(errorInfo);
        }

        /// <summary>
        /// Создает результат операции "Ошибка" с данными об ошибке.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ErrorTag Error(in int errorCode, in string errorMessage)
        {
            return new ErrorTag(new ErrorInfo(errorCode, errorMessage));
        }

        /// <summary>
        /// Создает результат операции "Ошибка" с данными об ошибке.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ErrorTag Error(in Exception exception)
        {
            return new ErrorTag(new ErrorInfo(ExceptionDispatchInfo.Capture(exception)));
        }

        #endregion Методы (public)
    }
}