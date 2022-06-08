using GoGreenClient.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace GoGreenClient.Services
{   
    public class GoGreenClientService : IGoGreenClientService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _cofiguration;
        private readonly string _serviceUrl;
        public GoGreenClientService(HttpClient httpClient, IConfiguration cofiguration)
        {
            _httpClient = httpClient;
            _cofiguration = cofiguration;

            _serviceUrl = _cofiguration.GetValue<string>("serviceUrl"); 

        }

        public async Task<IEnumerable<Veggie>> GetAllVeggiesAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Veggie>>($"{_serviceUrl}/api/veggie");
        }
        public async Task<Veggie> GetVeggieAsync(string id)
        {
            return await _httpClient.GetFromJsonAsync<Veggie>($"{_serviceUrl}/api/veggie/{id}");
        }
        public async Task<Veggie> AddVeggieAsync(Veggie veggie)
        {
           var postResult = await _httpClient.PostAsJsonAsync($"{_serviceUrl}/api/veggie", veggie);
           return await postResult.Content.ReadFromJsonAsync<Veggie>();
        }

        public async Task DeleteVeggieAsync(string id)
        {
            await _httpClient.DeleteAsync($"{_serviceUrl}/api/veggie/{id}");
        }
      

        public async Task UpdateVeggieAsync(Veggie veggie)
        {
            await _httpClient.PutAsJsonAsync<Veggie>($"{_serviceUrl}/api/veggie", veggie);
        }
    }
}
