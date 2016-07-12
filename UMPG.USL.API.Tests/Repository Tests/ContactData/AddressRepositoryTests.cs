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
    public class AddressRepositoryTests
    {
        [Test]
        public void AddAddress_ReturnInt()
        {
            //Arrange
            var mockAddressRepository = A.Fake<IAddressRepository>();

            //Build expected
            int expected = 99;

            A.CallTo(() => mockAddressRepository.Add(A<Address>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            var result = mockAddressRepository.Add(A<Address>.Ignored);

            //Assert
            Assert.AreEqual(99, result);
            A.CallTo(() => mockAddressRepository.Add(A<Address>.Ignored)).WithAnyArguments().MustHaveHappened();
        }

        [Test]
        public void GetAddress_ReturnAddress()
        {
            //Arrange
            var mockAddressRepository = A.Fake<IAddressRepository>();

            //Build expected
            Address expected = new Address { };

            A.CallTo(() => mockAddressRepository.Get(A<int>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            var result = mockAddressRepository.Get(A<int>.Ignored);

            //Assert
            Assert.AreSame(expected, result);
            A.CallTo(() => mockAddressRepository.Get(A<int>.Ignored)).WithAnyArguments().MustHaveHappened();
        }

        [Test]
        public void GetAllAddress_ReturnListAddress()
        {
            //Arrange
            var mockAddressRepository = A.Fake<IAddressRepository>();

            //Build expected
            List<Address> expected = new List<Address> { };

            A.CallTo(() => mockAddressRepository.GetAll()).WithAnyArguments().Returns(expected);

            //Act
            var result = mockAddressRepository.GetAll();

            //Assert
            Assert.AreSame(expected, result);
            A.CallTo(() => mockAddressRepository.GetAll()).WithAnyArguments().MustHaveHappened();
        }

        [Test]
        public void Search_ReturnListAddress()
        {
            //Arrange
            var mockAddressRepository = A.Fake<IAddressRepository>();

            //Build expected
            List<Address> expected = new List<Address> { };

            A.CallTo(() => mockAddressRepository.Search(A<string>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            var result = mockAddressRepository.Search(A<string>.Ignored);

            //Assert
            Assert.AreSame(expected, result);
            A.CallTo(() => mockAddressRepository.Search(A<string>.Ignored)).WithAnyArguments().MustHaveHappened();
        }

        [Test]
        public void GetAddressesForLicensee_ReturnListAddress()
        {
            //Arrange
            var mockAddressRepository = A.Fake<IAddressRepository>();

            //Build expected
            List<Address> expected = new List<Address> { };

            A.CallTo(() => mockAddressRepository.GetAddressesForLicensee(A<int>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            var result = mockAddressRepository.GetAddressesForLicensee(A<int>.Ignored);

            //Assert
            Assert.AreSame(expected, result);
            A.CallTo(() => mockAddressRepository.GetAddressesForLicensee(A<int>.Ignored)).WithAnyArguments().MustHaveHappened();
        }


        [Test]
        public void UpdateAddress_ReturnVoid()
        {
            //Arrange
            var mockAddressRepository = A.Fake<IAddressRepository>();
            
            A.CallTo(() => mockAddressRepository.Update(A<Address>.Ignored)).WithAnyArguments();

            //Act
            mockAddressRepository.Update(A<Address>.Ignored);

            //Assert
            A.CallTo(() => mockAddressRepository.Update(A<Address>.Ignored)).WithAnyArguments().MustHaveHappened();
        }
    }
}
