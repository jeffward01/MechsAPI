using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.Models.LicenseModel;
using UMPG.USL.Models.StaticDropdownsData;

namespace UMPG.USL.API.Data.LicenseData
{
    public interface ILicensePRWriterRateStatusRepository
    {

        LicenseProductRecordingWriterRateStatus Add(LicenseProductRecordingWriterRateStatus licenseProductRecordingWriterRateStatus);

        LicenseProductRecordingWriterRateStatus Get(int Id);

        List<LicenseProductRecordingWriterRateStatus> GetAll();

        List<LicenseProductRecordingWriterRateStatus> Search(string query);

        List<LicenseProductRecordingWritersRateStatusDropdown> GetLicenseWriterRateStatusList(List<int> licenseWriterRateIds);

        List<LicenseProductRecordingWriterRateStatus> GetLicenseWriterRateStatus(List<int> licenseWriterRateIds);

        void Update(LicenseProductRecordingWriterRateStatus licenseRecordingWriterRateStatus);

        bool StatusExists(int rateId, int statusId);

        List<int> GetLicenseWriterRatesWithOutStatus(List<int> licenseWriterRateIds);

        List<int> GetLicenseWriterRatesIdsWithStatus(List<int> licenseWriterRateIds);

    }
}
