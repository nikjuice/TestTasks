using GoGreenService.Models;
using GoGreenService.Services;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Raven.Client.Documents.Session;

namespace GoGreenServiceTests
{
    public class GoGreenServiceTests
    {
        [Test]
        public void TestNameFieldValidation()
        {
           var sessionMock = new Mock<IAsyncDocumentSession>();
           var loggerMock = new Mock<ILogger<VeggieService>>();
           var service = new VeggieService(sessionMock.Object, loggerMock.Object);

            var result = service.AddAsync(new Veggie()
            {
                Name = "This is really huge name of the veggie to test the validation",
                Price = 44.43
            });

            Assert.AreEqual(null, result.Result);
        }
    }
}