using AutoMapper;
using Orders.Domain;
using Orders.Domain.Interfaces;
using Orders.Infrastructure.Dtos;
using System;

namespace Orders.Infrastructure
{
    public class CustomerClient : ICustomerRepository
    {
        private readonly IMapper _mapper;

        public CustomerClient(IMapper mapper)
        {
            _mapper = mapper;
        }

        public OrderCustomer GetCustomer(Guid customerId)
        {
            // симулируем запрос к мс с заказчиками
            var customerVm = MakeHttpRequest(customerId);

            // мапим в доменную модель с нужными данными
            return _mapper.Map<OrderCustomer>(customerVm);
        }

        private static CustomerDto MakeHttpRequest(Guid customerId)
        {
            return new CustomerDto
            {
                CustomerId = customerId,
                Name = "Тест"
            };
        }
    }
}
