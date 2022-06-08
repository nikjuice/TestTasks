using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoveITPriceService.Models
{
    public class RelocationPriceInfo
    {
        public decimal TotalGrossPrice { get; set; }
        public IList<RelocationPricePart> AdditionalInfo { get; set; }
    }

    /// <summary>
    /// Class to handle addtional information about the part of total price. To be visible for the administrators
    /// </summary>
    
}
