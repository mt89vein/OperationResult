namespace OperationResult
{
    /// <summary>
    /// Структура - тэг, для пометки операции завершенной успешно.
    /// </summary>
    public struct SuccessTag { }

    /// <summary>
    /// Структура - тэг, для пометки операции завершенной успешно
    /// с возвратом данных.
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
}