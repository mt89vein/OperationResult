using System;
using System.Collections.Generic;

namespace OperationResult
{
    /// <summary>
    /// Структура - тэг, для пометки операции завершенной успешно.
    /// </summary>
    public struct SuccessTag { }

    /// <summary>
    /// Структура - тэг, для пометки операции завершенной успешно с возвратом данных.
    /// </summary>
    /// <typeparam name="TResult">Тип данных результата.</typeparam>
    public struct SuccessTag<TResult> : IEquatable<SuccessTag<TResult>>
    {
        /// <summary>
        /// Данные результата.
        /// </summary>
        internal readonly TResult Value;

        /// <summary>
        /// Создает новый экземпляр структуры <see cref="SuccessTag{TResult}"/>.
        /// </summary>
        /// <param name="result">Данные результата операции.</param>
        internal SuccessTag(TResult result)
        {
            Value = result;
        }

        #region IEquatable support

        /// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.</returns>
        public bool Equals(SuccessTag<TResult> other)
        {
            return EqualityComparer<TResult>.Default.Equals(Value, other.Value);
        }

        /// <summary>Indicates whether this instance and a specified object are equal.</summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="obj" /> and this instance are the same type and represent the same value; otherwise, <see langword="false" />.</returns>
        public override bool Equals(object? obj)
        {
            return obj is SuccessTag<TResult> other && Equals(other);
        }

        /// <summary>Returns the hash code for this instance.</summary>
        /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
        public override int GetHashCode()
        {
            return EqualityComparer<TResult>.Default.GetHashCode(Value);
        }

        /// <summary>Returns a value that indicates whether the values of two <see cref="T:OperationResult.SuccessTag`1" /> objects are equal.</summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>true if the <paramref name="left" /> and <paramref name="right" /> parameters have the same value; otherwise, false.</returns>
        public static bool operator ==(SuccessTag<TResult> left, SuccessTag<TResult> right)
        {
            return left.Equals(right);
        }

        /// <summary>Returns a value that indicates whether two <see cref="T:OperationResult.SuccessTag`1" /> objects have different values.</summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>true if <paramref name="left" /> and <paramref name="right" /> are not equal; otherwise, false.</returns>
        public static bool operator !=(SuccessTag<TResult> left, SuccessTag<TResult> right)
        {
            return !left.Equals(right);
        }

        #endregion IEquatable support
    }
}