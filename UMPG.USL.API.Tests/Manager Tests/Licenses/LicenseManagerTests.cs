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
using UMPG.USL.API.Data.Recs;

namespace UMPG.USL.API.Tests.Manager_Tests.Licenses
{
    [TestFixture]
    public class LicenseManagerTests
    {/*
        [Test]
        public void GetLicense_ReturnLicense()
        {
            //Arrange
            var mockLicenseRepository = A.Fake<ILicenseRepository>();
            var mockLicenseProductRepository = A.Fake<ILicenseProductRepository>();
            var mockLicenseNoteRepository = A.Fake<ILicenseNoteRepository>();
            var mockLicenseSequenceRepository = A.Fake<ILicenseSequenceRepository>();
            var mockSearchProvider = A.Fake<ISearchProvider>();
            var mockLicensePRWriterRepository = A.Fake<ILicensePRWriterRepository>();
            var mockLicensePRWriterRateRepository = A.Fake<ILicensePRWriterRateRepository>();
            var mockLicenseProductRecordingRepository = A.Fake<ILicenseProductRecordingRepository>();
            var mockLicensePRWriterRateStatusRepository = A.Fake<ILicensePRWriterRateStatusRepository>();
            var mockLicenseUploadPreviewLicenseRepository = A.Fake<ILicenseUploadPreviewLicenseRepository>();
            var mockRecs = A.Fake<IRecs>();
            var mockRecsDataProvider = A.Fake<IRecsDataProvider>();

            //Build expected
            License expected = new License { };

            A.CallTo(() => mockLicenseRepository.Get(A<int>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            LicenseManager manager = new LicenseManager(mockLicenseRepository, mockLicenseProductRepository, mockLicenseNoteRepository, mockLicenseSequenceRepository, mockSearchProvider, mockLicensePRWriterRateRepository, mockLicenseProductRecordingRepository, mockLicensePRWriterRepository, mockLicensePRWriterRateStatusRepository, mockLicenseUploadPreviewLicenseRepository, mockRecs, mockRecsDataProvider);
            var result = manager.Get(A<int>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
            A.CallTo(() => mockLicenseRepository.Get(A<int>.Ignored)).WithAnyArguments().MustHaveHappened();
        }

        [Test]
        public void GetInboxLicenses_ReturnListLicense()
        {
            //Arrange
            var mockLicenseRepository = A.Fake<ILicenseRepository>();
            var mockLicenseProductRepository = A.Fake<ILicenseProductRepository>();
            var mockLicenseNoteRepository = A.Fake<ILicenseNoteRepository>();
            var mockLicenseSequenceRepository = A.Fake<ILicenseSequenceRepository>();
            var mockSearchProvider = A.Fake<ISearchProvider>();
            var mockLicensePRWriterRepository = A.Fake<ILicensePRWriterRepository>();
            var mockLicensePRWriterRateRepository = A.Fake<ILicensePRWriterRateRepository>();
            var mockLicenseProductRecordingRepository = A.Fake<ILicenseProductRecordingRepository>();
            var mockLicensePRWriterRateStatusRepository = A.Fake<ILicensePRWriterRateStatusRepository>();
            var mockLicenseUploadPreviewLicenseRepository = A.Fake<ILicenseUploadPreviewLicenseRepository>();
            var mockRecs = A.Fake<IRecs>();
            var mockRecsDataProvider = A.Fake<IRecsDataProvider>();

            //Build expected
            List<License> expected = new List<License> { };

            A.CallTo(() => mockLicenseRepository.GetInboxLicenses(A<int>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            LicenseManager manager = new LicenseManager(mockLicenseRepository, mockLicenseProductRepository, mockLicenseNoteRepository, mockLicenseSequenceRepository, mockSearchProvider, mockLicensePRWriterRateRepository, mockLicenseProductRecordingRepository, mockLicensePRWriterRepository, mockLicensePRWriterRateStatusRepository, mockLicenseUploadPreviewLicenseRepository, mockRecs, mockRecsDataProvider);
            var result = manager.GetInboxLicenses(A<int>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
            A.CallTo(() => mockLicenseRepository.GetInboxLicenses(A<int>.Ignored)).WithAnyArguments().MustHaveHappened();
        }

        [Test]
        public void Add_ReturnLicense()
        {
            //Arrange
            var mockLicenseRepository = A.Fake<ILicenseRepository>();
            var mockLicenseProductRepository = A.Fake<ILicenseProductRepository>();
            var mockLicenseNoteRepository = A.Fake<ILicenseNoteRepository>();
            var mockLicenseSequenceRepository = A.Fake<ILicenseSequenceRepository>();
            var mockSearchProvider = A.Fake<ISearchProvider>();
            var mockLicensePRWriterRepository = A.Fake<ILicensePRWriterRepository>();
            var mockLicensePRWriterRateRepository = A.Fake<ILicensePRWriterRateRepository>();
            var mockLicenseProductRecordingRepository = A.Fake<ILicenseProductRecordingRepository>();
            var mockLicensePRWriterRateStatusRepository = A.Fake<ILicensePRWriterRateStatusRepository>();
            var mockLicenseUploadPreviewLicenseRepository = A.Fake<ILicenseUploadPreviewLicenseRepository>();
            var mockRecs = A.Fake<IRecs>();
            var mockRecsDataProvider = A.Fake<IRecsDataProvider>();

            //Build expected
            License expected = new License { };

            //Build Request
            LicenseeLabelGroup llg = new LicenseeLabelGroup{};
            License request = new License
            {
                LicenseName = "Test",
                AssignedToId =99,
              //  LicenseName = "test",
                LicenseMethodId=99,
                LicenseTypeId=99,
                PriorityId=99,
                LicenseeId=99,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                ContactId=99,
                EffectiveDate = DateTime.Now,
                ReceivedDate = DateTime.Now,
                SignedDate = DateTime.Now,
                LicenseeLabelGroup = llg
            };

            A.CallTo(() => mockLicenseRepository.Add(request)).WithAnyArguments().Returns(expected);

            //Act
            LicenseManager manager = new LicenseManager(mockLicenseRepository, mockLicenseProductRepository, mockLicenseNoteRepository, mockLicenseSequenceRepository, mockSearchProvider, mockLicensePRWriterRateRepository, mockLicenseProductRecordingRepository, mockLicensePRWriterRepository, mockLicensePRWriterRateStatusRepository, mockLicenseUploadPreviewLicenseRepository, mockRecs, mockRecsDataProvider);
            var result = manager.Add(request);

            //Assert
            Assert.IsInstanceOf(typeof(License), result);
        }

        [Test]
        public void Search_ReturnListLicense()
        {
            //Arrange
            var mockLicenseRepository = A.Fake<ILicenseRepository>();
            var mockLicenseProductRepository = A.Fake<ILicenseProductRepository>();
            var mockLicenseNoteRepository = A.Fake<ILicenseNoteRepository>();
            var mockLicenseSequenceRepository = A.Fake<ILicenseSequenceRepository>();
            var mockSearchProvider = A.Fake<ISearchProvider>();
            var mockLicensePRWriterRepository = A.Fake<ILicensePRWriterRepository>();
            var mockLicensePRWriterRateRepository = A.Fake<ILicensePRWriterRateRepository>();
            var mockLicenseProductRecordingRepository = A.Fake<ILicenseProductRecordingRepository>();
            var mockLicensePRWriterRateStatusRepository = A.Fake<ILicensePRWriterRateStatusRepository>();
            var mockLicenseUploadPreviewLicenseRepository = A.Fake<ILicenseUploadPreviewLicenseRepository>();
            var mockRecs = A.Fake<IRecs>();
            var mockRecsDataProvider = A.Fake<IRecsDataProvider>();

            //Build expected
            List<License> expected = new List<License> { };

            A.CallTo(() => mockLicenseRepository.Search(A<string>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            LicenseManager manager = new LicenseManager(mockLicenseRepository, mockLicenseProductRepository, mockLicenseNoteRepository, mockLicenseSequenceRepository, mockSearchProvider, mockLicensePRWriterRateRepository, mockLicenseProductRecordingRepository, mockLicensePRWriterRepository, mockLicensePRWriterRateStatusRepository, mockLicenseUploadPreviewLicenseRepository, mockRecs, mockRecsDataProvider);
            var result = manager.Search(A<string>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
            A.CallTo(() => mockLicenseRepository.Search(A<string>.Ignored)).WithAnyArguments().MustHaveHappened();
        }

        [Test]
        public void PagedSearch_ReturnListLicense()
        {
            //Arrange
            var mockLicenseRepository = A.Fake<ILicenseRepository>();
            var mockLicenseProductRepository = A.Fake<ILicenseProductRepository>();
            var mockLicenseNoteRepository = A.Fake<ILicenseNoteRepository>();
            var mockLicenseSequenceRepository = A.Fake<ILicenseSequenceRepository>();
            var mockSearchProvider = A.Fake<ISearchProvider>();
            var mockLicensePRWriterRepository = A.Fake<ILicensePRWriterRepository>();
            var mockLicensePRWriterRateRepository = A.Fake<ILicensePRWriterRateRepository>();
            var mockLicenseProductRecordingRepository = A.Fake<ILicenseProductRecordingRepository>();
            var mockLicensePRWriterRateStatusRepository = A.Fake<ILicensePRWriterRateStatusRepository>();
            var mockLicenseUploadPreviewLicenseRepository = A.Fake<ILicenseUploadPreviewLicenseRepository>();
            var mockRecs = A.Fake<IRecs>();
            var mockRecsDataProvider = A.Fake<IRecsDataProvider>();

            //Build expected
            PagedResponse<License>  expected = new PagedResponse<License> { };

            A.CallTo(() => mockSearchProvider.SearchLicenses(A<LicenseRequest>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            LicenseManager manager = new LicenseManager(mockLicenseRepository, mockLicenseProductRepository, mockLicenseNoteRepository, mockLicenseSequenceRepository, mockSearchProvider, mockLicensePRWriterRateRepository, mockLicenseProductRecordingRepository, mockLicensePRWriterRepository, mockLicensePRWriterRateStatusRepository, mockLicenseUploadPreviewLicenseRepository, mockRecs, mockRecsDataProvider);
            var result = manager.PagedSearch(A<LicenseRequest>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
            A.CallTo(() => mockSearchProvider.SearchLicenses(A<LicenseRequest>.Ignored)).WithAnyArguments().MustHaveHappened();
        }

        [Test]
        public void UpdateLicense_ReturnBoolTRUE()
        {
            //Arrange
            var mockLicenseRepository = A.Fake<ILicenseRepository>();
            var mockLicenseProductRepository = A.Fake<ILicenseProductRepository>();
            var mockLicenseNoteRepository = A.Fake<ILicenseNoteRepository>();
            var mockLicenseSequenceRepository = A.Fake<ILicenseSequenceRepository>();
            var mockSearchProvider = A.Fake<ISearchProvider>();
            var mockLicensePRWriterRepository = A.Fake<ILicensePRWriterRepository>();
            var mockLicensePRWriterRateRepository = A.Fake<ILicensePRWriterRateRepository>();
            var mockLicenseProductRecordingRepository = A.Fake<ILicenseProductRecordingRepository>();
            var mockLicensePRWriterRateStatusRepository = A.Fake<ILicensePRWriterRateStatusRepository>();
            var mockLicenseUploadPreviewLicenseRepository = A.Fake<ILicenseUploadPreviewLicenseRepository>();
            var mockRecs = A.Fake<IRecs>();
            var mockRecsDataProvider = A.Fake<IRecsDataProvider>();

            //Build expected
            PagedResponse<License> expected = new PagedResponse<License> { };
             List<int> ids = new List<int>{};

            //Build Request 
             UpdateLicenseAssigneeRequest request = new UpdateLicenseAssigneeRequest
             {
                 LicenseIds = ids
             };

            A.CallTo(() => mockLicenseRepository.UpdateLicense(A<License>.Ignored)).WithAnyArguments();

            //Act
            LicenseManager manager = new LicenseManager(mockLicenseRepository, mockLicenseProductRepository, mockLicenseNoteRepository, mockLicenseSequenceRepository, mockSearchProvider, mockLicensePRWriterRateRepository, mockLicenseProductRecordingRepository, mockLicensePRWriterRepository, mockLicensePRWriterRateStatusRepository, mockLicenseUploadPreviewLicenseRepository, mockRecs, mockRecsDataProvider);
            var result = manager.UpdateLicense(request);

            //Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void UploadGeneratedLicensePreview_ReturInt()
        {
            //Arrange
            var mockLicenseRepository = A.Fake<ILicenseRepository>();
            var mockLicenseProductRepository = A.Fake<ILicenseProductRepository>();
            var mockLicenseNoteRepository = A.Fake<ILicenseNoteRepository>();
            var mockLicenseSequenceRepository = A.Fake<ILicenseSequenceRepository>();
            var mockSearchProvider = A.Fake<ISearchProvider>();
            var mockLicensePRWriterRepository = A.Fake<ILicensePRWriterRepository>();
            var mockLicensePRWriterRateRepository = A.Fake<ILicensePRWriterRateRepository>();
            var mockLicenseProductRecordingRepository = A.Fake<ILicenseProductRecordingRepository>();
            var mockLicensePRWriterRateStatusRepository = A.Fake<ILicensePRWriterRateStatusRepository>();
            var mockLicenseUploadPreviewLicenseRepository = A.Fake<ILicenseUploadPreviewLicenseRepository>();
            var mockRecs = A.Fake<IRecs>();
            var mockRecsDataProvider = A.Fake<IRecsDataProvider>();
            var mockB = A.Fake<UploadGeneratedLicensePreviewRequest>();

            //Build expected
            PagedResponse<License> expected = new PagedResponse<License> { };
            List<int> ids = new List<int> { };

            //Build Request 
            GenerateLicensePreviewRequest request = new GenerateLicensePreviewRequest(mockB) { };
            

            A.CallTo(() => mockLicenseRepository.UpdateLicense(A<License>.Ignored)).WithAnyArguments();//.Returns(expected);

            //Act
            LicenseManager manager = new LicenseManager(mockLicenseRepository, mockLicenseProductRepository, mockLicenseNoteRepository, mockLicenseSequenceRepository, mockSearchProvider, mockLicensePRWriterRateRepository, mockLicenseProductRecordingRepository, mockLicensePRWriterRepository, mockLicensePRWriterRateStatusRepository, mockLicenseUploadPreviewLicenseRepository, mockRecs, mockRecsDataProvider);
            var result = manager.UploadGeneratedLicensePreview(request);

            //Assert
            Assert.IsInstanceOf(typeof(int), result);
        }

        [Test]
        public void GetLicensesForProduct_ReturnListLicense()
        {
            //Arrange
            var mockLicenseRepository = A.Fake<ILicenseRepository>();
            var mockLicenseProductRepository = A.Fake<ILicenseProductRepository>();
            var mockLicenseNoteRepository = A.Fake<ILicenseNoteRepository>();
            var mockLicenseSequenceRepository = A.Fake<ILicenseSequenceRepository>();
            var mockSearchProvider = A.Fake<ISearchProvider>();
            var mockLicensePRWriterRepository = A.Fake<ILicensePRWriterRepository>();
            var mockLicensePRWriterRateRepository = A.Fake<ILicensePRWriterRateRepository>();
            var mockLicenseProductRecordingRepository = A.Fake<ILicenseProductRecordingRepository>();
            var mockLicensePRWriterRateStatusRepository = A.Fake<ILicensePRWriterRateStatusRepository>();
            var mockLicenseUploadPreviewLicenseRepository = A.Fake<ILicenseUploadPreviewLicenseRepository>();
            var mockRecs = A.Fake<IRecs>();
            var mockRecsDataProvider = A.Fake<IRecsDataProvider>();
            var mockB = A.Fake<UploadGeneratedLicensePreviewRequest>();

            //Build expected
            List<License> expected = new List<License> { };
            List<int> ids = new List<int> { };

            //Build Request 
            GenerateLicensePreviewRequest request = new GenerateLicensePreviewRequest(mockB) { };


            A.CallTo(() => mockLicenseRepository.GetByIds(A<List<int>>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            LicenseManager manager = new LicenseManager(mockLicenseRepository, mockLicenseProductRepository, mockLicenseNoteRepository, mockLicenseSequenceRepository, mockSearchProvider, mockLicensePRWriterRateRepository, mockLicenseProductRecordingRepository, mockLicensePRWriterRepository, mockLicensePRWriterRateStatusRepository, mockLicenseUploadPreviewLicenseRepository, mockRecs, mockRecsDataProvider);
            var result = manager.GetLicensesForProduct(A<int>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void EditLicense_ReturnLicense()
        {
            //Arrange
            var mockLicenseRepository = A.Fake<ILicenseRepository>();
            var mockLicenseProductRepository = A.Fake<ILicenseProductRepository>();
            var mockLicenseNoteRepository = A.Fake<ILicenseNoteRepository>();
            var mockLicenseSequenceRepository = A.Fake<ILicenseSequenceRepository>();
            var mockSearchProvider = A.Fake<ISearchProvider>();
            var mockLicensePRWriterRepository = A.Fake<ILicensePRWriterRepository>();
            var mockLicensePRWriterRateRepository = A.Fake<ILicensePRWriterRateRepository>();
            var mockLicenseProductRecordingRepository = A.Fake<ILicenseProductRecordingRepository>();
            var mockLicensePRWriterRateStatusRepository = A.Fake<ILicensePRWriterRateStatusRepository>();
            var mockLicenseUploadPreviewLicenseRepository = A.Fake<ILicenseUploadPreviewLicenseRepository>();
            var mockRecs = A.Fake<IRecs>();
            var mockRecsDataProvider = A.Fake<IRecsDataProvider>();
            var mockB = A.Fake<UploadGeneratedLicensePreviewRequest>();

            //Build expected
            License expected = new License { };
            List<int> ids = new List<int> { };

            //Build Request 
            LicenseeLabelGroup llg = new LicenseeLabelGroup { };
            License request = new License { LicenseeId = 99, LicenseName = "test", LicenseId = 99, LicenseeLabelGroup = llg };


            A.CallTo(() => mockLicenseRepository.GetLite(A<int>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            LicenseManager manager = new LicenseManager(mockLicenseRepository, mockLicenseProductRepository, mockLicenseNoteRepository, mockLicenseSequenceRepository, mockSearchProvider, mockLicensePRWriterRateRepository, mockLicenseProductRecordingRepository, mockLicensePRWriterRepository, mockLicensePRWriterRateStatusRepository, mockLicenseUploadPreviewLicenseRepository, mockRecs, mockRecsDataProvider);
            var result = manager.EditLicense(request);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void EditStatus_ReturnBoolTRUE()
        {
            //Arrange
            var mockLicenseRepository = A.Fake<ILicenseRepository>();
            var mockLicenseProductRepository = A.Fake<ILicenseProductRepository>();
            var mockLicenseNoteRepository = A.Fake<ILicenseNoteRepository>();
            var mockLicenseSequenceRepository = A.Fake<ILicenseSequenceRepository>();
            var mockSearchProvider = A.Fake<ISearchProvider>();
            var mockLicensePRWriterRepository = A.Fake<ILicensePRWriterRepository>();
            var mockLicensePRWriterRateRepository = A.Fake<ILicensePRWriterRateRepository>();
            var mockLicenseProductRecordingRepository = A.Fake<ILicenseProductRecordingRepository>();
            var mockLicensePRWriterRateStatusRepository = A.Fake<ILicensePRWriterRateStatusRepository>();
            var mockLicenseUploadPreviewLicenseRepository = A.Fake<ILicenseUploadPreviewLicenseRepository>();
            var mockRecs = A.Fake<IRecs>();
            var mockRecsDataProvider = A.Fake<IRecsDataProvider>();
            var mockB = A.Fake<UploadGeneratedLicensePreviewRequest>();

            //Build expected
            License expected = new License { };
            List<int> ids = new List<int> { };

            //Build Request 
            LicenseeLabelGroup llg = new LicenseeLabelGroup { };
            License request = new License { LicenseeId = 99, LicenseName = "test", LicenseId = 99, LicenseeLabelGroup = llg };
            
            A.CallTo(() => mockLicenseRepository.GetLite(A<int>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            LicenseManager manager = new LicenseManager(mockLicenseRepository, mockLicenseProductRepository, mockLicenseNoteRepository, mockLicenseSequenceRepository, mockSearchProvider, mockLicensePRWriterRateRepository, mockLicenseProductRecordingRepository, mockLicensePRWriterRepository, mockLicensePRWriterRateStatusRepository, mockLicenseUploadPreviewLicenseRepository, mockRecs, mockRecsDataProvider);
            var result = manager.EditStatus(request, true, A<DateTime>.Ignored);

            //Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void EditStatusLicenseProcessor_ReturnBoolTRUE()
        {
            //Arrange
            var mockLicenseRepository = A.Fake<ILicenseRepository>();
            var mockLicenseProductRepository = A.Fake<ILicenseProductRepository>();
            var mockLicenseNoteRepository = A.Fake<ILicenseNoteRepository>();
            var mockLicenseSequenceRepository = A.Fake<ILicenseSequenceRepository>();
            var mockSearchProvider = A.Fake<ISearchProvider>();
            var mockLicensePRWriterRepository = A.Fake<ILicensePRWriterRepository>();
            var mockLicensePRWriterRateRepository = A.Fake<ILicensePRWriterRateRepository>();
            var mockLicenseProductRecordingRepository = A.Fake<ILicenseProductRecordingRepository>();
            var mockLicensePRWriterRateStatusRepository = A.Fake<ILicensePRWriterRateStatusRepository>();
            var mockLicenseUploadPreviewLicenseRepository = A.Fake<ILicenseUploadPreviewLicenseRepository>();
            var mockRecs = A.Fake<IRecs>();
            var mockRecsDataProvider = A.Fake<IRecsDataProvider>();
            var mockB = A.Fake<UploadGeneratedLicensePreviewRequest>();

            //Build expected
            License expected = new License { };
            List<int> ids = new List<int> { };

            //Build Request 
            LicenseeLabelGroup llg = new LicenseeLabelGroup { };
            License request = new License { LicenseeId = 99, LicenseName = "test", LicenseId = 99, LicenseeLabelGroup = llg };

            A.CallTo(() => mockLicenseRepository.Get(A<int>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            LicenseManager manager = new LicenseManager(mockLicenseRepository, mockLicenseProductRepository, mockLicenseNoteRepository, mockLicenseSequenceRepository, mockSearchProvider, mockLicensePRWriterRateRepository, mockLicenseProductRecordingRepository, mockLicensePRWriterRepository, mockLicensePRWriterRateStatusRepository, mockLicenseUploadPreviewLicenseRepository, mockRecs, mockRecsDataProvider);
            var result = manager.EditStatusLicenseProcessor(A<int>.Ignored, A<DateTime>.Ignored);

            //Assert
            Assert.IsTrue(result);
        }


        [Test]
        public void EditLicenseStatusReport_ReturnBoolTRUE()
        {
            //Arrange
            var mockLicenseRepository = A.Fake<ILicenseRepository>();
            var mockLicenseProductRepository = A.Fake<ILicenseProductRepository>();
            var mockLicenseNoteRepository = A.Fake<ILicenseNoteRepository>();
            var mockLicenseSequenceRepository = A.Fake<ILicenseSequenceRepository>();
            var mockSearchProvider = A.Fake<ISearchProvider>();
            var mockLicensePRWriterRepository = A.Fake<ILicensePRWriterRepository>();
            var mockLicensePRWriterRateRepository = A.Fake<ILicensePRWriterRateRepository>();
            var mockLicenseProductRecordingRepository = A.Fake<ILicenseProductRecordingRepository>();
            var mockLicensePRWriterRateStatusRepository = A.Fake<ILicensePRWriterRateStatusRepository>();
            var mockLicenseUploadPreviewLicenseRepository = A.Fake<ILicenseUploadPreviewLicenseRepository>();
            var mockRecs = A.Fake<IRecs>();
            var mockRecsDataProvider = A.Fake<IRecsDataProvider>();
            var mockB = A.Fake<UploadGeneratedLicensePreviewRequest>();

            //Build expected
            License expected = new License { };
            List<int> ids = new List<int> { };

            //Build Request 
            LicenseeLabelGroup llg = new LicenseeLabelGroup { };
            License request = new License { LicenseeId = 99, LicenseName = "test", LicenseId = 99, LicenseeLabelGroup = llg };

            A.CallTo(() => mockLicenseRepository.Get(A<int>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            LicenseManager manager = new LicenseManager(mockLicenseRepository, mockLicenseProductRepository, mockLicenseNoteRepository, mockLicenseSequenceRepository, mockSearchProvider, mockLicensePRWriterRateRepository, mockLicenseProductRecordingRepository, mockLicensePRWriterRepository, mockLicensePRWriterRateStatusRepository, mockLicenseUploadPreviewLicenseRepository, mockRecs, mockRecsDataProvider);
            var result = manager.EditLicenseStatusReport(A<int>.Ignored);

            //Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void GetSendLicenseInfo_ReturnBoolTRUE()
        {
            //Arrange
            var mockLicenseRepository = A.Fake<ILicenseRepository>();
            var mockLicenseProductRepository = A.Fake<ILicenseProductRepository>();
            var mockLicenseNoteRepository = A.Fake<ILicenseNoteRepository>();
            var mockLicenseSequenceRepository = A.Fake<ILicenseSequenceRepository>();
            var mockSearchProvider = A.Fake<ISearchProvider>();
            var mockLicensePRWriterRepository = A.Fake<ILicensePRWriterRepository>();
            var mockLicensePRWriterRateRepository = A.Fake<ILicensePRWriterRateRepository>();
            var mockLicenseProductRecordingRepository = A.Fake<ILicenseProductRecordingRepository>();
            var mockLicensePRWriterRateStatusRepository = A.Fake<ILicensePRWriterRateStatusRepository>();
            var mockLicenseUploadPreviewLicenseRepository = A.Fake<ILicenseUploadPreviewLicenseRepository>();
            var mockRecs = A.Fake<IRecs>();
            var mockRecsDataProvider = A.Fake<IRecsDataProvider>();
            var mockB = A.Fake<UploadGeneratedLicensePreviewRequest>();

            //Build expected
            SendLicenseInfo expected = new SendLicenseInfo { };
            
            //Build Request 
            LicenseeLabelGroup llg = new LicenseeLabelGroup { };
            License request = new License { LicenseeId = 99, LicenseName = "test", LicenseId = 99, LicenseeLabelGroup = llg };

            A.CallTo(() => mockLicenseRepository.GetSendLicenseInfo(A<int>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            LicenseManager manager = new LicenseManager(mockLicenseRepository, mockLicenseProductRepository, mockLicenseNoteRepository, mockLicenseSequenceRepository, mockSearchProvider, mockLicensePRWriterRateRepository, mockLicenseProductRecordingRepository, mockLicensePRWriterRepository, mockLicensePRWriterRateStatusRepository, mockLicenseUploadPreviewLicenseRepository, mockRecs, mockRecsDataProvider);
            var result = manager.GetSendLicenseInfo(A<int>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void UpdateSendLicenseInfo_ReturnBoolFALSE()
        {
            //Arrange
            var mockLicenseRepository = A.Fake<ILicenseRepository>();
            var mockLicenseProductRepository = A.Fake<ILicenseProductRepository>();
            var mockLicenseNoteRepository = A.Fake<ILicenseNoteRepository>();
            var mockLicenseSequenceRepository = A.Fake<ILicenseSequenceRepository>();
            var mockSearchProvider = A.Fake<ISearchProvider>();
            var mockLicensePRWriterRepository = A.Fake<ILicensePRWriterRepository>();
            var mockLicensePRWriterRateRepository = A.Fake<ILicensePRWriterRateRepository>();
            var mockLicenseProductRecordingRepository = A.Fake<ILicenseProductRecordingRepository>();
            var mockLicensePRWriterRateStatusRepository = A.Fake<ILicensePRWriterRateStatusRepository>();
            var mockLicenseUploadPreviewLicenseRepository = A.Fake<ILicenseUploadPreviewLicenseRepository>();
            var mockRecs = A.Fake<IRecs>();
            var mockRecsDataProvider = A.Fake<IRecsDataProvider>();
            var mockB = A.Fake<UploadGeneratedLicensePreviewRequest>();

            //Build Request 
            LicenseeLabelGroup llg = new LicenseeLabelGroup { };
            License request = new License { LicenseeId = 99, LicenseName = "test", LicenseId = 99, LicenseeLabelGroup = llg };

            A.CallTo(() => mockLicenseRepository.UpdateSendLicenseInfo(A<SendLicenseInfo>.Ignored)).WithAnyArguments();

            //Act
            LicenseManager manager = new LicenseManager(mockLicenseRepository, mockLicenseProductRepository, mockLicenseNoteRepository, mockLicenseSequenceRepository, mockSearchProvider, mockLicensePRWriterRateRepository, mockLicenseProductRecordingRepository, mockLicensePRWriterRepository, mockLicensePRWriterRateStatusRepository, mockLicenseUploadPreviewLicenseRepository, mockRecs, mockRecsDataProvider);
            var result = manager.UpdateSendLicenseInfo(A<SendLicenseInfo>.Ignored);

            //Assert
            Assert.IsFalse(result);
        }
*/
        /*

        [Test] //Currently returns False, need to refactor unit test.  
        public void UpdateSendLicenseInfo_ReturnBoolTRUE()
        {
            //Arrange
            var mockLicenseRepository = A.Fake<ILicenseRepository>();
            var mockLicenseProductRepository = A.Fake<ILicenseProductRepository>();
            var mockLicenseNoteRepository = A.Fake<ILicenseNoteRepository>();
            var mockLicenseSequenceRepository = A.Fake<ILicenseSequenceRepository>();
            var mockSearchProvider = A.Fake<ISearchProvider>();
            var mockLicensePRWriterRepository = A.Fake<ILicensePRWriterRepository>();
            var mockLicensePRWriterRateRepository = A.Fake<ILicensePRWriterRateRepository>();
            var mockLicenseProductRecordingRepository = A.Fake<ILicenseProductRecordingRepository>();
            var mockLicensePRWriterRateStatusRepository = A.Fake<ILicensePRWriterRateStatusRepository>();
            var mockLicenseUploadPreviewLicenseRepository = A.Fake<ILicenseUploadPreviewLicenseRepository>();
            var mockRecs = A.Fake<IRecs>();
            var mockRecsDataProvider = A.Fake<IRecsDataProvider>();
            var mockB = A.Fake<UploadGeneratedLicensePreviewRequest>();

            //Build expected
            bool expected = true;

            //Build Request 
            LicenseeLabelGroup llg = new LicenseeLabelGroup { };
            List<SendLicenseContact> contactList = new List<SendLicenseContact>{};
            SendLicenseInfo request = new SendLicenseInfo
            {
                SendLicenseContactList = contactList,
                LicenseId = 99,
                LicenseeId = 99,
                LicenseTemplateId = 99,
            };

            SendLicenseInfo request1 = new SendLicenseInfo{};

            A.CallTo(() => mockLicenseRepository.UpdateSendLicenseInfo(A<SendLicenseInfo>.Ignored)).WithAnyArguments();
            A.CallTo(() => mockLicenseRepository.AddSendLicenseInfo(A<SendLicenseInfo>.Ignored)).WithAnyArguments().Returns(request1);

            //Act
            LicenseManager manager = new LicenseManager(mockLicenseRepository, mockLicenseProductRepository, mockLicenseNoteRepository, mockLicenseSequenceRepository, mockSearchProvider, mockLicensePRWriterRateRepository, mockLicenseProductRecordingRepository, mockLicensePRWriterRepository, mockLicensePRWriterRateStatusRepository, mockLicenseUploadPreviewLicenseRepository, mockRecs, mockRecsDataProvider);
            var result = manager.UpdateSendLicenseInfo(A<SendLicenseInfo>.Ignored);

            //Assert
            Assert.IsTrue(result);
        }
         * 
         * */



    }
}
