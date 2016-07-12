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
    public class PhoneRepositoryTests
    {
        public void AddPhone_ReturnInt()
        {
            //Arrange
            var mockPhoneRepository = A.Fake<IPhoneRepository>();
            
            //Build Expected 
            const int expected = 99;

            A.CallTo(() => mockPhoneRepository.Add(A<Phone>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            var result = mockPhoneRepository.Add(A<Phone>.Ignored);

            //Assert
            Assert.AreSame(expected, result);
            A.CallTo(() => mockPhoneRepository.Add(A<Phone>.Ignored)).WithAnyArguments().MustHaveHappened();
        }

        public void GetPhone_ReturnPhone()
        {
            //Arrange
            var mockPhoneRepository = A.Fake<IPhoneRepository>();

            //Build Expected 
            Phone expected = new Phone { };

            A.CallTo(() => mockPhoneRepository.Get(A<int>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            var result = mockPhoneRepository.Get(A<int>.Ignored);

            //Assert
            Assert.AreSame(expected, result);
            A.CallTo(() => mockPhoneRepository.Get(A<int>.Ignored)).WithAnyArguments().MustHaveHappened();
        }

        public void GetAllPhone_ReturnListPhone()
        {
            //Arrange
            var mockPhoneRepository = A.Fake<IPhoneRepository>();

            //Build Expected 
            List<Phone> expected = new List<Phone> { };

            A.CallTo(() => mockPhoneRepository.GetAll()).WithAnyArguments().Returns(expected);

            //Act
            var result = mockPhoneRepository.GetAll();

            //Assert
            Assert.AreSame(expected, result);
            A.CallTo(() => mockPhoneRepository.GetAll()).WithAnyArguments().MustHaveHappened();
        }

        public void SearchPhone_ReturnListPhone()
        {
            //Arrange
            var mockPhoneRepository = A.Fake<IPhoneRepository>();

            //Build Expected 
            List<Phone> expected = new List<Phone> { };

            A.CallTo(() => mockPhoneRepository.Search(A<string>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            var result = mockPhoneRepository.Search(A<string>.Ignored);

            //Assert
            Assert.AreSame(expected, result);
            A.CallTo(() => mockPhoneRepository.Search(A<string>.Ignored)).WithAnyArguments().MustHaveHappened();
        }

        public void UpdatePhone_ReturnListPhone()
        {
            //Arrange
            var mockPhoneRepository = A.Fake<IPhoneRepository>();
            
            A.CallTo(() => mockPhoneRepository.Update(A<Phone>.Ignored)).WithAnyArguments();

            //Act
            mockPhoneRepository.Update(A<Phone>.Ignored);

            //Assert
            A.CallTo(() => mockPhoneRepository.Update(A<Phone>.Ignored)).WithAnyArguments().MustHaveHappened();
        }
    }
}
