using MoveITPriceService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoveITPriceService.Calculations.Rules
{
    public class DistanceRuleCategory2 : IPriceRule
    {
        public bool IsApplicable(RelocationBasicInfo basicInfo)
        {
            return basicInfo.Distance >= 50 && basicInfo.Distance < 100;
        }
        public RelocationPricePart Execute(RelocationBasicInfo basicInfo)
        {
            return new RelocationPricePart()
            {
                PricePart = 5000 + basicInfo.Distance * 8,
                PriceDesription = $"Distance is above 50km and below or eqaul 100, base price is 5000kr and 1 km costs 8kr"
            };
        }       
    }
}
