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
    public class LicenseProductConfigurationManagerTests
    {
        [Test]
        public void GetLicenseProductConfigurations_ReturnListLicenseProductConfiguration()
        {
            //Arrange
            var mockILicenseRepository = A.Fake<ILicenseRepository>();
            var mockILicenseProductRepository = A.Fake<ILicenseProductRepository>();
            var mockILicenseProductConfigurationRepository = A.Fake<ILicenseProductConfigurationRepository>();
            var mockILicenseProductRecordingRepository = A.Fake<ILicenseProductRecordingRepository>();
            var mockILicensePRWriterNoteRepository = A.Fake<ILicensePRWriterNoteRepository>();
            var mockILicensePRWriterRateRepository = A.Fake<ILicensePRWriterRateRepository>();
            var mockILicensePRWriterRepository = A.Fake<ILicensePRWriterRepository>();
            var mockILicensePRWriterRateStatusRepository = A.Fake<ILicensePRWriterRateStatusRepository>();
            var mockIRecs = A.Fake<IRecs>();
            var mockIRecordingMedleyRepository = A.Fake<IRecordingMedleyRepository>();
            var mockILicenseRecordingMedleyRepository = A.Fake<ILicenseRecordingMedleyRepository>();
            var mockILicenseSolrManager = A.Fake<ILicenseSolrManager>();

            //Build expected
            List<LicenseProductConfiguration> expected = new List<LicenseProductConfiguration> { };
            
            A.CallTo(() => mockILicenseProductConfigurationRepository.GetLicenseProductConfigurations(A<int>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            LicenseProductConfigurationManager manager = new LicenseProductConfigurationManager(mockILicenseRepository, mockILicenseProductRepository, mockILicenseProductConfigurationRepository, mockILicenseProductRecordingRepository, mockILicensePRWriterNoteRepository, mockILicensePRWriterRateRepository, mockILicensePRWriterRepository, mockILicensePRWriterRateStatusRepository, mockIRecs, mockIRecordingMedleyRepository, mockILicenseRecordingMedleyRepository, mockILicenseSolrManager);
            var result =  manager.GetLicenseProductConfigurations(A<int>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetLicenseConfigurationList_ReturnListLicenseProductConfiguration()
        {
            //Arrange
            var mockILicenseRepository = A.Fake<ILicenseRepository>();
            var mockILicenseProductRepository = A.Fake<ILicenseProductRepository>();
            var mockILicenseProductConfigurationRepository = A.Fake<ILicenseProductConfigurationRepository>();
            var mockILicenseProductRecordingRepository = A.Fake<ILicenseProductRecordingRepository>();
            var mockILicensePRWriterNoteRepository = A.Fake<ILicensePRWriterNoteRepository>();
            var mockILicensePRWriterRateRepository = A.Fake<ILicensePRWriterRateRepository>();
            var mockILicensePRWriterRepository = A.Fake<ILicensePRWriterRepository>();
            var mockILicensePRWriterRateStatusRepository = A.Fake<ILicensePRWriterRateStatusRepository>();
            var mockIRecs = A.Fake<IRecs>();
            var mockIRecordingMedleyRepository = A.Fake<IRecordingMedleyRepository>();
            var mockILicenseRecordingMedleyRepository = A.Fake<ILicenseRecordingMedleyRepository>();
            var mockILicenseSolrManager = A.Fake<ILicenseSolrManager>();

            //Build expected
            List<LicenseProductConfiguration> expected = new List<LicenseProductConfiguration> { };

            A.CallTo(() => mockILicenseProductConfigurationRepository.GetLicenseConfigurationList(A<List<int>>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            LicenseProductConfigurationManager manager = new LicenseProductConfigurationManager(mockILicenseRepository, mockILicenseProductRepository, mockILicenseProductConfigurationRepository, mockILicenseProductRecordingRepository, mockILicensePRWriterNoteRepository, mockILicensePRWriterRateRepository, mockILicensePRWriterRepository, mockILicensePRWriterRateStatusRepository, mockIRecs, mockIRecordingMedleyRepository, mockILicenseRecordingMedleyRepository, mockILicenseSolrManager);
            var result = manager.GetLicenseConfigurationList(A<List<int>>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void UpdateLicenseProductConfiguration_ReturnListLicenseProductConfiguration()
        {
            //Arrange
            var mockILicenseRepository = A.Fake<ILicenseRepository>();
            var mockILicenseProductRepository = A.Fake<ILicenseProductRepository>();
            var mockILicenseProductConfigurationRepository = A.Fake<ILicenseProductConfigurationRepository>();
            var mockILicenseProductRecordingRepository = A.Fake<ILicenseProductRecordingRepository>();
            var mockILicensePRWriterNoteRepository = A.Fake<ILicensePRWriterNoteRepository>();
            var mockILicensePRWriterRateRepository = A.Fake<ILicensePRWriterRateRepository>();
            var mockILicensePRWriterRepository = A.Fake<ILicensePRWriterRepository>();
            var mockILicensePRWriterRateStatusRepository = A.Fake<ILicensePRWriterRateStatusRepository>();
            var mockIRecs = A.Fake<IRecs>();
            var mockIRecordingMedleyRepository = A.Fake<IRecordingMedleyRepository>();
            var mockILicenseRecordingMedleyRepository = A.Fake<ILicenseRecordingMedleyRepository>();
            var mockILicenseSolrManager = A.Fake<ILicenseSolrManager>();

            //Build expected
            List<LicenseProductConfiguration> expected = new List<LicenseProductConfiguration> {  };

            //Build request
            List<UpdateLicenseProductConfigurationRequest> request = new List<UpdateLicenseProductConfigurationRequest> {  };
       
            //Act
            LicenseProductConfigurationManager manager = new LicenseProductConfigurationManager(mockILicenseRepository, mockILicenseProductRepository, mockILicenseProductConfigurationRepository, mockILicenseProductRecordingRepository, mockILicensePRWriterNoteRepository, mockILicensePRWriterRateRepository, mockILicensePRWriterRepository, mockILicensePRWriterRateStatusRepository, mockIRecs, mockIRecordingMedleyRepository, mockILicenseRecordingMedleyRepository, mockILicenseSolrManager);
            var result = manager.UpdateLicenseProductConfiguration(request);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void AddLicenseProductConfiguration_ReturUpdateLicenseProductConfigurationResult()
        {
            //Arrange
            var mockILicenseRepository = A.Fake<ILicenseRepository>();
            var mockILicenseProductRepository = A.Fake<ILicenseProductRepository>();
            var mockILicenseProductConfigurationRepository = A.Fake<ILicenseProductConfigurationRepository>();
            var mockILicenseProductRecordingRepository = A.Fake<ILicenseProductRecordingRepository>();
            var mockILicensePRWriterNoteRepository = A.Fake<ILicensePRWriterNoteRepository>();
            var mockILicensePRWriterRateRepository = A.Fake<ILicensePRWriterRateRepository>();
            var mockILicensePRWriterRepository = A.Fake<ILicensePRWriterRepository>();
            var mockILicensePRWriterRateStatusRepository = A.Fake<ILicensePRWriterRateStatusRepository>();
            var mockIRecs = A.Fake<IRecs>();
            var mockIRecordingMedleyRepository = A.Fake<IRecordingMedleyRepository>();
            var mockILicenseRecordingMedleyRepository = A.Fake<ILicenseRecordingMedleyRepository>();
            var mockILicenseSolrManager = A.Fake<ILicenseSolrManager>();

            //Build expected
            UpdateLicenseProductConfigurationResult expected = new UpdateLicenseProductConfigurationResult { };

            //Build request
            UpdateLicenseProductConfigurationRequest request = new UpdateLicenseProductConfigurationRequest { productId = 99 };

            ProductHeader productHeader = new ProductHeader { };

            A.CallTo(() => mockIRecs.RetrieveProductHeader(request.productId)).WithAnyArguments().Returns(productHeader);

            //Act
            LicenseProductConfigurationManager manager = new LicenseProductConfigurationManager(mockILicenseRepository, mockILicenseProductRepository, mockILicenseProductConfigurationRepository, mockILicenseProductRecordingRepository, mockILicensePRWriterNoteRepository, mockILicensePRWriterRateRepository, mockILicensePRWriterRepository, mockILicensePRWriterRateStatusRepository, mockIRecs, mockIRecordingMedleyRepository, mockILicenseRecordingMedleyRepository, mockILicenseSolrManager);
            var result = manager.AddLicenseProductConfiguration(request);

            //Assert
            Assert.IsInstanceOf(typeof(UpdateLicenseProductConfigurationResult), result);
        }

        [Test]
        public void DeleteLicenseProductConfiguration_ReturnBoolTRUE()
        {
            //Arrange
            var mockILicenseRepository = A.Fake<ILicenseRepository>();
            var mockILicenseProductRepository = A.Fake<ILicenseProductRepository>();
            var mockILicenseProductConfigurationRepository = A.Fake<ILicenseProductConfigurationRepository>();
            var mockILicenseProductRecordingRepository = A.Fake<ILicenseProductRecordingRepository>();
            var mockILicensePRWriterNoteRepository = A.Fake<ILicensePRWriterNoteRepository>();
            var mockILicensePRWriterRateRepository = A.Fake<ILicensePRWriterRateRepository>();
            var mockILicensePRWriterRepository = A.Fake<ILicensePRWriterRepository>();
            var mockILicensePRWriterRateStatusRepository = A.Fake<ILicensePRWriterRateStatusRepository>();
            var mockIRecs = A.Fake<IRecs>();
            var mockIRecordingMedleyRepository = A.Fake<IRecordingMedleyRepository>();
            var mockILicenseRecordingMedleyRepository = A.Fake<ILicenseRecordingMedleyRepository>();
            var mockILicenseSolrManager = A.Fake<ILicenseSolrManager>();

            //Build expected
            UpdateLicenseProductConfigurationResult expected = new UpdateLicenseProductConfigurationResult { };

            //Build request
            UpdateLicenseProductConfigurationRequest request = new UpdateLicenseProductConfigurationRequest { productId = 99, licenseProductConfigurationId =99 };
            LicenseProductConfiguration lpc = new LicenseProductConfiguration { };

            ProductHeader productHeader = new ProductHeader { };

            A.CallTo(() => mockILicenseProductConfigurationRepository.Get(request.licenseProductConfigurationId)).WithAnyArguments().Returns(lpc);

            //Act
            LicenseProductConfigurationManager manager = new LicenseProductConfigurationManager(mockILicenseRepository, mockILicenseProductRepository, mockILicenseProductConfigurationRepository, mockILicenseProductRecordingRepository, mockILicensePRWriterNoteRepository, mockILicensePRWriterRateRepository, mockILicensePRWriterRepository, mockILicensePRWriterRateStatusRepository, mockIRecs, mockIRecordingMedleyRepository, mockILicenseRecordingMedleyRepository, mockILicenseSolrManager);
            var result = manager.DeleteLicenseProductConfiguration(request);

            //Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void UpdateLicenseProductRollups_ReturnVoid()
        {
            //Arrange
            var mockILicenseRepository = A.Fake<ILicenseRepository>();
            var mockILicenseProductRepository = A.Fake<ILicenseProductRepository>();
            var mockILicenseProductConfigurationRepository = A.Fake<ILicenseProductConfigurationRepository>();
            var mockILicenseProductRecordingRepository = A.Fake<ILicenseProductRecordingRepository>();
            var mockILicensePRWriterNoteRepository = A.Fake<ILicensePRWriterNoteRepository>();
            var mockILicensePRWriterRateRepository = A.Fake<ILicensePRWriterRateRepository>();
            var mockILicensePRWriterRepository = A.Fake<ILicensePRWriterRepository>();
            var mockILicensePRWriterRateStatusRepository = A.Fake<ILicensePRWriterRateStatusRepository>();
            var mockIRecs = A.Fake<IRecs>();
            var mockIRecordingMedleyRepository = A.Fake<IRecordingMedleyRepository>();
            var mockILicenseRecordingMedleyRepository = A.Fake<ILicenseRecordingMedleyRepository>();
            var mockILicenseSolrManager = A.Fake<ILicenseSolrManager>();

            //Build expected
            UpdateLicenseProductConfigurationResult expected = new UpdateLicenseProductConfigurationResult { };

            //Build request
           const int request = 99;

            A.CallTo(() => mockILicenseProductRepository.GetAllLicenseProducts(request)).WithAnyArguments();//.Returns(lpc);

            //Act
            LicenseProductConfigurationManager manager = new LicenseProductConfigurationManager(mockILicenseRepository, mockILicenseProductRepository, mockILicenseProductConfigurationRepository, mockILicenseProductRecordingRepository, mockILicensePRWriterNoteRepository, mockILicensePRWriterRateRepository, mockILicensePRWriterRepository, mockILicensePRWriterRateStatusRepository, mockIRecs, mockIRecordingMedleyRepository, mockILicenseRecordingMedleyRepository, mockILicenseSolrManager);
            manager.UpdateLicenseProductRollups(request);
            mockILicenseProductRepository.GetAllLicenseProducts(request);

            //Assert
            A.CallTo(() => mockILicenseProductRepository.GetAllLicenseProducts(request)).WithAnyArguments().MustHaveHappened();
        }


        [Test]
        public void UpdateLicenseProductRollups2_ReturnVoid()
        {
            //Arrange
            var mockILicenseRepository = A.Fake<ILicenseRepository>();
            var mockILicenseProductRepository = A.Fake<ILicenseProductRepository>();
            var mockILicenseProductConfigurationRepository = A.Fake<ILicenseProductConfigurationRepository>();
            var mockILicenseProductRecordingRepository = A.Fake<ILicenseProductRecordingRepository>();
            var mockILicensePRWriterNoteRepository = A.Fake<ILicensePRWriterNoteRepository>();
            var mockILicensePRWriterRateRepository = A.Fake<ILicensePRWriterRateRepository>();
            var mockILicensePRWriterRepository = A.Fake<ILicensePRWriterRepository>();
            var mockILicensePRWriterRateStatusRepository = A.Fake<ILicensePRWriterRateStatusRepository>();
            var mockIRecs = A.Fake<IRecs>();
            var mockIRecordingMedleyRepository = A.Fake<IRecordingMedleyRepository>();
            var mockILicenseRecordingMedleyRepository = A.Fake<ILicenseRecordingMedleyRepository>();
            var mockILicenseSolrManager = A.Fake<ILicenseSolrManager>();

            //Build expected
            UpdateLicenseProductConfigurationResult expected = new UpdateLicenseProductConfigurationResult { };

            //Build request
            const int request = 99;

            A.CallTo(() => mockILicenseProductRepository.GetAllLicenseProducts(request)).WithAnyArguments();

            //Act
            LicenseProductConfigurationManager manager = new LicenseProductConfigurationManager(mockILicenseRepository, mockILicenseProductRepository, mockILicenseProductConfigurationRepository, mockILicenseProductRecordingRepository, mockILicensePRWriterNoteRepository, mockILicensePRWriterRateRepository, mockILicensePRWriterRepository, mockILicensePRWriterRateStatusRepository, mockIRecs, mockIRecordingMedleyRepository, mockILicenseRecordingMedleyRepository, mockILicenseSolrManager);
            manager.UpdateLicenseProductRollups2(request);
            mockILicenseProductRepository.GetAllLicenseProducts(request);

            //Assert
            A.CallTo(() => mockILicenseProductRepository.GetAllLicenseProducts(request)).WithAnyArguments().MustHaveHappened();
        }

        [Test]
        public void UpdateAllLicensesConfiguration_ReturnVoid()
        {
            //Arrange
            var mockILicenseRepository = A.Fake<ILicenseRepository>();
            var mockILicenseProductRepository = A.Fake<ILicenseProductRepository>();
            var mockILicenseProductConfigurationRepository = A.Fake<ILicenseProductConfigurationRepository>();
            var mockILicenseProductRecordingRepository = A.Fake<ILicenseProductRecordingRepository>();
            var mockILicensePRWriterNoteRepository = A.Fake<ILicensePRWriterNoteRepository>();
            var mockILicensePRWriterRateRepository = A.Fake<ILicensePRWriterRateRepository>();
            var mockILicensePRWriterRepository = A.Fake<ILicensePRWriterRepository>();
            var mockILicensePRWriterRateStatusRepository = A.Fake<ILicensePRWriterRateStatusRepository>();
            var mockIRecs = A.Fake<IRecs>();
            var mockIRecordingMedleyRepository = A.Fake<IRecordingMedleyRepository>();
            var mockILicenseRecordingMedleyRepository = A.Fake<ILicenseRecordingMedleyRepository>();
            var mockILicenseSolrManager = A.Fake<ILicenseSolrManager>();

            //Build expected
            UpdateLicenseProductConfigurationResult expected = new UpdateLicenseProductConfigurationResult { };
            List<License> licenses = new List<License> { };
            //Build request
            const int request = 99;
            const int request1 = 99;
            A.CallTo(() => mockILicenseRepository.GetAll(request, request1)).WithAnyArguments().Returns(licenses);

            //Act
            LicenseProductConfigurationManager manager = new LicenseProductConfigurationManager(mockILicenseRepository, mockILicenseProductRepository, mockILicenseProductConfigurationRepository, mockILicenseProductRecordingRepository, mockILicensePRWriterNoteRepository, mockILicensePRWriterRateRepository, mockILicensePRWriterRepository, mockILicensePRWriterRateStatusRepository, mockIRecs, mockIRecordingMedleyRepository, mockILicenseRecordingMedleyRepository, mockILicenseSolrManager);
            manager.UpdateAllLicensesConfiguration(request, request1);
            mockILicenseRepository.GetAll(request, request1);

            //Assert
            A.CallTo(() => mockILicenseRepository.GetAll(request, request1)).WithAnyArguments().MustHaveHappened();
        }
    }
}
