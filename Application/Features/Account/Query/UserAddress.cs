using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Application.Extensions;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Account.Query
{
    public class UserAddress
    {
        public class Query : IRequest<AddressDto>
        {
            public ClaimsPrincipal User { get; set; }
        }

        public class Handler : IRequestHandler<Query, AddressDto>
        {
            private readonly UserManager<AppUser> _userManager;
            private readonly IMapper _mapper;

            public Handler(UserManager<AppUser> userManager, IMapper mapper)
            {
                _userManager = userManager;
                _mapper = mapper;
            }

            public async Task<AddressDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByClaimsPrincipleWithAddressAsync(request.User);
                return _mapper.Map<AddressDto>(user.Address);
            }
        }
    }
}
