using MoveIT.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;


namespace MoveITWeb.Models
{
    [Table("RelocationOffers")]
    public class RelocationOffer
    {
        public int ID { get; set; }
        public string ApplicationUserId { get; set; }
        public RelocationOfferReference Reference { get; set; }
        public RelocationInquiry Inquiry { get; set; }
        public RelocationPriceInfo PriceInfo { get; set; }
        public DateTime PlacedOn { get; set; }
      
    }
}
