using System;

namespace Orders.Domain
{
    /// <summary>
    /// Заказчик заказа.
    /// </summary>
    public sealed class OrderCustomer
    {
        /// <summary>
        /// Идентификатор заказчика.
        /// </summary>
        public Guid CustomerId { get; }

        /// <summary>
        /// Имя заказчика.
        /// </summary>
        public string Name { get; }

        /// <summary>Initializes a new instance of the <see cref="T:System.Object"></see> class.</summary>
        public OrderCustomer(Guid customerId, string name)
        {
            CustomerId = customerId;
            Name = name;
        }
    }
}