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


namespace UMPG.USL.API.Tests.Controller_Tests.License_Controller_Tests
{
    public class LicenseProductConfigurationTests
    {
        [Test]
        public void GetProducts_ReturnListLicenseProductConfiguration()
        {
            //Arrange
            var mockLicenseProfuctCOnfigurationManager = A.Fake<ILicenseProductConfigurationManager>();

            //Build expected
            List<LicenseProductConfiguration> expected = new List<LicenseProductConfiguration> { };

            A.CallTo(() => mockLicenseProfuctCOnfigurationManager.GetLicenseProductConfigurations(A<int>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            LicenseProductConfigurationController controller = new LicenseProductConfigurationController(mockLicenseProfuctCOnfigurationManager);
            var result = controller.GetProducts(A<int>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetLicenseConfigurationList_ReturnListLicenseProductConfiguration()
        {
            //Arrange
            var mockLicenseProfuctCOnfigurationManager = A.Fake<ILicenseProductConfigurationManager>();

            //Build expected
            List<LicenseProductConfiguration> expected = new List<LicenseProductConfiguration> { };

            A.CallTo(() => mockLicenseProfuctCOnfigurationManager.GetLicenseConfigurationList(A<List<int>>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            LicenseProductConfigurationController controller = new LicenseProductConfigurationController(mockLicenseProfuctCOnfigurationManager);
            var result = controller.GetLicenseConfigurationList(A<List<int>>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void DeleteLicenseProductConfiguration_ReturnUpdateLicenseProductConfigurationResultn()
        {
            //Arrange
            var mockLicenseProfuctCOnfigurationManager = A.Fake<ILicenseProductConfigurationManager>();

            //Build expected
            UpdateLicenseProductConfigurationResult expected = new UpdateLicenseProductConfigurationResult { };

            A.CallTo(() => mockLicenseProfuctCOnfigurationManager.AddLicenseProductConfiguration(A<UpdateLicenseProductConfigurationRequest>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            LicenseProductConfigurationController controller = new LicenseProductConfigurationController(mockLicenseProfuctCOnfigurationManager);
            var result = controller.AddLicenseProductConfiguration(A<UpdateLicenseProductConfigurationRequest>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void AddLicenseProductConfiguration_ReturnListUpdateLicenseProductConfigurationResult()
        {
            //Arrange
            var mockLicenseProfuctCOnfigurationManager = A.Fake<ILicenseProductConfigurationManager>();

            //Build expected
            List<UpdateLicenseProductConfigurationResult> expected = new List<UpdateLicenseProductConfigurationResult> { };

            A.CallTo(() => mockLicenseProfuctCOnfigurationManager.UpdateLicenseProductConfiguration(A<List<UpdateLicenseProductConfigurationRequest>>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            LicenseProductConfigurationController controller = new LicenseProductConfigurationController(mockLicenseProfuctCOnfigurationManager);
            var result = controller.AddLicenseProductConfiguration(A<List<UpdateLicenseProductConfigurationRequest>>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void DeleteLicenseProductConfiguration_ReturnBoolTRUE()
        {
            //Arrange
            var mockLicenseProfuctCOnfigurationManager = A.Fake<ILicenseProductConfigurationManager>();

            //Build expected
            const bool expected = true;

            A.CallTo(() => mockLicenseProfuctCOnfigurationManager.UpdateAllLicensesConfiguration(A<int>.Ignored, A<int>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            LicenseProductConfigurationController controller = new LicenseProductConfigurationController(mockLicenseProfuctCOnfigurationManager);
            var result = controller.UpdateAllLicensesConfiguration(A<int>.Ignored, A<int>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }
    }
}
