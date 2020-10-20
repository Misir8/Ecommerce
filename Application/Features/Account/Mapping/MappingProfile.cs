using AutoMapper;
using Domain.Entities;

namespace Application.Features.Account.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Address, AddressDto>().ReverseMap();
        }
    }
}
