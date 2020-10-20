using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.Services;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Account.Query
{
    public class CurrentUser
    {
        public class Query: IRequest<UserDto>
        {
            public string Email { get; set; }
        }

        public class Handler : IRequestHandler<Query, UserDto>
        {
            private readonly UserManager<AppUser> _userManager;
            private readonly IJwtGenerator _jwtGenerator;

            public Handler(UserManager<AppUser> userManager, IJwtGenerator jwtGenerator)
            {
                _userManager = userManager;
                _jwtGenerator = jwtGenerator;
            }
            public async Task<UserDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByEmailAsync(request.Email);
                return new UserDto
                {
                    DisplayName = user.DisplayName,
                    Token = await _jwtGenerator.CreateToken(user),
                    Username = user.UserName
                };
            }
        }
    }
}
