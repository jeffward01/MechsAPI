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
    public class AuditLicenseNoteRepositoryTests
    {
        [Test]
        public void GetLicenseNote_ReturnAuditLicenseNote()
        {
            //Arrange
            var mockAuditLicenseNoteRepository = A.Fake<IAuditLicenseNoteRepository>();

            //Build expected
            AuditLicenseNote expected = new AuditLicenseNote { };

            A.CallTo(() => mockAuditLicenseNoteRepository.Get(A<int>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            var result = mockAuditLicenseNoteRepository.Get(A<int>.Ignored);

            //Assert
            Assert.AreSame(expected, result);
            A.CallTo(() => mockAuditLicenseNoteRepository.Get(A<int>.Ignored)).WithAnyArguments().MustHaveHappened();
        }



        [Test]
        public void GetAll_ReturnListAuditLicenseNote()
        {
            //Arrange
            var mockAuditLicenseNoteRepository = A.Fake<IAuditLicenseNoteRepository>();

            //Build expected
            List<AuditLicenseNote> expected = new List<AuditLicenseNote> { };

            A.CallTo(() => mockAuditLicenseNoteRepository.GetAll()).WithAnyArguments().Returns(expected);

            //Act
            var result = mockAuditLicenseNoteRepository.GetAll();

            //Assert
            Assert.AreSame(expected, result);
            A.CallTo(() => mockAuditLicenseNoteRepository.GetAll()).WithAnyArguments().MustHaveHappened();
        }
    }
}
