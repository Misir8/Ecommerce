using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.Features.Account;
using Application.Features.Account.Command;
using Application.Features.Account.Query;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AccountController:BaseController
    {

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(Login.Query query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(Register.RegisterCommand command)
        {
            return await Mediator.Send(command);
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet]
        public async Task<UserDto> GetCurrentUser()
        {
            var email = HttpContext.User?.Claims?.
                FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            return await Mediator.Send(new CurrentUser.Query{Email = email});
        }


        [HttpGet("emailexists")]
        public async Task<bool> CheckEmailExistsAsync([FromQuery] string email)
        {
            return await Mediator.Send(new EmailExist.Query {Email = email});
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("address")]
        public async Task<AddressDto> GetUserAddress()
        {
            var user = HttpContext.User;
            return await Mediator.Send(new UserAddress.Query{User = user});
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPut("address")]
        public async Task<Unit> UpdateAddress(UpdateAddress.UpdateAddressCommand command)
        {
            command.User = HttpContext.User;
            return await Mediator.Send(command);
        }
    }
}
