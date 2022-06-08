using MoveITPriceService.Models;

namespace MoveITPriceService.Calculations.Rules
{
    internal interface IPriceRule
    {
        public bool IsApplicable(RelocationBasicInfo basicInfo);
        public RelocationPricePart Execute(RelocationBasicInfo basicInfo);       
    }
}