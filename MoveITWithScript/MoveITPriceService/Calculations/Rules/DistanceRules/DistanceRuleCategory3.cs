using MoveITPriceService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoveITPriceService.Calculations.Rules
{
    public class DistanceRuleCategory3 : IPriceRule
    {
        public bool IsApplicable(RelocationBasicInfo basicInfo)
        {
            return basicInfo.Distance >= 100;
        }
        public RelocationPricePart Execute(RelocationBasicInfo basicInfo)
        {
            return new RelocationPricePart()
            {
                PricePart = 10000 + basicInfo.Distance * 7,
                PriceDesription = $"Distance is below 50km, base price is 10000kr and 1 km costs 7kr"
            };
        }       
    }
}
