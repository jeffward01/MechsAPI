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
    public class ContactRepositoryTests
    {
        [Test]
        public void AddContact_ReturnContact()
        {
            //Arrange
            var mockContactRepository = A.Fake<IContactRepository>();

            //Build expected
            Contact expected = new Contact { };

            A.CallTo(() => mockContactRepository.Add(A<Contact>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            var result = mockContactRepository.Add(A<Contact>.Ignored);

            //Assert
            A.CallTo(() => mockContactRepository.Add(A<Contact>.Ignored)).WithAnyArguments().MustHaveHappened();
            Assert.AreSame(expected, result);
        }

        [Test]
        public void GetContact_ReturnContact()
        {
            //Arrange
            var mockContactRepository = A.Fake<IContactRepository>();

            //Build expected
            Contact expected = new Contact { };

            A.CallTo(() => mockContactRepository.Get(A<int>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            var result = mockContactRepository.Get(A<int>.Ignored);

            //Assert
            A.CallTo(() => mockContactRepository.Get(A<int>.Ignored)).WithAnyArguments().MustHaveHappened();
            Assert.AreSame(expected, result);
        }

        [Test]
        public void GetContactBySafeIDString_ReturnContact()
        {
            //Arrange
            var mockContactRepository = A.Fake<IContactRepository>();

            //Build expected
            Contact expected = new Contact { };

            A.CallTo(() => mockContactRepository.Get(A<string>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            var result = mockContactRepository.Get(A<string>.Ignored);

            //Assert
            A.CallTo(() => mockContactRepository.Get(A<string>.Ignored)).WithAnyArguments().MustHaveHappened();
            Assert.AreSame(expected, result);
        }

        [Test]
        public void GetAll_ReturnListContact()
        {
            //Arrange
            var mockContactRepository = A.Fake<IContactRepository>();

            //Build expected
            List<Contact> expected = new List<Contact> { };

            A.CallTo(() => mockContactRepository.GetAll()).WithAnyArguments().Returns(expected);

            //Act
            var result = mockContactRepository.GetAll();

            //Assert
            A.CallTo(() => mockContactRepository.GetAll()).WithAnyArguments().MustHaveHappened();
            Assert.AreSame(expected, result);
        }
    }
}
