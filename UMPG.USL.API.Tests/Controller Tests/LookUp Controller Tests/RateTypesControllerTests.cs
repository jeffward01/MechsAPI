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
    public class RateTypesControllerTests
    {
        [Test]
        public void GetAllLU_RateType_ReturnListLU_RateType()
        {
            //Arrange
            var mockRateTypeManager = A.Fake<IRateTypeManager>();

            //Build expected
            List<LU_RateType> expected = new List<LU_RateType> { };

            A.CallTo(() => mockRateTypeManager.GetAll()).Returns(expected);

            //Call  
            RateTypesController controller = new RateTypesController(mockRateTypeManager);
            var result = controller.Get();

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetRateType_ReturnLU_RateType()
        {
            //Arrange
            var mockRateTypeManager = A.Fake<IRateTypeManager>();

            //Build expected
            LU_RateType expected = new LU_RateType { };

            A.CallTo(() => mockRateTypeManager.Get(A<int>.Ignored)).Returns(expected);

            //Call  
            RateTypesController controller = new RateTypesController(mockRateTypeManager);
            var result = controller.GetRateType(A<int>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void SearchLU_RateType_ReturnListLU_RateType()
        {
            //Arrange
            var mockRateTypeManager = A.Fake<IRateTypeManager>();

            //Build expected
            List<LU_RateType> expected = new List<LU_RateType> { };

            A.CallTo(() => mockRateTypeManager.Search(A<string>.Ignored)).Returns(expected);

            //Call  
            RateTypesController controller = new RateTypesController(mockRateTypeManager);
            var result = controller.Search(A<string>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }
    }
}
