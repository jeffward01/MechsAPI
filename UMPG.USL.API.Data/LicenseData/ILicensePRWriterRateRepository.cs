using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.Models.LicenseModel;

namespace UMPG.USL.API.Data.LicenseData
{
    public interface ILicensePRWriterRateRepository
    {

        LicenseProductRecordingWriterRate Add(LicenseProductRecordingWriterRate licenseProductRecordingWriterRate);

        LicenseProductRecordingWriterRate Get(int Id);

        List<LicenseProductRecordingWriterRate> GetAll();

        List<LicenseProductRecordingWriterRate> Search(string query);

        List<LicenseProductRecordingWriterRate> GetLicenseProductRecordingWriterRates(int licenseWriterId);

        List<LicenseProductRecordingWriterRate> GetLicenseRecordingWriterRatesFromIds(List<int> licenseWriterIds, List<int> configIds);

        List<LicenseProductRecordingWriterRate> GetLicenseProductRecordingWriterRatesByWriterIdsConfig(List<int> licenseWriterIds, int configuration_id);

        void Update(LicenseProductRecordingWriterRate licenseProductRecordingWriterRate);

        List<LicenseProductRecordingWriterRate> GetLicenseProductRecordingWriterRatesByWriterIdConfig(
            int licenseWriterId, int configurationId);

        List<LicenseProductRecordingWriterRate> GetLicenseRecordingWriterRateIds(List<int> licenseWriterIds);

        List<LicenseProductRecordingWriterRate> GetRatesByRatesIds(List<int> rateIds);

        List<int> GetLicensedConfigIds(int licenseWriterId);

    }
}
