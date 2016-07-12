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
using UMPG.USL.API.Data.ContactData;


namespace UMPG.USL.API.Tests.Controller_Tests.License_Controller_Tests
{
   public class LicenseesControllerTests
    {
        [Test]
       public void GetAll_ReturnListLicensee()
        {
            //Arrange
            var mockLicenseeManager = A.Fake<ILicenseeManager>();

            //Build Expected
            List<Licensee> expected = new List<Licensee> {};
            A.CallTo(() => mockLicenseeManager.GetAll()).Returns(expected);

            //Act
            LicenseeController controller = new LicenseeController(mockLicenseeManager);
            var result = controller.Get();

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Search_ReturnListLicensee()
        {
            //Arrange
            var mockLicenseeManager = A.Fake<ILicenseeManager>();

            //Build Expected
            List<Licensee> expected = new List<Licensee> { };
            A.CallTo(() => mockLicenseeManager.Search(A<string>.Ignored)).Returns(expected);

            //Act
            LicenseeController controller = new LicenseeController(mockLicenseeManager);
            var result = controller.Search(A<string>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetLicensees_ReturnPagedResponseLicensee()
        {
            //Arrange
            var mockLicenseeManager = A.Fake<ILicenseeManager>();

            //Build Expected
            PagedResponse<Licensee> expected = new PagedResponse<Licensee> { };
            A.CallTo(() => mockLicenseeManager.GetLicensees(A<LicenseeAdminRequest>.Ignored)).Returns(expected);

            //Act
            LicenseeController controller = new LicenseeController(mockLicenseeManager);
            var result = controller.GetLicensees(A<LicenseeAdminRequest>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void AddLicensee_ReturnLicensee()
        {
            //Arrange
            var mockLicenseeManager = A.Fake<ILicenseeManager>();

            //Build Expected
            Licensee expected = new Licensee { };
            A.CallTo(() => mockLicenseeManager.AddLicensee(A<AddLicenseeRequest>.Ignored)).Returns(expected);

            //Act
            LicenseeController controller = new LicenseeController(mockLicenseeManager);
            var result = controller.AddLicensee(A<AddLicenseeRequest>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void EditLicensee_ReturnLicensee()
        {
            //Arrange
            var mockLicenseeManager = A.Fake<ILicenseeManager>();

            //Build Expected
            Licensee expected = new Licensee { };
            A.CallTo(() => mockLicenseeManager.EditLicensee(A<Licensee>.Ignored)).Returns(expected);

            //Act
            LicenseeController controller = new LicenseeController(mockLicenseeManager);
            var result = controller.AddLicensee(A<Licensee>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void DeleteLicensee_ReturnLicensee()
        {
            //Arrange
            var mockLicenseeManager = A.Fake<ILicenseeManager>();

            //Build Expected
            const bool expected = true;

            A.CallTo(() => mockLicenseeManager.DeleteLicensee(A<Licensee>.Ignored)).Returns(expected);

            //Act
            LicenseeController controller = new LicenseeController(mockLicenseeManager);
            var result = controller.DeleteLicensee(A<Licensee>.Ignored);

            //Assert
            Assert.IsTrue(result);
        }
    }
}
