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
using UMPG.USL.API.Business.Contacts;
using UMPG.USL.Models.ContactModel;
using UMPG.USL.API.Controllers.ContactCTRL;


namespace UMPG.USL.API.Tests.Controller_Tests.Contact_Controller_Tests
{
    public class ContactDefaultControllerTests
    {
        [Test]
        public void GetAllContactDefaults_RetrunListContactDefaults()
        {
            //Arrange
            var mockContactDefaultManager = A.Fake<IContactDefaultManager>();

            //Build expected
            List<ContactDefault> expected = new List<ContactDefault> { };

            A.CallTo(() => mockContactDefaultManager.GetAll()).WithAnyArguments().Returns(expected);

            //Act
            ContactDefaultController contactDefaultController = new ContactDefaultController(mockContactDefaultManager);
            var returned = contactDefaultController.Get();

            //Assert
            Assert.AreEqual(expected, returned);
        }

        [Test]
        public void GetDefault_RetrunContactDefaults()
        {
            //Arrange
            var mockContactDefaultManager = A.Fake<IContactDefaultManager>();

            //Build expected
            ContactDefault expected = new ContactDefault { };

            A.CallTo(() => mockContactDefaultManager.Get(A<int>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            ContactDefaultController contactDefaultController = new ContactDefaultController(mockContactDefaultManager);
            var returned = contactDefaultController.GetDefault(A<int>.Ignored);

            //Assert
            Assert.AreEqual(expected, returned);
        }

        [Test]
        public void Add_RetrunContactDefaults()
        {
            //Arrange
            var mockContactDefaultManager = A.Fake<IContactDefaultManager>();

            //Build expected
            ContactDefault expected = new ContactDefault { };

            A.CallTo(() => mockContactDefaultManager.Add(A<ContactDefault>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            ContactDefaultController contactDefaultController = new ContactDefaultController(mockContactDefaultManager);
            var returned = contactDefaultController.Add(A<ContactDefault>.Ignored);

            //Assert
            Assert.AreEqual(expected, returned);
        }

        [Test]
        public void Save_RetrunContactDefaults()
        {
            //Arrange
            var mockContactDefaultManager = A.Fake<IContactDefaultManager>();

            //Build expected
            ContactDefault expected = new ContactDefault { };

            A.CallTo(() => mockContactDefaultManager.Save(A<ContactDefault>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            ContactDefaultController contactDefaultController = new ContactDefaultController(mockContactDefaultManager);
            var returned = contactDefaultController.Save(A<ContactDefault>.Ignored);

            //Assert
            Assert.AreEqual(expected, returned);
        }
    }
}
