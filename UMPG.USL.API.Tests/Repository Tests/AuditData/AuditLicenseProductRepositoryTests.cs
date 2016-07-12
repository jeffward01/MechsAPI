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
using UMPG.USL.API.Business;
using System.Web.Http;
using UMPG.USL.API.Controllers;
 
using UMPG.USL.Security.Safe;
using System.Net;
using UMPG.USL.API.Data.AuditData;

namespace UMPG.USL.API.Tests.Repository_Tests
{
    [TestFixture]
    public class AuditLicenseProductRepositoryTests
    {
        [Test]
        public void GetAuditLicenseProducts_ReturnListAuditLicenseProduct()
        {
            //Arrange
            var mockAuditLicenseProductRepository = A.Fake<IAuditLicenseProductRepository>();

            //Build expected
            List<AuditLicenseProduct> expected = new List<AuditLicenseProduct> { };

            A.CallTo(() => mockAuditLicenseProductRepository.GetAuditLicenseProducts(A<int>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            var returnedResult = mockAuditLicenseProductRepository.GetAuditLicenseProducts(A<int>.Ignored);

            //Assert
            Assert.AreSame(expected, returnedResult);
            A.CallTo(() => mockAuditLicenseProductRepository.GetAuditLicenseProducts(A<int>.Ignored)).WithAnyArguments().MustHaveHappened();
        }

        [Test]
        public void GetAuditLicenseProduct_ReturnAuditLicenseProduct()
        {
            //Arrange
            var mockAuditLicenseProductRepository = A.Fake<IAuditLicenseProductRepository>();

            //Build expected
            AuditLicenseProduct expected = new AuditLicenseProduct { };

            A.CallTo(() => mockAuditLicenseProductRepository.GetAuditLicenseProduct(A<int>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            var returnedResult = mockAuditLicenseProductRepository.GetAuditLicenseProduct(A<int>.Ignored);

            //Assert
            Assert.AreSame(expected, returnedResult);
            A.CallTo(() => mockAuditLicenseProductRepository.GetAuditLicenseProduct(A<int>.Ignored)).WithAnyArguments().MustHaveHappened();
        }

        [Test]
        public void GetAuditForProducts_ReturnListAuditLicenseProduct()
        {
            //Arrange
            var mockAuditLicenseProductRepository = A.Fake<IAuditLicenseProductRepository>();

            //Build expected
            List<AuditProductProcedureResult> expected = new List<AuditProductProcedureResult> { };

            A.CallTo(() => mockAuditLicenseProductRepository.GetAuditForProducts(A<AuditGenericRequest>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            var returnedResult = mockAuditLicenseProductRepository.GetAuditForProducts(A<AuditGenericRequest>.Ignored);

            //Assert
            Assert.AreSame(expected, returnedResult);
            A.CallTo(() => mockAuditLicenseProductRepository.GetAuditForProducts(A<AuditGenericRequest>.Ignored)).WithAnyArguments().MustHaveHappened();
        }
    }
}
