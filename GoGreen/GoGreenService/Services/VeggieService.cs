using GoGreenService.Models;
using Microsoft.Extensions.Logging;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoGreenService.Services
{

    public class VeggieService : IVeggieService
    {
        private readonly IAsyncDocumentSession _session;
        private readonly ILogger<VeggieService> _logger;

        public VeggieService(IAsyncDocumentSession session, ILogger<VeggieService> logger)
        {
            _session = session;
            _logger = logger;
        }

        public async Task<Veggie> AddAsync(Veggie veggie)
        {
            try
            {
                _logger.LogInformation($"Adding {veggie.Name} with price = {veggie.Price}");
                
                if(veggie.Name.Length > 20)
                {
                    _logger.LogError($"Validation error, name is more than 30 symbols ");
                    return null;
                }

                veggie.CreatedTimeStamp = DateTime.Now;
                    

               await  _session.StoreAsync(veggie);
               await  _session.SaveChangesAsync();

               return veggie;
            }
            catch(Exception ex)
            {
                _logger.LogError($"Can't save {veggie.Name} to store ", ex);

                return null;
            }           

        }

        public async Task<bool> UpdateAsync(Veggie veggie)
        {
            try
            {
                _logger.LogInformation($"Update veggie with id {veggie.Id}");

               
                await _session.StoreAsync(veggie);
                await _session.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Can't update {veggie.Name} to store ", ex);
                return false;
            }
        }

        public async Task<bool> DeleteAsync(string id)
        {
            try
            {
                _logger.LogInformation($"Deleting veggie with id {id}");

                 _session.Delete(id);
                
                await _session.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Can't delete veggie with id  {id} from store ", ex);

                return false;
            }
        }

        public async Task<Veggie> GetAsync(string id)
        {
            try
            {
                _logger.LogInformation($"Getting  veggie with id {id}");
                return await _session.LoadAsync<Veggie>(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Can't get veggie with id  {id} from store ", ex);
                return null;
            }
        }
        public async Task<IEnumerable<Veggie>> GetAsync()
        {
            try
            {
                _logger.LogInformation($"Getting all veggies");
                return await _session.Query<Veggie>().OrderBy(v => v.CreatedTimeStamp).ToListAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Can't get all veggies  from store ", ex);
                return null;
            }
        }
     
    }
}
