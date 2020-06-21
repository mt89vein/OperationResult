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

    public class OrderLineVm
    {
        /// <summary>
        /// Наименование позиции заказа.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Стоимость позиции.
        /// </summary>
        public decimal Cost { get; set; }

        /// <summary>
        /// Количество позиций.
        /// </summary>
        public decimal Quantity { get; set; }
    }
}