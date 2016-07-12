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
using UMPG.USL.Models.Enums;


namespace UMPG.USL.API.Tests.Manager_Tests.Licenses
{
    [TestFixture]
    public class LicenseProductSolrManagerTests
    {
        /*
        [Test]  <-- Cant mock Non-Virtual methods
        public void ClearCache_ReturnVoid()
        {
            //Arrange
            Dictionary<string, Dictionary<string, object>> methodsCache = new Dictionary<string, Dictionary<string, object>>();
            Dictionary<int, List<LicenseProductRecording>> licenseProductRecordingsCache = new Dictionary<int, List<LicenseProductRecording>>();
                        
            //Act
            methodsCache.Clear();
            licenseProductRecordingsCache.Clear();

            //Assert
            Assert.(A<Dictionary<String, Dictionary<string, object>>>.Ignored.Clear());
            
        }
         * 
         * /*
         * */

        [Test]
        public void UpdateLicense_ReturnBoolTRUE()
        {
            //Arrange
            var mockILicenseRepository = A.Fake<ILicenseRepository>();
            var mockILicenseProductRepository = A.Fake<ILicenseProductRepository>();
            var mockILicenseNoteRepository = A.Fake<ILicenseNoteRepository>();
            var mockILicenseSequenceRepository = A.Fake<ILicenseSequenceRepository>();
            var mockSolrSearchProvider = A.Fake<ISearchProvider>();
            var mockILicensePRWriterRateRepository = A.Fake<ILicensePRWriterRateRepository>();
            var mockILicenseProductRecordingRepository = A.Fake<ILicenseProductRecordingRepository>();
            var mockILicensePRWriterRepository = A.Fake<ILicensePRWriterRepository>();
            var mockILicensePRWriterRateStatusRepository = A.Fake<ILicensePRWriterRateStatusRepository>();
            var mockILicenseUploadPreviewLicenseRepository = A.Fake<ILicenseUploadPreviewLicenseRepository>();
            var mockILicenseProductConfigurationRepository = A.Fake<ILicenseProductConfigurationRepository>();
            var mockIRecs = A.Fake<IRecs>();
            var mockISolrUpdate = A.Fake<ISolrUpdate>();
            Dictionary<string, Dictionary<string, object>> methodsCache = new Dictionary<string, Dictionary<string, object>>();
            Dictionary<int, List<LicenseProductRecording>> licenseProductRecordingsCache = new Dictionary<int, List<LicenseProductRecording>>();

            //Build expected
            Contact contact = new Contact { };
            LU_LicenseMethod lum = new LU_LicenseMethod{};
            LU_LicenseStatus lus = new LU_LicenseStatus { LicenseStatusId = 99 };
            LU_LicenseType luss = new LU_LicenseType { LicenseTypeId = 99, LicenseType = "string" };
            LU_Priority lup = new LU_Priority{PriorityId = 99, Priority = "string"};
            Licensee licensee = new Licensee { LicenseeId = 99 };
            License license = new License { Contact = contact, Licensee = licensee, CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now, LicenseMethod = lum, LicensePriority = lup, LicenseStatus = lus, LicenseType = luss };
           
            A.CallTo(() => mockILicenseRepository.Get(99)).WithAnyArguments().Returns(license);

            //Act
            LicenseProductSolrManager manager = new LicenseProductSolrManager(mockILicenseRepository, mockILicenseProductRepository, mockILicenseNoteRepository, mockILicenseSequenceRepository, mockSolrSearchProvider, mockILicensePRWriterRateRepository, mockILicenseProductRecordingRepository, mockILicensePRWriterRepository, mockILicensePRWriterRateStatusRepository, mockILicenseUploadPreviewLicenseRepository, mockIRecs, mockILicenseProductConfigurationRepository, mockISolrUpdate);
            var result = manager.UpdateLicense(99, true);

            //Assert
            Assert.IsTrue(result);
        }


        [Test]
        public void UpdateLicenseStatus_ReturnBoolTRUE()
        {
            //Arrange
            var mockILicenseRepository = A.Fake<ILicenseRepository>();
            var mockILicenseProductRepository = A.Fake<ILicenseProductRepository>();
            var mockILicenseNoteRepository = A.Fake<ILicenseNoteRepository>();
            var mockILicenseSequenceRepository = A.Fake<ILicenseSequenceRepository>();
            var mockSolrSearchProvider = A.Fake<ISearchProvider>();
            var mockILicensePRWriterRateRepository = A.Fake<ILicensePRWriterRateRepository>();
            var mockILicenseProductRecordingRepository = A.Fake<ILicenseProductRecordingRepository>();
            var mockILicensePRWriterRepository = A.Fake<ILicensePRWriterRepository>();
            var mockILicensePRWriterRateStatusRepository = A.Fake<ILicensePRWriterRateStatusRepository>();
            var mockILicenseUploadPreviewLicenseRepository = A.Fake<ILicenseUploadPreviewLicenseRepository>();
            var mockILicenseProductConfigurationRepository = A.Fake<ILicenseProductConfigurationRepository>();
            var mockIRecs = A.Fake<IRecs>();
            var mockISolrUpdate = A.Fake<ISolrUpdate>();
            Dictionary<string, Dictionary<string, object>> methodsCache = new Dictionary<string, Dictionary<string, object>>();
            Dictionary<int, List<LicenseProductRecording>> licenseProductRecordingsCache = new Dictionary<int, List<LicenseProductRecording>>();

            //Build expected
            Contact contact = new Contact { };
            LU_LicenseMethod lum = new LU_LicenseMethod { };
            LU_LicenseStatus lus = new LU_LicenseStatus { LicenseStatusId = 99 };
            LU_LicenseType luss = new LU_LicenseType { LicenseTypeId = 99, LicenseType = "string" };
            LU_Priority lup = new LU_Priority { PriorityId = 99, Priority = "string" };
            Licensee licensee = new Licensee { LicenseeId = 99 };
            License license = new License { Contact = contact, Licensee = licensee, CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now, LicenseMethod = lum, LicensePriority = lup, LicenseStatus = lus, LicenseType = luss };

            A.CallTo(() => mockILicenseRepository.Get(99)).WithAnyArguments().Returns(license);

            //Act
            LicenseProductSolrManager manager = new LicenseProductSolrManager(mockILicenseRepository, mockILicenseProductRepository, mockILicenseNoteRepository, mockILicenseSequenceRepository, mockSolrSearchProvider, mockILicensePRWriterRateRepository, mockILicenseProductRecordingRepository, mockILicensePRWriterRepository, mockILicensePRWriterRateStatusRepository, mockILicenseUploadPreviewLicenseRepository, mockIRecs, mockILicenseProductConfigurationRepository, mockISolrUpdate);
            var result = manager.UpdateLicenseStatus(99);

            //Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void UpdateLicenseAssignee_ReturnBoolTRUE()
        {
            //Arrange
            var mockILicenseRepository = A.Fake<ILicenseRepository>();
            var mockILicenseProductRepository = A.Fake<ILicenseProductRepository>();
            var mockILicenseNoteRepository = A.Fake<ILicenseNoteRepository>();
            var mockILicenseSequenceRepository = A.Fake<ILicenseSequenceRepository>();
            var mockSolrSearchProvider = A.Fake<ISearchProvider>();
            var mockILicensePRWriterRateRepository = A.Fake<ILicensePRWriterRateRepository>();
            var mockILicenseProductRecordingRepository = A.Fake<ILicenseProductRecordingRepository>();
            var mockILicensePRWriterRepository = A.Fake<ILicensePRWriterRepository>();
            var mockILicensePRWriterRateStatusRepository = A.Fake<ILicensePRWriterRateStatusRepository>();
            var mockILicenseUploadPreviewLicenseRepository = A.Fake<ILicenseUploadPreviewLicenseRepository>();
            var mockILicenseProductConfigurationRepository = A.Fake<ILicenseProductConfigurationRepository>();
            var mockIRecs = A.Fake<IRecs>();
            var mockISolrUpdate = A.Fake<ISolrUpdate>();
            Dictionary<string, Dictionary<string, object>> methodsCache = new Dictionary<string, Dictionary<string, object>>();
            Dictionary<int, List<LicenseProductRecording>> licenseProductRecordingsCache = new Dictionary<int, List<LicenseProductRecording>>();

            //Build Request
            List<int> list = new List<int> { 1, 2, 3, 5 };

            //Build expected
            Contact contact = new Contact { };
            LU_LicenseMethod lum = new LU_LicenseMethod { };
            LU_LicenseStatus lus = new LU_LicenseStatus { LicenseStatusId = 99 };
            LU_LicenseType luss = new LU_LicenseType { LicenseTypeId = 99, LicenseType = "string" };
            LU_Priority lup = new LU_Priority { PriorityId = 99, Priority = "string" };
            Licensee licensee = new Licensee { LicenseeId = 99 };
            License license = new License { Contact = contact, Licensee = licensee, CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now, LicenseMethod = lum, LicensePriority = lup, LicenseStatus = lus, LicenseType = luss };

            A.CallTo(() => mockILicenseRepository.Get(99)).WithAnyArguments().Returns(license);

            //Act
            LicenseProductSolrManager manager = new LicenseProductSolrManager(mockILicenseRepository, mockILicenseProductRepository, mockILicenseNoteRepository, mockILicenseSequenceRepository, mockSolrSearchProvider, mockILicensePRWriterRateRepository, mockILicenseProductRecordingRepository, mockILicensePRWriterRepository, mockILicensePRWriterRateStatusRepository, mockILicenseUploadPreviewLicenseRepository, mockIRecs, mockILicenseProductConfigurationRepository, mockISolrUpdate);
            var result = manager.UpdateLicenseAssignee(list);

            //Assert
            Assert.IsTrue(result);
        }
    }
}
