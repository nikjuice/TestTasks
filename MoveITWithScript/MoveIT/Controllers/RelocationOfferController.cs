using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MoveIT.Models;
using MoveITWeb.Builders;
using MoveITWeb.Services;
using MoveITWeb.ViewModel;

namespace MoveIT.Controllers
{

    [ApiController]
    public class RelocationOfferController : ControllerBase
    {
        private readonly ILogger<RelocationOfferController> _logger;
        private readonly RelocationOfferService _offerService;
        private readonly RelocationCustomerOfferViewModelBuilder _builder;

        public RelocationOfferController(ILogger<RelocationOfferController> logger,
                                        RelocationCustomerOfferViewModelBuilder builder,
                                        RelocationOfferService offerService)
        {
            _logger = logger;
            _offerService = offerService;
            _builder = builder;
        }

        //[Authorize(Policy = "IsAdministrator")]
        [Route("api/offer/calculate")]
        [HttpPost]
        public async Task<decimal> CalculateOffer([FromBody] RelocationInquiry inquiry)
        {
            var price = await _offerService.CalculatePrice(inquiry);

            return price;
        }

        [Authorize]
        [Route("api/offer/submit")]
        [HttpPost]
        public async Task<RelocationCustomerOfferViewModel> SubmitOffer([FromBody] RelocationInquiry inquiry)
        {
             var offer = await _offerService.SubmitRelocationOfferAsync(inquiry);

            return _builder.Build(offer);
        }
        [Authorize(Policy = "IsAdministrator")]
        [Route("api/offer/listall")]
        [HttpGet]
        public IList<RelocationCustomerOfferViewModel> GetAllOffersList()
        {
            var offers = _offerService.GetAllList();

            return  offers.Select(o => _builder.Build(o)).ToList();
        }

        [Route("api/offer/list")]
        [Authorize]        
        [HttpGet]
        public async Task<IList<RelocationCustomerOfferViewModel>> GetOfferList()
        {
            var offers = await _offerService.GetList();            

            return offers.Select(o => _builder.Build(o)).ToList();
        }


        [Route("api/offer/getbylink")]
        [HttpGet]
        public RelocationCustomerOfferViewModel GetOfferByLink(string base64)
        {
            var offer =  _offerService.GetOfferByGuid(base64);

            return _builder.Build(offer);
        }
    }
}
