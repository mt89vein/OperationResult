using System.Runtime.CompilerServices;

namespace OperationResult
{
#pragma warning disable CS0660 // Type defines operator == or operator != but does not override Object.Equals(object o)
#pragma warning disable CS0661 // Type defines operator == or operator != but does not override Object.GetHashCode()
#pragma warning disable CA1815 // Override equals and operator equals on value types

    /// <summary>
    /// Результат выполнения операции.
    /// </summary>
    /// <typeparam name="TResult">Тип значения результата.</typeparam>
    public readonly struct Result<TResult> : IResult<TResult>
    {
        #region Поля, свойства

        /// <summary>
        /// Кэшированный результат с ошибкой.
        /// </summary>
        private static readonly Result<TResult> _errorResult = new Result<TResult>(false);

        /// <summary>
        /// Кэшированный результат успешной операции без значения.
        /// </summary>
        private static readonly Result<TResult> _successResult = new Result<TResult>(true);

        /// <summary>
        /// Значение результата операции.
        /// </summary>
        public TResult Value { get; }

        /// <summary>
        /// Является ли результат выполнения успешным.
        /// </summary>
        public bool IsSuccess { get; }

        /// <summary>
        /// Является ли результат выполнения не успешным.
        /// </summary>
        public bool IsError => !IsSuccess;

        #endregion Поля, свойства

        #region Конструкторы

        private Result(bool isSuccess)
        {
            IsSuccess = isSuccess;
            Value = default;
        }

        private Result(in TResult result)
        {
            IsSuccess = true;
            Value = result;
        }

        #endregion Конструкторы

        #region Операторы приведения типов

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator bool(in Result<TResult> result)
        {
            return result.IsSuccess;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Result<TResult>(in TResult result)
        {
            return new Result<TResult>(result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Result<TResult>(in SuccessTag<TResult> tag)
        {
            return new Result<TResult>(tag.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Result<TResult>(in ErrorTag<TResult> tag)
        {
            return new Result<TResult>(tag.Error);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Result<TResult>(in ErrorTag _)
        {
            return _errorResult;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Result<TResult>(in SuccessTag _)
        {
            return _successResult;
        }

        #endregion Операторы приведения типов
    }

    /// <summary>
    /// Результат выполнения операции (с данными об ошибке).
    /// </summary>
    /// <typeparam name="TResult">Значение результата операции.</typeparam>
    /// <typeparam name="TError">Данные ошибки операции.</typeparam>
    public readonly struct Result<TResult, TError> : IResult<TResult, TError>
    {
        #region Поля, свойства

        /// <summary>
        /// Значение результата операции.
        /// </summary>
        public TResult Value { get; }

        /// <summary>
        /// Данные ошибки операции.
        /// </summary>
        public TError Error { get; }

        /// <summary>
        /// Мета-данные ошибки.
        /// </summary>
        public ErrorInfo ErrorInfo { get; }

        /// <summary>
        /// Является ли результат выполнения успешным.
        /// </summary>
        public bool IsSuccess { get; }

        /// <summary>
        /// Является ли результат выполнения не успешным.
        /// </summary>
        public bool IsError => !IsSuccess;

        #endregion Поля, свойства

        #region Конструкторы

        private Result(in TResult result)
        {
            IsSuccess = true;
            Value = result;
            Error = default;
            ErrorInfo = default;
        }

        private Result(in TError error, in ErrorInfo errorInfo)
        {
            IsSuccess = false;
            Value = default;
            Error = error;
            ErrorInfo = errorInfo;
        }

        private Result(in ErrorInfo errorInfo)
        {
            IsSuccess = false;
            Value = default;
            Error = default;
            ErrorInfo = errorInfo;
        }

        #endregion Конструкторы

        #region Методы (public)

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Deconstruct(out TResult result, out TError error, out ErrorInfo errorInfo)
        {
            result = Value;
            error = Error;
            errorInfo = ErrorInfo;
        }

        #endregion Методы (public)

        #region Операторы приведения типов

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator bool(Result<TResult, TError> result)
        {
            return result.IsSuccess;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Result<TResult, TError>(TResult result)
        {
            return new Result<TResult, TError>(result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Result<TResult, TError>(in SuccessTag<TResult> tag)
        {
            return new Result<TResult, TError>(tag.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Result<TResult, TError>(in ErrorTag<TError> tag)
        {
            return new Result<TResult, TError>(tag.Error, tag.ErrorInfo);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Result<TResult, TError>(in ErrorTag tag)
        {
            return new Result<TResult, TError>(tag.ErrorInfo);
        }

        #endregion Операторы приведения типов
    }

    /// <summary>
    /// Результат выполнения операции (с двумя видами возможных ошибок).
    /// </summary>
    /// <typeparam name="TResult">Значение результата операции.</typeparam>
    /// <typeparam name="TError1">Первый тип ошибки операции.</typeparam>
    /// <typeparam name="TError2">Второй тип ошибки операции.</typeparam>
    public readonly struct Result<TResult, TError1, TError2> : IResult<TResult, TError1, TError2>
    {
        #region Поля, свойства

        /// <summary>
        /// Значение результата операции.
        /// </summary>
        public TResult Value { get; }

        /// <summary>
        /// Данные ошибки операции.
        /// </summary>
        public object Error { get; }

        /// <summary>
        /// Мета-данные ошибки.
        /// </summary>
        public ErrorInfo ErrorInfo { get; }

        /// <summary>
        /// Является ли результат выполнения успешным.
        /// </summary>
        public bool IsSuccess { get; }

        /// <summary>
        /// Является ли результат выполнения не успешным.
        /// </summary>
        public bool IsError => !IsSuccess;

        #endregion Поля, свойства

        #region Конструкторы

        private Result(TResult result)
        {
            IsSuccess = true;
            Value = result;
            Error = null;
            ErrorInfo = default;
        }

        private Result(object error, in ErrorInfo errorInfo)
        {
            IsSuccess = false;
            Value = default;
            Error = error;
            ErrorInfo = errorInfo;
        }

        private Result(in ErrorInfo errorInfo)
        {
            IsSuccess = false;
            Value = default;
            Error = default;
            ErrorInfo = errorInfo;
        }

        #endregion Конструкторы

        #region Методы (public)

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Deconstruct(out TResult result, out object error, out ErrorInfo errorInfo)
        {
            result = Value;
            error = Error;
            errorInfo = ErrorInfo;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Deconstruct(out TResult result, out TError1 error, out ErrorInfo errorInfo)
        {
            result = Value;
            error = GetError<TError1>();
            errorInfo = ErrorInfo;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Deconstruct(out TResult result, out TError2 error, out ErrorInfo errorInfo)
        {
            result = Value;
            error = GetError<TError2>();
            errorInfo = ErrorInfo;
        }

        /// <summary>
        /// Проверить, является ли ошибка указанного типа.
        /// </summary>
        /// <typeparam name="TError">Тип ошибки.</typeparam>
        /// <returns>true, если ошибка указанного типа.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool HasError<TError>() => Error is TError;

        /// <summary>
        /// Получить данные ошибки.
        /// </summary>
        /// <typeparam name="TError">Тип ошибки.</typeparam>
        /// <returns>Данные ошибки.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TError GetError<TError>() => HasError<TError>()
            ? (TError)Error
            : default;

        #endregion Методы (public)

        #region Операторы приведения типов

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator bool(Result<TResult, TError1, TError2> result)
        {
            return result.IsSuccess;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Result<TResult, TError1, TError2>(TResult result)
        {
            return new Result<TResult, TError1, TError2>(result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Result<TResult, TError1, TError2>(in SuccessTag<TResult> tag)
        {
            return new Result<TResult, TError1, TError2>(tag.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Result<TResult, TError1, TError2>(in ErrorTag<TError1> tag)
        {
            return new Result<TResult, TError1, TError2>(tag.Error, tag.ErrorInfo);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Result<TResult, TError1, TError2>(in ErrorTag<TError2> tag)
        {
            return new Result<TResult, TError1, TError2>(tag.Error, tag.ErrorInfo);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Result<TResult, TError1, TError2>(in ErrorTag tag)
        {
            return new Result<TResult, TError1, TError2>(tag.ErrorInfo);
        }

        #endregion Операторы приведения типов
    }

    /// <summary>
    /// Результат выполнения операции (с тремя видами возможных ошибок).
    /// </summary>
    /// <typeparam name="TResult">Значение результата операции.</typeparam>
    /// <typeparam name="TError1">Первый тип ошибки операции.</typeparam>
    /// <typeparam name="TError2">Второй тип ошибки операции.</typeparam>
    /// <typeparam name="TError3">Третий тип ошибки операции.</typeparam>
    public readonly struct Result<TResult, TError1, TError2, TError3> : IResult<TResult, TError1, TError2, TError3>
    {
        #region Поля, свойства

        /// <summary>
        /// Значение результата операции.
        /// </summary>
        public TResult Value { get; }

        /// <summary>
        /// Данные ошибки операции.
        /// </summary>
        public object Error { get; }

        /// <summary>
        /// Мета-данные ошибки.
        /// </summary>
        public ErrorInfo ErrorInfo { get; }

        /// <summary>
        /// Является ли результат выполнения успешным.
        /// </summary>
        public bool IsSuccess { get; }

        /// <summary>
        /// Является ли результат выполнения не успешным.
        /// </summary>
        public bool IsError => !IsSuccess;

        #endregion Поля, свойства

        #region Конструктор

        private Result(TResult result)
        {
            IsSuccess = true;
            Value = result;
            Error = null;
            ErrorInfo = default;
        }

        private Result(object error, in ErrorInfo errorInfo)
        {
            IsSuccess = false;
            Value = default;
            Error = error;
            ErrorInfo = errorInfo;
        }

        private Result(in ErrorInfo errorInfo)
        {
            IsSuccess = false;
            Value = default;
            Error = null;
            ErrorInfo = errorInfo;
        }

        #endregion Конструктор

        #region Методы (public)

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Deconstruct(out TResult result, out object error, out ErrorInfo errorInfo)
        {
            result = Value;
            error = Error;
            errorInfo = ErrorInfo;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Deconstruct(out TResult result, out TError1 error, out ErrorInfo errorInfo)
        {
            result = Value;
            error = GetError<TError1>();
            errorInfo = ErrorInfo;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Deconstruct(out TResult result, out TError2 error, out ErrorInfo errorInfo)
        {
            result = Value;
            error = GetError<TError2>();
            errorInfo = ErrorInfo;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Deconstruct(out TResult result, out TError3 error, out ErrorInfo errorInfo)
        {
            result = Value;
            error = GetError<TError3>();
            errorInfo = ErrorInfo;
        }

        /// <summary>
        /// Проверить, является ли ошибка указанного типа.
        /// </summary>
        /// <typeparam name="TError">Тип ошибки.</typeparam>
        /// <returns>true, если ошибка указанного типа.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool HasError<TError>() => Error is TError;

        /// <summary>
        /// Получить данные ошибки.
        /// </summary>
        /// <typeparam name="TError">Тип ошибки.</typeparam>
        /// <returns>Данные ошибки.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TError GetError<TError>() => HasError<TError>()
            ? (TError)Error
            : default;

        #endregion Методы (public)

        #region Операторы приведения типов

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator bool(Result<TResult, TError1, TError2, TError3> result)
        {
            return result.IsSuccess;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Result<TResult, TError1, TError2, TError3>(TResult result)
        {
            return new Result<TResult, TError1, TError2, TError3>(result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Result<TResult, TError1, TError2, TError3>(in SuccessTag<TResult> tag)
        {
            return new Result<TResult, TError1, TError2, TError3>(tag.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Result<TResult, TError1, TError2, TError3>(in ErrorTag<TError1> tag)
        {
            return new Result<TResult, TError1, TError2, TError3>(tag.Error, tag.ErrorInfo);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Result<TResult, TError1, TError2, TError3>(in ErrorTag<TError2> tag)
        {
            return new Result<TResult, TError1, TError2, TError3>(tag.Error, tag.ErrorInfo);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Result<TResult, TError1, TError2, TError3>(in ErrorTag<TError3> tag)
        {
            return new Result<TResult, TError1, TError2, TError3>(tag.Error, tag.ErrorInfo);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Result<TResult, TError1, TError2, TError3>(in ErrorTag tag)
        {
            return new Result<TResult, TError1, TError2, TError3>(tag.ErrorInfo);
        }

        #endregion Операторы приведения типов
    }
#pragma warning restore CS0660 // Type defines operator == or operator != but does not override Object.Equals(object o)
#pragma warning restore CS0661 // Type defines operator == or operator != but does not override Object.GetHashCode()
#pragma warning restore CA1815 // Override equals and operator equals on value types
}
