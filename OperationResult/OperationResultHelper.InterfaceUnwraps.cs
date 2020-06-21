using System;
using System.Runtime.CompilerServices;

namespace OperationResult
{
    public static partial class OperationResultHelper
    {
        /// <summary>
        /// Получить результат выполнения.
        /// </summary>
        /// <typeparam name="TUnwrapResult">Тип результата выполнения.</typeparam>
        /// <param name="operationResult">Результат операции.</param>
        /// <param name="onSuccess">Действие, которое необходимо выполнить, в случае успешного результата операции.</param>
        /// <param name="onError">Действие, которое необходимо выполнить, в случае неуспешного результата операции.</param>
        /// <returns>Результат выполнения.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TUnwrapResult Unwrap<TUnwrapResult>(
            this IResult<object> operationResult,
            Func<object, TUnwrapResult> onSuccess,
            Func<ErrorInfo, TUnwrapResult> onError
        )
        {
            return operationResult.IsSuccess
                ? onSuccess(operationResult.Value)
                : onError(operationResult.ErrorInfo);
        }

        /// <summary>
        /// Получить результат выполнения.
        /// </summary>
        /// <typeparam name="TUnwrapResult">Тип результата выполнения.</typeparam>
        /// <typeparam name="TResult">Тип результа операции.</typeparam>
        /// <param name="operationResult">Результат операции.</param>
        /// <param name="onSuccess">Действие, которое необходимо выполнить, в случае успешного результата операции.</param>
        /// <param name="onError">Действие, которое необходимо выполнить, в случае неуспешного результата операции.</param>
        /// <returns>Результат выполнения.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TUnwrapResult Unwrap<TUnwrapResult, TResult>(
            this IResult<TResult> operationResult,
            Func<TResult, TUnwrapResult> onSuccess,
            Func<ErrorInfo, TUnwrapResult> onError
        )
        {
            return operationResult.IsSuccess
                ? onSuccess(operationResult.Value)
                : onError(operationResult.ErrorInfo);
        }
    }
}
