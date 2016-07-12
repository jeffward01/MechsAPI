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
    public class PaidQuartersControllerTests
    {
        [Test]
        public void GetAll_ReturnListLU_PaidQuarter()
        {
            //Arrange
            var mockPaidQuarterManager = A.Fake<IPaidQuarterManager>();

            //Build expected
            List<LU_PaidQuarter> expected = new List<LU_PaidQuarter> { };

            A.CallTo(() => mockPaidQuarterManager.GetAll()).Returns(expected);

            //Call  
            PaidQuartersController controller = new PaidQuartersController(mockPaidQuarterManager);
            var result = controller.Get();

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetRolling10years_ReturnListLU_PaidQuarter()
        {
            //Arrange
            var mockPaidQuarterManager = A.Fake<IPaidQuarterManager>();

            //Build expected
            List<LU_PaidQuarter> expected = new List<LU_PaidQuarter> { };

            A.CallTo(() => mockPaidQuarterManager.GetRolling10years()).Returns(expected);

            //Call  
            PaidQuartersController controller = new PaidQuartersController(mockPaidQuarterManager);
            var result = controller.GetRolling10years();

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetPaidQuarter_ReturnListLU_PaidQuarter()
        {
            //Arrange
            var mockPaidQuarterManager = A.Fake<IPaidQuarterManager>();

            //Build expected
            LU_PaidQuarter expected = new LU_PaidQuarter { };

            A.CallTo(() => mockPaidQuarterManager.Get(A<int>.Ignored)).Returns(expected);

            //Call  
            PaidQuartersController controller = new PaidQuartersController(mockPaidQuarterManager);
            var result = controller.GetPaidQuarter(A<int>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Search_ReturnListLU_PaidQuarter()
        {
            //Arrange
            var mockPaidQuarterManager = A.Fake<IPaidQuarterManager>();

            //Build expected
            List<LU_PaidQuarter> expected = new List<LU_PaidQuarter> { };

            A.CallTo(() => mockPaidQuarterManager.Search(A<string>.Ignored)).Returns(expected);

            //Call  
            PaidQuartersController controller = new PaidQuartersController(mockPaidQuarterManager);
            var result = controller.Search(A<string>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }
    }
}
