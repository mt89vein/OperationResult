using System;
using System.Collections.Generic;

namespace Orders.Api.Models
{
    public class OrderVm
    {
        /// <summary>
        /// Идентификатор заказа.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Заказчик.
        /// </summary>
        public Guid CustomerId { get; set; }

        /// <summary>
        /// Список позиций заказа.
        /// </summary>
        public IEnumerable<OrderLineVm> OrderLines { get; set; } = new List<OrderLineVm>();
    }
}