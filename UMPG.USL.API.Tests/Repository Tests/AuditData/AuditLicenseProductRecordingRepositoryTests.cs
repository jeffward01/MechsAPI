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
    public class AuditLicenseProductRecordingRepositoryTests
    {
        [Test]
        public void GetAuditLicenseProductRecordingsBrief_ReturnListAuditLicenseProductRecordingt()
        {
            //Arrange
            var mockAuditLicenseProductRecordingRepository = A.Fake<IAuditLicenseProductRecordingRepository>();

            //Build expected
            List<AuditLicenseProductRecording> expected = new List<AuditLicenseProductRecording> { };

            A.CallTo(() => mockAuditLicenseProductRecordingRepository.GetAuditLicenseProductRecordingsBrief(A<int>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            var returnedResult = mockAuditLicenseProductRecordingRepository.GetAuditLicenseProductRecordingsBrief(A<int>.Ignored);

            //Assert
            Assert.AreSame(expected, returnedResult);
            A.CallTo(() => mockAuditLicenseProductRecordingRepository.GetAuditLicenseProductRecordingsBrief(A<int>.Ignored)).WithAnyArguments().MustHaveHappened();
        }

        [Test]
        public void GetAuditLicenseRecordingsList_ReturnListAuditLicenseProductRecordingt()
        {
            //Arrange
            var mockAuditLicenseProductRecordingRepository = A.Fake<IAuditLicenseProductRecordingRepository>();

            //Build expected
            List<AuditLicenseProductRecording> expected = new List<AuditLicenseProductRecording> { };

            A.CallTo(() => mockAuditLicenseProductRecordingRepository.GetAuditLicenseRecordingsList(A<List<int>>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            var returnedResult = mockAuditLicenseProductRecordingRepository.GetAuditLicenseRecordingsList(A<List<int>>.Ignored);

            //Assert
            Assert.AreSame(expected, returnedResult);
            A.CallTo(() => mockAuditLicenseProductRecordingRepository.GetAuditLicenseRecordingsList(A<List<int>>.Ignored)).WithAnyArguments().MustHaveHappened();
        }

        [Test]
        public void GetAuditLicenseProductRecordingsFromList_ReturnListAuditLicenseProductRecordingt()
        {
            //Arrange
            var mockAuditLicenseProductRecordingRepository = A.Fake<IAuditLicenseProductRecordingRepository>();

            //Build expected
            List<AuditLicenseProductRecording> expected = new List<AuditLicenseProductRecording> { };

            A.CallTo(() => mockAuditLicenseProductRecordingRepository.GetAuditLicenseProductRecordingsFromList(A<List<int>>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            var returnedResult = mockAuditLicenseProductRecordingRepository.GetAuditLicenseProductRecordingsFromList(A<List<int>>.Ignored);

            //Assert
            Assert.AreSame(expected, returnedResult);
            A.CallTo(() => mockAuditLicenseProductRecordingRepository.GetAuditLicenseProductRecordingsFromList(A<List<int>>.Ignored)).WithAnyArguments().MustHaveHappened();
        }
    }
}
