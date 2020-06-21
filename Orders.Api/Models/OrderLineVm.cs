namespace Orders.Api.Models
{
    public class OrderLineVm
    {
        /// <summary>
        /// Наименование позиции заказа.
        /// </summary>
        public string? Name { get; set; }

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