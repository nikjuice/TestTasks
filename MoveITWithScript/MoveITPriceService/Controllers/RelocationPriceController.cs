using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MoveITPriceService.Calculations;
using MoveITPriceService.Models;
using System.Text.Json;

namespace MoveITPriceService.Controllers
{
    [ApiController]
    public class RelocationPriceController : ControllerBase
    {
        private readonly ILogger<RelocationPriceController> _logger;
        private readonly RelocationPriceCalculator _priceCalculator;

        public RelocationPriceController(ILogger<RelocationPriceController> logger, RelocationPriceCalculator priceCalculator)
        {
            _logger = logger;
            _priceCalculator = priceCalculator;
        }

        [HttpPost]
        [Route("price/getprice")]
        public RelocationPriceInfo CalculatePrice([FromBody] RelocationBasicInfo basicInfo )
        {

            return _priceCalculator.CalculatePrice(basicInfo);
        }
    }
}
