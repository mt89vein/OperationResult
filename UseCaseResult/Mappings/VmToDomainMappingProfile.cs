using AutoMapper;
using Orders.Api.Models;
using Orders.Domain;
using System.Collections.Generic;

namespace Orders.Api.Mappings
{
    public class VmToDomainMappingProfile : Profile
    {
        public VmToDomainMappingProfile()
        {
            CreateMap<OrderVm, Order>()
                .ConstructUsing((dto, ctx) => new Order(
                        dto.Id,
                        dto.CustomerId,
                        ctx.Mapper.Map<IEnumerable<OrderLine>>(dto.OrderLines)
                    )
                );
            CreateMap<OrderLineVm, OrderLine>()
                .ConstructUsing(dto => new OrderLine(dto.Name, dto.Cost, dto.Quantity));
        }
    }
}