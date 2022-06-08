using Microsoft.Extensions.Logging;
using Moq;
using MoveITPriceService.Calculations;
using MoveITPriceService.Models;
using NUnit.Framework;

namespace MoveITPriceService.UnitTests
{
    [TestFixture]
    public class RelocationPriceCalculatorTests
    {
        [Test]
        public void ComplexTestFromTaskWithDifferentCategories()
        {
            var logger = Mock.Of<ILogger<RelocationPriceCalculator>>();
            var calculator = new RelocationPriceCalculator(logger);
            var basicInfoTestData = new RelocationBasicInfo()
            {
                Distance = 10,
                Area = 49,
                SpecialArea = 0
            };

            var actual = calculator.CalculatePrice(basicInfoTestData);

            Assert.AreEqual(1100, actual.TotalGrossPrice);
        }
        [Test]
        public void ComplexTestFromTaskWithDifferentCategories2()
        {
            var logger = Mock.Of<ILogger<RelocationPriceCalculator>>();
            var calculator = new RelocationPriceCalculator(logger);
            var basicInfoTestData = new RelocationBasicInfo()
            {
                Distance = 10,
                Area = 10,
                SpecialArea = 25
            };

            var actual = calculator.CalculatePrice(basicInfoTestData);

            Assert.AreEqual(2200, actual.TotalGrossPrice);
        }
        [Test]
        public void ComplexTestFromTaskWithDifferentCategories3()
        {
            var logger = Mock.Of<ILogger<RelocationPriceCalculator>>();
            var calculator = new RelocationPriceCalculator(logger);
            var basicInfoTestData = new RelocationBasicInfo()
            {
                Distance = 10,
                Area = 50,
                SpecialArea = 0
            };

            var actual = calculator.CalculatePrice(basicInfoTestData);

            Assert.AreEqual(2200, actual.TotalGrossPrice);
        }
        [Test]
        public void ComplexTestFromTaskWithDifferentCategories4()
        {
            var logger = Mock.Of<ILogger<RelocationPriceCalculator>>();
            var calculator = new RelocationPriceCalculator(logger);
            var basicInfoTestData = new RelocationBasicInfo()
            {
                Distance = 10,
                Area = 100,
                SpecialArea = 0
            };

            var actual = calculator.CalculatePrice(basicInfoTestData);

            Assert.AreEqual(3300, actual.TotalGrossPrice);
        }
        [Test]
        public void ComplexTestFromTaskWithDifferentCategories5()
        {
            var logger = Mock.Of<ILogger<RelocationPriceCalculator>>();
            var calculator = new RelocationPriceCalculator(logger);
            var basicInfoTestData = new RelocationBasicInfo()
            {
                Distance = 10,
                Area = 150,
                SpecialArea = 0
            };

            var actual = calculator.CalculatePrice(basicInfoTestData);

            Assert.AreEqual(4400, actual.TotalGrossPrice);
        }
        [Test]
        public void ComplexTestFromTaskWithDifferentCategories6()
        {
            var logger = Mock.Of<ILogger<RelocationPriceCalculator>>();
            var calculator = new RelocationPriceCalculator(logger);
            var basicInfoTestData = new RelocationBasicInfo()
            {
                Distance = 10,
                Area = 150,
                SpecialArea = 0,
                Options = "Piano"
            };

            var actual = calculator.CalculatePrice(basicInfoTestData);

            Assert.AreEqual(9400, actual.TotalGrossPrice);
        }

        [Test]
        public void DistanceRuleTest()
        {
            var logger = Mock.Of<ILogger<RelocationPriceCalculator>>();
            var calculator = new RelocationPriceCalculator(logger);
            var basicInfoTestData = new RelocationBasicInfo()
            {
                Distance = 10,
                Area = 0,
                SpecialArea = 0
            };

            var actual = calculator.CalculatePrice(basicInfoTestData);

            Assert.AreEqual(1100, actual.TotalGrossPrice);
        }

        [Test]
        public void DistanceRuleTest2()
        {
            var logger = Mock.Of<ILogger<RelocationPriceCalculator>>();
            var calculator = new RelocationPriceCalculator(logger);
            var basicInfoTestData = new RelocationBasicInfo()
            {
                Distance = 49,
                Area = 0,
                SpecialArea = 0
            };

            var actual = calculator.CalculatePrice(basicInfoTestData);

            Assert.AreEqual(1490, actual.TotalGrossPrice);
        }
        [Test]
        public void DistanceRuleTest3()
        {
            var logger = Mock.Of<ILogger<RelocationPriceCalculator>>();
            var calculator = new RelocationPriceCalculator(logger);
            var basicInfoTestData = new RelocationBasicInfo()
            {
                Distance = 50,
                Area = 0,
                SpecialArea = 0
            };

            var actual = calculator.CalculatePrice(basicInfoTestData);

            Assert.AreEqual(5400, actual.TotalGrossPrice);
        }
        [Test]
        public void DistanceRuleTest4()
        {
            var logger = Mock.Of<ILogger<RelocationPriceCalculator>>();
            var calculator = new RelocationPriceCalculator(logger);
            var basicInfoTestData = new RelocationBasicInfo()
            {
                Distance = 51,
                Area = 0,
                SpecialArea = 0
            };

            var actual = calculator.CalculatePrice(basicInfoTestData);

            Assert.AreEqual(5408, actual.TotalGrossPrice);
        }
        [Test]
        public void DistanceRuleTest5()
        {
            var logger = Mock.Of<ILogger<RelocationPriceCalculator>>();
            var calculator = new RelocationPriceCalculator(logger);
            var basicInfoTestData = new RelocationBasicInfo()
            {
                Distance = 99,
                Area = 0,
                SpecialArea = 0
            };

            var actual = calculator.CalculatePrice(basicInfoTestData);

            Assert.AreEqual(5792, actual.TotalGrossPrice);
        }

        [Test]
        public void DistanceRuleTest6()
        {
            var logger = Mock.Of<ILogger<RelocationPriceCalculator>>();
            var calculator = new RelocationPriceCalculator(logger);
            var basicInfoTestData = new RelocationBasicInfo()
            {
                Distance = 100,
                Area = 0,
                SpecialArea = 0
            };

            var actual = calculator.CalculatePrice(basicInfoTestData);

            Assert.AreEqual(10700, actual.TotalGrossPrice);
        }

        [Test]
        public void DistanceRuleTest7()
        {
            var logger = Mock.Of<ILogger<RelocationPriceCalculator>>();
            var calculator = new RelocationPriceCalculator(logger);
            var basicInfoTestData = new RelocationBasicInfo()
            {
                Distance = 101,
                Area = 0,
                SpecialArea = 0
            };

            var actual = calculator.CalculatePrice(basicInfoTestData);

            Assert.AreEqual(10707, actual.TotalGrossPrice);
        }
    }
}
