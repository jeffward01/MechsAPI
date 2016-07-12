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
using UMPG.USL.API.Business.Lookups;
using UMPG.USL.Models.LookupModel;
using UMPG.USL.API.Controllers.LookUpCTRL;
using UMPG.USL.API.Business.Recs;
using UMPG.USL.API.Controllers.RECsCTRL;

namespace UMPG.USL.API.Tests.Controller_Tests.RECs_Controller_Tests
{
    [TestFixture]
    public class ConfiguratinsControllerTests
    {
        [Test]
        public void GetAll_ReturnListRecsConfigurations()
        {
            //Arrange
            var mockConfigurationManager = A.Fake<IConfigurationManager>();

            //Build expected
            List<RecsConfigurations> expected = new List<RecsConfigurations> { };

            A.CallTo(() => mockConfigurationManager.GetConfigurations()).Returns(expected);

            //Call  
            ConfigurationsController controller = new ConfigurationsController(mockConfigurationManager);
            var result = controller.Get();

            //Assert
            Assert.AreEqual(expected, result);
        }
    }
}
