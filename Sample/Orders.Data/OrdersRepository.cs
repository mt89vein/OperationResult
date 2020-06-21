using Orders.Domain;
using Orders.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Orders.Data
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly List<Order> _orders = new List<Order>();

        public void Add(Order order)
        {
            _orders.Add(order);
        }

        public Order GetOrder(Guid id)
        {
            return _orders.FirstOrDefault(o => o.Id == id);
        }

        public IEnumerable<Order> GetOrders(Guid customerId)
        {
            return _orders.Where(o => o.CustomerId == customerId);
        }
    }
}
