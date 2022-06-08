using MoveITPriceService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoveITPriceService.Calculations.Rules
{
    public class AreaRuleCategory1 : AreaPriceRule
    {
        public AreaRuleCategory1(decimal DistancePrice) : base(DistancePrice) { }
        public override bool IsApplicable(RelocationBasicInfo basicInfo)
        {
            return true;
        }
        public override RelocationPricePart Execute(RelocationBasicInfo basicInfo)
        {
            decimal totalArea = basicInfo.Area;
            string specialAreaDescription = "";

            if(basicInfo.SpecialArea > 0)
            {
                totalArea += basicInfo.SpecialArea * 2;
                specialAreaDescription = $"Special area present, the special area {basicInfo.SpecialArea} m2 will be multiplied by 2";
            }

            int numberOfAdditionalCars = Convert.ToInt32(Math.Truncate(totalArea / 50));

            return new RelocationPricePart()
            {
                PricePart = numberOfAdditionalCars * DistancePrice,
                PriceDesription = $"AreaRuleCategory1: for every 50m2 additional car is required that costs {DistancePrice}. {numberOfAdditionalCars + 1} needed! {specialAreaDescription}"
            };
        }       
    }
}
