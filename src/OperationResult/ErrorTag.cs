using System;

namespace OperationResult
{
    /// <summary>
    /// Структура - тэг, для пометки операции завершенной с ошибкой.
    /// </summary>
    public struct ErrorTag<TError> : IEquatable<ErrorTag<TError>>
        where TError : class
    {
        /// <summary>
        /// Мета-данные ошибки.
        /// </summary>
#pragma warning disable IDE1006 // Naming Styles
        internal readonly ErrorInfo<TError> ErrorInfo;
#pragma warning restore IDE1006 // Naming Styles

        /// <summary>
        /// Создает новый экземпляр структуры <see cref="ErrorTag{TError}"/>
        /// </summary>
        /// <param name="errorInfo">Метаданные ошибки.</param>
        internal ErrorTag(in ErrorInfo<TError> errorInfo)
        {
            ErrorInfo = errorInfo;
        }

        #region IEquatable support

        /// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.</returns>
        public bool Equals(ErrorTag<TError> other)
        {
            return ErrorInfo.Equals(other.ErrorInfo);
        }

        /// <summary>Indicates whether this instance and a specified object are equal.</summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="obj" /> and this instance are the same type and represent the same value; otherwise, <see langword="false" />.</returns>
        public override bool Equals(object? obj)
        {
            return obj is ErrorTag<TError> other && Equals(other);
        }

        /// <summary>Returns the hash code for this instance.</summary>
        /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
        public override int GetHashCode()
        {
            return ErrorInfo.GetHashCode();
        }

        /// <summary>Returns a value that indicates whether the values of two <see cref="T:OperationResult.ErrorTag" /> objects are equal.</summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>true if the <paramref name="left" /> and <paramref name="right" /> parameters have the same value; otherwise, false.</returns>
        public static bool operator ==(ErrorTag<TError> left, ErrorTag<TError> right)
        {
            return left.Equals(right);
        }

        /// <summary>Returns a value that indicates whether two <see cref="T:OperationResult.ErrorTag" /> objects have different values.</summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>true if <paramref name="left" /> and <paramref name="right" /> are not equal; otherwise, false.</returns>
        public static bool operator !=(ErrorTag<TError> left, ErrorTag<TError> right)
        {
            return !left.Equals(right);
        }

        #endregion IEquatable support
    }

    /// <summary>
    /// Структура - тэг, для пометки операции завершенной с ошибкой.
    /// </summary>
    public struct ErrorTag : IEquatable<ErrorTag>
    {
        /// <summary>
        /// Мета-данные ошибки.
        /// </summary>
#pragma warning disable IDE1006 // Naming Styles
        internal readonly ErrorInfo ErrorInfo;
#pragma warning restore IDE1006 // Naming Styles

        /// <summary>
        /// Создает новый экземпляр структуры <see cref="ErrorTag"/>
        /// </summary>
        /// <param name="errorInfo">Метаданные ошибки.</param>
        internal ErrorTag(in ErrorInfo errorInfo)
        {
            ErrorInfo = errorInfo;
        }

        #region IEquatable support

        /// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.</returns>
        public bool Equals(ErrorTag other)
        {
            return ErrorInfo.Equals(other.ErrorInfo);
        }

        /// <summary>Indicates whether this instance and a specified object are equal.</summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="obj" /> and this instance are the same type and represent the same value; otherwise, <see langword="false" />.</returns>
        public override bool Equals(object? obj)
        {
            return obj is ErrorTag other && Equals(other);
        }

        /// <summary>Returns the hash code for this instance.</summary>
        /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
        public override int GetHashCode()
        {
            return ErrorInfo.GetHashCode();
        }

        /// <summary>Returns a value that indicates whether the values of two <see cref="T:OperationResult.ErrorTag" /> objects are equal.</summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>true if the <paramref name="left" /> and <paramref name="right" /> parameters have the same value; otherwise, false.</returns>
        public static bool operator ==(ErrorTag left, ErrorTag right)
        {
            return left.Equals(right);
        }

        /// <summary>Returns a value that indicates whether two <see cref="T:OperationResult.ErrorTag" /> objects have different values.</summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>true if <paramref name="left" /> and <paramref name="right" /> are not equal; otherwise, false.</returns>
        public static bool operator !=(ErrorTag left, ErrorTag right)
        {
            return !left.Equals(right);
        }

        #endregion IEquatable support
    }
}