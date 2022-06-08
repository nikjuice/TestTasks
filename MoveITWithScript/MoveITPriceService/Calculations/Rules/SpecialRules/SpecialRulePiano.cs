using MoveITPriceService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoveITPriceService.Calculations.Rules
{
    public class SpecialRulePiano : IPriceRule
    {
        public bool IsApplicable(RelocationBasicInfo basicInfo)
        {
            if (string.IsNullOrEmpty(basicInfo.Options))
            {
                return false;
            }

            return basicInfo.Options.Split(";").FirstOrDefault(o => o.Contains("Piano")) != null;
        }
        public RelocationPricePart Execute(RelocationBasicInfo basicInfo)
        {           
            return new RelocationPricePart()
            {
                PricePart = 5000,
                PriceDesription = $"Piano relocation costs additional 5000 kr"
            };
        }       
    }
}
