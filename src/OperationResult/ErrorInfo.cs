using System;
using System.Runtime.ExceptionServices;

namespace OperationResult
{
    /// <summary>
    /// Мета-данные ошибки.
    /// </summary>
    public readonly struct ErrorInfo : IEquatable<ErrorInfo>
    {
        /// <summary>
        /// Код ошибки.
        /// </summary>
        public int? Code { get; }

        /// <summary>
        /// Текст ошибки.
        /// </summary>
        public string? Message { get; }

        /// <summary>
        /// Данные об исключении.
        /// </summary>
        public ExceptionDispatchInfo? ExceptionDispatchInfo { get; }

        /// <summary>
        /// Создает новый экземпляр структуры <see cref="ErrorInfo"/>.
        /// </summary>
        /// <param name="exceptionDispatchInfo">Данные об исключении.</param>
        internal ErrorInfo(ExceptionDispatchInfo exceptionDispatchInfo)
        {
            Code = default;
            Message = default;
            ExceptionDispatchInfo = exceptionDispatchInfo;
        }

        /// <summary>
        /// Создает новый экземпляр структуры <see cref="ErrorInfo"/>.
        /// </summary>
        /// <param name="code">Код ошибки.</param>
        /// <param name="message">Текст ошибки.</param>
        public ErrorInfo(in int code, in string message)
        {
            Code = code;
            Message = message;
            ExceptionDispatchInfo = default;
        }

        /// <summary>
        /// Возвращает строковое представление мета-данных ошибки.
        /// </summary>
        public override string ToString()
        {
            if (ExceptionDispatchInfo?.SourceException != null)
            {
                return ExceptionDispatchInfo.SourceException.ToString();
            }

            return $"Code: {Code}, Message: {Message}.";
        }

        #region IEquatable support

        /// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.</returns>
        public bool Equals(ErrorInfo other)
        {
            return Code == other.Code && Message == other.Message && Equals(ExceptionDispatchInfo, other.ExceptionDispatchInfo);
        }

        /// <summary>Indicates whether this instance and a specified object are equal.</summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="obj" /> and this instance are the same type and represent the same value; otherwise, <see langword="false" />.</returns>
        public override bool Equals(object? obj)
        {
            return obj is ErrorInfo other && Equals(other);
        }

        /// <summary>Returns the hash code for this instance.</summary>
        /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Code, Message, ExceptionDispatchInfo);
        }

        /// <summary>Returns a value that indicates whether the values of two <see cref="T:OperationResult.ErrorInfo" /> objects are equal.</summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>true if the <paramref name="left" /> and <paramref name="right" /> parameters have the same value; otherwise, false.</returns>
        public static bool operator ==(ErrorInfo left, ErrorInfo right)
        {
            return left.Equals(right);
        }

        /// <summary>Returns a value that indicates whether two <see cref="T:OperationResult.ErrorInfo" /> objects have different values.</summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>true if <paramref name="left" /> and <paramref name="right" /> are not equal; otherwise, false.</returns>
        public static bool operator !=(ErrorInfo left, ErrorInfo right)
        {
            return !left.Equals(right);
        }

        #endregion IEquatable support
    }
}