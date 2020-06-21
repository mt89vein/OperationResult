using System.Runtime.CompilerServices;

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
        public static ErrorTag<TError> Error<TError>(in TError error)
        {
            return new ErrorTag<TError>(error);
        }

        /// <summary>
        /// Создает результат операции "Ошибка" с данными об ошибке.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ErrorTag<TError> Error<TError>(in ErrorInfo errorInfo, in TError error)
        {
            return new ErrorTag<TError>(errorInfo, error);
        }

        /// <summary>
        /// Создает результат операции "Ошибка" с данными об ошибке.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ErrorTag<TError> Error<TError>(in int errorCode, in string errorMessage, in TError error)
        {
            return new ErrorTag<TError>(new ErrorInfo(errorCode, errorMessage), error);
        }

        /// <summary>
        /// Создает результат операции "Ошибка" с данными об ошибке.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ErrorTag Error(in int errorCode, in string errorMessage)
        {
            return new ErrorTag(new ErrorInfo(errorCode, errorMessage));
        }
    }
}