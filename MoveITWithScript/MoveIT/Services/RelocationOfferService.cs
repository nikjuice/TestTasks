

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MoveIT.Data;
using MoveIT.Models;
using MoveITWeb.Models;
using MoveITWeb.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MoveITWeb.Services
{
    public class RelocationOfferService
    {
        private readonly RelocationPriceService _priceService;
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly RelocationOfferReferenceService _referenceService;

        public RelocationOfferService(RelocationPriceService priceService, 
               ApplicationDbContext dbContext, 
               UserManager<ApplicationUser> userManager,
               IHttpContextAccessor httpContextAccessor,
               RelocationOfferReferenceService referenceService
            )
        {
            _priceService = priceService;
            _dbContext = dbContext;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _referenceService = referenceService;

    }

        public async Task<decimal> CalculatePrice(RelocationInquiry inquiry)
        {
            if (inquiry == null) throw new ArgumentException(nameof(inquiry));

            var priceInfo = await _priceService.GetCalculatedPriceAsync(inquiry);         

            return priceInfo.TotalGrossPrice;
        }

        public async Task<RelocationOffer> SubmitRelocationOfferAsync(RelocationInquiry inquiry)
        {
            if (inquiry == null) throw new ArgumentException(nameof(inquiry));

            var priceInfo = await _priceService.GetCalculatedPriceAsync(inquiry);

            var offer = new RelocationOffer()
            {
                ApplicationUserId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier),
                Inquiry = inquiry,
                PriceInfo = priceInfo,
                PlacedOn = DateTime.Now
            };

            var savedOffer = _dbContext.Add(offer);
            await _dbContext.SaveChangesAsync();

            offer.ID = savedOffer.Entity.ID;
            var reference = await _referenceService.CreateReference(offer.ID);
            offer.Reference = reference;

            return offer;

        }

        public async Task<IList<RelocationOffer>> GetList()
        {
            var user = await _userManager.FindByIdAsync(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

            if(user == null)
            {
                throw new Exception("User not found or not available");
            }

            return _dbContext.RelocationOffers.Where(ro => ro.ApplicationUserId == user.Id)
                                    .Include(o => o.PriceInfo)
                                        .ThenInclude(pi => pi.AdditionalInfo)
                                    .Include(o => o.Inquiry)
                                       .ThenInclude(i => i.AddressFrom)
                                     .Include(o => o.Inquiry)
                                       .ThenInclude(i => i.AddressTo)
                                     .Include(o => o.Reference)
                                     .ToList();



        }

        public IList<RelocationOffer> GetAllList()
        {
           

            return _dbContext.RelocationOffers
                                    .Include(o => o.PriceInfo)
                                        .ThenInclude(pi => pi.AdditionalInfo)
                                    .Include(o => o.Inquiry)
                                       .ThenInclude(i => i.AddressFrom)
                                     .Include(o => o.Inquiry)
                                       .ThenInclude(i => i.AddressTo)
                                     .Include(o => o.Reference)
                                     .ToList();



        }

        public  RelocationOffer GetOfferByGuid(string base64Guid)
        {
            var offerId = _referenceService.GetOfferIdByGuid(base64Guid); 


            return  _dbContext.RelocationOffers.Where(ro => ro.ID == offerId)
                                    .Include(o => o.PriceInfo)
                                        .ThenInclude(pi => pi.AdditionalInfo)
                                    .Include(o => o.Inquiry)
                                       .ThenInclude(i => i.AddressFrom)
                                     .Include(o => o.Inquiry)
                                       .ThenInclude(i => i.AddressTo)
                                     .Include(o => o.Reference)
                                     .FirstOrDefault();



        }

    }
}
