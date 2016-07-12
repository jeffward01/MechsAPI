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

namespace UMPG.USL.API.Tests.Controller_Tests.LookUp_Controller_Tests
{
    [TestFixture]
    public class LicenseMethodsControllerTests
    {
        [Test]
        public void GetAllLULicenseMethods_ReturnListLULicenseMethods()
        {
            //Arrange 
            var mockLicenseMethodManager = A.Fake<ILicenseMethodManager>();

            //Build expected
            List<LU_LicenseMethod> expected = new List<LU_LicenseMethod> { };

            A.CallTo(() => mockLicenseMethodManager.GetAll()).Returns(expected);

            //Act
            LicenseMethodsController controller = new LicenseMethodsController(mockLicenseMethodManager);
            var result = controller.Get();

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetLicenseMethod_ReturnLULicenseMethods()
        {
            //Arrange 
            var mockLicenseMethodManager = A.Fake<ILicenseMethodManager>();

            //Build expected
            LU_LicenseMethod expected = new LU_LicenseMethod { };

            A.CallTo(() => mockLicenseMethodManager.Get(A<int>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            LicenseMethodsController controller = new LicenseMethodsController(mockLicenseMethodManager);
            var result = controller.GetLicenseMethod(A<int>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void SearchLicenseMethods_ReturnListLULicenseMethods()
        {
            //Arrange 
            var mockLicenseMethodManager = A.Fake<ILicenseMethodManager>();

            //Build expected
            List<LU_LicenseMethod> expected = new List<LU_LicenseMethod> { };

            A.CallTo(() => mockLicenseMethodManager.Search(A<string>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            LicenseMethodsController controller = new LicenseMethodsController(mockLicenseMethodManager);
            var result = controller.Search(A<string>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }
    }
}
