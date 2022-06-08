using GoGreenService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoGreenService.Services
{
    public interface IVeggieService
    {
        Task<IEnumerable<Veggie>> GetAsync();
        Task<Veggie> GetAsync(string id);
        Task<Veggie> AddAsync(Veggie veggie);
        Task<bool> UpdateAsync(Veggie veggie);
        Task<bool> DeleteAsync(string id);
    }
}
