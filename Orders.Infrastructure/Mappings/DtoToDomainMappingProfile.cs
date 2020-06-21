using AutoMapper;
using Orders.Domain;
using Orders.Infrastructure.Dtos;

namespace Orders.Infrastructure.Mappings
{
    public class DtoToDomainMappingProfile : Profile
    {
        public DtoToDomainMappingProfile()
        {
            CreateMap<CustomerDto, OrderCustomer>().ConstructUsing(dto => new OrderCustomer(dto.CustomerId, dto.Name));
        }
    }

    /// <summary>
    /// Маркер для указания из какой сборки нужна сканировать профили маппинга.
    /// </summary>
    public interface IHasMappingProfile {}
}
