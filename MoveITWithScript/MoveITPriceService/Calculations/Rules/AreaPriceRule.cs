
using MoveITPriceService.Models;

namespace MoveITPriceService.Calculations.Rules
{
    public abstract class AreaPriceRule : IPriceRule
    {
        public AreaPriceRule(decimal DistancePrice ) 
        {
            this.DistancePrice = DistancePrice;
        }
        protected decimal DistancePrice { get; private set; }

        public abstract RelocationPricePart Execute(RelocationBasicInfo basicInfo);
        public abstract bool IsApplicable(RelocationBasicInfo basicInfo);
    }
}
