using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoveIT.Models
{
    public class RelocationPriceInfo
    {
        public int ID { get; set; }
        public decimal TotalGrossPrice { get; set; }
        public IList<RelocationPricePart> AdditionalInfo { get; set; }
    }

    /// <summary>
    /// Class to handle addtional information about the part of total price. To be visible for the administrators
    /// </summary>
    
}
