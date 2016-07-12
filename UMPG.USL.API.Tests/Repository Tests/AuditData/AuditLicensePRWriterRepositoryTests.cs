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
    public class AuditLicensePRWriterRepositoryTests
    {
        [Test]
        public void Get_ReturnAuditLicenseProductRecordingWriter()
        {
            //Arrange
            var mockAuditLicensePRWriterRepository = A.Fake<IAuditLicensePRWriterRepository>();

            //Build expected
            AuditLicenseProductRecordingWriter expected = new AuditLicenseProductRecordingWriter { };

            A.CallTo(() => mockAuditLicensePRWriterRepository.Get(A<int>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            var returnedResult = mockAuditLicensePRWriterRepository.Get(A<int>.Ignored);

            //Assert
            Assert.AreSame(expected, returnedResult);
            A.CallTo(() => mockAuditLicensePRWriterRepository.Get(A<int>.Ignored)).WithAnyArguments().MustHaveHappened();
        }

        [Test]
        public void GetLicenseWriters_ReturnListAuditLicenseProductRecordingWriter()
        {
            //Arrange
            var mockAuditLicensePRWriterRepository = A.Fake<IAuditLicensePRWriterRepository>();

            //Build expected
            List<AuditLicenseProductRecordingWriter> expected = new List<AuditLicenseProductRecordingWriter> { };

            A.CallTo(() => mockAuditLicensePRWriterRepository.GetLicenseWriters(A<int>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            var returnedResult = mockAuditLicensePRWriterRepository.GetLicenseWriters(A<int>.Ignored);

            //Assert
            Assert.AreSame(expected, returnedResult);
            A.CallTo(() => mockAuditLicensePRWriterRepository.GetLicenseWriters(A<int>.Ignored)).WithAnyArguments().MustHaveHappened();
        }

        [Test]
        public void GetLicenseConfigurationList_ReturnListAuditLicenseProductConfiguration()
        {
            //Arrange
            var mockAuditLicensePRWriterRepository = A.Fake<IAuditLicensePRWriterRepository>();

            //Build expected
            List<AuditLicenseProductRecordingWriter> expected = new List<AuditLicenseProductRecordingWriter> { };

            A.CallTo(() => mockAuditLicensePRWriterRepository.GetAuditLicenseWriterList(A<List<int>>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            var result = mockAuditLicensePRWriterRepository.GetAuditLicenseWriterList(A<List<int>>.Ignored);

            //Assert
            Assert.AreSame(expected, result);
            A.CallTo(() => mockAuditLicensePRWriterRepository.GetAuditLicenseWriterList(A<List<int>>.Ignored)).WithAnyArguments().MustHaveHappened();
        }

        [Test]
        public void GetAuditLicensePrWritersFromIds_ReturnListAuditLicenseProductConfiguration()
        {
            //Arrange
            var mockAuditLicensePRWriterRepository = A.Fake<IAuditLicensePRWriterRepository>();

            //Build expected
            List<AuditLicenseProductRecordingWriter> expected = new List<AuditLicenseProductRecordingWriter> { };

            A.CallTo(() => mockAuditLicensePRWriterRepository.GetAuditLicensePrWritersFromIds(A<List<int>>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            var result = mockAuditLicensePRWriterRepository.GetAuditLicensePrWritersFromIds(A<List<int>>.Ignored);

            //Assert
            Assert.AreSame(expected, result);
            A.CallTo(() => mockAuditLicensePRWriterRepository.GetAuditLicensePrWritersFromIds(A<List<int>>.Ignored)).WithAnyArguments().MustHaveHappened();
        }
    }
}
