using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OperationResult;
using Orders.Api.Models;
using Orders.Application;
using Orders.Domain;
using System;
using System.Net.Mime;
using System.Runtime.ExceptionServices;
using static Orders.Application.OrderOperationResult;

namespace Orders.Api.Controllers
{
    [ApiController]
    [Route("orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IMapper _mapper;

        public OrdersController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Order), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddOrder(OrderVm orderVm, [FromServices] AddOrderUseCase addOrderUseCase)
        {
            // TODO: validate OrderVm with fluentValidation

            var order = _mapper.Map<Order>(orderVm);
            addOrderUseCase.AddOrder(order);

            return CreatedAtRoute(new { orderId = order.Id }, _mapper.Map<OrderVm>(order));
        }

        [HttpGet("{orderId}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(OrderVm), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(Guid orderId, Guid customerId, [FromServices] OrdersPresenter ordersPresenter)
        {
            // для упрощения примера - customerId получаем по api, в реальном приложении обычно получаешь его из данных токена аутентификации.

            return ordersPresenter
                .GetOrder(orderId, customerId)
                .Unwrap<IActionResult, Order, ErrorInfo, ExceptionDispatchInfo>(
                    onSuccess: result => Ok(_mapper.Map<OrderVm>(result)),
                    onError1: errorInfo => errorInfo.Code switch
                    {
                        ORDER_ACCESS_FORBIDDEN_CODE => NotFound(errorInfo.Message),

                        ORDER_NOT_FOUND_CODE => NotFound(errorInfo.Message),

                        _ => Problem(errorInfo.ToString())
                    },
                    onError2: exceptionDispatchInfo => throw new GetOrderException(
                        "Необработанная ошибка при получении данных заказа",
                        exceptionDispatchInfo.SourceException
                    ));
        }
    }

    public sealed class GetOrderException : Exception
    {
        public GetOrderException(string errorMessage, Exception innerException)
            : base(errorMessage, innerException)
        {
        }
    }
}