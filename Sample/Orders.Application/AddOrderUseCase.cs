using OperationResult;
using Orders.Domain;
using Orders.Domain.Interfaces;
using System.Collections.Generic;

namespace Orders.Application
{
    public sealed class AddOrderUseCase
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly ICustomerRepository _customerRepository;

        public AddOrderUseCase(IOrdersRepository ordersRepository, ICustomerRepository customerRepository)
        {
            _ordersRepository = ordersRepository;
            _customerRepository = customerRepository;
        }

        public Result<Order, OrderValidationResult> AddOrder(Order order)
        {
            // var customer = _customerRepository.GetCustomer(order.CustomerId);

            // _orderValidator.Validate(order, customer);
            // _discountService.ApplyDiscount(order, customer);

            if (order.OrderLines.Count == 0)
            {
                return new OrderValidationResult
                {
                    Errors = new List<string>
                    {
                        "Количество позиций не может быть пустым."
                    }
                };
            }

            _ordersRepository.Add(order);

            // уведомления других МС о создании нового заказа

            // OrderCheckoutUseCase - уже будет ответственным за запуск выполнения заказа

            return order;
        }
    }

    public class OrderValidationResult
    {
        public List<string> Errors { get; set; }
    }
}
