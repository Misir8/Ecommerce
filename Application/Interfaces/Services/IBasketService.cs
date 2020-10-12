using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Interfaces.Services
{
    public interface IBasketService
    {
        Task<CustomerBasket> GetBasketAsync(string basketId);
        Task<CustomerBasket> UpdateBasketAsync(CustomerBasket customerBasket);
        Task<bool> DeleteBasketAsync(string basketId);
    }
}
