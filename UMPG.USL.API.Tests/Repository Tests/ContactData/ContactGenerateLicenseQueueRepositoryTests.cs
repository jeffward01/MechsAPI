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
using UMPG.USL.API.Data.ContactData;
using UMPG.USL.Models.ContactModel;

namespace UMPG.USL.API.Tests.Repository_Tests.ContactData
{
    public class ContactGenerateLicenseQueueRepositoryTests
    {
        [Test]
        public void GetContactEmail_ReturnContactEmail()
        {
            //Arrange
            var mockContactGenerateLicenseQueueRepository = A.Fake<IContactGenerateLicenseQueueRepository>();

            //Build expected
            ContactGeneratedLicenseQueue expected = new ContactGeneratedLicenseQueue { };

            A.CallTo(() => mockContactGenerateLicenseQueueRepository.Add(A<ContactGeneratedLicenseQueue>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            var result = mockContactGenerateLicenseQueueRepository.Add(A<ContactGeneratedLicenseQueue>.Ignored);

            //Assert
            Assert.AreSame(expected, result);
            A.CallTo(() => mockContactGenerateLicenseQueueRepository.Add(A<ContactGeneratedLicenseQueue>.Ignored)).WithAnyArguments().MustHaveHappened();
        }
    }
}
