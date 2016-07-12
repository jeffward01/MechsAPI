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
using UMPG.USL.Models.StaticDropdownsData;
using UMPG.USL.Models.LicenseTemplateModel;

namespace UMPG.USL.API.Tests.Controller_Tests.License_Controller_Tests
{
    public class LicenseProductControllerTests
    {
        [Test]
        public void GetProducts_ReturnListLicenseProduct()
        {
            //Arrange
            var mockLicenseProductManager = A.Fake<ILicenseProductManager>();

            //Build expected
            List<LicenseProduct> expected = new List<LicenseProduct> { };

            A.CallTo(() => mockLicenseProductManager.GetProducts(A<int>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            LicenseProductController controller = new LicenseProductController(mockLicenseProductManager);
            var result = controller.GetProducts(A<int>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetSelectedProduct_ReturnLicenseProduct()
        {
            //Arrange
            var mockLicenseProductManager = A.Fake<ILicenseProductManager>();

            //Build expected
            LicenseProduct expected = new LicenseProduct { };

            A.CallTo(() => mockLicenseProductManager.GetSelectedProduct(A<int>.Ignored, A<int>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            LicenseProductController controller = new LicenseProductController(mockLicenseProductManager);
            var result = controller.GetSelectedProduct(A<int>.Ignored, A<int>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void DeleteLicenseProduct_ReturnBoolTRUE()
        {
            //Arrange
            var mockLicenseProductManager = A.Fake<ILicenseProductManager>();

            //Build expected
            const bool expected = true;

            A.CallTo(() => mockLicenseProductManager.DeleteLicenseProduct(A<int>.Ignored, A<int>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            LicenseProductController controller = new LicenseProductController(mockLicenseProductManager);
            var result = controller.DeleteLicenseProduct(A<int>.Ignored, A<int>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }


        [Test]
        public void GetRecordings_ReturnListLicenseProductRecording()
        {
            //Arrange
            var mockLicenseProductManager = A.Fake<ILicenseProductManager>();

            //Build expected
            List<LicenseProductRecording> expected = new List<LicenseProductRecording> { };

            A.CallTo(() => mockLicenseProductManager.GetLicenseProductRecordings(A<int>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            LicenseProductController controller = new LicenseProductController(mockLicenseProductManager);
            var result = controller.GetRecordings(A<int>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetLicenseProductRecordingWritersNo_ReturnInt()
        {
            //Arrange
            var mockLicenseProductManager = A.Fake<ILicenseProductManager>();

            //Build expected
            const int expected = 99;

            A.CallTo(() => mockLicenseProductManager.GetLicenseProductRecordingWritersNo(A<int>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            LicenseProductController controller = new LicenseProductController(mockLicenseProductManager);
            var result = controller.GetLicenseProductRecordingWritersNo(A<int>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetLicensePRWriterRates_ReturnListLicenseProductRecording()
        {
            //Arrange
            var mockLicenseProductManager = A.Fake<ILicenseProductManager>();

            //Build expected
            List<LicenseProductRecordingWriterRate> expected = new List<LicenseProductRecordingWriterRate> { };

            A.CallTo(() => mockLicenseProductManager.GetLicensePRWriterRates(A<int>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            LicenseProductController controller = new LicenseProductController(mockLicenseProductManager);
            var result = controller.GetLicensePRWriterRates(A<int>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetLicenseProductConfigurations_ReturnListLicenseProductRecording()
        {
            //Arrange
            var mockLicenseProductManager = A.Fake<ILicenseProductManager>();

            //Build expected
            List<LicenseProductConfiguration> expected = new List<LicenseProductConfiguration> { };

            A.CallTo(() => mockLicenseProductManager.GetLicenseProductConfigurations(A<int>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            LicenseProductController controller = new LicenseProductController(mockLicenseProductManager);
            var result = controller.GetLicenseProductConfigurations(A<int>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void UpdateLicenseProductConfigurations_ReturnBoolTRUE()
        {
            //Arrange
            var mockLicenseProductManager = A.Fake<ILicenseProductManager>();

            //Build expected
            const bool expected = true;

            A.CallTo(() => mockLicenseProductManager.UpdateLicenseProductConfigurations(A<UpdateLicenseProductConfigurationsStatusRequest>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            LicenseProductController controller = new LicenseProductController(mockLicenseProductManager);
            var result = controller.UpdateLicenseProductConfigurations(A<UpdateLicenseProductConfigurationsStatusRequest>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetProductConfigurationsAll_ReturnListProductConfiguration()
        {
            //Arrange
            var mockLicenseProductManager = A.Fake<ILicenseProductManager>();

            //Build expected
            List<ProductConfiguration> expected = new List<ProductConfiguration> { };

            A.CallTo(() => mockLicenseProductManager.GetProductConfigurationsAll(A<GetProductConfigurationsAllRequest>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            LicenseProductController controller = new LicenseProductController(mockLicenseProductManager);
            var result = controller.GetProductConfigurationsAll(A<GetProductConfigurationsAllRequest>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void UpdateProductConfigurationsAll_ReturnBoolTRUE()
        {
            //Arrange
            var mockLicenseProductManager = A.Fake<ILicenseProductManager>();

            //Build expected
            const bool expected = true;

            A.CallTo(() => mockLicenseProductManager.UpdateProductConfigurationsAll(A<List<UpdateProductConfigurationsAllRequest>>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            LicenseProductController controller = new LicenseProductController(mockLicenseProductManager);
            var result = controller.UpdateProductConfigurationsAll(A<List<UpdateProductConfigurationsAllRequest>>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void UpdateLicenseProducts_ReturnBoolTRUE()
        {
            //Arrange
            var mockLicenseProductManager = A.Fake<ILicenseProductManager>();

            //Build expected
            const bool expected = true;

            A.CallTo(() => mockLicenseProductManager.UpdateLicenseProducts(A<UpdateLicenseProductsRequest>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            LicenseProductController controller = new LicenseProductController(mockLicenseProductManager);
            var result = controller.UpdateLicenseProducts(A<UpdateLicenseProductsRequest>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void UpdateLicenseProductSchedule_ReturnBoolTRUE()
        {
            //Arrange
            var mockLicenseProductManager = A.Fake<ILicenseProductManager>();

            A.CallTo(() => mockLicenseProductManager.UpdateLicenseProductSchedule(A<List<LicenseProduct>>.Ignored)).WithAnyArguments();

            //Act
            LicenseProductController controller = new LicenseProductController(mockLicenseProductManager);
            var result = controller.UpdateLicenseProductSchedule(A<List<LicenseProduct>>.Ignored);

            //Assert
            A.CallTo(() => mockLicenseProductManager.UpdateLicenseProductSchedule(A<List<LicenseProduct>>.Ignored)).WithAnyArguments().MustHaveHappened();
        }

        [Test]
        public void GetLicenseProductConfigurationsAll_ReturnBoolTRUE()
        {
            //Arrange
            var mockLicenseProductManager = A.Fake<ILicenseProductManager>();

            //Build expected
            List<LicenseProductConfigurations> expected = new List<LicenseProductConfigurations> { };

            A.CallTo(() => mockLicenseProductManager.GetLicenseProductConfigurationsAll(A<int>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            LicenseProductController controller = new LicenseProductController(mockLicenseProductManager);
            var result = controller.GetLicenseProductConfigurationsAll(A<int>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void UpdateLicenseProductConfigurationsAll_ReturnBoolTRUE()
        {
            //Arrange
            var mockLicenseProductManager = A.Fake<ILicenseProductManager>();
            
            //Build expected
            const bool expected = true;

            A.CallTo(() => mockLicenseProductManager.UpdateLicenseProductConfigurationsAll(A<List<UpdateLicenseProductConfigurationsRequest>>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            LicenseProductController controller = new LicenseProductController(mockLicenseProductManager);
            var result = controller.UpdateLicenseProductConfigurations(A<List<UpdateLicenseProductConfigurationsRequest>>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetAllLicenseRelatedIds_ReturnGetWritersRatesRequest()
        {
            //Arrange
            var mockLicenseProductManager = A.Fake<ILicenseProductManager>();

            //Build expected
            GetWritersRatesRequest expected = new GetWritersRatesRequest { };

            A.CallTo(() => mockLicenseProductManager.GetAllLicenseRelatedIds(A<int>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            LicenseProductController controller = new LicenseProductController(mockLicenseProductManager);
            var result = controller.GetAllLicenseRelatedIds(A<int>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetAllLicenseRecordingRelatedIds_ReturnGetWritersRatesRequest()
        {
            //Arrange
            var mockLicenseProductManager = A.Fake<ILicenseProductManager>();

            //Build expected
            GetWritersRatesRequest expected = new GetWritersRatesRequest { };

            A.CallTo(() => mockLicenseProductManager.GetAllLicenseRecordingRelatedIds(A<List<int>>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            LicenseProductController controller = new LicenseProductController(mockLicenseProductManager);
            var result = controller.GetAllLicenseRecordingRelatedIds(A<List<int>>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetLicenseProductRecordingsDropdown_ReturnListLicenseProductRecordingsDropdown()
        {
            //Arrange
            var mockLicenseProductManager = A.Fake<ILicenseProductManager>();

            //Build expected
            List<LicenseProductRecordingsDropdown> expected = new List<LicenseProductRecordingsDropdown> { };

            A.CallTo(() => mockLicenseProductManager.GetLicenseProductRecordingsDropdown(A<int>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            LicenseProductController controller = new LicenseProductController(mockLicenseProductManager);
            var result = controller.GetLicenseProductRecordingsDropdown(A<int>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetLicenseProductRecordingsWritersDropdown_ReturnListLicenseProductRecordingsDropdown()
        {
            //Arrange
            var mockLicenseProductManager = A.Fake<ILicenseProductManager>();

            //Build expected
            List<LicenseProductRecordingWritersDropdown> expected = new List<LicenseProductRecordingWritersDropdown> { };

            A.CallTo(() => mockLicenseProductManager.GetLicenseProductRecordingsWritersDropdown(A<GetWritersRatesRequest>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            LicenseProductController controller = new LicenseProductController(mockLicenseProductManager);
            var result = controller.GetLicenseProductRecordingsWritersDropdown(A<GetWritersRatesRequest>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }


        [Test]
        public void EditRates_ReturnBoolTRUE()
        {
            //Arrange
            var mockLicenseProductManager = A.Fake<ILicenseProductManager>();

            //Build expected
            const bool expected = true;

            A.CallTo(() => mockLicenseProductManager.EditIndividualWriterRates(A<List<EditRatesSaveRequest>>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            LicenseProductController controller = new LicenseProductController(mockLicenseProductManager);
            var result = controller.EditRates(A<List<EditRatesSaveRequest>>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetWritersNoForEditRates_ReturnListInt()
        {
            //Arrange
            var mockLicenseProductManager = A.Fake<ILicenseProductManager>();

            //Build expected
            List<int> expected = new List<int> { };

            A.CallTo(() => mockLicenseProductManager.GetWritersNoForEditRates(A<GetWritersRatesRequest>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            LicenseProductController controller = new LicenseProductController(mockLicenseProductManager);
            var result = controller.GetWritersNoForEditRates(A<GetWritersRatesRequest>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetLicenseProductRecordingsV2_ReturnListWorksRecording()
        {
            //Arrange
            var mockLicenseProductManager = A.Fake<ILicenseProductManager>();

            //Build expected
            List<WorksRecording> expected = new List<WorksRecording> { };

            A.CallTo(() => mockLicenseProductManager.GetLicenseProductRecordingsV2(A<int>.Ignored, A<string>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            LicenseProductController controller = new LicenseProductController(mockLicenseProductManager);
            var result = controller.GetLicenseProductRecordingsV2(A<int>.Ignored, A<string>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetLicenseWritersV2_ReturnListWorksWriter()
        {
            //Arrange
            var mockLicenseProductManager = A.Fake<ILicenseProductManager>();

            //Build expected
            List<WorksWriter> expected = new List<WorksWriter> { };

            A.CallTo(() => mockLicenseProductManager.GetLicenseWritersV2(A<int>.Ignored, A<string>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            LicenseProductController controller = new LicenseProductController(mockLicenseProductManager);
            var result = controller.GetLicenseWritersV2(A<int>.Ignored, A<string>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetLicenseWritersV2_ReturnLicenseTemplate()
        {
            //Arrange
            var mockLicenseProductManager = A.Fake<ILicenseProductManager>();

            //Build expected
            LicenseTemplate expected = new LicenseTemplate { };

            A.CallTo(() => mockLicenseProductManager.GetLicenseTemplate(A<int>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            LicenseProductController controller = new LicenseProductController(mockLicenseProductManager);
            var result = controller.GetLicensePreview(A<int>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void CloneLicense_ReturnCloneLicenseResult()
        {
            //Arrange
            var mockLicenseProductManager = A.Fake<ILicenseProductManager>();

            //Build expected
            CloneLicenseResult expected = new CloneLicenseResult { };

            A.CallTo(() => mockLicenseProductManager.CloneLicense(A<int>.Ignored, A<string>.Ignored,A<int>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            LicenseProductController controller = new LicenseProductController(mockLicenseProductManager);
            var result = controller.CloneLicense(A<int>.Ignored, A<string>.Ignored, A<int>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetLicenseWriterRateIdsWithOutHolds_ReturnListInt()
        {
            //Arrange
            var mockLicenseProductManager = A.Fake<ILicenseProductManager>();

            //Build expected
            List<int> expected = new List<int> { };

            A.CallTo(() => mockLicenseProductManager.GetLicenseWriterRateIdsWithOutHolds(A<int>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            LicenseProductController controller = new LicenseProductController(mockLicenseProductManager);
            var result = controller.GetLicenseWriterRateStatusesWithOutHolds(A<int>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetYearsForEditRates_ReturnListInt()
        {
            //Arrange
            var mockLicenseProductManager = A.Fake<ILicenseProductManager>();

            //Build expected
            List<int> expected = new List<int> { };

            A.CallTo(() => mockLicenseProductManager.GetYearsForEditRates()).WithAnyArguments().Returns(expected);

            //Act
            LicenseProductController controller = new LicenseProductController(mockLicenseProductManager);
            var result = controller.GetYearsForEditRates();

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void EditWriterConsent_ReturnBoolTRUE()
        {
            //Arrange
            var mockLicenseProductManager = A.Fake<ILicenseProductManager>();

            //Build expected
            const bool expected = true;

            A.CallTo(() => mockLicenseProductManager.EditWriterConsent(A<EditWriterConsentSaveRequest>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            LicenseProductController controller = new LicenseProductController(mockLicenseProductManager);
            var result = controller.EditWriterConsent(A<EditWriterConsentSaveRequest>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void EditWriterIncluded_ReturnBoolTRUE()
        {
            //Arrange
            var mockLicenseProductManager = A.Fake<ILicenseProductManager>();

            //Build expected
            const bool expected = true;

            A.CallTo(() => mockLicenseProductManager.EditWriterIsIncluded(A<EditWriterIncludedSaveRequest>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            LicenseProductController controller = new LicenseProductController(mockLicenseProductManager);
            var result = controller.EditWriterIncluded(A<EditWriterIncludedSaveRequest>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void EditPaidQuarter_ReturnBoolTRUE()
        {
            //Arrange
            var mockLicenseProductManager = A.Fake<ILicenseProductManager>();

            //Build expected
            const bool expected = true;

            A.CallTo(() => mockLicenseProductManager.EditPaidQuarter(A<EditPaidQuarterSaveRequest>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            LicenseProductController controller = new LicenseProductController(mockLicenseProductManager);
            var result = controller.EditPaidQuarter(A<EditPaidQuarterSaveRequest>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }
    }
}
