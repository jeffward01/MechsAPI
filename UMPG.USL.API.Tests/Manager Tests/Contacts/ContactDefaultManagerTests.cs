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
using UMPG.USL.API.Data.ContactData;
using UMPG.USL.API.Business.Contacts;

namespace UMPG.USL.API.Tests.Manager_Tests.Contacts
{
    [TestFixture]
    public class ContactDefaultManagerTests
    {
        [Test]
        public void GetAllContactDefaults_ReturnListContactDefault()
        {
            //Arrange
            var mockContactDefaultRepository = A.Fake<IContactDefaultRepository>();

            //Build expected
            List<ContactDefault> expected = new List<ContactDefault> { };

            A.CallTo(() => mockContactDefaultRepository.GetAll()).Returns(expected);

            //Act
            ContactDefaultManager manager = new ContactDefaultManager(mockContactDefaultRepository);
            var result = manager.GetAll();

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetReturn_ContactDefault()
        {
            //Arrange
            var mockContactDefaultRepository = A.Fake<IContactDefaultRepository>();

            //Build expected
            ContactDefault expected = new ContactDefault { };

            A.CallTo(() => mockContactDefaultRepository.Get(A<int>.Ignored)).Returns(expected);

            //Act
            ContactDefaultManager manager = new ContactDefaultManager(mockContactDefaultRepository);
            var result = manager.Get(A<int>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void AddReturn_ContactDefault()
        {
            //Arrange
            var mockContactDefaultRepository = A.Fake<IContactDefaultRepository>();

            //Build expected
            ContactDefault expected = new ContactDefault { };

            A.CallTo(() => mockContactDefaultRepository.Add(A<ContactDefault>.Ignored)).Returns(expected);

            //Act
            ContactDefaultManager manager = new ContactDefaultManager(mockContactDefaultRepository);
            var result = manager.Add(A<ContactDefault>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void SaveReturn_ContactDefault()
        {
            //Arrange
            var mockContactDefaultRepository = A.Fake<IContactDefaultRepository>();

            //Build expected
            ContactDefault expected = new ContactDefault { };

            A.CallTo(() => mockContactDefaultRepository.Save(A<ContactDefault>.Ignored)).Returns(expected);

            //Act
            ContactDefaultManager manager = new ContactDefaultManager(mockContactDefaultRepository);
            var result = manager.Save(A<ContactDefault>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }
    }
}
