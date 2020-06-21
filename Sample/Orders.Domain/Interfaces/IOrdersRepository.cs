using System;
using System.Collections.Generic;

namespace Orders.Domain.Interfaces
{
    public interface IOrdersRepository // TODO тут должен быть Generic repository для IAggregateRoot
    {
        void Add(Order order);

        Order GetOrder(Guid id);

        IEnumerable<Order> GetOrders(Guid customerId);
    }

    public interface ICustomerRepository
    {
        OrderCustomer GetCustomer(Guid customerId);
    }
}
