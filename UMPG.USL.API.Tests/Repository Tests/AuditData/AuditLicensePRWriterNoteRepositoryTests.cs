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
   public class AuditLicensePRWriterNoteRepositoryTests
    {
        [Test]
        public void GetAuditLicenseProductRecordingWriterNote_ReturnAuditLicenseProductRecordingWriterNote()
        {
            //Arrange
            var mockAuditLicensePRWriterNoteRepository = A.Fake<IAuditLicensePRWriterNoteRepository>();

            //Build expected
            AuditLicenseProductRecordingWriterNote expected = new AuditLicenseProductRecordingWriterNote { };

            A.CallTo(() => mockAuditLicensePRWriterNoteRepository.Get(A<int>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            var returnedResult = mockAuditLicensePRWriterNoteRepository.Get(A<int>.Ignored);

            //Assert
            Assert.AreSame(expected, returnedResult);
            A.CallTo(() => mockAuditLicensePRWriterNoteRepository.Get(A<int>.Ignored)).WithAnyArguments().MustHaveHappened();
        }

        [Test]
        public void GetAllAuditLicenseProductRecordingWriterNote_ReturnListAuditLicenseProductRecordingWriterNote()
        {
            //Arrange
            var mockAuditLicensePRWriterNoteRepository = A.Fake<IAuditLicensePRWriterNoteRepository>();

            //Build expected
            List<AuditLicenseProductRecordingWriterNote> expected = new List<AuditLicenseProductRecordingWriterNote> { };

            A.CallTo(() => mockAuditLicensePRWriterNoteRepository.GetAll(A<int>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            var returnedResult = mockAuditLicensePRWriterNoteRepository.GetAll(A<int>.Ignored);

            //Assert
            Assert.AreSame(expected, returnedResult);
            A.CallTo(() => mockAuditLicensePRWriterNoteRepository.GetAll(A<int>.Ignored)).WithAnyArguments().MustHaveHappened();
        }

          [Test]
        public void GetAuditLicenseProductRecordingWriterNotes_ReturnListAuditLicenseProductRecordingWriterNote()
        {
            //Arrange
            var mockAuditLicensePRWriterNoteRepository = A.Fake<IAuditLicensePRWriterNoteRepository>();

            //Build expected
            List<AuditLicenseProductRecordingWriterNote> expected = new List<AuditLicenseProductRecordingWriterNote> { };

            A.CallTo(() => mockAuditLicensePRWriterNoteRepository.GetAuditLicenseProductRecordingWriterNotes(A<List<int>>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            var returnedResult = mockAuditLicensePRWriterNoteRepository.GetAuditLicenseProductRecordingWriterNotes(A<List<int>>.Ignored);

            //Assert
            Assert.AreSame(expected, returnedResult);
            A.CallTo(() => mockAuditLicensePRWriterNoteRepository.GetAuditLicenseProductRecordingWriterNotes(A<List<int>>.Ignored)).WithAnyArguments().MustHaveHappened();
        }
    }
}
