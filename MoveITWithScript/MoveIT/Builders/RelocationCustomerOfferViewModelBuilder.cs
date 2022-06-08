using MoveITWeb.Models;
using MoveITWeb.Services;
using MoveITWeb.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoveITWeb.Builders
{
    public class RelocationCustomerOfferViewModelBuilder
    {
        private readonly RelocationOfferReferenceService _referenceService;

        public RelocationCustomerOfferViewModelBuilder(RelocationOfferReferenceService referenceService)
        {
            _referenceService = referenceService;
        }
        public RelocationCustomerOfferViewModel Build(RelocationOffer offer)
        {
            return new RelocationCustomerOfferViewModel()
            {
                OfferId = offer.ID.ToString(),
                Inquiry = offer.Inquiry,
                TotalPriceFormatted = $"{offer.PriceInfo.TotalGrossPrice} kr",
                OfferDateFormatted = offer.PlacedOn.ToString("MM/dd/yyyy HH:mm"),
                ShortUrl = _referenceService.GenerateUrl(offer.Reference)
            };
        }
    }
}
