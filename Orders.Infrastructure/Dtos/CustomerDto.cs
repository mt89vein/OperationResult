using System;

namespace Orders.Infrastructure.Dtos
{
    public class CustomerDto
    {
        /// <summary>
        /// Идентификатор заказчика.
        /// </summary>
        public Guid CustomerId { get; set; }

        /// <summary>
        /// Имя заказчика.
        /// </summary>
        public string Name { get; set; }

        // many many other fields
    }
}