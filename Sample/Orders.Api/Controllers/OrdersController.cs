using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OperationResult;
using Orders.Api.Models;
using Orders.Application;
using Orders.Domain;
using System;
using System.Net.Mime;
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
        public IActionResult CreateOrder(OrderVm orderVm, [FromServices] AddOrderUseCase addOrderUseCase)
        {
            // для упрощения примера - customerId получаем по api, в реальном приложении обычно достаем его из токена аутентификации.

            // TODO: validate OrderVm with fluentValidation

            return addOrderUseCase
                .AddOrder(_mapper.Map<Order>(orderVm))
                .Unwrap<IActionResult, Order, OrderValidationResult>(
                    onSuccess: order => CreatedAtRoute(new {orderId = order.Id}, _mapper.Map<OrderVm>(order)),
                    onError: errorInfo => errorInfo switch
                    {
                        _ when !(errorInfo.Error is null) => BadRequest(errorInfo.Error),

                        _ => throw new CreateOrderException(
                            "Необработанная ошибка при создании заказа",
                            errorInfo.ExceptionDispatchInfo?.SourceException
                        )
                    });
        }

        [HttpGet("{orderId}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(OrderVm), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(Guid orderId, Guid customerId, [FromServices] OrdersPresenter ordersPresenter)
        {
            // для упрощения примера - customerId получаем по api, в реальном приложении обычно достаем его из токена аутентификации.

            return ordersPresenter
                .GetOrder(orderId, customerId)
                .Unwrap<IActionResult, Order>(
                    onSuccess: order => Ok(_mapper.Map<OrderVm>(order)),
                    onError: errorInfo => errorInfo switch
                    {
                        { Code: ORDER_ACCESS_FORBIDDEN_CODE } => NotFound(errorInfo.Message),

                        { Code: ORDER_NOT_FOUND_CODE } => NotFound(errorInfo.Message),

                        _ => throw new GetOrderException(
                            "Необработанная ошибка при получении данных заказа",
                            errorInfo.ExceptionDispatchInfo?.SourceException
                        )
                    });
        }

        [HttpGet("old/{orderId}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(OrderVm), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetByIdOld(Guid orderId, Guid customerId, [FromServices] OrdersPresenter ordersPresenter)
        {
            // для упрощения примера - customerId получаем по api, в реальном приложении обычно достаем его из токена аутентификации.
            try
            {
                var order = ordersPresenter.GetOrderOld(orderId, customerId);

                if (order == null)
                {
                    return NotFound("Не найден заказ.");
                }

                return Ok(order);
            }
            catch (InvalidOperationException)
            {
                return NotFound(); // чужой заказ
            }
            catch (Exception e)
            {
                throw new GetOrderException(
                    "Необработанная ошибка при получении данных заказа",
                    e
                );
            }
        }
    }

    public sealed class GetOrderException : Exception
    {
        public GetOrderException(string errorMessage, Exception? innerException)
            : base(errorMessage, innerException)
        {
        }
    }

    public sealed class CreateOrderException : Exception
    {
        public CreateOrderException(string errorMessage, Exception? innerException)
            : base(errorMessage, innerException)
        {
        }
    }
}