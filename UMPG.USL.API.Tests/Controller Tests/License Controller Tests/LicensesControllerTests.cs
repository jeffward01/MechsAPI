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
using UMPG.USL.API.Business.Contacts;
using UMPG.USL.Models.LicenseModel;
using UMPG.USL.API.Controllers.LicenseCTRL;
using UMPG.USL.Models.LicenseGenerate;
using UMPG.USL.Models.ContactModel;
using System.IO;
using UMPG.USL.Common;
using System.Net.Http;
using System.Web.Http.Hosting;
using System.Web.Http;
using System.Web;


namespace UMPG.USL.API.Tests.Controller_Tests.License_Controller_Tests
{
   public class LicensesControllerTests
    {
        [Test]
        public void Get_ReturnListLisence()
        {
            //Arrange
            var mockLicenseMangager = A.Fake<ILicenseManager>();
            var mockContactMangager = A.Fake<IContactManager>();
            var mockCOntactGenerateLicenseQueueManager = A.Fake<IContactGenerateLicenseQueueManager>();
            var mockGenerateLicenseManager = A.Fake<IGenerateLicenseManager>();

            //Build expected
            List<License> expected = new List<License> { };

            A.CallTo(() => mockLicenseMangager.GetAll()).Returns(expected);

            //Act
            LicenseController controller = new LicenseController(mockLicenseMangager, mockContactMangager, mockCOntactGenerateLicenseQueueManager, mockGenerateLicenseManager);
            var result = controller.Get();

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Search_ReturnListLisence()
        {
            //Arrange
            var mockLicenseMangager = A.Fake<ILicenseManager>();
            var mockContactMangager = A.Fake<IContactManager>();
            var mockCOntactGenerateLicenseQueueManager = A.Fake<IContactGenerateLicenseQueueManager>();
            var mockGenerateLicenseManager = A.Fake<IGenerateLicenseManager>();

            //Build expected
            List<License> expected = new List<License> { };

            A.CallTo(() => mockLicenseMangager.Search(A<string>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            LicenseController controller = new LicenseController(mockLicenseMangager, mockContactMangager, mockCOntactGenerateLicenseQueueManager, mockGenerateLicenseManager);
            var result = controller.Search(A<string>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void PagedSearch_ReturnPagedResponseLisence()
        {
            //Arrange
            var mockLicenseMangager = A.Fake<ILicenseManager>();
            var mockContactMangager = A.Fake<IContactManager>();
            var mockCOntactGenerateLicenseQueueManager = A.Fake<IContactGenerateLicenseQueueManager>();
            var mockGenerateLicenseManager = A.Fake<IGenerateLicenseManager>();

            //Build expected
            PagedResponse<License> expected = new PagedResponse<License> { };

            A.CallTo(() => mockLicenseMangager.PagedSearch(A<LicenseRequest>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            LicenseController controller = new LicenseController(mockLicenseMangager, mockContactMangager, mockCOntactGenerateLicenseQueueManager, mockGenerateLicenseManager);
            var result = controller.PagedSearch(A<LicenseRequest>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void AddLicense_ReturnLisence()
        {
            //Arrange
            var mockLicenseMangager = A.Fake<ILicenseManager>();
            var mockContactMangager = A.Fake<IContactManager>();
            var mockCOntactGenerateLicenseQueueManager = A.Fake<IContactGenerateLicenseQueueManager>();
            var mockGenerateLicenseManager = A.Fake<IGenerateLicenseManager>();

            //Build expected
            License expected = new License { };

            A.CallTo(() => mockLicenseMangager.Add(A<License>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            LicenseController controller = new LicenseController(mockLicenseMangager, mockContactMangager, mockCOntactGenerateLicenseQueueManager, mockGenerateLicenseManager);
            var result = controller.Add(A<License>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

       [Test]
       public void UploadGeneratedLicensePreview_ReturnBoolTRUE()
        {
           //Arrange
            var mockLicenseMangager = A.Fake<ILicenseManager>();
            var mockContactMangager = A.Fake<IContactManager>();
            var mockCOntactGenerateLicenseQueueManager = A.Fake<IContactGenerateLicenseQueueManager>();
            var mockGenerateLicenseManager = A.Fake<IGenerateLicenseManager>();

           //Build request
            UploadGeneratedLicensePreviewRequest request = new UploadGeneratedLicensePreviewRequest
            {
                htmlText = "test",
                licenseId = 999,
                fileName = "test",
                Subject = "test",
                Content = "test",
                fromContactId = 99
            };

           //Build expected Outputs 
            var amazonHelper = new AmazonS3Helper();

            byte[] info = new UTF8Encoding(true).GetBytes(request.htmlText);

            Stream stream = new MemoryStream(info);

            
            ContactEmail contactEmail = new ContactEmail { ContactEmailId = 999, ContactId = 999, EmailAddress = "test@test.com"};

            SendLicenseInfo sendLicenseInfo = new SendLicenseInfo { };

            GenerateLicensePreviewRequest dataRequest = new GenerateLicensePreviewRequest(request);

            ContactGeneratedLicenseQueue contactQueue = new ContactGeneratedLicenseQueue { };
           
           //Configure Calls
            A.CallTo(() => mockContactMangager.GetContactEmail(A<int>.Ignored)).WithAnyArguments().Returns(contactEmail);
            A.CallTo(() => mockLicenseMangager.GetSendLicenseInfo(A<int>.Ignored)).WithAnyArguments().Returns(sendLicenseInfo);
           
           //Act
            LicenseController controller = new LicenseController(mockLicenseMangager, mockContactMangager, mockCOntactGenerateLicenseQueueManager, mockGenerateLicenseManager);
          //  var result = controller.UploadGeneratedLicensePreview(request);
            mockLicenseMangager.GetSendLicenseInfo(A<int>.Ignored);
           //Assert
            A.CallTo(() => mockLicenseMangager.GetSendLicenseInfo(A<int>.Ignored)).WithAnyArguments().MustHaveHappened();
        }


       [Test]
       public void UpdateGeneratedLicenseStatus_ReturnBoolTRUE()
       {
           //Arrange
           var mockLicenseMangager = A.Fake<ILicenseManager>();
           var mockContactMangager = A.Fake<IContactManager>();
           var mockCOntactGenerateLicenseQueueManager = A.Fake<IContactGenerateLicenseQueueManager>();
           var mockGenerateLicenseManager = A.Fake<IGenerateLicenseManager>();

           A.CallTo(() => mockGenerateLicenseManager.UpdateGenerateLicenseStatus(A<LicenseUserAction>.Ignored)).WithAnyArguments();

           //Act
           LicenseController controller = new LicenseController(mockLicenseMangager, mockContactMangager, mockCOntactGenerateLicenseQueueManager, mockGenerateLicenseManager);
           controller.UpdateGeneratedLicenseStatus(A<LicenseUserAction>.Ignored);

           //Assert
           A.CallTo(() => mockGenerateLicenseManager.UpdateGenerateLicenseStatus(A<LicenseUserAction>.Ignored)).WithAnyArguments().MustHaveHappened();
       }

       [Test]
       public void GetInboxLicenses_ReturnListLisence()
       {
           //Arrange
           var mockLicenseMangager = A.Fake<ILicenseManager>();
           var mockContactMangager = A.Fake<IContactManager>();
           var mockCOntactGenerateLicenseQueueManager = A.Fake<IContactGenerateLicenseQueueManager>();
           var mockGenerateLicenseManager = A.Fake<IGenerateLicenseManager>();

           //Build expected
           List<License> expected = new List<License> { };

           A.CallTo(() => mockLicenseMangager.GetInboxLicenses(A<int>.Ignored)).WithAnyArguments().Returns(expected);

           //Act
           LicenseController controller = new LicenseController(mockLicenseMangager, mockContactMangager, mockCOntactGenerateLicenseQueueManager, mockGenerateLicenseManager);
           var result = controller.GetInboxLicenses(A<int>.Ignored);

           //Assert
           Assert.AreEqual(expected, result);
       }

       [Test]
       public void GetLicenseDetails_ReturnLisence()
       {
           //Arrange
           var mockLicenseMangager = A.Fake<ILicenseManager>();
           var mockContactMangager = A.Fake<IContactManager>();
           var mockCOntactGenerateLicenseQueueManager = A.Fake<IContactGenerateLicenseQueueManager>();
           var mockGenerateLicenseManager = A.Fake<IGenerateLicenseManager>();

           //Build expected
           License expected = new License { };

           A.CallTo(() => mockLicenseMangager.Get(A<int>.Ignored)).WithAnyArguments().Returns(expected);

           //Act
           LicenseController controller = new LicenseController(mockLicenseMangager, mockContactMangager, mockCOntactGenerateLicenseQueueManager, mockGenerateLicenseManager);
           var result = controller.GetLicenseDetails(A<int>.Ignored);

           //Assert
           Assert.AreEqual(expected, result);
       }


       [Test]
       public void GetProductLicenses_ReturnListLisence()
       {
           //Arrange
           var mockLicenseMangager = A.Fake<ILicenseManager>();
           var mockContactMangager = A.Fake<IContactManager>();
           var mockCOntactGenerateLicenseQueueManager = A.Fake<IContactGenerateLicenseQueueManager>();
           var mockGenerateLicenseManager = A.Fake<IGenerateLicenseManager>();

           //Build expected
           List<License> expected = new List<License> { };

           A.CallTo(() => mockLicenseMangager.GetLicensesForProduct(A<int>.Ignored)).WithAnyArguments().Returns(expected);

           //Act
           LicenseController controller = new LicenseController(mockLicenseMangager, mockContactMangager, mockCOntactGenerateLicenseQueueManager, mockGenerateLicenseManager);
           var result = controller.GetProductLicenses(A<int>.Ignored);

           //Assert
           Assert.AreEqual(expected, result);
       }

       [Test]
       public void UpdateLicense_ReturnLisence()
       {
           //Arrange
           var mockLicenseMangager = A.Fake<ILicenseManager>();
           var mockContactMangager = A.Fake<IContactManager>();
           var mockCOntactGenerateLicenseQueueManager = A.Fake<IContactGenerateLicenseQueueManager>();
           var mockGenerateLicenseManager = A.Fake<IGenerateLicenseManager>();

           //Build expected
           License expected = new License { };

           A.CallTo(() => mockLicenseMangager.EditLicense(A<License>.Ignored)).WithAnyArguments().Returns(expected);

           //Act
           LicenseController controller = new LicenseController(mockLicenseMangager, mockContactMangager, mockCOntactGenerateLicenseQueueManager, mockGenerateLicenseManager);
           var result = controller.UpdateLicense(A<License>.Ignored);

           //Assert
           Assert.AreEqual(expected, result);
       }

       [Test]
       public void CreateLicense_ReturnLisence()
       {
           //Arrange
           var mockLicenseMangager = A.Fake<ILicenseManager>();
           var mockContactMangager = A.Fake<IContactManager>();
           var mockCOntactGenerateLicenseQueueManager = A.Fake<IContactGenerateLicenseQueueManager>();
           var mockGenerateLicenseManager = A.Fake<IGenerateLicenseManager>();

           //Build expected
           License expected = new License { };

           A.CallTo(() => mockLicenseMangager.Add(A<License>.Ignored)).WithAnyArguments().Returns(expected);

           //Act
           LicenseController controller = new LicenseController(mockLicenseMangager, mockContactMangager, mockCOntactGenerateLicenseQueueManager, mockGenerateLicenseManager);
           var result = controller.CreateLicense(A<License>.Ignored);

           //Assert
           Assert.AreEqual(expected, result);
       }

       [Test]
       public void EditStatus_ReturnBoolTrue()
       {
           //Arrange
           var mockLicenseMangager = A.Fake<ILicenseManager>();
           var mockContactMangager = A.Fake<IContactManager>();
           var mockCOntactGenerateLicenseQueueManager = A.Fake<IContactGenerateLicenseQueueManager>();
           var mockGenerateLicenseManager = A.Fake<IGenerateLicenseManager>();

           //Build expected
           const bool expected = true;

           A.CallTo(() => mockLicenseMangager.EditStatus(A<License>.Ignored,A<bool>.Ignored,null)).WithAnyArguments().Returns(expected);

           //Act
           LicenseController controller = new LicenseController(mockLicenseMangager, mockContactMangager, mockCOntactGenerateLicenseQueueManager, mockGenerateLicenseManager);
           var result = controller.EditStatus(A<License>.Ignored);

           //Assert
           Assert.AreEqual(expected, result);
       }

       [Test]
       public void GetSendLicenseInfo_ReturnLisence()
       {
           //Arrange
           var mockLicenseMangager = A.Fake<ILicenseManager>();
           var mockContactMangager = A.Fake<IContactManager>();
           var mockCOntactGenerateLicenseQueueManager = A.Fake<IContactGenerateLicenseQueueManager>();
           var mockGenerateLicenseManager = A.Fake<IGenerateLicenseManager>();

           //Build expected
           SendLicenseInfo expected = new SendLicenseInfo { };

           A.CallTo(() => mockLicenseMangager.GetSendLicenseInfo(A<int>.Ignored)).WithAnyArguments().Returns(expected);

           //Act
           LicenseController controller = new LicenseController(mockLicenseMangager, mockContactMangager, mockCOntactGenerateLicenseQueueManager, mockGenerateLicenseManager);
           var result = controller.GetSendLicenseInfo(A<int>.Ignored);

           //Assert
           Assert.AreEqual(expected, result);
       }
       
       [Test]
       public void UpdateSendLicenseInfo_ReturnBoolTrue()
       {
           //Arrange
           var mockLicenseMangager = A.Fake<ILicenseManager>();
           var mockContactMangager = A.Fake<IContactManager>();
           var mockCOntactGenerateLicenseQueueManager = A.Fake<IContactGenerateLicenseQueueManager>();
           var mockGenerateLicenseManager = A.Fake<IGenerateLicenseManager>();

           //Build expected
           const bool expected = true;

           A.CallTo(() => mockLicenseMangager.UpdateSendLicenseInfo(A<SendLicenseInfo>.Ignored)).WithAnyArguments().Returns(expected);

           //Act
           LicenseController controller = new LicenseController(mockLicenseMangager, mockContactMangager, mockCOntactGenerateLicenseQueueManager, mockGenerateLicenseManager);
           var result = controller.UpdateSendLicenseInfo(A<SendLicenseInfo>.Ignored);

           //Assert
           Assert.AreEqual(expected, result);
       }

       /*

       [Test]
       [Ignore("Need to test StreamReader and break StreamReader off to mock-able Interface.")]
       public void EditStatusLicenseProcessor_ReturnBoolTrue()
       {
           //Arrange
           var mockLicenseMangager = A.Fake<ILicenseManager>();
           var mockContactMangager = A.Fake<IContactManager>();
           var mockCOntactGenerateLicenseQueueManager = A.Fake<IContactGenerateLicenseQueueManager>();
           var mockGenerateLicenseManager = A.Fake<IGenerateLicenseManager>();

           var mockHttpRequest = A.Fake<HttpRequestBase>();
           var mockHttpContext = A.Fake<HttpContextBase>();

           var stream1 = new MemoryStream(Encoding.Default.GetBytes("Test"));
           A.CallTo(() => mockHttpRequest.InputStream).Returns(stream1);
           A.CallTo(() => mockHttpContext.Request).Returns(mockHttpRequest);
           StreamReader stream = new StreamReader(mockHttpContext.Request.InputStream);
           
           //Build expected
           const bool expected = true;
                              
           //Act
           LicenseController controller = new LicenseController(mockLicenseMangager, mockContactMangager, mockCOntactGenerateLicenseQueueManager, mockGenerateLicenseManager);
         
           var result = controller.EditStatusLicenseProcessor(A<int>.Ignored);

           
           string inputString = stream.ReadToEnd();
           DateTime dateTime = inputString.ToDateTime();

           A.CallTo(() => mockLicenseMangager.EditStatusLicenseProcessor(A<int>.Ignored, dateTime)).WithAnyArguments().Returns(expected);

           //Assert
           Assert.AreEqual(expected, result);
       }
       */

       [Test]
       public void EditLicenseStatusReport_ReturnBoolTrue()
       {
           //Arrange
           var mockLicenseMangager = A.Fake<ILicenseManager>();
           var mockContactMangager = A.Fake<IContactManager>();
           var mockCOntactGenerateLicenseQueueManager = A.Fake<IContactGenerateLicenseQueueManager>();
           var mockGenerateLicenseManager = A.Fake<IGenerateLicenseManager>();

           //Build expected
           const bool expected = true;

           A.CallTo(() => mockLicenseMangager.EditLicenseStatusReport(A<int>.Ignored)).WithAnyArguments().Returns(expected);

           //Act
           LicenseController controller = new LicenseController(mockLicenseMangager, mockContactMangager, mockCOntactGenerateLicenseQueueManager, mockGenerateLicenseManager);
           var result = controller.EditLicenseStatusReport(A<int>.Ignored);

           //Assert
           Assert.AreEqual(expected, result);
       }
    }
}
