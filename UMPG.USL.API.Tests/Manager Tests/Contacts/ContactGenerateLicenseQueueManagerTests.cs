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
using UMPG.USL.API.Data.LicenseData;
using UMPG.USL.Models.LicenseGenerate;
using UMPG.USL.API.Business.Contacts;
using UMPG.USL.API.Data.ContactData;

namespace UMPG.USL.API.Tests.Manager_Tests.Contacts
{
    [TestFixture]
    public class ContactGenerateLicenseQueueManagerTests
    {
        [Test]
        public void Add_ReturnContactGeneratedLicenseQueue()
        {
            //Arrange
            var mockContactGenerateLicenseQueueRepository = A.Fake<IContactGenerateLicenseQueueRepository>();

            //Build expected
            ContactGeneratedLicenseQueue expected = new ContactGeneratedLicenseQueue { };

            A.CallTo(() => mockContactGenerateLicenseQueueRepository.Add(A<ContactGeneratedLicenseQueue>.Ignored)).Returns(expected);

            //Act
            ContactGenerateLicenseQueueManager manager = new ContactGenerateLicenseQueueManager(mockContactGenerateLicenseQueueRepository);
            var result = manager.Add(A<ContactGeneratedLicenseQueue>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }
    }
}
