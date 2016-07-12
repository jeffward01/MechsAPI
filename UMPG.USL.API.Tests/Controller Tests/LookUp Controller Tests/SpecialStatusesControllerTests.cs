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
    public class SpecialStatusesControllerTests
    {
        [Test]
        public void GetAll_ReturnListLU_SpecialStatus()
        {
            //Arrange
            var mockSpecialStatusManager = A.Fake<ISpecialStatusManager>();

            //Build expected
            List<LU_SpecialStatus> expected = new List<LU_SpecialStatus> { };

            A.CallTo(() => mockSpecialStatusManager.GetAll()).Returns(expected);

            //Call  
            SpecialStatusesController controller = new SpecialStatusesController(mockSpecialStatusManager);
            var result = controller.Get();

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetSpecialStatus_ReturnLU_SpecialStatus()
        {
            //Arrange
            var mockSpecialStatusManager = A.Fake<ISpecialStatusManager>();

            //Build expected
            LU_SpecialStatus expected = new LU_SpecialStatus { };

            A.CallTo(() => mockSpecialStatusManager.Get(A<int>.Ignored)).Returns(expected);

            //Call  
            SpecialStatusesController controller = new SpecialStatusesController(mockSpecialStatusManager);
            var result = controller.GetSpecialStatus(A<int>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Search_ReturnListLU_SpecialStatus()
        {
            //Arrange
            var mockSpecialStatusManager = A.Fake<ISpecialStatusManager>();

            //Build expected
            List<LU_SpecialStatus> expected = new List<LU_SpecialStatus> { };

            A.CallTo(() => mockSpecialStatusManager.Search(A<string>.Ignored)).Returns(expected);

            //Call  
            SpecialStatusesController controller = new SpecialStatusesController(mockSpecialStatusManager);
            var result = controller.Search(A<string>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }
    }
}
