using System;
using System.Runtime.CompilerServices;

namespace OperationResult
{
    public static partial class OperationResultHelper
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TUnwrapResult Unwrap<TUnwrapResult>(
            this IResult<object> operationResult,
            Func<object, TUnwrapResult> onSuccess,
            Func<TUnwrapResult> onFailure
        )
        {
            return operationResult.IsSuccess
                ? onSuccess(operationResult.Value)
                : onFailure();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TUnwrapResult Unwrap<TUnwrapResult, TResult>(
            this IResult<TResult> operationResult,
            Func<TResult, TUnwrapResult> onSuccess,
            Func<TUnwrapResult> onFailure
        )
        {
            return operationResult.IsSuccess
                ? onSuccess(operationResult.Value)
                : onFailure();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TUnwrapResult Unwrap<TUnwrapResult, TResult, TError>(
            this IResult<TResult, TError> operationResult,
            Func<TResult, TUnwrapResult> onSuccess,
            Func<TError, TUnwrapResult> onError
        )
        {
            if (operationResult.IsSuccess)
            {
                return onSuccess(operationResult.Value);
            }

            if (operationResult.ErrorInfo is TError error)
            {
                return onError(error);
            }

            return onError(operationResult.Error);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TUnwrapResult Unwrap<TUnwrapResult, TResult, TError1, TError2>(
            this IResult<TResult, TError1, TError2> operationResult,
            Func<TResult, TUnwrapResult> onSuccess,
            Func<TError1, TUnwrapResult> onError1,
            Func<TError2, TUnwrapResult> onError2
        )
        {
            if (operationResult.IsSuccess)
            {
                return onSuccess(operationResult.Value);
            }

            if (operationResult.ErrorInfo is TError1 error1)
            {
                return onError1(error1);
            }

            if (operationResult.ErrorInfo is TError2 error2)
            {
                return onError2(error2);
            }

            if (operationResult.HasError<TError1>())
            {
                return onError1(operationResult.GetError<TError1>());
            }

            return onError2(operationResult.GetError<TError2>());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TUnwrapResult Unwrap<TUnwrapResult, TResult, TError1, TError2, TError3>(
            this IResult<TResult, TError1, TError2, TError3> operationResult,
            Func<TResult, TUnwrapResult> onSuccess,
            Func<TError1, TUnwrapResult> onError1,
            Func<TError2, TUnwrapResult> onError2,
            Func<TError3, TUnwrapResult> onError3
        )
        {
            if (operationResult.IsSuccess)
            {
                return onSuccess(operationResult.Value);
            }

            if (operationResult.ErrorInfo is TError1 error1)
            {
                return onError1(error1);
            }

            if (operationResult.ErrorInfo is TError2 error2)
            {
                return onError2(error2);
            }

            if (operationResult.ErrorInfo is TError2 error3)
            {
                return onError2(error3);
            }

            if (operationResult.HasError<TError1>())
            {
                return onError1(operationResult.GetError<TError1>());
            }

            if (operationResult.HasError<TError2>())
            {
                return onError2(operationResult.GetError<TError2>());
            }

            return onError3(operationResult.GetError<TError3>());
        }
    }
}
