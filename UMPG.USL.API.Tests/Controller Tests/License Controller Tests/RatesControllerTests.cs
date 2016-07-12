using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using NUnit;
using NUnit.Framework;
using UMPG.USL.API.Business.Audits;
using UMPG.USL.Models;
using UMPG.USL.Models.AuditModel;
using UMPG.USL.Models.Recs;
using UMPG.USL.API.Business.Licenses;
using UMPG.USL.Models.LicenseModel;
using UMPG.USL.API.Controllers.LicenseCTRL;


namespace UMPG.USL.API.Tests.Controller_Tests.License_Controller_Tests
{
   public class RatesControllerTests
    {
        [Test]
       public void GetRatesFromWriters_ReturnListLicenseProductRecordingWriterRate()
        {
            //Arrange
            var mockLicensePRWriterRateManager = A.Fake<ILicensePRWriterRateManager>();

            //Build expected 
            List<LicenseProductRecordingWriterRate> expected = new List<LicenseProductRecordingWriterRate> { };

            A.CallTo(() => mockLicensePRWriterRateManager.GetEditWriterRates(A<GetWritersRatesRequest>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            RatesController controller = new RatesController(mockLicensePRWriterRateManager);
            var result = controller.GetRates(A<GetWritersRatesRequest>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }
    }
}
