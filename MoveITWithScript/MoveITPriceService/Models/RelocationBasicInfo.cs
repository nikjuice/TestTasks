using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoveITPriceService.Models
{
    public class RelocationBasicInfo
    {
        public decimal Distance { get; set; }
        public decimal Area { get; set; }
        public decimal SpecialArea { get; set; }
        public string Options { get; set; }

    }
}
