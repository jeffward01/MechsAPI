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
using UMPG.USL.API.Data.ContactData;

namespace UMPG.USL.API.Tests.Manager_Tests.Licenses
{
    [TestFixture]
    public class LicenseeManagerTests
    {
        [Test]
        public void GetLicensee_ReturnLicensee()
        {
            //Arrange
            var mockLicenseeRepository = A.Fake<ILicenseeRepository>();
            var mockContactRepository = A.Fake<IContactRepository>();
            var mockAddressRepository = A.Fake<IAddressRepository>();

            //Build Expected
            Licensee expected =new Licensee{};
            A.CallTo(() => mockLicenseeRepository.Get(A<int>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            LicenseeManager manager = new LicenseeManager(mockLicenseeRepository, mockContactRepository, mockAddressRepository);
            var result = manager.Get(A<int>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetAllLicensee_ReturnListLicensee()
        {
            //Arrange
            var mockLicenseeRepository = A.Fake<ILicenseeRepository>();
            var mockContactRepository = A.Fake<IContactRepository>();
            var mockAddressRepository = A.Fake<IAddressRepository>();

            //Build Expected
            List<Licensee> expected = new List<Licensee> { };

            A.CallTo(() => mockLicenseeRepository.GetAll()).WithAnyArguments().Returns(expected);

            //Act
            LicenseeManager manager = new LicenseeManager(mockLicenseeRepository, mockContactRepository, mockAddressRepository);
            var result = manager.GetAll();

            //Assert
            Assert.AreEqual(expected, result);
        }


        [Test]
        public void Search_ReturnListLicensee()
        {
            //Arrange
            var mockLicenseeRepository = A.Fake<ILicenseeRepository>();
            var mockContactRepository = A.Fake<IContactRepository>();
            var mockAddressRepository = A.Fake<IAddressRepository>();

            //Build Expected
            List<Licensee> expected = new List<Licensee> { };

            A.CallTo(() => mockLicenseeRepository.Search(A<string>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            LicenseeManager manager = new LicenseeManager(mockLicenseeRepository, mockContactRepository, mockAddressRepository);
            var result = manager.Search(A<string>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetLicensees_ReturnPagedResponseLicensee()
        {
            //Arrange
            var mockLicenseeRepository = A.Fake<ILicenseeRepository>();
            var mockContactRepository = A.Fake<IContactRepository>();
            var mockAddressRepository = A.Fake<IAddressRepository>();

            //Build Expected
            PagedResponse<Licensee> expected = new PagedResponse<Licensee> { };

            A.CallTo(() => mockLicenseeRepository.GetLicensees(A<LicenseeAdminRequest>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            LicenseeManager manager = new LicenseeManager(mockLicenseeRepository, mockContactRepository, mockAddressRepository);
            var result = manager.GetLicensees(A<LicenseeAdminRequest>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void AddLicensee_ReturnLicensee()
        {
            //Arrange
            var mockLicenseeRepository = A.Fake<ILicenseeRepository>();
            var mockContactRepository = A.Fake<IContactRepository>();
            var mockAddressRepository = A.Fake<IAddressRepository>();

            //Build Expected
            Licensee expected = new Licensee { };

            //Build Request
            AddLicenseeRequest request = new AddLicenseeRequest
            {
                Name = "Test",
                CreatedBy = 99,
            };

            A.CallTo(() => mockLicenseeRepository.Add(A<Licensee>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            LicenseeManager manager = new LicenseeManager(mockLicenseeRepository, mockContactRepository, mockAddressRepository);
            var result = manager.AddLicensee(request);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void EditLicensee_ReturnLicensee()
        {
            //Arrange
            var mockLicenseeRepository = A.Fake<ILicenseeRepository>();
            var mockContactRepository = A.Fake<IContactRepository>();
            var mockAddressRepository = A.Fake<IAddressRepository>();

            //Build Expected
            Licensee expected = new Licensee { };

            //Build Request
            Licensee request = new Licensee
            {
                Name = "Test",
                CreatedBy = 99,
            };

            A.CallTo(() => mockLicenseeRepository.EditLicensee(A<Licensee>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            LicenseeManager manager = new LicenseeManager(mockLicenseeRepository, mockContactRepository, mockAddressRepository);
            var result = manager.EditLicensee(request);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void DeleteLicensee_ReturnBoolTRUE()
        {
            //Arrange
            var mockLicenseeRepository = A.Fake<ILicenseeRepository>();
            var mockContactRepository = A.Fake<IContactRepository>();
            var mockAddressRepository = A.Fake<IAddressRepository>();

            List<Contact> contact = new List<Contact> { };

            //Build Expected
            Licensee expected = new Licensee { };
            const bool expected1 = true;

            //Build Request
            List<LicenseeLabelGroup> labelGroup = new List<LicenseeLabelGroup>{};

            Licensee request = new Licensee
            {
                Name = "Test",
                CreatedBy = 99,
                LicenseeContactsFiltered = contact,
                LicenseeLabelGroup = labelGroup
            };

            A.CallTo(() => mockLicenseeRepository.EditLicensee(A<Licensee>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            LicenseeManager manager = new LicenseeManager(mockLicenseeRepository, mockContactRepository, mockAddressRepository);
            var result = manager.DeleteLicensee(request);

            //Assert
            Assert.AreEqual(expected1, result);
        }
    }
}
