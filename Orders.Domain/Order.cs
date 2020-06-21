using System;
using System.Collections.Generic;
using System.Linq;

namespace Orders.Domain
{
    /// <summary>
    /// Заказ.
    /// </summary>
    public sealed class Order // IEntity<Guid>, IAggregateRoot<OrderLine>
    {
        /// <summary>
        /// Позиции заказа.
        /// </summary>
        private readonly HashSet<OrderLine> _orderLines;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object"></see> class.</summary>
        public Order(Guid customerId)
            : this(Guid.NewGuid(), customerId)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="T:System.Object"></see> class.</summary>
        public Order(Guid id, Guid customerId, IEnumerable<OrderLine>? orderLines = null)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("OrderId is empty", nameof(id));
            }
            if (customerId == Guid.Empty)
            {
                throw new ArgumentException("CustomerId is empty", nameof(customerId));
            }
            Id = id;
            CustomerId = customerId;
            _orderLines = new HashSet<OrderLine>(orderLines ?? Enumerable.Empty<OrderLine>());
        }

        /// <summary>
        /// Идентификатор заказа.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Заказчик.
        /// </summary>
        public Guid CustomerId { get; }

        public decimal TotalCost => _orderLines.Sum(ol => ol.TotalCost);

        public IReadOnlyCollection<OrderLine> OrderLines => _orderLines;

        // other order props.

        public void AddOrderLine(string name, decimal cost, decimal quantity)
        {
            var orderLine = new OrderLine(name, cost, quantity);

            var existOrderLine = _orderLines.FirstOrDefault(ol => ol == orderLine);

            if (existOrderLine == null!)
            {
                _orderLines.Add(orderLine);
            }
            else
            {
                existOrderLine.AddQuantity(orderLine.Quantity);
            }
        }

        // other aggregate root methods
    }
}
