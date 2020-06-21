using Orders.Domain;
using Orders.Domain.Interfaces;

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

        public void AddOrder(Order order)
        {
            // var customer = _customerRepository.GetCustomer(order.CustomerId);

            // _orderValidator.Validate(order, customer);
            // _discountService.ApplyDiscount(order, customer);

            _ordersRepository.Add(order);

            // уведомления других МС о создании нового заказа

            // OrderCheckoutUseCase - уже будет ответственным за запуск выполнения заказа
        }
    }
}
