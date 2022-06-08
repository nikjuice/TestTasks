using Microsoft.AspNetCore.Http;
using MoveIT.Data;
using MoveITWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoveITWeb.Services
{
    public class RelocationOfferReferenceService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RelocationOfferReferenceService(ApplicationDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<RelocationOfferReference> CreateReference(int offerId)
        {
            if (offerId == 0) throw new ArgumentException(nameof(offerId));

            var offerReference = new RelocationOfferReference()
            {
                RelocationOfferId = offerId,
                Reference = Guid.NewGuid()
            };

            _dbContext.Add(offerReference);
            await _dbContext.SaveChangesAsync();

            return offerReference;
        }

        public string GenerateUrl(RelocationOfferReference offerReference)
        {
            if(offerReference == null)
            {
                return String.Empty;
            }

            var CurrentContext = _httpContextAccessor.HttpContext;
            var baseUrl = $"{CurrentContext.Request.Scheme}://{CurrentContext.Request.Host}{CurrentContext.Request.PathBase}";
            var token = Convert.ToBase64String(offerReference.Reference.ToByteArray()).Replace("/", "-").Replace("+", "_").Replace("=", "");
            var offerShortUrl = $"{baseUrl}/offer/{token}";

            return offerShortUrl;
        }

        public int  GetOfferIdByGuid(string base64)
        {
            Guid guid = default(Guid);           

            try
            {
                guid = new Guid(Convert.FromBase64String(base64.Replace("-", "/").Replace("_", "+") + "=="));
            }
            catch (Exception ex)
            {
                throw new Exception("Error during Base64 conversion to GUID", ex);
            }

            var reference =_dbContext.RelocationOfferReferences.Where(ror => ror.Reference == guid).FirstOrDefault();

            if(reference == null)
            {
                throw new Exception("Can't find offer");
            }

            return reference.RelocationOfferId;
           
        }
    }
}
