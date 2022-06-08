using MoveIT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoveITWeb.ViewModel
{
    public class RelocationCustomerOfferViewModel
    {
        public string OfferId { get; set; }
        public RelocationInquiry Inquiry { get; set; }
        public string TotalPriceFormatted { get; set; }
        public string OfferDateFormatted { get; set; }
        public string ShortUrl { get; set; }
    }
}
