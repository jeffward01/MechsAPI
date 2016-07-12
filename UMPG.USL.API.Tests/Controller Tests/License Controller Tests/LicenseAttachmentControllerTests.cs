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


namespace UMPG.USL.API.Tests.Controller_Tests.License_Controller_Tests
{
    public class LicenseAttachmentControllerTests
    {
        [Test]
        public void GetAll_ReturnListLisenseAttachment()
        {
            //Arrange
            var mockLicenseAttachementManager = A.Fake<ILicenseAttachmentManager>();

            //Build expected
            List<LicenseAttachment> expected = new List<LicenseAttachment> { };

            A.CallTo(() => mockLicenseAttachementManager.GetAll()).Returns(expected);

            //Act
            LicenseAttachmentController licenseController = new LicenseAttachmentController(mockLicenseAttachementManager);
            var result = licenseController.Get();

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetAllAttachmentsByLicenseId_ReturnListLisenseAttachment()
        {
            //Arrange
            var mockLicenseAttachementManager = A.Fake<ILicenseAttachmentManager>();

            //Build expected
            List<LicenseAttachment> expected = new List<LicenseAttachment> { };

            A.CallTo(() => mockLicenseAttachementManager.GetAllAttachmentsByLicenseId(A<int>.Ignored)).Returns(expected);

            //Act
            LicenseAttachmentController licenseController = new LicenseAttachmentController(mockLicenseAttachementManager);
            var result = licenseController.GetAllAttachmentsByLicenseId(A<int>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }
        /*

        [Test]
        [Ignore("Need to convert AmazonS3Helper to interface to make testable")]
        public void UploadAttachmentsByLicenseId_ReturnListLisenseAttachment()
        {
            //Arrange
            var mockLicenseAttachementManager = A.Fake<ILicenseAttachmentManager>();
            var mockAmazonHelper = A.Fake<AmazonS3Helper>();

            LicenseAttachment expected = new LicenseAttachment
            {
                licenseAttachmentId = 99,
                licenseId = 99,
                fileName = "test",
                fileType = "test",
                uploaddedDate = A<DateTime>.Ignored,
                virtualFilePath = "test",
                Contact = new Contact()
            };

            const string tempUrl = "test";

            A.CallTo(() => mockAmazonHelper.GetPresignedUrl(expected.virtualFilePath, expected.fileName)).WithAnyArguments().Returns(tempUrl);
            A.CallTo(() => mockLicenseAttachementManager.Get(A<int>.Ignored)).Invokes(() => mockAmazonHelper.GetPresignedUrl(expected.virtualFilePath, expected.fileName)).Returns(expected);
         
            //Act
            LicenseAttachmentController licenseController = new LicenseAttachmentController(mockLicenseAttachementManager);
            licenseController.Request = new HttpRequestMessage();
            licenseController.Configuration = new HttpConfiguration();

            //licenseController.GetAttachmentUrl(A<int>.Ignored);

            mockAmazonHelper.GetPresignedUrl(A<string>.Ignored,A<string>.Ignored);
            //Assert
            A.CallTo(() => mockAmazonHelper.GetPresignedUrl(expected.virtualFilePath, expected.fileName)).WithAnyArguments().MustHaveHappened();
        }

         * 
        [Test]
        [Ignore("Need to convert AmazonS3Helper to interface to make testable")]
        public void UploadAttachmentsByLicenseId_ReturnHttpResponseMessage()
        {      
        }
        */
        [Test]
        public void UpdateProductConfigurationsAll_ReturnHttpResponseMessage()
        {
            //Arrange
            var mockLicenseAttachementManager = A.Fake<ILicenseAttachmentManager>();

            //Build request
            LicenseAttachment requested = new LicenseAttachment {  };

            A.CallTo(() => mockLicenseAttachementManager.RemoveLicenseAttachment(A<LicenseAttachment>.Ignored)).WithAnyArguments();

            //Act
            LicenseAttachmentController licenseController = new LicenseAttachmentController(mockLicenseAttachementManager);
            licenseController.Request = new HttpRequestMessage();
            licenseController.Configuration = new HttpConfiguration();

            var result = licenseController.UpdateProductConfigurationsAll(requested);

            //Assert
            Assert.IsInstanceOf(typeof(HttpResponseMessage), result);
            A.CallTo(() => mockLicenseAttachementManager.RemoveLicenseAttachment(A<LicenseAttachment>.Ignored)).WithAnyArguments().MustHaveHappened();
        }

        [Test]
        public void UpdateProductConfigurationsListAll_ReturnListHttpResponseMessage()
        {
            //Arrange
            var mockLicenseAttachementManager = A.Fake<ILicenseAttachmentManager>();
            
            //Build request
            List<LicenseAttachment> requested = new List<LicenseAttachment> { new LicenseAttachment(), new LicenseAttachment() };
           
            A.CallTo(() => mockLicenseAttachementManager.RemoveLicenseAttachment(A<LicenseAttachment>.Ignored)).WithAnyArguments();

             //Act
            LicenseAttachmentController licenseController = new LicenseAttachmentController(mockLicenseAttachementManager);
            licenseController.Request = new HttpRequestMessage();
            licenseController.Configuration = new HttpConfiguration();

            var result = licenseController.UpdateProductConfigurationsAll(requested);

            //Assert
            Assert.IsInstanceOf(typeof(HttpResponseMessage), result);
            A.CallTo(() => mockLicenseAttachementManager.RemoveLicenseAttachment(A<LicenseAttachment>.Ignored)).WithAnyArguments().MustHaveHappened();
        }

        [Test]
        public void Search_ReturnListLisenseAttachment()
        {
            //Arrange
            var mockLicenseAttachementManager = A.Fake<ILicenseAttachmentManager>();

            //Build expected
            List<LicenseAttachment> expected = new List<LicenseAttachment> { };

            A.CallTo(() => mockLicenseAttachementManager.Search(A<string>.Ignored)).Returns(expected);

            //Act
            LicenseAttachmentController licenseController = new LicenseAttachmentController(mockLicenseAttachementManager);
            var result = licenseController.Search(A<string>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }
    }
}

