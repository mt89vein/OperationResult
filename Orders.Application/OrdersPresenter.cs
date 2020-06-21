using OperationResult;
using Orders.Domain;
using Orders.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;
using static OperationResult.OperationResultHelper;
using static Orders.Application.OrderOperationResult;

namespace Orders.Application
{
    public sealed class OrdersPresenter
    {
        private readonly IOrdersRepository _ordersRepository;

        public OrdersPresenter(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

        public IEnumerable<Order> GetOrders(Guid customerId)
        {
            return _ordersRepository.GetOrders(customerId);
        }

        public Result<Order, ErrorInfo, ExceptionDispatchInfo> GetOrder(Guid orderId, Guid customerId)
        {
            try
            {
                var order = _ordersRepository.GetOrder(orderId);

                if (order == null)
                {
                    return OrderNotFound(orderId);
                }

                if (order.CustomerId != customerId)
                {
                    return OrderAccessForbidden(orderId, customerId);
                }

                return Ok(order);
            }
            catch (Exception e)
            {
                return Error(ExceptionDispatchInfo.Capture(e));
            }
        }

        public Order GetOrderOld(Guid orderId, Guid customerId)
        {
            var order = _ordersRepository.GetOrder(orderId);

            if (order == null)
            {
                return null; // throw new OrderNotFoundException($"Не найден заказ с идентификатором {orderId}");
            }

            if (order.CustomerId != customerId)
            {
                throw new InvalidOperationException($"Попытка запросить заказ {orderId} не принадлежащий заказчику {customerId}.");
            }

            return order;
        }
    }
}