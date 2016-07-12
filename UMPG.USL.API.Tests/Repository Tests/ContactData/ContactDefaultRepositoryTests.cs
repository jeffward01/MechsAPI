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
    public class ContactDefaultRepositoryTests
    {
        [Test]
        public void SaveContactDefault_ReturnContactDefault()
        {
            //Arrange
            var mockContactDefaultReposiotry = A.Fake<IContactDefaultRepository>();

            //Build expected
            ContactDefault expected = new ContactDefault { };

            A.CallTo(() => mockContactDefaultReposiotry.Save(A<ContactDefault>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            var result = mockContactDefaultReposiotry.Save(A<ContactDefault>.Ignored);

            //Assert
            A.CallTo(() => mockContactDefaultReposiotry.Save(A<ContactDefault>.Ignored)).WithAnyArguments().MustHaveHappened();
            Assert.AreSame(expected, result);
        }

        [Test]
        public void AddContactDefault_ReturnContactDefault()
        {
            //Arrange
            var mockContactDefaultReposiotry = A.Fake<IContactDefaultRepository>();

            //Build expected
            ContactDefault expected = new ContactDefault { };

            A.CallTo(() => mockContactDefaultReposiotry.Add(A<ContactDefault>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            var result = mockContactDefaultReposiotry.Add(A<ContactDefault>.Ignored);

            //Assert
            A.CallTo(() => mockContactDefaultReposiotry.Add(A<ContactDefault>.Ignored)).WithAnyArguments().MustHaveHappened();
            Assert.AreSame(expected, result);
        }


        [Test]
        public void GetContactDefault_ReturnInt()
        {
            //Arrange
            var mockContactDefaultReposiotry = A.Fake<IContactDefaultRepository>();

            //Build expected
            ContactDefault expected = new ContactDefault { };

            A.CallTo(() => mockContactDefaultReposiotry.Get(A<int>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            var result = mockContactDefaultReposiotry.Get(A<int>.Ignored);

            //Assert
            A.CallTo(() => mockContactDefaultReposiotry.Get(A<int>.Ignored)).WithAnyArguments().MustHaveHappened();
            Assert.AreSame(expected, result);
        }
        
        [Test]
        public void GetContactDefault_ReturnListContactDefault()
        {
            //Arrange
            var mockContactDefaultReposiotry = A.Fake<IContactDefaultRepository>();

            //Build expected
            List<ContactDefault> expected = new List<ContactDefault> { };

            A.CallTo(() => mockContactDefaultReposiotry.GetAll()).WithAnyArguments().Returns(expected);

            //Act
            var result = mockContactDefaultReposiotry.GetAll();

            //Assert
            A.CallTo(() => mockContactDefaultReposiotry.GetAll()).WithAnyArguments().MustHaveHappened();
            Assert.AreSame(expected, result);
        }










    }
}
