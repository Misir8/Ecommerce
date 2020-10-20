using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Interfaces.Services
{
    public interface IJwtGenerator
    {
        Task<string> CreateToken(AppUser user);
    }
}
