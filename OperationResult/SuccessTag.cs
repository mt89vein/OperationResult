namespace OperationResult
{
#pragma warning disable CS0660 // Type defines operator == or operator != but does not override Object.Equals(object o)
#pragma warning disable CS0661 // Type defines operator == or operator != but does not override Object.GetHashCode()
#pragma warning disable CA1815 // Override equals and operator equals on value types
    /// <summary>
    /// Структура - тэг, для пометки операции завершенной успешно.
    /// </summary>
    public struct SuccessTag { }

    /// <summary>
    /// Структура - тэг, для пометки операции завершенной успешно с возвратом данных.
    /// </summary>
    /// <typeparam name="TResult">Тип данных результата.</typeparam>
    public struct SuccessTag<TResult>
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
    }
#pragma warning restore CS0660 // Type defines operator == or operator != but does not override Object.Equals(object o)
#pragma warning restore CS0661 // Type defines operator == or operator != but does not override Object.GetHashCode()
#pragma warning restore CA1815 // Override equals and operator equals on value types
}