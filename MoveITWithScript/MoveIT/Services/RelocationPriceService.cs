

using Microsoft.Extensions.Configuration;
using MoveIT.Models;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MoveITWeb.Services
{
    public class RelocationPriceService
    {
        private readonly IConfiguration _configuration;
        public RelocationPriceService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<RelocationPriceInfo> GetCalculatedPriceAsync(RelocationInquiry inquiry)
        {
            if (inquiry == null) throw new ArgumentException(nameof(inquiry));

            var basicInfo = MapToBasicInfo(inquiry);
            RelocationPriceInfo priceInfo = null;

            HttpContent content = new StringContent(JsonSerializer.Serialize(basicInfo), Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PostAsync(_configuration["General:PriceServiceUrl"], content))
                {                    
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    priceInfo = await JsonSerializer.DeserializeAsync<RelocationPriceInfo>(apiResponse, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                }
            }

            return priceInfo;

        }

        private RelocationBasicInfo MapToBasicInfo(RelocationInquiry inquiry)
        {
            return new RelocationBasicInfo()
            {
                Area = decimal.Parse(inquiry.Area),
                Distance = decimal.Parse(inquiry.Distance),
                SpecialArea = decimal.Parse(inquiry.SpecialArea),
                Options = inquiry.Options
            };
        }
    }
}
