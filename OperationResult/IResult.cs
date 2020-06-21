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
        /// Мета-даные ошибки.
        /// </summary>
        ErrorInfo ErrorInfo { get; }

        /// <summary>
        /// Является ли результат выполнения не успешным.
        /// </summary>
        bool IsError { get; }
    }
}
