using System;

namespace Orders.Domain
{
    /// <summary>
    /// Позиция заказа.
    /// </summary>
    public sealed class OrderLine : IEquatable<OrderLine>
        // : ValueObject
    {
        // артикул и прочие штуки тоже тут
        // чтобы определять что это за элемент.

        /// <summary>
        /// Наименование позиции заказа.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Стоимость позиции.
        /// </summary>
        public decimal Cost { get; }

        /// <summary>
        /// Количество позиций.
        /// </summary>
        public decimal Quantity { get; private set; }

        /// <summary>
        /// Итоговая стоимость позиции.
        /// </summary>
        public decimal TotalCost => Cost * Quantity;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object"></see> class.</summary>
        public OrderLine(string name, decimal cost, decimal quantity)
        {
            if (cost <= 0) throw new ArgumentOutOfRangeException(nameof(cost));
            if (quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity));

            Name = name ?? throw new ArgumentNullException(nameof(name));
            Cost = cost;
            Quantity = quantity;
        }

        public void AddQuantity(decimal quantity)
        {
            Quantity += quantity;
        }

        #region IEquatable support

        /// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the <paramref name="other">other</paramref> parameter; otherwise, false.</returns>
        public bool Equals(OrderLine other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Name == other.Name; // equality by name
        }

        /// <summary>Determines whether the specified object is equal to the current object.</summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>true if the specified object  is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is OrderLine other && Equals(other);
        }

        /// <summary>Serves as the default hash function.</summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        /// <summary>Returns a value that indicates whether the values of two <see cref="T:Orders.Domain.OrderLine" /> objects are equal.</summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>true if the <paramref name="left" /> and <paramref name="right" /> parameters have the same value; otherwise, false.</returns>
        public static bool operator ==(OrderLine left, OrderLine right)
        {
            return Equals(left, right);
        }

        /// <summary>Returns a value that indicates whether two <see cref="T:Orders.Domain.OrderLine" /> objects have different values.</summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>true if <paramref name="left" /> and <paramref name="right" /> are not equal; otherwise, false.</returns>
        public static bool operator !=(OrderLine left, OrderLine right)
        {
            return !Equals(left, right);
        }

        #endregion IEquatable support
    }
}