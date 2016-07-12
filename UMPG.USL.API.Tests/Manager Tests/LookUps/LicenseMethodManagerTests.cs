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
using UMPG.USL.API.Business.Lookups;
using UMPG.USL.Models.LookupModel;
using UMPG.USL.API.Data.LookupData;

namespace UMPG.USL.API.Tests.Manager_Tests.LookUps
{
    [TestFixture]
    public class LicenseMethodManagerTests
    {
        [Test]
        public void Get_ReturnLicenseMethod()
        {
            //Arrange
            var mockILicenseMethodRepository = A.Fake<ILicenseMethodRepository>();

            //Build expected
            LU_LicenseMethod expected = new LU_LicenseMethod { };

            A.CallTo(() => mockILicenseMethodRepository.Get(A<int>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            LicenseMethodManager manager = new LicenseMethodManager(mockILicenseMethodRepository);
            var result = manager.Get(A<int>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetAll_ReturnListLicenseMethod()
        {
            //Arrange
            var mockILicenseMethodRepository = A.Fake<ILicenseMethodRepository>();

            //Build expected
            List<LU_LicenseMethod> expected = new List<LU_LicenseMethod> { };

            A.CallTo(() => mockILicenseMethodRepository.GetAll()).WithAnyArguments().Returns(expected);

            //Act
            LicenseMethodManager manager = new LicenseMethodManager(mockILicenseMethodRepository);
            var result = manager.GetAll();

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Search_ReturnLicenseMethod()
        {
            //Arrange
            var mockILicenseMethodRepository = A.Fake<ILicenseMethodRepository>();

            //Build expected
            List<LU_LicenseMethod> expected = new List<LU_LicenseMethod> { };

            A.CallTo(() => mockILicenseMethodRepository.Search(A<string>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            LicenseMethodManager manager = new LicenseMethodManager(mockILicenseMethodRepository);
            var result = manager.Search(A<string>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }
    }
}
