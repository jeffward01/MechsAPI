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
    public class AuditLicenseRepositoryTests
    {
        [Test]
        public void GetAuditLicenseAttachement_ReturnAuditLicense()
        {
            //Arrange
            var mockAuditLicenseRepository = A.Fake<IAuditLicenseRepository>();

            //Build expected
            AuditLicense expected = new AuditLicense { };

            A.CallTo(() => mockAuditLicenseRepository.Get(A<int>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            var returnedResult = mockAuditLicenseRepository.Get(A<int>.Ignored);

            //Assert
            Assert.AreSame(expected, returnedResult);
            A.CallTo(() => mockAuditLicenseRepository.Get(A<int>.Ignored)).WithAnyArguments().MustHaveHappened();
        }

        [Test]
        public void GetAll_ReturnAuditLicense()
        {
            //Arrange
            var mockAuditLicenseRepository = A.Fake<IAuditLicenseRepository>();

            //Build expected
            List<AuditLicense> expected = new List<AuditLicense> { };

            A.CallTo(() => mockAuditLicenseRepository.GetAll()).WithAnyArguments().Returns(expected);

            //Act
            var returnedResult = mockAuditLicenseRepository.GetAll();

            //Assert
            Assert.AreSame(expected, returnedResult);
            A.CallTo(() => mockAuditLicenseRepository.GetAll()).WithAnyArguments().MustHaveHappened();
        }

        [Test]
        public void GetAuditForLicense_ReturnListAuditLicense()
        {
            //Arrange
            var mockAuditLicenseRepository = A.Fake<IAuditLicenseRepository>();

            //Build expected
            List<AuditLicenseProcedureResult> expected = new List<AuditLicenseProcedureResult> { };

            A.CallTo(() => mockAuditLicenseRepository.GetAuditForLicense(A<AuditGenericRequest>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            var returnedResult = mockAuditLicenseRepository.GetAuditForLicense(A<AuditGenericRequest>.Ignored);

            //Assert
            Assert.AreSame(expected, returnedResult);
            A.CallTo(() => mockAuditLicenseRepository.GetAuditForLicense(A<AuditGenericRequest>.Ignored)).WithAnyArguments().MustHaveHappened();
        }
    }
}
