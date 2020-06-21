﻿using System;

namespace OperationResult
{
#pragma warning disable CS0660 // Type defines operator == or operator != but does not override Object.Equals(object o)
#pragma warning disable CS0661 // Type defines operator == or operator != but does not override Object.GetHashCode()
#pragma warning disable CA1815 // Override equals and operator equals on value types
    /// <summary>
    /// Структура - тэг, для пометки операции завершенной с ошибкой.
    /// </summary>
    public struct ErrorTag
    {
        /// <summary>
        /// Мета-данные ошибки.
        /// </summary>
        internal readonly ErrorInfo ErrorInfo;

        /// <summary>
        /// Создает новый экземпляр структуры <see cref="ErrorTag"/>
        /// </summary>
        /// <param name="errorInfo">Метаданные ошибки.</param>
        internal ErrorTag(in ErrorInfo errorInfo)
        {
            ErrorInfo = errorInfo;
        }
    }

    /// <summary>
    /// Структура - тэг, для пометки операции завершенной с ошибкой
    /// с данными об ошибке.
    /// </summary>
    /// <typeparam name="TError">Тип данных ошибки.</typeparam>
    public readonly struct ErrorTag<TError>
    {
        /// <summary>
        /// Мета-данные ошибки.
        /// </summary>
        internal readonly ErrorInfo ErrorInfo;

        /// <summary>
        /// Данные ошибки.
        /// </summary>
        internal readonly TError Error;

        /// <summary>
        /// Создает новый экземпляр структуры <see cref="ErrorTag{TError}"/>
        /// </summary>
        /// <param name="error">Данные ошибки.</param>
        internal ErrorTag(TError error)
        {
            ErrorInfo = default;
            Error = error;
        }

        /// <summary>
        /// Создает новый экземпляр структуры <see cref="ErrorTag{TError}"/>
        /// </summary>
        /// <param name="errorInfo">Мета-данные ошибки.</param>
        /// <param name="error">Данные ошибки.</param>
        internal ErrorTag(in ErrorInfo errorInfo, in TError error)
        {
            ErrorInfo = errorInfo;
            Error = error;
        }
    }

    /// <summary>
    /// Мета-данные ошибки.
    /// </summary>
    public readonly struct ErrorInfo : IEquatable<ErrorInfo>
    {
        /// <summary>
        /// Код ошибки.
        /// </summary>
        public int Code { get; }

        /// <summary>
        /// Текст ошибки.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Создает новый экземпляр структуры <see cref="ErrorInfo"/>.
        /// </summary>
        /// <param name="code">Код ошибки.</param>
        /// <param name="message">Текст ошибки.</param>
        public ErrorInfo(in int code, in string message)
        {
            Code = code;
            Message = message;
        }

        /// <summary>
        /// Возвращает строковое представление мета-данных ошибки.
        /// </summary>
        public override string ToString()
        {
            return $"Code: {Code}, Message: {Message}.";
        }

        #region IEqatable support

        /// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.</returns>
        public bool Equals(ErrorInfo other)
        {
            return Code == other.Code && Message == other.Message;
        }

        /// <summary>Indicates whether this instance and a specified object are equal.</summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="obj" /> and this instance are the same type and represent the same value; otherwise, <see langword="false" />.</returns>
        public override bool Equals(object obj)
        {
            return obj is ErrorInfo other && Equals(other);
        }

        /// <summary>Returns the hash code for this instance.</summary>
        /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Code, Message);
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

        #endregion IEqatable support
    }
#pragma warning restore CS0660 // Type defines operator == or operator != but does not override Object.Equals(object o)
#pragma warning restore CS0661 // Type defines operator == or operator != but does not override Object.GetHashCode()
#pragma warning restore CA1815 // Override equals and operator equals on value types
}