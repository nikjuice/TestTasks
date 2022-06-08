using MoveITPriceService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoveITPriceService.Calculations.Rules
{
    public class DistanceRuleCategory1 : IPriceRule
    {
        public bool IsApplicable(RelocationBasicInfo basicInfo)
        {
            return basicInfo.Distance < 50;
        }
        public RelocationPricePart Execute(RelocationBasicInfo basicInfo)
        {           
            return new RelocationPricePart()
            {
                PricePart = 1000 + basicInfo.Distance * 10,
                PriceDesription = $"Distance is below or equal 50km, base price is 1000kr and 1 km costs 10kr"
            };
        }       
    }
}
