using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.Models.LicenseModel;
using UMPG.USL.Models.Recs;

namespace UMPG.USL.API.Data.LicenseData
{
    public interface ILicenseProductRecordingRepository
    {
        int GetLicenseProductRecordingsNo(int licenseproductId);

        List<int> GetLicenseProductRecordingsIds(int licenseproductId);

        List<int> GetLicenseRecordingsIdsByLicenseProductId(int licenseproductId);

        List<LicenseProductRecording> GetLicenseProductRecordingsBrief(int licenseproductId);

        List<LicenseProductRecording> GetLicenseProductRecordingsFromList(List<int> LicenseproductIds);

        LicenseProductRecording Add(LicenseProductRecording licenseProductRecording);

        void Update(LicenseProductRecording licenseProductRecording);

        List<LicenseProductRecording> GetLicenseRecordingsList(List<int> licenseProductIds);

        List<LicenseProductRecording> GetLicenseRecordingsByLicenseProductId(int licenseProductId);

        List<LicenseProductRecording> GetLicenseProductRecordingsFromIds(List<int> licenseRecordingIds);

        LicenseProductRecording GetLicenseRecordingsByRecsTrackId(int trackId);

    }
}
