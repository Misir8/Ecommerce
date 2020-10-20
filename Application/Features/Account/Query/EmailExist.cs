using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Account.Query
{
    public class EmailExist
    {
        public class Query: IRequest<bool>
        {
            public string Email { get; set; }
        }

        public class Handler : IRequestHandler<Query, bool>
        {
            private readonly UserManager<AppUser> _userManager;

            public Handler(UserManager<AppUser> userManager)
            {
                _userManager = userManager;
            }
            public async Task<bool> Handle(Query request, CancellationToken cancellationToken)
            {
                if (request.Email == null) return false;
                return await _userManager.FindByEmailAsync(request.Email) != null;
            }
        }
    }
}
