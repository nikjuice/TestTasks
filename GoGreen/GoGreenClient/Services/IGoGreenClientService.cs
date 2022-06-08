using GoGreenClient.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoGreenClient.Services
{
    interface IGoGreenClientService
    {
        Task<IEnumerable<Veggie>> GetAllVeggiesAsync();
        Task<Veggie> GetVeggieAsync(string id);
        Task DeleteVeggieAsync(string id);
        Task<Veggie> AddVeggieAsync(Veggie veggie);
        Task UpdateVeggieAsync(Veggie veggie);
    }
}
