using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.Models.LicenseModel;
using UMPG.USL.Models.LicenseTemplateModel;
using UMPG.USL.Models.Recs;
using UMPG.USL.Models.StaticDropdownsData;

namespace UMPG.USL.API.Business.Licenses
{
    public interface ILicenseProductManager
    {
        List<LicenseProduct> GetProducts(int licenseId);
        string GetCatalogNumber(int productConfigId);

        List<LicenseProduct> GetProductsNew(int licenseId);

        LicenseProductOverview2 BuildLicenseProductOverview2(long licenseProductId);

        List<LicenseOverview> BuildLicenseProductOverview_tom(int productId, int trackId, int caecode);

        List<LicenseProductRecordingWriterRate> GetWriterRateOverviewSkinny(int productId, int trackId,
            int caecode);

       bool DeleteLicenseProduct(int licenseId, int productId);

        LicenseProduct GetSelectedProduct(int licenseId, int productId);
        List<LicenseOverview> BuildLicenseProductOverview_tom_Original(int productId, int trackId, int caecode); //17775

        List<LicenseProductRecording> GetLicenseProductRecordings(int licenseproductId);

        List<LicenseProductRecordingWriter> GetLicenseProductRecordingWriters(int licenseRecordingId, string worksCode);

        List<LicenseProductRecordingWriterRate> GetLicensePRWriterRates(int licenseWriterId);

        int GetLicenseProductRecordingWritersNo(int licenseRecordingId);

        //List<ProductConfiguration> GetLicenseProductConfigurations(int licenseId);

        List<LicenseProductConfiguration> GetLicenseProductConfigurations(int licenseId);

        bool UpdateLicenseProductConfigurations(UpdateLicenseProductConfigurationsStatusRequest request);
        bool UpdateLicenseProducts(UpdateLicenseProductsRequest request);


        List<LicenseProductConfigurations> GetLicenseProductConfigurationsAll(int licenseId);
        //todo: delete this
        //List<LicenseProductDropdown> GetLicenseProductDropDown(int licenseId);

        bool UpdateLicenseProductConfigurationsAll(List<UpdateLicenseProductConfigurationsRequest> request);

        List<LicenseProductRecordingWriter> GetLicenseWriters(int licenseRecordingId);

        List<ProductConfiguration> GetProductConfigurationsAll(GetProductConfigurationsAllRequest request);

        bool UpdateProductConfigurationsAll(List<UpdateProductConfigurationsAllRequest> requests);
       
        List<LicenseProductRecordingWritersRateStatusDropdown> GetLicenseWriterRateStatusList(List<int> licenseWriterIds);

        GetWritersRatesRequest GetAllLicenseRelatedIds(int licenseid);

        GetWritersRatesRequest GetAllLicenseRecordingRelatedIds(List<int> licenseRecordingids);

        //New methods with upside down data
        List<WorksRecording> GetLicenseProductRecordingsV2(int licenseProductId, string safeId);
        List<WorksWriter> GetLicenseWritersV2(int licenseRecordingId, string worksCode);


        // New methods for edit rates 
        List<LicenseProductRecordingsDropdown> GetLicenseProductRecordingsDropdown(int licenseId);
        List<LicenseProductRecordingWritersDropdown> GetLicenseProductRecordingsWritersDropdown(GetWritersRatesRequest request);
        List<int> GetWritersNoForEditRates(GetWritersRatesRequest request);
        bool EditRatesAndWriters(EditRatesSaveRequest request);
        bool EditIndividualWriterRates(List<EditRatesSaveRequest> request);

        //Method for geting the license preview 
        LicenseTemplate GetLicenseTemplate(int licenseId);


        CloneLicenseResult CloneLicense(int licenseId, string clonetype, int contactid);

        List<int> GetLicenseWriterRateIdsWithOutHolds(int licenseid);
        List<int> GetYearsForEditRates();

        bool EditWriterConsent(EditWriterConsentSaveRequest request);
        bool EditWriterIsIncluded(EditWriterIncludedSaveRequest request);
        bool EditPaidQuarter(EditPaidQuarterSaveRequest request);

        void UpdateLicenseProductSchedule(List<LicenseProduct> licenseProducts);

        LicenseProductOverview GetLicenseProductOverview(int recProductId);

    }
}
