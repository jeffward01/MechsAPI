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
    [TestFixture]
    public class ContactEmailRepositoryTests
    {
        [Test]
        public void AddContactEmail_ReturnContactEmail()
        {
            //Arrange
            var mockContactEmailRepository = A.Fake<IContactEmailRepository>();

            //Build expected
            ContactEmail expected = new ContactEmail { };

            A.CallTo(() => mockContactEmailRepository.Add(A<ContactEmail>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            var result = mockContactEmailRepository.Add(A<ContactEmail>.Ignored);

            //Assert
            Assert.AreSame(expected, result);
            A.CallTo(() => mockContactEmailRepository.Add(A<ContactEmail>.Ignored)).WithAnyArguments().MustHaveHappened();
        }

        [Test]
        public void GetContactEmail_ReturnContact()
        {
            //Arrange
            var mockContactEmailRepository = A.Fake<IContactEmailRepository>();

            //Build expected
            Contact expected = new Contact { };

            A.CallTo(() => mockContactEmailRepository.Get(A<string>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            var result = mockContactEmailRepository.Get(A<string>.Ignored);

            //Assert
            Assert.AreSame(expected, result);
            A.CallTo(() => mockContactEmailRepository.Get(A<string>.Ignored)).WithAnyArguments().MustHaveHappened();
        }

        [Test]
        public void GetContactEmail_ReturnContactEmail()
        {
            //Arrange
            var mockContactEmailRepository = A.Fake<IContactEmailRepository>();

            //Build expected
            ContactEmail expected = new ContactEmail { };

            A.CallTo(() => mockContactEmailRepository.GetContactEmail(A<int>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            var result = mockContactEmailRepository.GetContactEmail(A<int>.Ignored);

            //Assert
            Assert.AreSame(expected, result);
            A.CallTo(() => mockContactEmailRepository.GetContactEmail(A<int>.Ignored)).WithAnyArguments().MustHaveHappened();
        }

        [Test]
        public void UpdateContactEmail_ReturnVoid()
        {
            //Arrange
            var mockContactEmailRepository = A.Fake<IContactEmailRepository>();

            //Build expected
            ContactEmail expected = new ContactEmail { };

            A.CallTo(() => mockContactEmailRepository.Update(A<ContactEmail>.Ignored)).WithAnyArguments();

            //Act
            mockContactEmailRepository.Update(A<ContactEmail>.Ignored);

            //Assert
            A.CallTo(() => mockContactEmailRepository.Update(A<ContactEmail>.Ignored)).WithAnyArguments().MustHaveHappened();
        }
    }
}
