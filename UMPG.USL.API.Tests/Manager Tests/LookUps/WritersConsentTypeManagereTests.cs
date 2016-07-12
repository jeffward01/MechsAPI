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
using UMPG.USL.Common;
using System.Net.Http;
using System.Web.Http;
 
using System.Net;
using UMPG.USL.Models.ContactModel;
using UMPG.USL.API.Data.AuditData;
using UMPG.USL.API.Data.LicenseData;
using UMPG.USL.Models.LicenseGenerate;
using UMPG.USL.API.Business.Lookups;
using UMPG.USL.Models.LookupModel;
using UMPG.USL.API.Data.LookupData;


namespace UMPG.USL.API.Tests.Manager_Tests.LookUps
{
    [TestFixture]
    public class WritersConsentTypeManagereTests
    {
        [Test]
        public void Get_ReturnWritersConsentType()
        {
            //Arrange
            var mockIWritersConsentTypeRepository = A.Fake<IWritersConsentTypeRepository>();

            //Build expected
            LU_WritersConsentType expected = new LU_WritersConsentType { };

            A.CallTo(() => mockIWritersConsentTypeRepository.Get(A<int>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            WritersConsentTypeManager manager = new WritersConsentTypeManager(mockIWritersConsentTypeRepository);
            var result = manager.Get(A<int>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }


        [Test]
        public void GetAll_ReturnListWritersConsentType()
        {
            //Arrange
            var mockIWritersConsentTypeRepository = A.Fake<IWritersConsentTypeRepository>();

            //Build expected
            List<LU_WritersConsentType> expected = new List<LU_WritersConsentType> { };

            A.CallTo(() => mockIWritersConsentTypeRepository.GetAll()).WithAnyArguments().Returns(expected);

            //Act
            WritersConsentTypeManager manager = new WritersConsentTypeManager(mockIWritersConsentTypeRepository);
            var result = manager.GetAll();

            //Assert
            Assert.AreEqual(expected, result);
        }


        [Test]
        public void GetWritersConsentForLookup_ReturnListWritersConsentType()
        {
            //Arrange
            var mockIWritersConsentTypeRepository = A.Fake<IWritersConsentTypeRepository>();

            //Build expected
            List<LU_WritersConsentType> expected = new List<LU_WritersConsentType> { };

            A.CallTo(() => mockIWritersConsentTypeRepository.GetWritersConsentForLookup()).WithAnyArguments().Returns(expected);

            //Act
            WritersConsentTypeManager manager = new WritersConsentTypeManager(mockIWritersConsentTypeRepository);
            var result = manager.GetWritersConsentForLookup();

            //Assert
            Assert.AreEqual(expected, result);
        }


        [Test]
        public void GetPaidQuarterForLookup_ReturnListLU_PaidQuarterType()
        {
            //Arrange
            var mockIWritersConsentTypeRepository = A.Fake<IWritersConsentTypeRepository>();

            //Build expected
            List<LU_PaidQuarterType> expected = new List<LU_PaidQuarterType> { };

            A.CallTo(() => mockIWritersConsentTypeRepository.GetPaidQuarterForLookup()).WithAnyArguments().Returns(expected);

            //Act
            WritersConsentTypeManager manager = new WritersConsentTypeManager(mockIWritersConsentTypeRepository);
            var result = manager.GetPaidQuarterForLookup();

            //Assert
            Assert.AreEqual(expected, result);
        }


        [Test]
        public void GetWritersIncludeExcludeForLookup_ReturnListLU_WritersIncludeExcludeType()
        {
            //Arrange
            var mockIWritersConsentTypeRepository = A.Fake<IWritersConsentTypeRepository>();

            //Build expected
            List<LU_WritersIncludeExcludeType> expected = new List<LU_WritersIncludeExcludeType> { };

            A.CallTo(() => mockIWritersConsentTypeRepository.GetWritersIncludeExcludeForLookup()).WithAnyArguments().Returns(expected);

            //Act
            WritersConsentTypeManager manager = new WritersConsentTypeManager(mockIWritersConsentTypeRepository);
            var result = manager.GetWritersIncludeExcludeForLookup();

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Search_ReturnListLU_ReturnLUWritersConsentType()
        {
            //Arrange
            var mockIWritersConsentTypeRepository = A.Fake<IWritersConsentTypeRepository>();

            //Build expected
            List<LU_WritersConsentType> expected = new List<LU_WritersConsentType> { };

            A.CallTo(() => mockIWritersConsentTypeRepository.Search(A<string>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            WritersConsentTypeManager manager = new WritersConsentTypeManager(mockIWritersConsentTypeRepository);
            var result = manager.Search(A<string>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }
    }
}
