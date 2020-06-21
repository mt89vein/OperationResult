namespace OperationResult
{
    /// <summary>
    /// Интерфейс, представляющий результат выполнения операции.
    /// </summary>
    /// <typeparam name="TResult">Тип значения результата.</typeparam>
    public interface IResult<out TResult>
    {
        /// <summary>
        /// Значение результата операции.
        /// </summary>
        TResult Value { get; }

        /// <summary>
        /// Является ли результат выполнения успешным.
        /// </summary>
        bool IsSuccess { get; }

        /// <summary>
        /// Является ли результат выполнения не успешным.
        /// </summary>
        bool IsError { get; }
    }

    /// <summary>
    /// Интерфейс, представляющий результат выполнения операции,
    /// с указанным вариантом ошибки.
    /// </summary>
    /// <typeparam name="TResult">Тип значения результата.</typeparam>
    /// <typeparam name="TError">Тип данных ошибки.</typeparam>
    public interface IResult<TResult, TError> : IResult<TResult>
    {
        /// <summary>
        /// Данные об ошибке.
        /// </summary>
        TError Error { get; }

        /// <summary>
        /// Мета-данные ошибки.
        /// </summary>
        public ErrorInfo ErrorInfo { get; }

        void Deconstruct(out TResult result, out TError error, out ErrorInfo errorInfo)
        {
            result = Value;
            error = Error;
            errorInfo = ErrorInfo;
        }
    }

    /// <summary>
    /// Интерфейс, представляющий результат выполнения операции,
    /// с двумя вариантами ошибки.
    /// </summary>
    /// <typeparam name="TResult">Тип значения результата.</typeparam>
    /// <typeparam name="TError1">Первый тип данных ошибки.</typeparam>
    /// <typeparam name="TError2">Второй тип данных ошибки.</typeparam>
    public interface IResult<TResult, TError1, TError2> : IResult<TResult>
    {
        /// <summary>
        /// Данные об ошибке.
        /// </summary>
        object Error { get; }

        /// <summary>
        /// Мета-данные ошибки.
        /// </summary>
        public ErrorInfo ErrorInfo { get; }

        bool HasError<TError>() => Error is TError;

        TError GetError<TError>()
        {
            return Error is TError error
                ? error
                : default;
        }

        void Deconstruct(out TResult result, out TError1 error, out ErrorInfo errorInfo)
        {
            result = Value;
            error = GetError<TError1>();
            errorInfo = ErrorInfo;
        }

        void Deconstruct(out TResult result, out TError2 error, out ErrorInfo errorInfo)
        {
            result = Value;
            error = GetError<TError2>();
            errorInfo = ErrorInfo;
        }
    }

    /// <summary>
    /// Интерфейс, представляющий результат выполнения операции,
    /// с тремя вариантами ошибки.
    /// </summary>
    /// <typeparam name="TResult">Тип значения результата.</typeparam>
    /// <typeparam name="TError1">Первый тип данных ошибки.</typeparam>
    /// <typeparam name="TError2">Второй тип данных ошибки.</typeparam>
    /// <typeparam name="TError3">Третий тип данных ошибки.</typeparam>
    public interface IResult<TResult, TError1, TError2, TError3> : IResult<TResult, TError1, TError2>
    {
        void Deconstruct(out TResult result, out TError3 error, out ErrorInfo errorInfo)
        {
            result = Value;
            error = GetError<TError3>();
            errorInfo = ErrorInfo;
        }
    }
}
