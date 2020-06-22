using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;

namespace OperationResult
{
    /// <summary>
    /// Результат выполнения операции.
    /// </summary>
    /// <typeparam name="TResult">Значение результата операции.</typeparam>
    /// <typeparam name="TError">Тип данных ошибки.</typeparam>
    public readonly struct Result<TResult, TError> : IResult<TResult, TError>, IEquatable<Result<TResult, TError>>
        where TError : class
    {
        #region Свойства

        /// <summary>
        /// Значение результата операции.
        /// </summary>
        public TResult Value { get; }

        /// <summary>
        /// Мета-данные ошибки.
        /// </summary>
        public ErrorInfo<TError> ErrorInfo { get; }

        /// <summary>
        /// Является ли результат выполнения успешным.
        /// </summary>
        public bool IsSuccess { get; }

        /// <summary>
        /// Является ли результат выполнения не успешным.
        /// </summary>
        public bool IsError => !IsSuccess;

        #endregion Свойства

        #region Конструкторы

        private Result(in TResult result)
        {
            IsSuccess = true;
            Value = result;
            ErrorInfo = default;
        }

        private Result(in ErrorInfo<TError> errorInfo)
        {
            IsSuccess = false;
            Value = default!;
            ErrorInfo = errorInfo;
        }

        #endregion Конструкторы

        #region Методы (public)

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Deconstruct(out TResult result, out ErrorInfo<TError> errorInfo)
        {
            result = Value;
            errorInfo = ErrorInfo;
        }

        #endregion Методы (public)

        #region Операторы приведения типов

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator bool(in Result<TResult, TError> result)
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
            return new Result<TResult, TError>(tag.ErrorInfo);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Result<TResult, TError>(in TError error)
        {
            return new Result<TResult, TError>(new ErrorInfo<TError>(error));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Result<TResult, TError>(in Exception exception)
        {
            return new Result<TResult, TError>(new ErrorInfo<TError>(ExceptionDispatchInfo.Capture(exception)));
        }

        #endregion Операторы приведения типов

        #region IEquatable support

        /// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.</returns>
        public bool Equals(Result<TResult, TError> other)
        {
            return EqualityComparer<TResult>.Default.Equals(Value, other.Value) && ErrorInfo.Equals(other.ErrorInfo) && IsSuccess == other.IsSuccess;
        }

        /// <summary>Indicates whether this instance and a specified object are equal.</summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="obj" /> and this instance are the same type and represent the same value; otherwise, <see langword="false" />.</returns>
        public override bool Equals(object? obj)
        {
            return obj is Result<TResult, TError> other && Equals(other);
        }

        /// <summary>Returns the hash code for this instance.</summary>
        /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Value, ErrorInfo, IsSuccess);
        }

        /// <summary>Returns a value that indicates whether the values of two <see cref="T:OperationResult.Result`1" /> objects are equal.</summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>true if the <paramref name="left" /> and <paramref name="right" /> parameters have the same value; otherwise, false.</returns>
        public static bool operator ==(Result<TResult, TError> left, Result<TResult, TError> right)
        {
            return left.Equals(right);
        }

        /// <summary>Returns a value that indicates whether two <see cref="T:OperationResult.Result`1" /> objects have different values.</summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>true if <paramref name="left" /> and <paramref name="right" /> are not equal; otherwise, false.</returns>
        public static bool operator !=(Result<TResult, TError> left, Result<TResult, TError> right)
        {
            return !left.Equals(right);
        }

        #endregion IEquatable support
    }

    /// <summary>
    /// Результат выполнения операции.
    /// </summary>
    /// <typeparam name="TResult">Значение результата операции.</typeparam>
    public readonly struct Result<TResult> : IResult<TResult>, IEquatable<Result<TResult>>
    {
        #region Свойства

        /// <summary>
        /// Значение результата операции.
        /// </summary>
        public TResult Value { get; }

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

        #endregion Свойства

        #region Конструкторы

        private Result(in TResult result)
        {
            IsSuccess = true;
            Value = result;
            ErrorInfo = default;
        }

        private Result(in ErrorInfo errorInfo)
        {
            IsSuccess = false;
            Value = default!;
            ErrorInfo = errorInfo;
        }

        #endregion Конструкторы

        #region Методы (public)

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Deconstruct(out TResult result, out ErrorInfo errorInfo)
        {
            result = Value;
            errorInfo = ErrorInfo;
        }

        #endregion Методы (public)

        #region Операторы приведения типов

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator bool(in Result<TResult> result)
        {
            return result.IsSuccess;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Result<TResult>(TResult result)
        {
            return new Result<TResult>(result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Result<TResult>(in SuccessTag<TResult> tag)
        {
            return new Result<TResult>(tag.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Result<TResult>(in ErrorTag tag)
        {
            return new Result<TResult>(tag.ErrorInfo);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Result<TResult>(in Exception exception)
        {
            return new Result<TResult>(new ErrorInfo(ExceptionDispatchInfo.Capture(exception)));
        }

        #endregion Операторы приведения типов

        #region IEquatable support

        /// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.</returns>
        public bool Equals(Result<TResult> other)
        {
            return EqualityComparer<TResult>.Default.Equals(Value, other.Value) && ErrorInfo.Equals(other.ErrorInfo) && IsSuccess == other.IsSuccess;
        }

        /// <summary>Indicates whether this instance and a specified object are equal.</summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="obj" /> and this instance are the same type and represent the same value; otherwise, <see langword="false" />.</returns>
        public override bool Equals(object? obj)
        {
            return obj is Result<TResult> other && Equals(other);
        }

        /// <summary>Returns the hash code for this instance.</summary>
        /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Value, ErrorInfo, IsSuccess);
        }

        /// <summary>Returns a value that indicates whether the values of two <see cref="T:OperationResult.Result`1" /> objects are equal.</summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>true if the <paramref name="left" /> and <paramref name="right" /> parameters have the same value; otherwise, false.</returns>
        public static bool operator ==(Result<TResult> left, Result<TResult> right)
        {
            return left.Equals(right);
        }

        /// <summary>Returns a value that indicates whether two <see cref="T:OperationResult.Result`1" /> objects have different values.</summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>true if <paramref name="left" /> and <paramref name="right" /> are not equal; otherwise, false.</returns>
        public static bool operator !=(Result<TResult> left, Result<TResult> right)
        {
            return !left.Equals(right);
        }

        #endregion IEquatable support
    }
}
