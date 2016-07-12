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
namespace UMPG.USL.API.Tests.Controller_Tests.LookUp_Controller_Tests
{
    [TestFixture]
    public class LicenseStatusControllerTests
    {
        [Test]
        public void GetAllLULicenseStatus_ReturnListLU_LicenseStatus()
        {
            //Arrange
            var mockLicenseStatusManager = A.Fake<ILicenseStatusManager>();

            //Build expected
            List<LU_LicenseStatus> expected = new List<LU_LicenseStatus> { };

            A.CallTo(() => mockLicenseStatusManager.GetAll()).Returns(expected);

            //Call  
            LicenseStatusesController controller = new LicenseStatusesController(mockLicenseStatusManager);
            var result = controller.Get();

            //Assert
            Assert.AreEqual(expected, result);
        }
    }
}
