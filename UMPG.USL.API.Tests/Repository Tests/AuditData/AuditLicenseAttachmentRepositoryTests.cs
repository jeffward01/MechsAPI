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
    public class AuditLicenseAttachmentRepositoryTests
    {
        [Test]
        public void GetAuditLicenseAttachement_ReturnAuditLicenseAttachment()
        {
            //Arrange
            var mockAuditLicenseAttachment = A.Fake<IAuditLicenseAttachmentRepository>();

            //Build expected
            AuditLicenseAttachment expected = new AuditLicenseAttachment { };
            
            A.CallTo(() => mockAuditLicenseAttachment.Get(A<int>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            var returnedResult = mockAuditLicenseAttachment.Get(A<int>.Ignored);
       
            //Assert
            Assert.AreSame(expected, returnedResult);
            A.CallTo(() => mockAuditLicenseAttachment.Get(A<int>.Ignored)).WithAnyArguments().MustHaveHappened();
        }

        [Test]
        public void GetByLicenseId_ReturnAuditLicenseAttachment()
        {
            //Arrange
            var mockAuditLicenseAttachment = A.Fake<IAuditLicenseAttachmentRepository>();

            //Build expected
            AuditLicenseAttachment expected = new AuditLicenseAttachment { };

            A.CallTo(() => mockAuditLicenseAttachment.GetByLicenseId(A<string>.Ignored, A<int>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            var returnedResult = mockAuditLicenseAttachment.GetByLicenseId(A<string>.Ignored, A<int>.Ignored);

            //Assert
            Assert.AreSame(expected, returnedResult);
            A.CallTo(() => mockAuditLicenseAttachment.GetByLicenseId(A<string>.Ignored, A<int>.Ignored)).WithAnyArguments().MustHaveHappened();
        }

        [Test]
        public void Search_ReturnAuditLicenseAttachment()
        {
            //Arrange
            var mockAuditLicenseAttachment = A.Fake<IAuditLicenseAttachmentRepository>();

            //Build expected
            List<AuditLicenseAttachment> expected = new List<AuditLicenseAttachment> { };

            A.CallTo(() => mockAuditLicenseAttachment.Search(A<string>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            var returnedResult = mockAuditLicenseAttachment.Search(A<string>.Ignored);

            //Assert
            Assert.AreSame(expected, returnedResult);
            A.CallTo(() => mockAuditLicenseAttachment.Search(A<string>.Ignored)).WithAnyArguments().MustHaveHappened();
        }
    }
}
