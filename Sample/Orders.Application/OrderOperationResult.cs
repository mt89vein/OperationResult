using OperationResult;
using System;
using static OperationResult.OperationResultHelper;

namespace Orders.Application
{
    public static class OrderOperationResult
    {
        public const int ORDER_NOT_FOUND_CODE = 1;
        public const int ORDER_ACCESS_FORBIDDEN_CODE = 2;

        public static ErrorTag OrderNotFound(Guid orderId) => Error(
            ORDER_NOT_FOUND_CODE,
            $"Заказ с идентификатором {orderId} не найден."
        );

        public static ErrorTag OrderAccessForbidden(Guid orderId, Guid customerId) => Error(
            ORDER_ACCESS_FORBIDDEN_CODE,
            $"Попытка запросить заказ {orderId} не принадлежащий заказчику {customerId}."
        );
    }
}