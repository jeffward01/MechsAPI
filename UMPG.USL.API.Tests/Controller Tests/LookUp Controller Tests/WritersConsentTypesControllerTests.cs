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
    public class WritersConsentTypesControllerTests
    {
        [Test]
        public void GetAll_ReturnListLUWritersConsentType()
        {
            //Arrange
            var mockWritersConsentTypeManager = A.Fake<IWritersConsentTypeManager>();

            //Build expected
            List<LU_WritersConsentType> expected = new List<LU_WritersConsentType> { };

            A.CallTo(() => mockWritersConsentTypeManager.GetAll()).Returns(expected);

            //Call  
            WritersConsentTypesController controller = new WritersConsentTypesController(mockWritersConsentTypeManager);
            var result = controller.Get();

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetAllLUWriterConsentType_ReturnLUWritersConsentType()
        {
            //Arrange
            var mockWritersConsentTypeManager = A.Fake<IWritersConsentTypeManager>();

            //Build expected
            LU_WritersConsentType expected = new LU_WritersConsentType { };

            A.CallTo(() => mockWritersConsentTypeManager.Get(A<int>.Ignored)).Returns(expected);

            //Call  
            WritersConsentTypesController controller = new WritersConsentTypesController(mockWritersConsentTypeManager);
            var result = controller.GetWritersConsentType(A<int>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetWritersConsentForLookup_ReturnListLUWritersConsentType()
        {
            //Arrange
            var mockWritersConsentTypeManager = A.Fake<IWritersConsentTypeManager>();

            //Build expected
            List<LU_WritersConsentType> expected = new List<LU_WritersConsentType> { };

            A.CallTo(() => mockWritersConsentTypeManager.GetWritersConsentForLookup()).Returns(expected);

            //Call  
            WritersConsentTypesController controller = new WritersConsentTypesController(mockWritersConsentTypeManager);
            var result = controller.GetWritersConsentForLookup();

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetPaidQuarterForLookup_ReturnListLU_PaidQuarterType()
        {
            //Arrange
            var mockWritersConsentTypeManager = A.Fake<IWritersConsentTypeManager>();

            //Build expected
            List<LU_PaidQuarterType> expected = new List<LU_PaidQuarterType> { };

            A.CallTo(() => mockWritersConsentTypeManager.GetPaidQuarterForLookup()).Returns(expected);

            //Call  
            WritersConsentTypesController controller = new WritersConsentTypesController(mockWritersConsentTypeManager);
            var result = controller.GetPaidQuarterForLookup();

            //Assert
            Assert.AreEqual(expected, result);
        }


        [Test]
        public void GetWritersIncludeExcludeForLookup_ReturnListLU_WritersIncludeExcludeType()
        {
            //Arrange
            var mockWritersConsentTypeManager = A.Fake<IWritersConsentTypeManager>();

            //Build expected
            List<LU_WritersIncludeExcludeType> expected = new List<LU_WritersIncludeExcludeType> { };

            A.CallTo(() => mockWritersConsentTypeManager.GetWritersIncludeExcludeForLookup()).Returns(expected);

            //Call  
            WritersConsentTypesController controller = new WritersConsentTypesController(mockWritersConsentTypeManager);
            var result = controller.GetWritersIncludeExcludeForLookup();

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Search_ReturnLisLU_WritersConsentType()
        {
            //Arrange
            var mockWritersConsentTypeManager = A.Fake<IWritersConsentTypeManager>();

            //Build expected
            List<LU_WritersConsentType> expected = new List<LU_WritersConsentType> { };

            A.CallTo(() => mockWritersConsentTypeManager.Search(A<string>.Ignored)).Returns(expected);

            //Call  
            WritersConsentTypesController controller = new WritersConsentTypesController(mockWritersConsentTypeManager);
            var result = controller.Search(A<string>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }
    }
}
