using System.Collections.Generic;
using UMPG.USL.Models.LicenseModel;

namespace UMPG.USL.API.Data.LicenseData
{
    public interface ILicensePRWriterRateRepository
    {
        LicenseProductRecordingWriterRate Add(LicenseProductRecordingWriterRate licenseProductRecordingWriterRate);

        LicenseProductRecordingWriterRate Get(int Id);

        List<LicenseProductRecordingWriterRate> GetAll();

        bool LicenseProductHasWriterRate(int product_configuration_id, int licneseWriterId);

        List<LicenseProductRecordingWriterRate> Search(string query);

        List<LicenseProductRecordingWriterRate> GetLicenseProductRecordingWriterRates(int licenseWriterId);

        List<LicenseProductRecordingWriterRate> GetLicenseRecordingWriterRatesFromIds(List<int> licenseWriterIds, List<int> configIds);

        List<LicenseProductRecordingWriterRate> GetLicenseRecordingWriterRatesFromIdsFull(List<int> licenseWriterIds,
            List<int> configIds);

        List<LicenseProductRecordingWriterRate> GetLicenseProductRecordingWriterRatesByWriterIdsConfig(List<int> licenseWriterIds, int configuration_id);

        void Update(LicenseProductRecordingWriterRate licenseProductRecordingWriterRate);

        List<LicenseProductRecordingWriterRate> GetLicenseProductRecordingWriterRatesByWriterIdConfig(
            int licenseWriterId, int configurationId);

        List<LicenseProductRecordingWriterRate> GetLicenseRecordingWriterRateIds(List<int> licenseWriterIds);

        List<LicenseProductRecordingWriterRate> GetRatesByRatesIds(List<int> rateIds);

        List<int> GetLicensedConfigIds(int licenseWriterId);

        List<int?> GetLicensedProductConfigIds(int licenseWriterId);
    }
}