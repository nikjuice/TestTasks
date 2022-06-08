using Microsoft.Extensions.Logging;
using MoveITPriceService.Calculations.Rules;
using MoveITPriceService.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MoveITPriceService.Calculations
{
    public class RelocationPriceCalculator
    {
        private readonly ILogger<RelocationPriceCalculator> _logger;

        public RelocationPriceCalculator(ILogger<RelocationPriceCalculator> logger)
        {
            _logger = logger;
        }

        public RelocationPriceInfo CalculatePrice(RelocationBasicInfo basicInfo)
        {
            if (basicInfo == null) throw new ArgumentNullException(nameof(basicInfo));

            List<RelocationPricePart> pricePartList = new List<RelocationPricePart>();

            pricePartList.AddRange(CalculateDistancePrice(basicInfo, out decimal totalDistancePrice));
            pricePartList.AddRange(CaculateAreaPrice(basicInfo, totalDistancePrice, out decimal totalAreaPrice));
            pricePartList.AddRange(CaculateSpecialPrice(basicInfo, out decimal totalSpecialPrice));

            return new RelocationPriceInfo()
            {
                TotalGrossPrice = totalDistancePrice + totalAreaPrice + totalSpecialPrice,
                AdditionalInfo = pricePartList
            };

        }



        private IList<RelocationPricePart> CalculateDistancePrice(RelocationBasicInfo basicInfo, out decimal totalDistancePrice)
        {
            var distanceRules = InitDistanceRules();

            var distancePrices = distanceRules
                                            .Where(rule => rule.IsApplicable(basicInfo)).ToList()
                                            .Select(rule => rule.Execute(basicInfo)).ToList();

            totalDistancePrice = distancePrices.Sum(p => p.PricePart);

            distancePrices.ForEach(p => _logger.LogInformation($"Added price: {p.PricePart} kr. Price description: {p.PriceDesription}"));

            _logger.LogInformation($"Distance price was calculated, {totalDistancePrice} kr");

            return distancePrices;
        }

        private IList<RelocationPricePart> CaculateAreaPrice(RelocationBasicInfo basicInfo, decimal totalDistancePrice, out decimal totalAreaPrice)
        {
            var areaRules = InitAreaRules(totalDistancePrice);

            var areaPrices = areaRules
                                    .Where(rule => rule.IsApplicable(basicInfo)).ToList()
                                    .Select(rule => rule.Execute(basicInfo)).ToList();

            areaPrices.ForEach(p => _logger.LogInformation($"Added price: {p.PricePart} kr. Price description: {p.PriceDesription}"));

            totalAreaPrice = areaPrices.Sum(p => p.PricePart);

            _logger.LogInformation($"Area price was calculated, {totalAreaPrice} kr");

            return areaPrices;
        }

        private IList<RelocationPricePart> CaculateSpecialPrice(RelocationBasicInfo basicInfo, out decimal totalSpecialPrice)
        {
            var specialRules = InitSpecialRules();

            var specialPrices = specialRules
                                    .Where(rule => rule.IsApplicable(basicInfo)).ToList()
                                    .Select(rule => rule.Execute(basicInfo)).ToList();

            specialPrices.ForEach(p => _logger.LogInformation($"Added price: {p.PricePart} Price description: {p.PriceDesription}"));

            totalSpecialPrice = specialPrices.Sum(p => p.PricePart);

            _logger.LogInformation($"Special price was calculated, {totalSpecialPrice} kr");

            return specialPrices;
        }



        private IList<IPriceRule> InitDistanceRules()
        {
            var distanceRules = new List<IPriceRule>();        
            distanceRules.Add(new DistanceRuleCategory1());
            distanceRules.Add(new DistanceRuleCategory2());
            distanceRules.Add(new DistanceRuleCategory3());

            return distanceRules;
        }

        private IList<IPriceRule> InitAreaRules(decimal DistancePrice)
        {
            var areaRules = new List<IPriceRule>();            
            areaRules.Add(new AreaRuleCategory1(DistancePrice));

            return areaRules;
        }

        private IList<IPriceRule> InitSpecialRules()
        {
            var specialRules = new List<IPriceRule>();                 
            specialRules.Add(new SpecialRulePiano());
            
            return specialRules;
        }
    }
}
