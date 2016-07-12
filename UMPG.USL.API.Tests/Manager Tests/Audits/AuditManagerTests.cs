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

namespace UMPG.USL.API.Tests.Manager_Tests.Audits
{
    [TestFixture]
    public class AuditManagerTests
    {
        [Test]
        public void GetAuditForLicense_ReturnListAuditLicenseProcedureResult()
        {
            //Arrange
            var mockAuditLicenseRepository = A.Fake<IAuditLicenseRepository>();
            var mockAuditlicenseProductRepository = A.Fake<IAuditLicenseProductRepository>();

            //Build Expected
            List<AuditLicenseProcedureResult> expected = new List<AuditLicenseProcedureResult> { };

            A.CallTo(() => mockAuditLicenseRepository.GetAuditForLicense(A<AuditGenericRequest>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            AuditManager manager = new AuditManager(mockAuditLicenseRepository, mockAuditlicenseProductRepository);
            var result = manager.GetAuditForLicense(A<AuditGenericRequest>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);

        }
    }
}
