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
using UMPG.USL.API.Data.LookupData;
using UMPG.USL.Models.LookupModel;
using UMPG.USL.Models.StaticDropdownsData;
using UMPG.USL.Models.LicenseTemplateModel;


namespace UMPG.USL.API.Tests.Manager_Tests.Licenses
{
    [TestFixture]
    public class LicenseProductManagerTests
    {
        [Test]
        public void GetProductConfigurationsAll_ReturnListProductConfiguration()
        {
            //Arrange
            var mockILicenseRepository = A.Fake<ILicenseRepository>();
            var mockILicenseSequenceRepository = A.Fake<ILicenseSequenceRepository>();
            var mockILicenseProductRepository = A.Fake<ILicenseProductRepository>();
            var mockILicenseProductRecordingRepository = A.Fake<ILicenseProductRecordingRepository>();
            var mockILicensePRWriterRepository = A.Fake<ILicensePRWriterRepository>();
            var mockILicensePRWriterRateRepository = A.Fake<ILicensePRWriterRateRepository>();
            var mockILicensePRWriterNoteRepository = A.Fake<ILicensePRWriterNoteRepository>();
            var mockILicensePRWriterRateStatusRepository = A.Fake<ILicensePRWriterRateStatusRepository>();
            var mockILicenseProductConfigurationRepository = A.Fake<ILicenseProductConfigurationRepository>();
            var mockIRecsDataProvider = A.Fake<IRecsDataProvider>();
            var mockILicenseNoteRepository = A.Fake<ILicenseNoteRepository>();
            var mockIAgreementStatutoryRateRepository = A.Fake<IAgreementStatutoryRateRepository>();
            var mockIStatRateRepository = A.Fake<IStatRateRepository>();
            var mockILicenseSolrManager = A.Fake<ILicenseSolrManager>();
            var mockILicenseAttachmentManager = A.Fake<ILicenseAttachmentManager>();
            var mockIRecordingMedleyRepository = A.Fake<IRecordingMedleyRepository>(); var mockIRecRepository = A.Fake<IRecs>();

            //Build expected
            List<ProductConfiguration> expected = new List<ProductConfiguration> { };
            LicenseProduct lp = new LicenseProduct { };
            //build request
            List<int> productIds = new List<int>{};
            
            GetProductConfigurationsAllRequest request = new GetProductConfigurationsAllRequest { ProductIds = productIds, LicenseId = 99 };

            A.CallTo(() => mockILicenseProductRepository.GetLicenseProduct(A<int>.Ignored, A<int>.Ignored)).WithAnyArguments().Returns(lp);
            
            //Act
            LicenseProductManager manager = new LicenseProductManager(mockILicenseRepository, mockILicenseSequenceRepository, mockILicenseProductRepository, mockILicenseProductRecordingRepository, mockILicensePRWriterRepository, mockILicensePRWriterRateRepository, mockILicensePRWriterNoteRepository, mockILicensePRWriterRateStatusRepository, mockILicenseProductConfigurationRepository, mockIRecsDataProvider, mockILicenseNoteRepository, mockIAgreementStatutoryRateRepository, mockIStatRateRepository, mockILicenseSolrManager, mockILicenseAttachmentManager, mockIRecordingMedleyRepository, mockIRecRepository);
            var result = manager.GetProductConfigurationsAll(request);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void UpdateProductConfigurationsAll_ReturnBoolFALSE()
        {
            //Arrange
            var mockILicenseRepository = A.Fake<ILicenseRepository>();
            var mockILicenseSequenceRepository = A.Fake<ILicenseSequenceRepository>();
            var mockILicenseProductRepository = A.Fake<ILicenseProductRepository>();
            var mockILicenseProductRecordingRepository = A.Fake<ILicenseProductRecordingRepository>();
            var mockILicensePRWriterRepository = A.Fake<ILicensePRWriterRepository>();
            var mockILicensePRWriterRateRepository = A.Fake<ILicensePRWriterRateRepository>();
            var mockILicensePRWriterNoteRepository = A.Fake<ILicensePRWriterNoteRepository>();
            var mockILicensePRWriterRateStatusRepository = A.Fake<ILicensePRWriterRateStatusRepository>();
            var mockILicenseProductConfigurationRepository = A.Fake<ILicenseProductConfigurationRepository>();
            var mockIRecsDataProvider = A.Fake<IRecsDataProvider>();
            var mockILicenseNoteRepository = A.Fake<ILicenseNoteRepository>();
            var mockIAgreementStatutoryRateRepository = A.Fake<IAgreementStatutoryRateRepository>();
            var mockIStatRateRepository = A.Fake<IStatRateRepository>();
            var mockILicenseSolrManager = A.Fake<ILicenseSolrManager>();
            var mockILicenseAttachmentManager = A.Fake<ILicenseAttachmentManager>();
            var mockIRecordingMedleyRepository = A.Fake<IRecordingMedleyRepository>(); var mockIRecRepository = A.Fake<IRecs>();

            //Build expected
            List<ProductConfiguration> expected = new List<ProductConfiguration> { };
            LicenseProduct lp = new LicenseProduct { };
            //build request
            List<int> productIds = new List<int> { };

            List<UpdateProductConfigurationsAllRequest> request = new List<UpdateProductConfigurationsAllRequest> {  };
            UpdateProductConfigurationsAllRequest request1 = new UpdateProductConfigurationsAllRequest { ProductId = 99, LicenseId = 99 };
            request.Add(request1);

            A.CallTo(() => mockILicenseProductRepository.GetLicenseProduct(A<int>.Ignored, A<int>.Ignored)).WithAnyArguments().Returns(lp);

            //Act
            LicenseProductManager manager = new LicenseProductManager(mockILicenseRepository, mockILicenseSequenceRepository, mockILicenseProductRepository, mockILicenseProductRecordingRepository, mockILicensePRWriterRepository, mockILicensePRWriterRateRepository, mockILicensePRWriterNoteRepository, mockILicensePRWriterRateStatusRepository, mockILicenseProductConfigurationRepository, mockIRecsDataProvider, mockILicenseNoteRepository, mockIAgreementStatutoryRateRepository, mockIStatRateRepository, mockILicenseSolrManager, mockILicenseAttachmentManager, mockIRecordingMedleyRepository, mockIRecRepository);
            var result = manager.UpdateProductConfigurationsAll(request);

            //Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void DeleteLicenseProduct_ReturnBoolFALSE()
        {
            //Arrange
            var mockILicenseRepository = A.Fake<ILicenseRepository>();
            var mockILicenseSequenceRepository = A.Fake<ILicenseSequenceRepository>();
            var mockILicenseProductRepository = A.Fake<ILicenseProductRepository>();
            var mockILicenseProductRecordingRepository = A.Fake<ILicenseProductRecordingRepository>();
            var mockILicensePRWriterRepository = A.Fake<ILicensePRWriterRepository>();
            var mockILicensePRWriterRateRepository = A.Fake<ILicensePRWriterRateRepository>();
            var mockILicensePRWriterNoteRepository = A.Fake<ILicensePRWriterNoteRepository>();
            var mockILicensePRWriterRateStatusRepository = A.Fake<ILicensePRWriterRateStatusRepository>();
            var mockILicenseProductConfigurationRepository = A.Fake<ILicenseProductConfigurationRepository>();
            var mockIRecsDataProvider = A.Fake<IRecsDataProvider>();
            var mockILicenseNoteRepository = A.Fake<ILicenseNoteRepository>();
            var mockIAgreementStatutoryRateRepository = A.Fake<IAgreementStatutoryRateRepository>();
            var mockIStatRateRepository = A.Fake<IStatRateRepository>();
            var mockILicenseSolrManager = A.Fake<ILicenseSolrManager>();
            var mockILicenseAttachmentManager = A.Fake<ILicenseAttachmentManager>();
            var mockIRecordingMedleyRepository = A.Fake<IRecordingMedleyRepository>(); var mockIRecRepository = A.Fake<IRecs>();

            //Build expected
            List<ProductConfiguration> expected = new List<ProductConfiguration> { };
            LicenseProduct lp = new LicenseProduct { };
            //build request
            List<int> productIds = new List<int> { };

            List<UpdateProductConfigurationsAllRequest> request = new List<UpdateProductConfigurationsAllRequest> { };
            UpdateProductConfigurationsAllRequest request1 = new UpdateProductConfigurationsAllRequest { ProductId = 99, LicenseId = 99 };
            request.Add(request1);

            A.CallTo(() => mockILicenseProductRepository.GetLicenseProduct(A<int>.Ignored, A<int>.Ignored)).WithAnyArguments().Returns(lp);

            //Act
            LicenseProductManager manager = new LicenseProductManager(mockILicenseRepository, mockILicenseSequenceRepository, mockILicenseProductRepository, mockILicenseProductRecordingRepository, mockILicensePRWriterRepository, mockILicensePRWriterRateRepository, mockILicensePRWriterNoteRepository, mockILicensePRWriterRateStatusRepository, mockILicenseProductConfigurationRepository, mockIRecsDataProvider, mockILicenseNoteRepository, mockIAgreementStatutoryRateRepository, mockIStatRateRepository, mockILicenseSolrManager, mockILicenseAttachmentManager, mockIRecordingMedleyRepository, mockIRecRepository);
            var result = manager.UpdateProductConfigurationsAll(request);

            //Assert
            Assert.IsFalse(result);
        }


        [Test]
        public void UpdateLicenseProductRollups_ReturnVoid()
        {
            //Arrange
            var mockILicenseRepository = A.Fake<ILicenseRepository>();
            var mockILicenseSequenceRepository = A.Fake<ILicenseSequenceRepository>();
            var mockILicenseProductRepository = A.Fake<ILicenseProductRepository>();
            var mockILicenseProductRecordingRepository = A.Fake<ILicenseProductRecordingRepository>();
            var mockILicensePRWriterRepository = A.Fake<ILicensePRWriterRepository>();
            var mockILicensePRWriterRateRepository = A.Fake<ILicensePRWriterRateRepository>();
            var mockILicensePRWriterNoteRepository = A.Fake<ILicensePRWriterNoteRepository>();
            var mockILicensePRWriterRateStatusRepository = A.Fake<ILicensePRWriterRateStatusRepository>();
            var mockILicenseProductConfigurationRepository = A.Fake<ILicenseProductConfigurationRepository>();
            var mockIRecsDataProvider = A.Fake<IRecsDataProvider>();
            var mockILicenseNoteRepository = A.Fake<ILicenseNoteRepository>();
            var mockIAgreementStatutoryRateRepository = A.Fake<IAgreementStatutoryRateRepository>();
            var mockIStatRateRepository = A.Fake<IStatRateRepository>();
            var mockILicenseSolrManager = A.Fake<ILicenseSolrManager>();
            var mockILicenseAttachmentManager = A.Fake<ILicenseAttachmentManager>();
            var mockIRecordingMedleyRepository = A.Fake<IRecordingMedleyRepository>(); var mockIRecRepository = A.Fake<IRecs>();

            //Build expected
            List<ProductConfiguration> expected = new List<ProductConfiguration> { };
            List<LicenseProduct> lp = new List<LicenseProduct> { };
            //build request
            List<int> productIds = new List<int> { };

            const int request = 99;

            A.CallTo(() => mockILicenseProductRepository.GetLicenseProducts(A<int>.Ignored)).WithAnyArguments().Returns(lp);

            //Act
            LicenseProductManager manager = new LicenseProductManager(mockILicenseRepository, mockILicenseSequenceRepository, mockILicenseProductRepository, mockILicenseProductRecordingRepository, mockILicensePRWriterRepository, mockILicensePRWriterRateRepository, mockILicensePRWriterNoteRepository, mockILicensePRWriterRateStatusRepository, mockILicenseProductConfigurationRepository, mockIRecsDataProvider, mockILicenseNoteRepository, mockIAgreementStatutoryRateRepository, mockIStatRateRepository, mockILicenseSolrManager, mockILicenseAttachmentManager, mockIRecordingMedleyRepository, mockIRecRepository);
             manager.UpdateLicenseProductRollups(request);

            //Assert
            A.CallTo(() => mockILicenseProductRepository.GetLicenseProducts(A<int>.Ignored)).WithAnyArguments().MustHaveHappened();
        }


        [Test]
        public void GetSelectedProduct_ReturnVoid()
        {
            //Arrange
            var mockILicenseRepository = A.Fake<ILicenseRepository>();
            var mockILicenseSequenceRepository = A.Fake<ILicenseSequenceRepository>();
            var mockILicenseProductRepository = A.Fake<ILicenseProductRepository>();
            var mockILicenseProductRecordingRepository = A.Fake<ILicenseProductRecordingRepository>();
            var mockILicensePRWriterRepository = A.Fake<ILicensePRWriterRepository>();
            var mockILicensePRWriterRateRepository = A.Fake<ILicensePRWriterRateRepository>();
            var mockILicensePRWriterNoteRepository = A.Fake<ILicensePRWriterNoteRepository>();
            var mockILicensePRWriterRateStatusRepository = A.Fake<ILicensePRWriterRateStatusRepository>();
            var mockILicenseProductConfigurationRepository = A.Fake<ILicenseProductConfigurationRepository>();
            var mockIRecsDataProvider = A.Fake<IRecsDataProvider>();
            var mockILicenseNoteRepository = A.Fake<ILicenseNoteRepository>();
            var mockIAgreementStatutoryRateRepository = A.Fake<IAgreementStatutoryRateRepository>();
            var mockIStatRateRepository = A.Fake<IStatRateRepository>();
            var mockILicenseSolrManager = A.Fake<ILicenseSolrManager>();
            var mockILicenseAttachmentManager = A.Fake<ILicenseAttachmentManager>();
            var mockIRecordingMedleyRepository = A.Fake<IRecordingMedleyRepository>(); var mockIRecRepository = A.Fake<IRecs>();

            //Build expected
            LicenseProduct expected = new LicenseProduct { };
            LicenseProduct lp = new LicenseProduct { };
            List<RecsConfiguration> configs = new List<RecsConfiguration> { };
            ProductHeader ph = new ProductHeader { Configurations = configs };
            LicenseProduct licenseProduct = new LicenseProduct { ProductHeader = ph, ProductId = 99 };

            
            //build request
            const int request = 99;
            const int request1 = 99;

            A.CallTo(() => mockILicenseProductRepository.GetLicenseProduct(request,request1)).WithAnyArguments().Returns(lp);
            A.CallTo(() => mockIRecsDataProvider.RetrieveProductHeader(licenseProduct.ProductId)).WithAnyArguments().Returns(ph);

            //Act
            LicenseProductManager manager = new LicenseProductManager(mockILicenseRepository, mockILicenseSequenceRepository, mockILicenseProductRepository, mockILicenseProductRecordingRepository, mockILicensePRWriterRepository, mockILicensePRWriterRateRepository, mockILicensePRWriterNoteRepository, mockILicensePRWriterRateStatusRepository, mockILicenseProductConfigurationRepository, mockIRecsDataProvider, mockILicenseNoteRepository, mockIAgreementStatutoryRateRepository, mockIStatRateRepository, mockILicenseSolrManager, mockILicenseAttachmentManager, mockIRecordingMedleyRepository, mockIRecRepository);
            manager.GetSelectedProduct(request, request1);

            //Assert
            A.CallTo(() => mockILicenseProductRepository.GetLicenseProduct(request, request1)).WithAnyArguments().MustHaveHappened();
        }

        [Test]
        public void GetLicenseProductConfigurationIdTotals_ReturnListLicenseProductConfigurationTotals()
        {
            //Arrange
            var mockILicenseRepository = A.Fake<ILicenseRepository>();
            var mockILicenseSequenceRepository = A.Fake<ILicenseSequenceRepository>();
            var mockILicenseProductRepository = A.Fake<ILicenseProductRepository>();
            var mockILicenseProductRecordingRepository = A.Fake<ILicenseProductRecordingRepository>();
            var mockILicensePRWriterRepository = A.Fake<ILicensePRWriterRepository>();
            var mockILicensePRWriterRateRepository = A.Fake<ILicensePRWriterRateRepository>();
            var mockILicensePRWriterNoteRepository = A.Fake<ILicensePRWriterNoteRepository>();
            var mockILicensePRWriterRateStatusRepository = A.Fake<ILicensePRWriterRateStatusRepository>();
            var mockILicenseProductConfigurationRepository = A.Fake<ILicenseProductConfigurationRepository>();
            var mockIRecsDataProvider = A.Fake<IRecsDataProvider>();
            var mockILicenseNoteRepository = A.Fake<ILicenseNoteRepository>();
            var mockIAgreementStatutoryRateRepository = A.Fake<IAgreementStatutoryRateRepository>();
            var mockIStatRateRepository = A.Fake<IStatRateRepository>();
            var mockILicenseSolrManager = A.Fake<ILicenseSolrManager>();
            var mockILicenseAttachmentManager = A.Fake<ILicenseAttachmentManager>();
            var mockIRecordingMedleyRepository = A.Fake<IRecordingMedleyRepository>(); var mockIRecRepository = A.Fake<IRecs>();

            //Build expected
            List<LicenseProductConfigurationTotals> expected = new List<LicenseProductConfigurationTotals> { };
            List<int> lp = new List<int> { };
            List<RecsConfiguration> configs = new List<RecsConfiguration> { };
            ProductHeader ph = new ProductHeader { Configurations = configs };
            LicenseProduct licenseProduct = new LicenseProduct { ProductHeader = ph, ProductId = 99 };
            
            //build request
            const int request = 99;
            
            A.CallTo(() => mockILicenseProductConfigurationRepository.GetLicenseProductConfigurationIds(request)).WithAnyArguments().Returns(lp);
        
            //Act
            LicenseProductManager manager = new LicenseProductManager(mockILicenseRepository, mockILicenseSequenceRepository, mockILicenseProductRepository, mockILicenseProductRecordingRepository, mockILicensePRWriterRepository, mockILicensePRWriterRateRepository, mockILicensePRWriterNoteRepository, mockILicensePRWriterRateStatusRepository, mockILicenseProductConfigurationRepository, mockIRecsDataProvider, mockILicenseNoteRepository, mockIAgreementStatutoryRateRepository, mockIStatRateRepository, mockILicenseSolrManager, mockILicenseAttachmentManager, mockIRecordingMedleyRepository, mockIRecRepository);
            var result =  manager.GetLicenseProductConfigurationIdTotals(request);

            //Assert
            Assert.AreEqual(expected, result);
        }


        [Test]
        public void GetProducts_ReturnListLicenseProduct()
        {
            //Arrange
            var mockILicenseRepository = A.Fake<ILicenseRepository>();
            var mockILicenseSequenceRepository = A.Fake<ILicenseSequenceRepository>();
            var mockILicenseProductRepository = A.Fake<ILicenseProductRepository>();
            var mockILicenseProductRecordingRepository = A.Fake<ILicenseProductRecordingRepository>();
            var mockILicensePRWriterRepository = A.Fake<ILicensePRWriterRepository>();
            var mockILicensePRWriterRateRepository = A.Fake<ILicensePRWriterRateRepository>();
            var mockILicensePRWriterNoteRepository = A.Fake<ILicensePRWriterNoteRepository>();
            var mockILicensePRWriterRateStatusRepository = A.Fake<ILicensePRWriterRateStatusRepository>();
            var mockILicenseProductConfigurationRepository = A.Fake<ILicenseProductConfigurationRepository>();
            var mockIRecsDataProvider = A.Fake<IRecsDataProvider>();
            var mockILicenseNoteRepository = A.Fake<ILicenseNoteRepository>();
            var mockIAgreementStatutoryRateRepository = A.Fake<IAgreementStatutoryRateRepository>();
            var mockIStatRateRepository = A.Fake<IStatRateRepository>();
            var mockILicenseSolrManager = A.Fake<ILicenseSolrManager>();
            var mockILicenseAttachmentManager = A.Fake<ILicenseAttachmentManager>();
            var mockIRecordingMedleyRepository = A.Fake<IRecordingMedleyRepository>(); var mockIRecRepository = A.Fake<IRecs>();

            //Build expected
            List<LicenseProductConfigurationTotals> expected = new List<LicenseProductConfigurationTotals> { };
            List<int> lp = new List<int> { };
            List<RecsConfiguration> configs = new List<RecsConfiguration> { };
            ProductHeader ph = new ProductHeader { Configurations = configs };
            LicenseProduct licenseProduct = new LicenseProduct { ProductHeader = ph, ProductId = 99 };

            //build request
            const int request = 99;
            
            A.CallTo(() => mockILicenseProductConfigurationRepository.GetLicenseProductConfigurationIds(request)).WithAnyArguments().Returns(lp);

            //Act
            LicenseProductManager manager = new LicenseProductManager(mockILicenseRepository, mockILicenseSequenceRepository, mockILicenseProductRepository, mockILicenseProductRecordingRepository, mockILicensePRWriterRepository, mockILicensePRWriterRateRepository, mockILicensePRWriterNoteRepository, mockILicensePRWriterRateStatusRepository, mockILicenseProductConfigurationRepository, mockIRecsDataProvider, mockILicenseNoteRepository, mockIAgreementStatutoryRateRepository, mockIStatRateRepository, mockILicenseSolrManager, mockILicenseAttachmentManager, mockIRecordingMedleyRepository, mockIRecRepository);
            var result = manager.GetProducts(request);

            //Assert
            Assert.AreEqual(expected, result);
        }


        [Test]
        public void GetProductsV2_ReturnListLicenseProduct()
        {
            //Arrange
            var mockILicenseRepository = A.Fake<ILicenseRepository>();
            var mockILicenseSequenceRepository = A.Fake<ILicenseSequenceRepository>();
            var mockILicenseProductRepository = A.Fake<ILicenseProductRepository>();
            var mockILicenseProductRecordingRepository = A.Fake<ILicenseProductRecordingRepository>();
            var mockILicensePRWriterRepository = A.Fake<ILicensePRWriterRepository>();
            var mockILicensePRWriterRateRepository = A.Fake<ILicensePRWriterRateRepository>();
            var mockILicensePRWriterNoteRepository = A.Fake<ILicensePRWriterNoteRepository>();
            var mockILicensePRWriterRateStatusRepository = A.Fake<ILicensePRWriterRateStatusRepository>();
            var mockILicenseProductConfigurationRepository = A.Fake<ILicenseProductConfigurationRepository>();
            var mockIRecsDataProvider = A.Fake<IRecsDataProvider>();
            var mockILicenseNoteRepository = A.Fake<ILicenseNoteRepository>();
            var mockIAgreementStatutoryRateRepository = A.Fake<IAgreementStatutoryRateRepository>();
            var mockIStatRateRepository = A.Fake<IStatRateRepository>();
            var mockILicenseSolrManager = A.Fake<ILicenseSolrManager>();
            var mockILicenseAttachmentManager = A.Fake<ILicenseAttachmentManager>();
            var mockIRecordingMedleyRepository = A.Fake<IRecordingMedleyRepository>(); var mockIRecRepository = A.Fake<IRecs>();

            //Build expected
            List<LicenseProductConfigurationTotals> expected = new List<LicenseProductConfigurationTotals> { };
            List<int> lp = new List<int> { };
            List<RecsConfiguration> configs = new List<RecsConfiguration> { };
            ProductHeader ph = new ProductHeader { Configurations = configs };
            LicenseProduct licenseProduct = new LicenseProduct { ProductHeader = ph, ProductId = 99 };

            //build request
            const int request = 99;
            
            A.CallTo(() => mockILicenseProductConfigurationRepository.GetLicenseProductConfigurationIds(request)).WithAnyArguments().Returns(lp);

            //Act
            LicenseProductManager manager = new LicenseProductManager(mockILicenseRepository, mockILicenseSequenceRepository, mockILicenseProductRepository, mockILicenseProductRecordingRepository, mockILicensePRWriterRepository, mockILicensePRWriterRateRepository, mockILicensePRWriterNoteRepository, mockILicensePRWriterRateStatusRepository, mockILicenseProductConfigurationRepository, mockIRecsDataProvider, mockILicenseNoteRepository, mockIAgreementStatutoryRateRepository, mockIStatRateRepository, mockILicenseSolrManager, mockILicenseAttachmentManager, mockIRecordingMedleyRepository, mockIRecRepository);
            var result = manager.GetProductsV2(request);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetAllLicenseRelatedIds_ReturnGetWritersRatesRequest()
        {
            //Arrange
            var mockILicenseRepository = A.Fake<ILicenseRepository>();
            var mockILicenseSequenceRepository = A.Fake<ILicenseSequenceRepository>();
            var mockILicenseProductRepository = A.Fake<ILicenseProductRepository>();
            var mockILicenseProductRecordingRepository = A.Fake<ILicenseProductRecordingRepository>();
            var mockILicensePRWriterRepository = A.Fake<ILicensePRWriterRepository>();
            var mockILicensePRWriterRateRepository = A.Fake<ILicensePRWriterRateRepository>();
            var mockILicensePRWriterNoteRepository = A.Fake<ILicensePRWriterNoteRepository>();
            var mockILicensePRWriterRateStatusRepository = A.Fake<ILicensePRWriterRateStatusRepository>();
            var mockILicenseProductConfigurationRepository = A.Fake<ILicenseProductConfigurationRepository>();
            var mockIRecsDataProvider = A.Fake<IRecsDataProvider>();
            var mockILicenseNoteRepository = A.Fake<ILicenseNoteRepository>();
            var mockIAgreementStatutoryRateRepository = A.Fake<IAgreementStatutoryRateRepository>();
            var mockIStatRateRepository = A.Fake<IStatRateRepository>();
            var mockILicenseSolrManager = A.Fake<ILicenseSolrManager>();
            var mockILicenseAttachmentManager = A.Fake<ILicenseAttachmentManager>();
            var mockIRecordingMedleyRepository = A.Fake<IRecordingMedleyRepository>(); var mockIRecRepository = A.Fake<IRecs>();

            //Build expected
            GetWritersRatesRequest expected = new GetWritersRatesRequest { };
            List<int> lp = new List<int> { };
            List<RecsConfiguration> configs = new List<RecsConfiguration> { };
            ProductHeader ph = new ProductHeader { Configurations = configs };
            LicenseProduct licenseProduct = new LicenseProduct { ProductHeader = ph, ProductId = 99 };

            //build request
            const int request = 99;
            
            A.CallTo(() => mockILicenseProductConfigurationRepository.GetLicenseProductConfigurationIds(request)).WithAnyArguments().Returns(lp);

            //Act
            LicenseProductManager manager = new LicenseProductManager(mockILicenseRepository, mockILicenseSequenceRepository, mockILicenseProductRepository, mockILicenseProductRecordingRepository, mockILicensePRWriterRepository, mockILicensePRWriterRateRepository, mockILicensePRWriterNoteRepository, mockILicensePRWriterRateStatusRepository, mockILicenseProductConfigurationRepository, mockIRecsDataProvider, mockILicenseNoteRepository, mockIAgreementStatutoryRateRepository, mockIStatRateRepository, mockILicenseSolrManager, mockILicenseAttachmentManager, mockIRecordingMedleyRepository, mockIRecRepository);
            var result = manager.GetAllLicenseRelatedIds(request);

            //Assert
            Assert.IsInstanceOf(typeof(GetWritersRatesRequest), result);
        }


        [Test]
        public void GetLicenseWriterRateIdsWithOutHolds_ReturnListInt()
        {
            //Arrange
            var mockILicenseRepository = A.Fake<ILicenseRepository>();
            var mockILicenseSequenceRepository = A.Fake<ILicenseSequenceRepository>();
            var mockILicenseProductRepository = A.Fake<ILicenseProductRepository>();
            var mockILicenseProductRecordingRepository = A.Fake<ILicenseProductRecordingRepository>();
            var mockILicensePRWriterRepository = A.Fake<ILicensePRWriterRepository>();
            var mockILicensePRWriterRateRepository = A.Fake<ILicensePRWriterRateRepository>();
            var mockILicensePRWriterNoteRepository = A.Fake<ILicensePRWriterNoteRepository>();
            var mockILicensePRWriterRateStatusRepository = A.Fake<ILicensePRWriterRateStatusRepository>();
            var mockILicenseProductConfigurationRepository = A.Fake<ILicenseProductConfigurationRepository>();
            var mockIRecsDataProvider = A.Fake<IRecsDataProvider>();
            var mockILicenseNoteRepository = A.Fake<ILicenseNoteRepository>();
            var mockIAgreementStatutoryRateRepository = A.Fake<IAgreementStatutoryRateRepository>();
            var mockIStatRateRepository = A.Fake<IStatRateRepository>();
            var mockILicenseSolrManager = A.Fake<ILicenseSolrManager>();
            var mockILicenseAttachmentManager = A.Fake<ILicenseAttachmentManager>();
            var mockIRecordingMedleyRepository = A.Fake<IRecordingMedleyRepository>(); var mockIRecRepository = A.Fake<IRecs>();

            //Build expected
            List<int> expected = new List<int> { };
            List<int> lp = new List<int> { };
            List<RecsConfiguration> configs = new List<RecsConfiguration> { };
            ProductHeader ph = new ProductHeader { Configurations = configs };
            LicenseProduct licenseProduct = new LicenseProduct { ProductHeader = ph, ProductId = 99 };

            //build request
            const int request = 99;

            A.CallTo(() => mockILicensePRWriterRateStatusRepository.GetLicenseWriterRatesWithOutStatus(lp)).WithAnyArguments().Returns(expected);

            //Act
            LicenseProductManager manager = new LicenseProductManager(mockILicenseRepository, mockILicenseSequenceRepository, mockILicenseProductRepository, mockILicenseProductRecordingRepository, mockILicensePRWriterRepository, mockILicensePRWriterRateRepository, mockILicensePRWriterNoteRepository, mockILicensePRWriterRateStatusRepository, mockILicenseProductConfigurationRepository, mockIRecsDataProvider, mockILicenseNoteRepository, mockIAgreementStatutoryRateRepository, mockIStatRateRepository, mockILicenseSolrManager, mockILicenseAttachmentManager, mockIRecordingMedleyRepository, mockIRecRepository);
            var result = manager.GetLicenseWriterRateIdsWithOutHolds(request);

            //Assert
            Assert.AreEqual(expected, result);
        }


        [Test]
        public void GetYearsForEditRates_ReturnListInt()
        {
            //Arrange
            var mockILicenseRepository = A.Fake<ILicenseRepository>();
            var mockILicenseSequenceRepository = A.Fake<ILicenseSequenceRepository>();
            var mockILicenseProductRepository = A.Fake<ILicenseProductRepository>();
            var mockILicenseProductRecordingRepository = A.Fake<ILicenseProductRecordingRepository>();
            var mockILicensePRWriterRepository = A.Fake<ILicensePRWriterRepository>();
            var mockILicensePRWriterRateRepository = A.Fake<ILicensePRWriterRateRepository>();
            var mockILicensePRWriterNoteRepository = A.Fake<ILicensePRWriterNoteRepository>();
            var mockILicensePRWriterRateStatusRepository = A.Fake<ILicensePRWriterRateStatusRepository>();
            var mockILicenseProductConfigurationRepository = A.Fake<ILicenseProductConfigurationRepository>();
            var mockIRecsDataProvider = A.Fake<IRecsDataProvider>();
            var mockILicenseNoteRepository = A.Fake<ILicenseNoteRepository>();
            var mockIAgreementStatutoryRateRepository = A.Fake<IAgreementStatutoryRateRepository>();
            var mockIStatRateRepository = A.Fake<IStatRateRepository>();
            var mockILicenseSolrManager = A.Fake<ILicenseSolrManager>();
            var mockILicenseAttachmentManager = A.Fake<ILicenseAttachmentManager>();
            var mockIRecordingMedleyRepository = A.Fake<IRecordingMedleyRepository>(); var mockIRecRepository = A.Fake<IRecs>();

            //Build expected
            List<int> expected = new List<int> { };
            List<AgreementStatutoryRate> agr = new List<AgreementStatutoryRate> { };

            A.CallTo(() => mockIAgreementStatutoryRateRepository.GetAll()).WithAnyArguments().Returns(agr);

            //Act
            LicenseProductManager manager = new LicenseProductManager(mockILicenseRepository, mockILicenseSequenceRepository, mockILicenseProductRepository, mockILicenseProductRecordingRepository, mockILicensePRWriterRepository, mockILicensePRWriterRateRepository, mockILicensePRWriterNoteRepository, mockILicensePRWriterRateStatusRepository, mockILicenseProductConfigurationRepository, mockIRecsDataProvider, mockILicenseNoteRepository, mockIAgreementStatutoryRateRepository, mockIStatRateRepository, mockILicenseSolrManager, mockILicenseAttachmentManager, mockIRecordingMedleyRepository, mockIRecRepository);
            var result = manager.GetYearsForEditRates();

            //Assert
            Assert.AreEqual(expected, result);
        }



        [Test]
        public void GetAllLicenseRecordingRelatedIds_ReturnGetWritersRatesRequest()
        {
            //Arrange
            var mockILicenseRepository = A.Fake<ILicenseRepository>();
            var mockILicenseSequenceRepository = A.Fake<ILicenseSequenceRepository>();
            var mockILicenseProductRepository = A.Fake<ILicenseProductRepository>();
            var mockILicenseProductRecordingRepository = A.Fake<ILicenseProductRecordingRepository>();
            var mockILicensePRWriterRepository = A.Fake<ILicensePRWriterRepository>();
            var mockILicensePRWriterRateRepository = A.Fake<ILicensePRWriterRateRepository>();
            var mockILicensePRWriterNoteRepository = A.Fake<ILicensePRWriterNoteRepository>();
            var mockILicensePRWriterRateStatusRepository = A.Fake<ILicensePRWriterRateStatusRepository>();
            var mockILicenseProductConfigurationRepository = A.Fake<ILicenseProductConfigurationRepository>();
            var mockIRecsDataProvider = A.Fake<IRecsDataProvider>();
            var mockILicenseNoteRepository = A.Fake<ILicenseNoteRepository>();
            var mockIAgreementStatutoryRateRepository = A.Fake<IAgreementStatutoryRateRepository>();
            var mockIStatRateRepository = A.Fake<IStatRateRepository>();
            var mockILicenseSolrManager = A.Fake<ILicenseSolrManager>();
            var mockILicenseAttachmentManager = A.Fake<ILicenseAttachmentManager>();
            var mockIRecordingMedleyRepository = A.Fake<IRecordingMedleyRepository>(); var mockIRecRepository = A.Fake<IRecs>();

            //build request
            List<int> request = new List<int> { };
          
            //Build expected
            GetWritersRatesRequest first = new GetWritersRatesRequest { LicenseRecordingIds = new List<int> { } };
            GetWritersRatesRequest expected = new GetWritersRatesRequest { LicenseRecordingIds = new List<int> { } };
            List<LicenseProductConfiguration> lpc = new List<LicenseProductConfiguration> { };
            List<LicenseProductRecording> lpr = new List<LicenseProductRecording> { };
            List<int> ids = new List<int> { };


            A.CallTo(() => mockILicenseProductConfigurationRepository.GetLicenseConfigurationList(request)).WithAnyArguments().Returns(lpc);
            A.CallTo(() => mockILicenseProductRecordingRepository.GetLicenseProductRecordingsFromList(request)).WithAnyArguments().Returns(lpr);
            A.CallTo(() => mockILicensePRWriterRepository.GetLicenseRecordingWriterIds(first.LicenseRecordingIds)).WithAnyArguments().Returns(ids);
           

            //Act
            LicenseProductManager manager = new LicenseProductManager(mockILicenseRepository, mockILicenseSequenceRepository, mockILicenseProductRepository, mockILicenseProductRecordingRepository, mockILicensePRWriterRepository, mockILicensePRWriterRateRepository, mockILicensePRWriterNoteRepository, mockILicensePRWriterRateStatusRepository, mockILicenseProductConfigurationRepository, mockIRecsDataProvider, mockILicenseNoteRepository, mockIAgreementStatutoryRateRepository, mockIStatRateRepository, mockILicenseSolrManager, mockILicenseAttachmentManager, mockIRecordingMedleyRepository, mockIRecRepository);
            var result = manager.GetAllLicenseRecordingRelatedIds(request);

            //Assert
            Assert.IsInstanceOf(typeof(GetWritersRatesRequest), result);
        }

        [Test]
        public void GetLicenseProductRecordings_ReturnListLicenseProductRecording()
        {
            //Arrange
            var mockILicenseRepository = A.Fake<ILicenseRepository>();
            var mockILicenseSequenceRepository = A.Fake<ILicenseSequenceRepository>();
            var mockILicenseProductRepository = A.Fake<ILicenseProductRepository>();
            var mockILicenseProductRecordingRepository = A.Fake<ILicenseProductRecordingRepository>();
            var mockILicensePRWriterRepository = A.Fake<ILicensePRWriterRepository>();
            var mockILicensePRWriterRateRepository = A.Fake<ILicensePRWriterRateRepository>();
            var mockILicensePRWriterNoteRepository = A.Fake<ILicensePRWriterNoteRepository>();
            var mockILicensePRWriterRateStatusRepository = A.Fake<ILicensePRWriterRateStatusRepository>();
            var mockILicenseProductConfigurationRepository = A.Fake<ILicenseProductConfigurationRepository>();
            var mockIRecsDataProvider = A.Fake<IRecsDataProvider>();
            var mockILicenseNoteRepository = A.Fake<ILicenseNoteRepository>();
            var mockIAgreementStatutoryRateRepository = A.Fake<IAgreementStatutoryRateRepository>();
            var mockIStatRateRepository = A.Fake<IStatRateRepository>();
            var mockILicenseSolrManager = A.Fake<ILicenseSolrManager>();
            var mockILicenseAttachmentManager = A.Fake<ILicenseAttachmentManager>();
            var mockIRecordingMedleyRepository = A.Fake<IRecordingMedleyRepository>(); var mockIRecRepository = A.Fake<IRecs>();

            //build request
            int request = 99;
            LicenseProduct lp = new LicenseProduct { };
            LicenseProductRecording singleLPR = new LicenseProductRecording{LicenseRecordingId = 99 };
            List<LicenseProductRecording> expected = new List<LicenseProductRecording> { };
            expected.Add(singleLPR);

            //Build expected
            GetWritersRatesRequest first = new GetWritersRatesRequest { LicenseRecordingIds = new List<int> { } };
            List<int> ids = new List<int> { };

            A.CallTo(() => mockILicenseProductRepository.Get(request)).WithAnyArguments().Returns(lp);
            A.CallTo(() => mockILicenseProductRecordingRepository.GetLicenseProductRecordingsBrief(request)).WithAnyArguments().Returns(expected);
            A.CallTo(() => mockILicensePRWriterRepository.GetLicenseRecordingWriterIds(first.LicenseRecordingIds)).WithAnyArguments().Returns(ids);


            //Act
            LicenseProductManager manager = new LicenseProductManager(mockILicenseRepository, mockILicenseSequenceRepository, mockILicenseProductRepository, mockILicenseProductRecordingRepository, mockILicensePRWriterRepository, mockILicensePRWriterRateRepository, mockILicensePRWriterNoteRepository, mockILicensePRWriterRateStatusRepository, mockILicenseProductConfigurationRepository, mockIRecsDataProvider, mockILicenseNoteRepository, mockIAgreementStatutoryRateRepository, mockIStatRateRepository, mockILicenseSolrManager, mockILicenseAttachmentManager, mockIRecordingMedleyRepository, mockIRecRepository);
            var result = manager.GetLicenseProductRecordings(request);

            //Assert
            Assert.AreEqual(expected, result);
        }


        [Test]
        public void GetLicenseProductRecordingsBrief_ReturnListLicenseProductRecording()
        {
            //Arrange
            var mockILicenseRepository = A.Fake<ILicenseRepository>();
            var mockILicenseSequenceRepository = A.Fake<ILicenseSequenceRepository>();
            var mockILicenseProductRepository = A.Fake<ILicenseProductRepository>();
            var mockILicenseProductRecordingRepository = A.Fake<ILicenseProductRecordingRepository>();
            var mockILicensePRWriterRepository = A.Fake<ILicensePRWriterRepository>();
            var mockILicensePRWriterRateRepository = A.Fake<ILicensePRWriterRateRepository>();
            var mockILicensePRWriterNoteRepository = A.Fake<ILicensePRWriterNoteRepository>();
            var mockILicensePRWriterRateStatusRepository = A.Fake<ILicensePRWriterRateStatusRepository>();
            var mockILicenseProductConfigurationRepository = A.Fake<ILicenseProductConfigurationRepository>();
            var mockIRecsDataProvider = A.Fake<IRecsDataProvider>();
            var mockILicenseNoteRepository = A.Fake<ILicenseNoteRepository>();
            var mockIAgreementStatutoryRateRepository = A.Fake<IAgreementStatutoryRateRepository>();
            var mockIStatRateRepository = A.Fake<IStatRateRepository>();
            var mockILicenseSolrManager = A.Fake<ILicenseSolrManager>();
            var mockILicenseAttachmentManager = A.Fake<ILicenseAttachmentManager>();
            var mockIRecordingMedleyRepository = A.Fake<IRecordingMedleyRepository>(); var mockIRecRepository = A.Fake<IRecs>();

            List<LicenseProductRecording> expected = new List<LicenseProductRecording> { };
          
            //Act
            LicenseProductManager manager = new LicenseProductManager(mockILicenseRepository, mockILicenseSequenceRepository, mockILicenseProductRepository, mockILicenseProductRecordingRepository, mockILicensePRWriterRepository, mockILicensePRWriterRateRepository, mockILicensePRWriterNoteRepository, mockILicensePRWriterRateStatusRepository, mockILicenseProductConfigurationRepository, mockIRecsDataProvider, mockILicenseNoteRepository, mockIAgreementStatutoryRateRepository, mockIStatRateRepository, mockILicenseSolrManager, mockILicenseAttachmentManager, mockIRecordingMedleyRepository, mockIRecRepository);
            var result = manager.GetLicenseProductRecordingsBrief(A<int>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }


        [Test]
        public void GetLicenseWriters_ReturnListLicenseProductRecordingWriter()
        {
            //Arrange
            var mockILicenseRepository = A.Fake<ILicenseRepository>();
            var mockILicenseSequenceRepository = A.Fake<ILicenseSequenceRepository>();
            var mockILicenseProductRepository = A.Fake<ILicenseProductRepository>();
            var mockILicenseProductRecordingRepository = A.Fake<ILicenseProductRecordingRepository>();
            var mockILicensePRWriterRepository = A.Fake<ILicensePRWriterRepository>();
            var mockILicensePRWriterRateRepository = A.Fake<ILicensePRWriterRateRepository>();
            var mockILicensePRWriterNoteRepository = A.Fake<ILicensePRWriterNoteRepository>();
            var mockILicensePRWriterRateStatusRepository = A.Fake<ILicensePRWriterRateStatusRepository>();
            var mockILicenseProductConfigurationRepository = A.Fake<ILicenseProductConfigurationRepository>();
            var mockIRecsDataProvider = A.Fake<IRecsDataProvider>();
            var mockILicenseNoteRepository = A.Fake<ILicenseNoteRepository>();
            var mockIAgreementStatutoryRateRepository = A.Fake<IAgreementStatutoryRateRepository>();
            var mockIStatRateRepository = A.Fake<IStatRateRepository>();
            var mockILicenseSolrManager = A.Fake<ILicenseSolrManager>();
            var mockILicenseAttachmentManager = A.Fake<ILicenseAttachmentManager>();
            var mockIRecordingMedleyRepository = A.Fake<IRecordingMedleyRepository>(); var mockIRecRepository = A.Fake<IRecs>();

            List<LicenseProductRecordingWriter> expected = new List<LicenseProductRecordingWriter> { };

            A.CallTo(() => mockILicensePRWriterRepository.GetLicenseWriters(A<int>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            LicenseProductManager manager = new LicenseProductManager(mockILicenseRepository, mockILicenseSequenceRepository, mockILicenseProductRepository, mockILicenseProductRecordingRepository, mockILicensePRWriterRepository, mockILicensePRWriterRateRepository, mockILicensePRWriterNoteRepository, mockILicensePRWriterRateStatusRepository, mockILicenseProductConfigurationRepository, mockIRecsDataProvider, mockILicenseNoteRepository, mockIAgreementStatutoryRateRepository, mockIStatRateRepository, mockILicenseSolrManager, mockILicenseAttachmentManager, mockIRecordingMedleyRepository, mockIRecRepository);
            var result = manager.GetLicenseWriters(A<int>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetLicenseWriterRateStatusList_ReturnListLicenseProductRecordingWritersRateStatusDropdown()
        {
            //Arrange
            var mockILicenseRepository = A.Fake<ILicenseRepository>();
            var mockILicenseSequenceRepository = A.Fake<ILicenseSequenceRepository>();
            var mockILicenseProductRepository = A.Fake<ILicenseProductRepository>();
            var mockILicenseProductRecordingRepository = A.Fake<ILicenseProductRecordingRepository>();
            var mockILicensePRWriterRepository = A.Fake<ILicensePRWriterRepository>();
            var mockILicensePRWriterRateRepository = A.Fake<ILicensePRWriterRateRepository>();
            var mockILicensePRWriterNoteRepository = A.Fake<ILicensePRWriterNoteRepository>();
            var mockILicensePRWriterRateStatusRepository = A.Fake<ILicensePRWriterRateStatusRepository>();
            var mockILicenseProductConfigurationRepository = A.Fake<ILicenseProductConfigurationRepository>();
            var mockIRecsDataProvider = A.Fake<IRecsDataProvider>();
            var mockILicenseNoteRepository = A.Fake<ILicenseNoteRepository>();
            var mockIAgreementStatutoryRateRepository = A.Fake<IAgreementStatutoryRateRepository>();
            var mockIStatRateRepository = A.Fake<IStatRateRepository>();
            var mockILicenseSolrManager = A.Fake<ILicenseSolrManager>();
            var mockILicenseAttachmentManager = A.Fake<ILicenseAttachmentManager>();
            var mockIRecordingMedleyRepository = A.Fake<IRecordingMedleyRepository>(); var mockIRecRepository = A.Fake<IRecs>();

            List<LicenseProductRecordingWritersRateStatusDropdown> expected = new List<LicenseProductRecordingWritersRateStatusDropdown> { };

            A.CallTo(() => mockILicensePRWriterRateStatusRepository.GetLicenseWriterRateStatusList(A<List<int>>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            LicenseProductManager manager = new LicenseProductManager(mockILicenseRepository, mockILicenseSequenceRepository, mockILicenseProductRepository, mockILicenseProductRecordingRepository, mockILicensePRWriterRepository, mockILicensePRWriterRateRepository, mockILicensePRWriterNoteRepository, mockILicensePRWriterRateStatusRepository, mockILicenseProductConfigurationRepository, mockIRecsDataProvider, mockILicenseNoteRepository, mockIAgreementStatutoryRateRepository, mockIStatRateRepository, mockILicenseSolrManager, mockILicenseAttachmentManager, mockIRecordingMedleyRepository, mockIRecRepository);
            var result = manager.GetLicenseWriterRateStatusList(A<List<int>>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetLicenseProductRecordingWriters_ReturnListLicenseProductRecordingWriter()
        {
            //Arrange
            var mockILicenseRepository = A.Fake<ILicenseRepository>();
            var mockILicenseSequenceRepository = A.Fake<ILicenseSequenceRepository>();
            var mockILicenseProductRepository = A.Fake<ILicenseProductRepository>();
            var mockILicenseProductRecordingRepository = A.Fake<ILicenseProductRecordingRepository>();
            var mockILicensePRWriterRepository = A.Fake<ILicensePRWriterRepository>();
            var mockILicensePRWriterRateRepository = A.Fake<ILicensePRWriterRateRepository>();
            var mockILicensePRWriterNoteRepository = A.Fake<ILicensePRWriterNoteRepository>();
            var mockILicensePRWriterRateStatusRepository = A.Fake<ILicensePRWriterRateStatusRepository>();
            var mockILicenseProductConfigurationRepository = A.Fake<ILicenseProductConfigurationRepository>();
            var mockIRecsDataProvider = A.Fake<IRecsDataProvider>();
            var mockILicenseNoteRepository = A.Fake<ILicenseNoteRepository>();
            var mockIAgreementStatutoryRateRepository = A.Fake<IAgreementStatutoryRateRepository>();
            var mockIStatRateRepository = A.Fake<IStatRateRepository>();
            var mockILicenseSolrManager = A.Fake<ILicenseSolrManager>();
            var mockILicenseAttachmentManager = A.Fake<ILicenseAttachmentManager>();
            var mockIRecordingMedleyRepository = A.Fake<IRecordingMedleyRepository>(); var mockIRecRepository = A.Fake<IRecs>();

            List<LicenseProductRecordingWriter> expected = new List<LicenseProductRecordingWriter> { };

            A.CallTo(() => mockILicensePRWriterRepository.GetLicenseProductRecordingWriters(A<int>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            LicenseProductManager manager = new LicenseProductManager(mockILicenseRepository, mockILicenseSequenceRepository, mockILicenseProductRepository, mockILicenseProductRecordingRepository, mockILicensePRWriterRepository, mockILicensePRWriterRateRepository, mockILicensePRWriterNoteRepository, mockILicensePRWriterRateStatusRepository, mockILicenseProductConfigurationRepository, mockIRecsDataProvider, mockILicenseNoteRepository, mockIAgreementStatutoryRateRepository, mockIStatRateRepository, mockILicenseSolrManager, mockILicenseAttachmentManager, mockIRecordingMedleyRepository, mockIRecRepository);
            var result = manager.GetLicenseProductRecordingWriters(A<int>.Ignored, A<string>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetLicensePRWriterRates_ReturnListLicenseProductRecordingWriterRate()
        {
            //Arrange
            var mockILicenseRepository = A.Fake<ILicenseRepository>();
            var mockILicenseSequenceRepository = A.Fake<ILicenseSequenceRepository>();
            var mockILicenseProductRepository = A.Fake<ILicenseProductRepository>();
            var mockILicenseProductRecordingRepository = A.Fake<ILicenseProductRecordingRepository>();
            var mockILicensePRWriterRepository = A.Fake<ILicensePRWriterRepository>();
            var mockILicensePRWriterRateRepository = A.Fake<ILicensePRWriterRateRepository>();
            var mockILicensePRWriterNoteRepository = A.Fake<ILicensePRWriterNoteRepository>();
            var mockILicensePRWriterRateStatusRepository = A.Fake<ILicensePRWriterRateStatusRepository>();
            var mockILicenseProductConfigurationRepository = A.Fake<ILicenseProductConfigurationRepository>();
            var mockIRecsDataProvider = A.Fake<IRecsDataProvider>();
            var mockILicenseNoteRepository = A.Fake<ILicenseNoteRepository>();
            var mockIAgreementStatutoryRateRepository = A.Fake<IAgreementStatutoryRateRepository>();
            var mockIStatRateRepository = A.Fake<IStatRateRepository>();
            var mockILicenseSolrManager = A.Fake<ILicenseSolrManager>();
            var mockILicenseAttachmentManager = A.Fake<ILicenseAttachmentManager>();
            var mockIRecordingMedleyRepository = A.Fake<IRecordingMedleyRepository>(); var mockIRecRepository = A.Fake<IRecs>();

            List<LicenseProductRecordingWriterRate> expected = new List<LicenseProductRecordingWriterRate> { };

            A.CallTo(() => mockILicensePRWriterRateRepository.GetLicenseProductRecordingWriterRates(A<int>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            LicenseProductManager manager = new LicenseProductManager(mockILicenseRepository, mockILicenseSequenceRepository, mockILicenseProductRepository, mockILicenseProductRecordingRepository, mockILicensePRWriterRepository, mockILicensePRWriterRateRepository, mockILicensePRWriterNoteRepository, mockILicensePRWriterRateStatusRepository, mockILicenseProductConfigurationRepository, mockIRecsDataProvider, mockILicenseNoteRepository, mockIAgreementStatutoryRateRepository, mockIStatRateRepository, mockILicenseSolrManager, mockILicenseAttachmentManager, mockIRecordingMedleyRepository, mockIRecRepository);
            var result = manager.GetLicensePRWriterRates(A<int>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetLicenseProductRecordingWritersNo_ReturnInt()
        {
            //Arrange
            var mockILicenseRepository = A.Fake<ILicenseRepository>();
            var mockILicenseSequenceRepository = A.Fake<ILicenseSequenceRepository>();
            var mockILicenseProductRepository = A.Fake<ILicenseProductRepository>();
            var mockILicenseProductRecordingRepository = A.Fake<ILicenseProductRecordingRepository>();
            var mockILicensePRWriterRepository = A.Fake<ILicensePRWriterRepository>();
            var mockILicensePRWriterRateRepository = A.Fake<ILicensePRWriterRateRepository>();
            var mockILicensePRWriterNoteRepository = A.Fake<ILicensePRWriterNoteRepository>();
            var mockILicensePRWriterRateStatusRepository = A.Fake<ILicensePRWriterRateStatusRepository>();
            var mockILicenseProductConfigurationRepository = A.Fake<ILicenseProductConfigurationRepository>();
            var mockIRecsDataProvider = A.Fake<IRecsDataProvider>();
            var mockILicenseNoteRepository = A.Fake<ILicenseNoteRepository>();
            var mockIAgreementStatutoryRateRepository = A.Fake<IAgreementStatutoryRateRepository>();
            var mockIStatRateRepository = A.Fake<IStatRateRepository>();
            var mockILicenseSolrManager = A.Fake<ILicenseSolrManager>();
            var mockILicenseAttachmentManager = A.Fake<ILicenseAttachmentManager>();
            var mockIRecordingMedleyRepository = A.Fake<IRecordingMedleyRepository>(); var mockIRecRepository = A.Fake<IRecs>();

            const int expected = 99;

            A.CallTo(() => mockILicensePRWriterRepository.GetLicenseProductRecordingWritersNo(A<int>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            LicenseProductManager manager = new LicenseProductManager(mockILicenseRepository, mockILicenseSequenceRepository, mockILicenseProductRepository, mockILicenseProductRecordingRepository, mockILicensePRWriterRepository, mockILicensePRWriterRateRepository, mockILicensePRWriterNoteRepository, mockILicensePRWriterRateStatusRepository, mockILicenseProductConfigurationRepository, mockIRecsDataProvider, mockILicenseNoteRepository, mockIAgreementStatutoryRateRepository, mockIStatRateRepository, mockILicenseSolrManager, mockILicenseAttachmentManager, mockIRecordingMedleyRepository, mockIRecRepository);
            var result = manager.GetLicenseProductRecordingWritersNo(A<int>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }


        [Test]
        public void GetLicenseProductConfigurations_ReturnListLicenseProductConfiguration()
        {
            //Arrange
            var mockILicenseRepository = A.Fake<ILicenseRepository>();
            var mockILicenseSequenceRepository = A.Fake<ILicenseSequenceRepository>();
            var mockILicenseProductRepository = A.Fake<ILicenseProductRepository>();
            var mockILicenseProductRecordingRepository = A.Fake<ILicenseProductRecordingRepository>();
            var mockILicensePRWriterRepository = A.Fake<ILicensePRWriterRepository>();
            var mockILicensePRWriterRateRepository = A.Fake<ILicensePRWriterRateRepository>();
            var mockILicensePRWriterNoteRepository = A.Fake<ILicensePRWriterNoteRepository>();
            var mockILicensePRWriterRateStatusRepository = A.Fake<ILicensePRWriterRateStatusRepository>();
            var mockILicenseProductConfigurationRepository = A.Fake<ILicenseProductConfigurationRepository>();
            var mockIRecsDataProvider = A.Fake<IRecsDataProvider>();
            var mockILicenseNoteRepository = A.Fake<ILicenseNoteRepository>();
            var mockIAgreementStatutoryRateRepository = A.Fake<IAgreementStatutoryRateRepository>();
            var mockIStatRateRepository = A.Fake<IStatRateRepository>();
            var mockILicenseSolrManager = A.Fake<ILicenseSolrManager>();
            var mockILicenseAttachmentManager = A.Fake<ILicenseAttachmentManager>();
            var mockIRecordingMedleyRepository = A.Fake<IRecordingMedleyRepository>(); var mockIRecRepository = A.Fake<IRecs>();

            List<LicenseProductConfiguration> expected = new List<LicenseProductConfiguration> { };

            A.CallTo(() => mockILicenseProductRepository.GetLicenseProductConfigurations(A<int>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            LicenseProductManager manager = new LicenseProductManager(mockILicenseRepository, mockILicenseSequenceRepository, mockILicenseProductRepository, mockILicenseProductRecordingRepository, mockILicensePRWriterRepository, mockILicensePRWriterRateRepository, mockILicensePRWriterNoteRepository, mockILicensePRWriterRateStatusRepository, mockILicenseProductConfigurationRepository, mockIRecsDataProvider, mockILicenseNoteRepository, mockIAgreementStatutoryRateRepository, mockIStatRateRepository, mockILicenseSolrManager, mockILicenseAttachmentManager, mockIRecordingMedleyRepository, mockIRecRepository);
            var result = manager.GetLicenseProductConfigurations(A<int>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void UpdateLicenseProductConfigurations_ReturnBoolTRUE()
        {
            //Arrange
            var mockILicenseRepository = A.Fake<ILicenseRepository>();
            var mockILicenseSequenceRepository = A.Fake<ILicenseSequenceRepository>();
            var mockILicenseProductRepository = A.Fake<ILicenseProductRepository>();
            var mockILicenseProductRecordingRepository = A.Fake<ILicenseProductRecordingRepository>();
            var mockILicensePRWriterRepository = A.Fake<ILicensePRWriterRepository>();
            var mockILicensePRWriterRateRepository = A.Fake<ILicensePRWriterRateRepository>();
            var mockILicensePRWriterNoteRepository = A.Fake<ILicensePRWriterNoteRepository>();
            var mockILicensePRWriterRateStatusRepository = A.Fake<ILicensePRWriterRateStatusRepository>();
            var mockILicenseProductConfigurationRepository = A.Fake<ILicenseProductConfigurationRepository>();
            var mockIRecsDataProvider = A.Fake<IRecsDataProvider>();
            var mockILicenseNoteRepository = A.Fake<ILicenseNoteRepository>();
            var mockIAgreementStatutoryRateRepository = A.Fake<IAgreementStatutoryRateRepository>();
            var mockIStatRateRepository = A.Fake<IStatRateRepository>();
            var mockILicenseSolrManager = A.Fake<ILicenseSolrManager>();
            var mockILicenseAttachmentManager = A.Fake<ILicenseAttachmentManager>();
            var mockIRecordingMedleyRepository = A.Fake<IRecordingMedleyRepository>(); var mockIRecRepository = A.Fake<IRecs>();

            //Build request
            UpdateLicenseProductConfigurationsStatusRequest request = new UpdateLicenseProductConfigurationsStatusRequest
            {
                LicenseProductConfigurationId = 99,
            };

            LicenseProductConfiguration lpc = new LicenseProductConfiguration { };

            List<LicenseProductConfiguration> expected = new List<LicenseProductConfiguration> { };

            A.CallTo(() => mockILicenseProductRepository.GetLicenseProductConfigurationById(request.LicenseProductConfigurationId)).WithAnyArguments().Returns(lpc);

            //Act
            LicenseProductManager manager = new LicenseProductManager(mockILicenseRepository, mockILicenseSequenceRepository, mockILicenseProductRepository, mockILicenseProductRecordingRepository, mockILicensePRWriterRepository, mockILicensePRWriterRateRepository, mockILicensePRWriterNoteRepository, mockILicensePRWriterRateStatusRepository, mockILicenseProductConfigurationRepository, mockIRecsDataProvider, mockILicenseNoteRepository, mockIAgreementStatutoryRateRepository, mockIStatRateRepository, mockILicenseSolrManager, mockILicenseAttachmentManager, mockIRecordingMedleyRepository, mockIRecRepository);
            var result = manager.UpdateLicenseProductConfigurations(request);

            //Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void UpdateLicenseProducts_ReturnBoolFALSE()
        {
            //Arrange
            var mockILicenseRepository = A.Fake<ILicenseRepository>();
            var mockILicenseSequenceRepository = A.Fake<ILicenseSequenceRepository>();
            var mockILicenseProductRepository = A.Fake<ILicenseProductRepository>();
            var mockILicenseProductRecordingRepository = A.Fake<ILicenseProductRecordingRepository>();
            var mockILicensePRWriterRepository = A.Fake<ILicensePRWriterRepository>();
            var mockILicensePRWriterRateRepository = A.Fake<ILicensePRWriterRateRepository>();
            var mockILicensePRWriterNoteRepository = A.Fake<ILicensePRWriterNoteRepository>();
            var mockILicensePRWriterRateStatusRepository = A.Fake<ILicensePRWriterRateStatusRepository>();
            var mockILicenseProductConfigurationRepository = A.Fake<ILicenseProductConfigurationRepository>();
            var mockIRecsDataProvider = A.Fake<IRecsDataProvider>();
            var mockILicenseNoteRepository = A.Fake<ILicenseNoteRepository>();
            var mockIAgreementStatutoryRateRepository = A.Fake<IAgreementStatutoryRateRepository>();
            var mockIStatRateRepository = A.Fake<IStatRateRepository>();
            var mockILicenseSolrManager = A.Fake<ILicenseSolrManager>();
            var mockILicenseAttachmentManager = A.Fake<ILicenseAttachmentManager>();
            var mockIRecordingMedleyRepository = A.Fake<IRecordingMedleyRepository>(); var mockIRecRepository = A.Fake<IRecs>();

            //Build request
             List<KeyValuePair<int,List<int>>> lps = new List<KeyValuePair<int,List<int>>>{};
            UpdateLicenseProductsRequest request = new UpdateLicenseProductsRequest { LicenseId = 99, LicenseProducts = lps  };
       
            LicenseProductConfiguration lpc = new LicenseProductConfiguration { };

            List<LicenseProductConfiguration> expected = new List<LicenseProductConfiguration> { };
                      
            //Act
            LicenseProductManager manager = new LicenseProductManager(mockILicenseRepository, mockILicenseSequenceRepository, mockILicenseProductRepository, mockILicenseProductRecordingRepository, mockILicensePRWriterRepository, mockILicensePRWriterRateRepository, mockILicensePRWriterNoteRepository, mockILicensePRWriterRateStatusRepository, mockILicenseProductConfigurationRepository, mockIRecsDataProvider, mockILicenseNoteRepository, mockIAgreementStatutoryRateRepository, mockIStatRateRepository, mockILicenseSolrManager, mockILicenseAttachmentManager, mockIRecordingMedleyRepository, mockIRecRepository);
            var result = manager.UpdateLicenseProducts(request);

            //Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void GetLicenseProductConfigurationsAll_ReturnListLicenseProductConfigurations()
        {
            //Arrange
            var mockILicenseRepository = A.Fake<ILicenseRepository>();
            var mockILicenseSequenceRepository = A.Fake<ILicenseSequenceRepository>();
            var mockILicenseProductRepository = A.Fake<ILicenseProductRepository>();
            var mockILicenseProductRecordingRepository = A.Fake<ILicenseProductRecordingRepository>();
            var mockILicensePRWriterRepository = A.Fake<ILicensePRWriterRepository>();
            var mockILicensePRWriterRateRepository = A.Fake<ILicensePRWriterRateRepository>();
            var mockILicensePRWriterNoteRepository = A.Fake<ILicensePRWriterNoteRepository>();
            var mockILicensePRWriterRateStatusRepository = A.Fake<ILicensePRWriterRateStatusRepository>();
            var mockILicenseProductConfigurationRepository = A.Fake<ILicenseProductConfigurationRepository>();
            var mockIRecsDataProvider = A.Fake<IRecsDataProvider>();
            var mockILicenseNoteRepository = A.Fake<ILicenseNoteRepository>();
            var mockIAgreementStatutoryRateRepository = A.Fake<IAgreementStatutoryRateRepository>();
            var mockIStatRateRepository = A.Fake<IStatRateRepository>();
            var mockILicenseSolrManager = A.Fake<ILicenseSolrManager>();
            var mockILicenseAttachmentManager = A.Fake<ILicenseAttachmentManager>();
            var mockIRecordingMedleyRepository = A.Fake<IRecordingMedleyRepository>(); var mockIRecRepository = A.Fake<IRecs>();

            //Build request
            List<KeyValuePair<int, List<int>>> lps = new List<KeyValuePair<int, List<int>>> { };
            UpdateLicenseProductsRequest request = new UpdateLicenseProductsRequest { LicenseId = 99, LicenseProducts = lps };

            LicenseProductConfiguration lpc = new LicenseProductConfiguration { };

            List<LicenseProductConfiguration> expected = new List<LicenseProductConfiguration> { };
                      
            //Act
            LicenseProductManager manager = new LicenseProductManager(mockILicenseRepository, mockILicenseSequenceRepository, mockILicenseProductRepository, mockILicenseProductRecordingRepository, mockILicensePRWriterRepository, mockILicensePRWriterRateRepository, mockILicensePRWriterNoteRepository, mockILicensePRWriterRateStatusRepository, mockILicenseProductConfigurationRepository, mockIRecsDataProvider, mockILicenseNoteRepository, mockIAgreementStatutoryRateRepository, mockIStatRateRepository, mockILicenseSolrManager, mockILicenseAttachmentManager, mockIRecordingMedleyRepository, mockIRecRepository);
            var result = manager.GetLicenseProductConfigurationsAll(A<int>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetLicenseProductRecordingsV2_ReturnListWorksRecording()
        {
            //Arrange
            var mockILicenseRepository = A.Fake<ILicenseRepository>();
            var mockILicenseSequenceRepository = A.Fake<ILicenseSequenceRepository>();
            var mockILicenseProductRepository = A.Fake<ILicenseProductRepository>();
            var mockILicenseProductRecordingRepository = A.Fake<ILicenseProductRecordingRepository>();
            var mockILicensePRWriterRepository = A.Fake<ILicensePRWriterRepository>();
            var mockILicensePRWriterRateRepository = A.Fake<ILicensePRWriterRateRepository>();
            var mockILicensePRWriterNoteRepository = A.Fake<ILicensePRWriterNoteRepository>();
            var mockILicensePRWriterRateStatusRepository = A.Fake<ILicensePRWriterRateStatusRepository>();
            var mockILicenseProductConfigurationRepository = A.Fake<ILicenseProductConfigurationRepository>();
            var mockIRecsDataProvider = A.Fake<IRecsDataProvider>();
            var mockILicenseNoteRepository = A.Fake<ILicenseNoteRepository>();
            var mockIAgreementStatutoryRateRepository = A.Fake<IAgreementStatutoryRateRepository>();
            var mockIStatRateRepository = A.Fake<IStatRateRepository>();
            var mockILicenseSolrManager = A.Fake<ILicenseSolrManager>();
            var mockILicenseAttachmentManager = A.Fake<ILicenseAttachmentManager>();
            var mockIRecordingMedleyRepository = A.Fake<IRecordingMedleyRepository>(); var mockIRecRepository = A.Fake<IRecs>();

            //Build request
            List<KeyValuePair<int, List<int>>> lps = new List<KeyValuePair<int, List<int>>> { };
            UpdateLicenseProductsRequest request = new UpdateLicenseProductsRequest { LicenseId = 99, LicenseProducts = lps };

            LicenseProductConfiguration lpc = new LicenseProductConfiguration { };

            List<WorksRecording> expected = new List<WorksRecording> { };

            //Act
            LicenseProductManager manager = new LicenseProductManager(mockILicenseRepository, mockILicenseSequenceRepository, mockILicenseProductRepository, mockILicenseProductRecordingRepository, mockILicensePRWriterRepository, mockILicensePRWriterRateRepository, mockILicensePRWriterNoteRepository, mockILicensePRWriterRateStatusRepository, mockILicenseProductConfigurationRepository, mockIRecsDataProvider, mockILicenseNoteRepository, mockIAgreementStatutoryRateRepository, mockIStatRateRepository, mockILicenseSolrManager, mockILicenseAttachmentManager, mockIRecordingMedleyRepository, mockIRecRepository);
            var result = manager.GetLicenseProductRecordingsV2(A<int>.Ignored, A<string>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetLicenseProductRecordingsV3_ReturnListWorksRecording()
        {
            //Arrange
            var mockILicenseRepository = A.Fake<ILicenseRepository>();
            var mockILicenseSequenceRepository = A.Fake<ILicenseSequenceRepository>();
            var mockILicenseProductRepository = A.Fake<ILicenseProductRepository>();
            var mockILicenseProductRecordingRepository = A.Fake<ILicenseProductRecordingRepository>();
            var mockILicensePRWriterRepository = A.Fake<ILicensePRWriterRepository>();
            var mockILicensePRWriterRateRepository = A.Fake<ILicensePRWriterRateRepository>();
            var mockILicensePRWriterNoteRepository = A.Fake<ILicensePRWriterNoteRepository>();
            var mockILicensePRWriterRateStatusRepository = A.Fake<ILicensePRWriterRateStatusRepository>();
            var mockILicenseProductConfigurationRepository = A.Fake<ILicenseProductConfigurationRepository>();
            var mockIRecsDataProvider = A.Fake<IRecsDataProvider>();
            var mockILicenseNoteRepository = A.Fake<ILicenseNoteRepository>();
            var mockIAgreementStatutoryRateRepository = A.Fake<IAgreementStatutoryRateRepository>();
            var mockIStatRateRepository = A.Fake<IStatRateRepository>();
            var mockILicenseSolrManager = A.Fake<ILicenseSolrManager>();
            var mockILicenseAttachmentManager = A.Fake<ILicenseAttachmentManager>();
            var mockIRecordingMedleyRepository = A.Fake<IRecordingMedleyRepository>(); var mockIRecRepository = A.Fake<IRecs>();

            //Build request
            List<KeyValuePair<int, List<int>>> lps = new List<KeyValuePair<int, List<int>>> { };
            UpdateLicenseProductsRequest request = new UpdateLicenseProductsRequest { LicenseId = 99, LicenseProducts = lps };

            LicenseProductConfiguration lpc = new LicenseProductConfiguration { };

            List<WorksRecording> expected = new List<WorksRecording> { };

            //Act
            LicenseProductManager manager = new LicenseProductManager(mockILicenseRepository, mockILicenseSequenceRepository, mockILicenseProductRepository, mockILicenseProductRecordingRepository, mockILicensePRWriterRepository, mockILicensePRWriterRateRepository, mockILicensePRWriterNoteRepository, mockILicensePRWriterRateStatusRepository, mockILicenseProductConfigurationRepository, mockIRecsDataProvider, mockILicenseNoteRepository, mockIAgreementStatutoryRateRepository, mockIStatRateRepository, mockILicenseSolrManager, mockILicenseAttachmentManager, mockIRecordingMedleyRepository, mockIRecRepository);
            var result = manager.GetLicenseProductRecordingsV3(A<int>.Ignored, A<string>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetLicenseProductRecordingsDropdown_ReturnListLicenseProductRecordingsDropdown()
        {
            //Arrange
            var mockILicenseRepository = A.Fake<ILicenseRepository>();
            var mockILicenseSequenceRepository = A.Fake<ILicenseSequenceRepository>();
            var mockILicenseProductRepository = A.Fake<ILicenseProductRepository>();
            var mockILicenseProductRecordingRepository = A.Fake<ILicenseProductRecordingRepository>();
            var mockILicensePRWriterRepository = A.Fake<ILicensePRWriterRepository>();
            var mockILicensePRWriterRateRepository = A.Fake<ILicensePRWriterRateRepository>();
            var mockILicensePRWriterNoteRepository = A.Fake<ILicensePRWriterNoteRepository>();
            var mockILicensePRWriterRateStatusRepository = A.Fake<ILicensePRWriterRateStatusRepository>();
            var mockILicenseProductConfigurationRepository = A.Fake<ILicenseProductConfigurationRepository>();
            var mockIRecsDataProvider = A.Fake<IRecsDataProvider>();
            var mockILicenseNoteRepository = A.Fake<ILicenseNoteRepository>();
            var mockIAgreementStatutoryRateRepository = A.Fake<IAgreementStatutoryRateRepository>();
            var mockIStatRateRepository = A.Fake<IStatRateRepository>();
            var mockILicenseSolrManager = A.Fake<ILicenseSolrManager>();
            var mockILicenseAttachmentManager = A.Fake<ILicenseAttachmentManager>();
            var mockIRecordingMedleyRepository = A.Fake<IRecordingMedleyRepository>(); var mockIRecRepository = A.Fake<IRecs>();

            //Build request
            List<KeyValuePair<int, List<int>>> lps = new List<KeyValuePair<int, List<int>>> { };
            UpdateLicenseProductsRequest request = new UpdateLicenseProductsRequest { LicenseId = 99, LicenseProducts = lps };

            LicenseProductConfiguration lpc = new LicenseProductConfiguration { };

            List<LicenseProductRecordingsDropdown> expected = new List<LicenseProductRecordingsDropdown> { };

            //Act
            LicenseProductManager manager = new LicenseProductManager(mockILicenseRepository, mockILicenseSequenceRepository, mockILicenseProductRepository, mockILicenseProductRecordingRepository, mockILicensePRWriterRepository, mockILicensePRWriterRateRepository, mockILicensePRWriterNoteRepository, mockILicensePRWriterRateStatusRepository, mockILicenseProductConfigurationRepository, mockIRecsDataProvider, mockILicenseNoteRepository, mockIAgreementStatutoryRateRepository, mockIStatRateRepository, mockILicenseSolrManager, mockILicenseAttachmentManager, mockIRecordingMedleyRepository, mockIRecRepository);
            var result = manager.GetLicenseProductRecordingsDropdown(A<int>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetLicenseProductRecordingsWritersDropdown_ReturnListLicenseProductRecordingsDropdown()
        {
            //Arrange
            var mockILicenseRepository = A.Fake<ILicenseRepository>();
            var mockILicenseSequenceRepository = A.Fake<ILicenseSequenceRepository>();
            var mockILicenseProductRepository = A.Fake<ILicenseProductRepository>();
            var mockILicenseProductRecordingRepository = A.Fake<ILicenseProductRecordingRepository>();
            var mockILicensePRWriterRepository = A.Fake<ILicensePRWriterRepository>();
            var mockILicensePRWriterRateRepository = A.Fake<ILicensePRWriterRateRepository>();
            var mockILicensePRWriterNoteRepository = A.Fake<ILicensePRWriterNoteRepository>();
            var mockILicensePRWriterRateStatusRepository = A.Fake<ILicensePRWriterRateStatusRepository>();
            var mockILicenseProductConfigurationRepository = A.Fake<ILicenseProductConfigurationRepository>();
            var mockIRecsDataProvider = A.Fake<IRecsDataProvider>();
            var mockILicenseNoteRepository = A.Fake<ILicenseNoteRepository>();
            var mockIAgreementStatutoryRateRepository = A.Fake<IAgreementStatutoryRateRepository>();
            var mockIStatRateRepository = A.Fake<IStatRateRepository>();
            var mockILicenseSolrManager = A.Fake<ILicenseSolrManager>();
            var mockILicenseAttachmentManager = A.Fake<ILicenseAttachmentManager>();
            var mockIRecordingMedleyRepository = A.Fake<IRecordingMedleyRepository>(); var mockIRecRepository = A.Fake<IRecs>();

            //Build request
            List<KeyValuePair<int, List<int>>> lps = new List<KeyValuePair<int, List<int>>> { };
            List<int> numbers = new List<int> { };

            GetWritersRatesRequest request = new GetWritersRatesRequest { LicenseId = 99, LicenseRecordingIds = numbers };

            LicenseProductConfiguration lpc = new LicenseProductConfiguration { };

            List<LicenseProductRecordingsDropdown> expected = new List<LicenseProductRecordingsDropdown> { };

            //Act
            LicenseProductManager manager = new LicenseProductManager(mockILicenseRepository, mockILicenseSequenceRepository, mockILicenseProductRepository, mockILicenseProductRecordingRepository, mockILicensePRWriterRepository, mockILicensePRWriterRateRepository, mockILicensePRWriterNoteRepository, mockILicensePRWriterRateStatusRepository, mockILicenseProductConfigurationRepository, mockIRecsDataProvider, mockILicenseNoteRepository, mockIAgreementStatutoryRateRepository, mockIStatRateRepository, mockILicenseSolrManager, mockILicenseAttachmentManager, mockIRecordingMedleyRepository, mockIRecRepository);
            var result = manager.GetLicenseProductRecordingsWritersDropdown(request);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetWritersNoForEditRates_ReturnListLicenseProductRecordingsDropdown()
        {
            //Arrange
            var mockILicenseRepository = A.Fake<ILicenseRepository>();
            var mockILicenseSequenceRepository = A.Fake<ILicenseSequenceRepository>();
            var mockILicenseProductRepository = A.Fake<ILicenseProductRepository>();
            var mockILicenseProductRecordingRepository = A.Fake<ILicenseProductRecordingRepository>();
            var mockILicensePRWriterRepository = A.Fake<ILicensePRWriterRepository>();
            var mockILicensePRWriterRateRepository = A.Fake<ILicensePRWriterRateRepository>();
            var mockILicensePRWriterNoteRepository = A.Fake<ILicensePRWriterNoteRepository>();
            var mockILicensePRWriterRateStatusRepository = A.Fake<ILicensePRWriterRateStatusRepository>();
            var mockILicenseProductConfigurationRepository = A.Fake<ILicenseProductConfigurationRepository>();
            var mockIRecsDataProvider = A.Fake<IRecsDataProvider>();
            var mockILicenseNoteRepository = A.Fake<ILicenseNoteRepository>();
            var mockIAgreementStatutoryRateRepository = A.Fake<IAgreementStatutoryRateRepository>();
            var mockIStatRateRepository = A.Fake<IStatRateRepository>();
            var mockILicenseSolrManager = A.Fake<ILicenseSolrManager>();
            var mockILicenseAttachmentManager = A.Fake<ILicenseAttachmentManager>();
            var mockIRecordingMedleyRepository = A.Fake<IRecordingMedleyRepository>(); var mockIRecRepository = A.Fake<IRecs>();

            //Build request
            List<KeyValuePair<int, List<int>>> lps = new List<KeyValuePair<int, List<int>>> { };
            List<int> numbers = new List<int> { };

            GetWritersRatesRequest request = new GetWritersRatesRequest { LicenseId = 99, LicenseRecordingIds = numbers, AllLicenseProductIds = numbers,LicenseWriterIds = numbers, LicenseConfigIds = numbers, LicenseProductIds = numbers };

            LicenseProductConfiguration lpc = new LicenseProductConfiguration { };

            List<LicenseProductRecordingsDropdown> expected = new List<LicenseProductRecordingsDropdown> { };

            //Act
            LicenseProductManager manager = new LicenseProductManager(mockILicenseRepository, mockILicenseSequenceRepository, mockILicenseProductRepository, mockILicenseProductRecordingRepository, mockILicensePRWriterRepository, mockILicensePRWriterRateRepository, mockILicensePRWriterNoteRepository, mockILicensePRWriterRateStatusRepository, mockILicenseProductConfigurationRepository, mockIRecsDataProvider, mockILicenseNoteRepository, mockIAgreementStatutoryRateRepository, mockIStatRateRepository, mockILicenseSolrManager, mockILicenseAttachmentManager, mockIRecordingMedleyRepository, mockIRecRepository);
            var result = manager.GetWritersNoForEditRates(request);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void EditIndividualWriterRates_ReturnBoolTRUE()
        {
            //Arrange
            var mockILicenseRepository = A.Fake<ILicenseRepository>();
            var mockILicenseSequenceRepository = A.Fake<ILicenseSequenceRepository>();
            var mockILicenseProductRepository = A.Fake<ILicenseProductRepository>();
            var mockILicenseProductRecordingRepository = A.Fake<ILicenseProductRecordingRepository>();
            var mockILicensePRWriterRepository = A.Fake<ILicensePRWriterRepository>();
            var mockILicensePRWriterRateRepository = A.Fake<ILicensePRWriterRateRepository>();
            var mockILicensePRWriterNoteRepository = A.Fake<ILicensePRWriterNoteRepository>();
            var mockILicensePRWriterRateStatusRepository = A.Fake<ILicensePRWriterRateStatusRepository>();
            var mockILicenseProductConfigurationRepository = A.Fake<ILicenseProductConfigurationRepository>();
            var mockIRecsDataProvider = A.Fake<IRecsDataProvider>();
            var mockILicenseNoteRepository = A.Fake<ILicenseNoteRepository>();
            var mockIAgreementStatutoryRateRepository = A.Fake<IAgreementStatutoryRateRepository>();
            var mockIStatRateRepository = A.Fake<IStatRateRepository>();
            var mockILicenseSolrManager = A.Fake<ILicenseSolrManager>();
            var mockILicenseAttachmentManager = A.Fake<ILicenseAttachmentManager>();
            var mockIRecordingMedleyRepository = A.Fake<IRecordingMedleyRepository>(); var mockIRecRepository = A.Fake<IRecs>();

            //Build request
            List<KeyValuePair<int, List<int>>> lps = new List<KeyValuePair<int, List<int>>> { };
            List<int> numbers = new List<int> { };

            List<EditRatesSaveRequest> request = new List<EditRatesSaveRequest> { };

            LicenseProductConfiguration lpc = new LicenseProductConfiguration { };

            List<LicenseProductRecordingsDropdown> expected = new List<LicenseProductRecordingsDropdown> { };

            //Act
            LicenseProductManager manager = new LicenseProductManager(mockILicenseRepository, mockILicenseSequenceRepository, mockILicenseProductRepository, mockILicenseProductRecordingRepository, mockILicensePRWriterRepository, mockILicensePRWriterRateRepository, mockILicensePRWriterNoteRepository, mockILicensePRWriterRateStatusRepository, mockILicenseProductConfigurationRepository, mockIRecsDataProvider, mockILicenseNoteRepository, mockIAgreementStatutoryRateRepository, mockIStatRateRepository, mockILicenseSolrManager, mockILicenseAttachmentManager, mockIRecordingMedleyRepository, mockIRecRepository);
            var result = manager.EditIndividualWriterRates(request);

            //Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void EditRatesAndWriters_ReturnBoolTRUE()
        {
            //Arrange
            var mockILicenseRepository = A.Fake<ILicenseRepository>();
            var mockILicenseSequenceRepository = A.Fake<ILicenseSequenceRepository>();
            var mockILicenseProductRepository = A.Fake<ILicenseProductRepository>();
            var mockILicenseProductRecordingRepository = A.Fake<ILicenseProductRecordingRepository>();
            var mockILicensePRWriterRepository = A.Fake<ILicensePRWriterRepository>();
            var mockILicensePRWriterRateRepository = A.Fake<ILicensePRWriterRateRepository>();
            var mockILicensePRWriterNoteRepository = A.Fake<ILicensePRWriterNoteRepository>();
            var mockILicensePRWriterRateStatusRepository = A.Fake<ILicensePRWriterRateStatusRepository>();
            var mockILicenseProductConfigurationRepository = A.Fake<ILicenseProductConfigurationRepository>();
            var mockIRecsDataProvider = A.Fake<IRecsDataProvider>();
            var mockILicenseNoteRepository = A.Fake<ILicenseNoteRepository>();
            var mockIAgreementStatutoryRateRepository = A.Fake<IAgreementStatutoryRateRepository>();
            var mockIStatRateRepository = A.Fake<IStatRateRepository>();
            var mockILicenseSolrManager = A.Fake<ILicenseSolrManager>();
            var mockILicenseAttachmentManager = A.Fake<ILicenseAttachmentManager>();
            var mockIRecordingMedleyRepository = A.Fake<IRecordingMedleyRepository>(); var mockIRecRepository = A.Fake<IRecs>();

            //Build request
            List<EditRatesSaveRequest> rates = new List<EditRatesSaveRequest> { };
            List<KeyValuePair<int, List<int>>> lps = new List<KeyValuePair<int, List<int>>> { };
            List<int> numbers = new List<int> { };

            EditRatesSaveRequest request = new EditRatesSaveRequest { SelectedStatusesIds = numbers, SelectedWritersIds = numbers, Rates = rates };

            LicenseProductConfiguration lpc = new LicenseProductConfiguration { };

            List<LicenseProductRecordingsDropdown> expected = new List<LicenseProductRecordingsDropdown> { };

            //Act
            LicenseProductManager manager = new LicenseProductManager(mockILicenseRepository, mockILicenseSequenceRepository, mockILicenseProductRepository, mockILicenseProductRecordingRepository, mockILicensePRWriterRepository, mockILicensePRWriterRateRepository, mockILicensePRWriterNoteRepository, mockILicensePRWriterRateStatusRepository, mockILicenseProductConfigurationRepository, mockIRecsDataProvider, mockILicenseNoteRepository, mockIAgreementStatutoryRateRepository, mockIStatRateRepository, mockILicenseSolrManager, mockILicenseAttachmentManager, mockIRecordingMedleyRepository, mockIRecRepository);
            var result = manager.EditRatesAndWriters(request);

            //Assert
            Assert.IsTrue(result);
        }


        [Test]
        public void GetLicenseTemplate_ReturnLicenseTemplate()
        {
            //Arrange
            var mockILicenseRepository = A.Fake<ILicenseRepository>();
            var mockILicenseSequenceRepository = A.Fake<ILicenseSequenceRepository>();
            var mockILicenseProductRepository = A.Fake<ILicenseProductRepository>();
            var mockILicenseProductRecordingRepository = A.Fake<ILicenseProductRecordingRepository>();
            var mockILicensePRWriterRepository = A.Fake<ILicensePRWriterRepository>();
            var mockILicensePRWriterRateRepository = A.Fake<ILicensePRWriterRateRepository>();
            var mockILicensePRWriterNoteRepository = A.Fake<ILicensePRWriterNoteRepository>();
            var mockILicensePRWriterRateStatusRepository = A.Fake<ILicensePRWriterRateStatusRepository>();
            var mockILicenseProductConfigurationRepository = A.Fake<ILicenseProductConfigurationRepository>();
            var mockIRecsDataProvider = A.Fake<IRecsDataProvider>();
            var mockILicenseNoteRepository = A.Fake<ILicenseNoteRepository>();
            var mockIAgreementStatutoryRateRepository = A.Fake<IAgreementStatutoryRateRepository>();
            var mockIStatRateRepository = A.Fake<IStatRateRepository>();
            var mockILicenseSolrManager = A.Fake<ILicenseSolrManager>();
            var mockILicenseAttachmentManager = A.Fake<ILicenseAttachmentManager>();
            var mockIRecordingMedleyRepository = A.Fake<IRecordingMedleyRepository>(); var mockIRecRepository = A.Fake<IRecs>();

            //Build request
            List<EditRatesSaveRequest> rates = new List<EditRatesSaveRequest> { };
            List<KeyValuePair<int, List<int>>> lps = new List<KeyValuePair<int, List<int>>> { };
            List<int> numbers = new List<int> { };

            const int request = 99;

            LicenseProductConfiguration lpc = new LicenseProductConfiguration { };

            LicenseTemplate expected = new LicenseTemplate { };

            //Act
            LicenseProductManager manager = new LicenseProductManager(mockILicenseRepository, mockILicenseSequenceRepository, mockILicenseProductRepository, mockILicenseProductRecordingRepository, mockILicensePRWriterRepository, mockILicensePRWriterRateRepository, mockILicensePRWriterNoteRepository, mockILicensePRWriterRateStatusRepository, mockILicenseProductConfigurationRepository, mockIRecsDataProvider, mockILicenseNoteRepository, mockIAgreementStatutoryRateRepository, mockIStatRateRepository, mockILicenseSolrManager, mockILicenseAttachmentManager, mockIRecordingMedleyRepository, mockIRecRepository);
            var result = manager.GetLicenseTemplate(request);

            //Assert
            Assert.IsInstanceOf(typeof(LicenseTemplate), result);
        }

        [Test]
        public void GetLicenseTemplateV2_ReturnLicenseTemplate()
        {
            //Arrange
            var mockILicenseRepository = A.Fake<ILicenseRepository>();
            var mockILicenseSequenceRepository = A.Fake<ILicenseSequenceRepository>();
            var mockILicenseProductRepository = A.Fake<ILicenseProductRepository>();
            var mockILicenseProductRecordingRepository = A.Fake<ILicenseProductRecordingRepository>();
            var mockILicensePRWriterRepository = A.Fake<ILicensePRWriterRepository>();
            var mockILicensePRWriterRateRepository = A.Fake<ILicensePRWriterRateRepository>();
            var mockILicensePRWriterNoteRepository = A.Fake<ILicensePRWriterNoteRepository>();
            var mockILicensePRWriterRateStatusRepository = A.Fake<ILicensePRWriterRateStatusRepository>();
            var mockILicenseProductConfigurationRepository = A.Fake<ILicenseProductConfigurationRepository>();
            var mockIRecsDataProvider = A.Fake<IRecsDataProvider>();
            var mockILicenseNoteRepository = A.Fake<ILicenseNoteRepository>();
            var mockIAgreementStatutoryRateRepository = A.Fake<IAgreementStatutoryRateRepository>();
            var mockIStatRateRepository = A.Fake<IStatRateRepository>();
            var mockILicenseSolrManager = A.Fake<ILicenseSolrManager>();
            var mockILicenseAttachmentManager = A.Fake<ILicenseAttachmentManager>();
            var mockIRecordingMedleyRepository = A.Fake<IRecordingMedleyRepository>(); var mockIRecRepository = A.Fake<IRecs>();

            //Build request
            List<EditRatesSaveRequest> rates = new List<EditRatesSaveRequest> { };
            List<KeyValuePair<int, List<int>>> lps = new List<KeyValuePair<int, List<int>>> { };
            List<int> numbers = new List<int> { };

            const int request = 99;

            LicenseProductConfiguration lpc = new LicenseProductConfiguration { };

            LicenseTemplate expected = new LicenseTemplate { };

            //Act
            LicenseProductManager manager = new LicenseProductManager(mockILicenseRepository, mockILicenseSequenceRepository, mockILicenseProductRepository, mockILicenseProductRecordingRepository, mockILicensePRWriterRepository, mockILicensePRWriterRateRepository, mockILicensePRWriterNoteRepository, mockILicensePRWriterRateStatusRepository, mockILicenseProductConfigurationRepository, mockIRecsDataProvider, mockILicenseNoteRepository, mockIAgreementStatutoryRateRepository, mockIStatRateRepository, mockILicenseSolrManager, mockILicenseAttachmentManager, mockIRecordingMedleyRepository, mockIRecRepository);
            var result = manager.GetLicenseTemplateV2(request);

            //Assert
            Assert.IsInstanceOf(typeof(LicenseTemplate), result);
        }


        [Test]
        public void CloneLicense_ReturnCloneLicenseResult()
        {
            //Arrange
            var mockILicenseRepository = A.Fake<ILicenseRepository>();
            var mockILicenseSequenceRepository = A.Fake<ILicenseSequenceRepository>();
            var mockILicenseProductRepository = A.Fake<ILicenseProductRepository>();
            var mockILicenseProductRecordingRepository = A.Fake<ILicenseProductRecordingRepository>();
            var mockILicensePRWriterRepository = A.Fake<ILicensePRWriterRepository>();
            var mockILicensePRWriterRateRepository = A.Fake<ILicensePRWriterRateRepository>();
            var mockILicensePRWriterNoteRepository = A.Fake<ILicensePRWriterNoteRepository>();
            var mockILicensePRWriterRateStatusRepository = A.Fake<ILicensePRWriterRateStatusRepository>();
            var mockILicenseProductConfigurationRepository = A.Fake<ILicenseProductConfigurationRepository>();
            var mockIRecsDataProvider = A.Fake<IRecsDataProvider>();
            var mockILicenseNoteRepository = A.Fake<ILicenseNoteRepository>();
            var mockIAgreementStatutoryRateRepository = A.Fake<IAgreementStatutoryRateRepository>();
            var mockIStatRateRepository = A.Fake<IStatRateRepository>();
            var mockILicenseSolrManager = A.Fake<ILicenseSolrManager>();
            var mockILicenseAttachmentManager = A.Fake<ILicenseAttachmentManager>();
            var mockIRecordingMedleyRepository = A.Fake<IRecordingMedleyRepository>(); var mockIRecRepository = A.Fake<IRecs>();

            //Build request
            List<EditRatesSaveRequest> rates = new List<EditRatesSaveRequest> { };
            List<KeyValuePair<int, List<int>>> lps = new List<KeyValuePair<int, List<int>>> { };
            List<int> numbers = new List<int> { };

            LicenseProductConfiguration lpc = new LicenseProductConfiguration { };

            CloneLicenseResult expected = new CloneLicenseResult { };

            //Act
            LicenseProductManager manager = new LicenseProductManager(mockILicenseRepository, mockILicenseSequenceRepository, mockILicenseProductRepository, mockILicenseProductRecordingRepository, mockILicensePRWriterRepository, mockILicensePRWriterRateRepository, mockILicensePRWriterNoteRepository, mockILicensePRWriterRateStatusRepository, mockILicenseProductConfigurationRepository, mockIRecsDataProvider, mockILicenseNoteRepository, mockIAgreementStatutoryRateRepository, mockIStatRateRepository, mockILicenseSolrManager, mockILicenseAttachmentManager, mockIRecordingMedleyRepository, mockIRecRepository);
            var result = manager.CloneLicense(A<int>.Ignored, A<string>.Ignored, A<int>.Ignored);

            //Assert
            Assert.IsInstanceOf(typeof(CloneLicenseResult), result);
        }

        [Test]
        public void EditWriterIsIncluded_ReturnBoolTRUE()
        {
            //Arrange
            var mockILicenseRepository = A.Fake<ILicenseRepository>();
            var mockILicenseSequenceRepository = A.Fake<ILicenseSequenceRepository>();
            var mockILicenseProductRepository = A.Fake<ILicenseProductRepository>();
            var mockILicenseProductRecordingRepository = A.Fake<ILicenseProductRecordingRepository>();
            var mockILicensePRWriterRepository = A.Fake<ILicensePRWriterRepository>();
            var mockILicensePRWriterRateRepository = A.Fake<ILicensePRWriterRateRepository>();
            var mockILicensePRWriterNoteRepository = A.Fake<ILicensePRWriterNoteRepository>();
            var mockILicensePRWriterRateStatusRepository = A.Fake<ILicensePRWriterRateStatusRepository>();
            var mockILicenseProductConfigurationRepository = A.Fake<ILicenseProductConfigurationRepository>();
            var mockIRecsDataProvider = A.Fake<IRecsDataProvider>();
            var mockILicenseNoteRepository = A.Fake<ILicenseNoteRepository>();
            var mockIAgreementStatutoryRateRepository = A.Fake<IAgreementStatutoryRateRepository>();
            var mockIStatRateRepository = A.Fake<IStatRateRepository>();
            var mockILicenseSolrManager = A.Fake<ILicenseSolrManager>();
            var mockILicenseAttachmentManager = A.Fake<ILicenseAttachmentManager>();
            var mockIRecordingMedleyRepository = A.Fake<IRecordingMedleyRepository>(); var mockIRecRepository = A.Fake<IRecs>();

            //Build request
            List<EditRatesSaveRequest> rates = new List<EditRatesSaveRequest> { };
            List<KeyValuePair<int, List<int>>> lps = new List<KeyValuePair<int, List<int>>> { };
            List<int> numbers = new List<int> { };

            EditWriterIncludedSaveRequest request = new EditWriterIncludedSaveRequest
            {
                IsIncluded = true,
                SaveWriterIds = numbers,
            };

            LicenseProductConfiguration lpc = new LicenseProductConfiguration { };

            CloneLicenseResult expected = new CloneLicenseResult { };

            //Act
            LicenseProductManager manager = new LicenseProductManager(mockILicenseRepository, mockILicenseSequenceRepository, mockILicenseProductRepository, mockILicenseProductRecordingRepository, mockILicensePRWriterRepository, mockILicensePRWriterRateRepository, mockILicensePRWriterNoteRepository, mockILicensePRWriterRateStatusRepository, mockILicenseProductConfigurationRepository, mockIRecsDataProvider, mockILicenseNoteRepository, mockIAgreementStatutoryRateRepository, mockIStatRateRepository, mockILicenseSolrManager, mockILicenseAttachmentManager, mockIRecordingMedleyRepository, mockIRecRepository);
            var result = manager.EditWriterIsIncluded(request);

            //Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void EditWriterConsent_ReturnBoolTRUE()
        {
            //Arrange
            var mockILicenseRepository = A.Fake<ILicenseRepository>();
            var mockILicenseSequenceRepository = A.Fake<ILicenseSequenceRepository>();
            var mockILicenseProductRepository = A.Fake<ILicenseProductRepository>();
            var mockILicenseProductRecordingRepository = A.Fake<ILicenseProductRecordingRepository>();
            var mockILicensePRWriterRepository = A.Fake<ILicensePRWriterRepository>();
            var mockILicensePRWriterRateRepository = A.Fake<ILicensePRWriterRateRepository>();
            var mockILicensePRWriterNoteRepository = A.Fake<ILicensePRWriterNoteRepository>();
            var mockILicensePRWriterRateStatusRepository = A.Fake<ILicensePRWriterRateStatusRepository>();
            var mockILicenseProductConfigurationRepository = A.Fake<ILicenseProductConfigurationRepository>();
            var mockIRecsDataProvider = A.Fake<IRecsDataProvider>();
            var mockILicenseNoteRepository = A.Fake<ILicenseNoteRepository>();
            var mockIAgreementStatutoryRateRepository = A.Fake<IAgreementStatutoryRateRepository>();
            var mockIStatRateRepository = A.Fake<IStatRateRepository>();
            var mockILicenseSolrManager = A.Fake<ILicenseSolrManager>();
            var mockILicenseAttachmentManager = A.Fake<ILicenseAttachmentManager>();
            var mockIRecordingMedleyRepository = A.Fake<IRecordingMedleyRepository>(); var mockIRecRepository = A.Fake<IRecs>();

            //Build request
            List<EditRatesSaveRequest> rates = new List<EditRatesSaveRequest> { };
            List<KeyValuePair<int, List<int>>> lps = new List<KeyValuePair<int, List<int>>> { };
            List<int> numbers = new List<int> { };

            EditWriterConsentSaveRequest request = new EditWriterConsentSaveRequest
            {
                
                SaveWriterIds = numbers
            };

            LicenseProductConfiguration lpc = new LicenseProductConfiguration { };

            CloneLicenseResult expected = new CloneLicenseResult { };

            //Act
            LicenseProductManager manager = new LicenseProductManager(mockILicenseRepository, mockILicenseSequenceRepository, mockILicenseProductRepository, mockILicenseProductRecordingRepository, mockILicensePRWriterRepository, mockILicensePRWriterRateRepository, mockILicensePRWriterNoteRepository, mockILicensePRWriterRateStatusRepository, mockILicenseProductConfigurationRepository, mockIRecsDataProvider, mockILicenseNoteRepository, mockIAgreementStatutoryRateRepository, mockIStatRateRepository, mockILicenseSolrManager, mockILicenseAttachmentManager, mockIRecordingMedleyRepository, mockIRecRepository);
            var result = manager.EditWriterConsent(request);

            //Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void EditPaidQuarter_ReturnBoolTRUE()
        {
            //Arrange
            var mockILicenseRepository = A.Fake<ILicenseRepository>();
            var mockILicenseSequenceRepository = A.Fake<ILicenseSequenceRepository>();
            var mockILicenseProductRepository = A.Fake<ILicenseProductRepository>();
            var mockILicenseProductRecordingRepository = A.Fake<ILicenseProductRecordingRepository>();
            var mockILicensePRWriterRepository = A.Fake<ILicensePRWriterRepository>();
            var mockILicensePRWriterRateRepository = A.Fake<ILicensePRWriterRateRepository>();
            var mockILicensePRWriterNoteRepository = A.Fake<ILicensePRWriterNoteRepository>();
            var mockILicensePRWriterRateStatusRepository = A.Fake<ILicensePRWriterRateStatusRepository>();
            var mockILicenseProductConfigurationRepository = A.Fake<ILicenseProductConfigurationRepository>();
            var mockIRecsDataProvider = A.Fake<IRecsDataProvider>();
            var mockILicenseNoteRepository = A.Fake<ILicenseNoteRepository>();
            var mockIAgreementStatutoryRateRepository = A.Fake<IAgreementStatutoryRateRepository>();
            var mockIStatRateRepository = A.Fake<IStatRateRepository>();
            var mockILicenseSolrManager = A.Fake<ILicenseSolrManager>();
            var mockILicenseAttachmentManager = A.Fake<ILicenseAttachmentManager>();
            var mockIRecordingMedleyRepository = A.Fake<IRecordingMedleyRepository>(); var mockIRecRepository = A.Fake<IRecs>();

            //Build request
            List<EditRatesSaveRequest> rates = new List<EditRatesSaveRequest> { };
            List<KeyValuePair<int, List<int>>> lps = new List<KeyValuePair<int, List<int>>> { };
            List<int> numbers = new List<int> { };

            EditPaidQuarterSaveRequest request = new EditPaidQuarterSaveRequest
            {

                SaveWriterIds = numbers
            };

            LicenseProductConfiguration lpc = new LicenseProductConfiguration { };

            CloneLicenseResult expected = new CloneLicenseResult { };

            //Act
            LicenseProductManager manager = new LicenseProductManager(mockILicenseRepository, mockILicenseSequenceRepository, mockILicenseProductRepository, mockILicenseProductRecordingRepository, mockILicensePRWriterRepository, mockILicensePRWriterRateRepository, mockILicensePRWriterNoteRepository, mockILicensePRWriterRateStatusRepository, mockILicenseProductConfigurationRepository, mockIRecsDataProvider, mockILicenseNoteRepository, mockIAgreementStatutoryRateRepository, mockIStatRateRepository, mockILicenseSolrManager, mockILicenseAttachmentManager, mockIRecordingMedleyRepository, mockIRecRepository);
            var result = manager.EditPaidQuarter(request);

            //Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void UpdateLicenseProductSchedule_ReturnVoid()
        {
            //Arrange
            var mockILicenseRepository = A.Fake<ILicenseRepository>();
            var mockILicenseSequenceRepository = A.Fake<ILicenseSequenceRepository>();
            var mockILicenseProductRepository = A.Fake<ILicenseProductRepository>();
            var mockILicenseProductRecordingRepository = A.Fake<ILicenseProductRecordingRepository>();
            var mockILicensePRWriterRepository = A.Fake<ILicensePRWriterRepository>();
            var mockILicensePRWriterRateRepository = A.Fake<ILicensePRWriterRateRepository>();
            var mockILicensePRWriterNoteRepository = A.Fake<ILicensePRWriterNoteRepository>();
            var mockILicensePRWriterRateStatusRepository = A.Fake<ILicensePRWriterRateStatusRepository>();
            var mockILicenseProductConfigurationRepository = A.Fake<ILicenseProductConfigurationRepository>();
            var mockIRecsDataProvider = A.Fake<IRecsDataProvider>();
            var mockILicenseNoteRepository = A.Fake<ILicenseNoteRepository>();
            var mockIAgreementStatutoryRateRepository = A.Fake<IAgreementStatutoryRateRepository>();
            var mockIStatRateRepository = A.Fake<IStatRateRepository>();
            var mockILicenseSolrManager = A.Fake<ILicenseSolrManager>();
            var mockILicenseAttachmentManager = A.Fake<ILicenseAttachmentManager>();
            var mockIRecordingMedleyRepository = A.Fake<IRecordingMedleyRepository>(); var mockIRecRepository = A.Fake<IRecs>();

            //Build request
            List<EditRatesSaveRequest> rates = new List<EditRatesSaveRequest> { };
            List<KeyValuePair<int, List<int>>> lps = new List<KeyValuePair<int, List<int>>> { };
            List<int> numbers = new List<int> { };

            List<LicenseProduct> request = new List<LicenseProduct> { };
            LicenseProduct lp1 = new LicenseProduct { };
            request.Add(lp1);
            
            LicenseProductConfiguration lpc = new LicenseProductConfiguration { };

            CloneLicenseResult expected = new CloneLicenseResult { };

            //Act
            LicenseProductManager manager = new LicenseProductManager(mockILicenseRepository, mockILicenseSequenceRepository, mockILicenseProductRepository, mockILicenseProductRecordingRepository, mockILicensePRWriterRepository, mockILicensePRWriterRateRepository, mockILicensePRWriterNoteRepository, mockILicensePRWriterRateStatusRepository, mockILicenseProductConfigurationRepository, mockIRecsDataProvider, mockILicenseNoteRepository, mockIAgreementStatutoryRateRepository, mockIStatRateRepository, mockILicenseSolrManager, mockILicenseAttachmentManager, mockIRecordingMedleyRepository, mockIRecRepository);
            manager.UpdateLicenseProductSchedule(request);
            mockILicenseProductRepository.Update(lp1);
            //Assert
            A.CallTo(() => mockILicenseProductRepository.Update(lp1)).WithAnyArguments().MustHaveHappened();
        }
    }
}
