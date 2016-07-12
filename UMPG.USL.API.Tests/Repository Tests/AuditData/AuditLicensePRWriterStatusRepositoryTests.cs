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

namespace UMPG.USL.API.Tests.Repository_Tests.Audit_Data
{
    [TestFixture]
    public class AuditLicensePRWriterStatusRepositoryTests
    {
        [Test]
        public void Get_ReturnAuditLicenseProductRecordingWriterRateStatus()
        {
           //Arrange
            var mockAuditLicensePRWriterStatusRepository = A.Fake<IAuditLicensePRWriterStatusRepository>();

            //Build expected
            AuditLicenseProductRecordingWriterRateStatus  expected = new AuditLicenseProductRecordingWriterRateStatus { };

            A.CallTo(() => mockAuditLicensePRWriterStatusRepository.Get(A<int>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            var returnedResult = mockAuditLicensePRWriterStatusRepository.Get(A<int>.Ignored);

            //Assert
            Assert.AreSame(expected, returnedResult);
            A.CallTo(() => mockAuditLicensePRWriterStatusRepository.Get(A<int>.Ignored)).WithAnyArguments().MustHaveHappened();
        }

        [Test]
        public void GetAuditLicenseWriterRateStatus_ReturnListAuditLicenseProductRecordingWriterRateStatus()
        {
            //Arrange
            var mockAuditLicensePRWriterStatusRepository = A.Fake<IAuditLicensePRWriterStatusRepository>();

            //Build expected
            List<AuditLicenseProductRecordingWriterRateStatus> expected = new List<AuditLicenseProductRecordingWriterRateStatus> { };

            A.CallTo(() => mockAuditLicensePRWriterStatusRepository.GetAuditLicenseWriterRateStatus(A<List<int>>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            var returnedResult = mockAuditLicensePRWriterStatusRepository.GetAuditLicenseWriterRateStatus(A<List<int>>.Ignored);

            //Assert
            Assert.AreSame(expected, returnedResult);
            A.CallTo(() => mockAuditLicensePRWriterStatusRepository.GetAuditLicenseWriterRateStatus(A<List<int>>.Ignored)).WithAnyArguments().MustHaveHappened();
        }
    }
}
