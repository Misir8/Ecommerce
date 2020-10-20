using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Application.Extensions;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Account.Command
{
    public class UpdateAddress
    {
        public class UpdateAddressCommand : IRequest<Unit>
        {
            public string Firstname { get; set; }
            public string Lastname { get; set; }
            public string Street { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string ZipCode { get; set; }
            public ClaimsPrincipal User { get; set; }
        }

        public class CommandValidator : AbstractValidator<UpdateAddressCommand>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Firstname).NotEmpty();
                RuleFor(x => x.Lastname).NotEmpty();
                RuleFor(x => x.Street).NotEmpty();
                RuleFor(x => x.City).NotEmpty();
                RuleFor(x => x.State).NotEmpty();
                RuleFor(x => x.ZipCode).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<UpdateAddressCommand, Unit>
        {
            private readonly UserManager<AppUser> _userManager;
            private readonly IMapper _mapper;

            public Handler(UserManager<AppUser> userManager, IMapper mapper)
            {
                _userManager = userManager;
                _mapper = mapper;
            }
            public async Task<Unit> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByClaimsPrincipleWithAddressAsync(request.User);
                user.Address = new Address
                {
                    Firstname = request.Firstname,
                    Lastname = request.Lastname,
                    State = request.State,
                    City = request.City,
                    ZipCode = request.ZipCode,
                    Street = request.Street
                };
                var result = await _userManager.UpdateAsync(user);
                if(result.Succeeded) return Unit.Value;
                throw new Exception("problem resolve");
            }
        }
    }
}
