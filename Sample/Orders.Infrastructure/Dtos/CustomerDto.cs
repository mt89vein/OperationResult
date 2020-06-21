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
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        public string Name { get; set; }
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

        // many many other fields
    }
}