using System.Collections.Generic;
using UMPG.USL.Models.LicenseModel;

namespace UMPG.USL.API.Data.LicenseData
{
    public interface ILicenseProductRecordingRepository
    {
        int GetLicenseProductRecordingsNo(int licenseproductId);

        List<int> GetLicenseProductRecordingsIds(int licenseproductId);

        List<int> GetLicenseRecordingsIdsByLicenseProductId(int licenseproductId);

        List<LicenseProductRecording> GetLicenseProductRecordingsBrief(int licenseproductId);

        List<LicenseProductRecording> GetLicenseProductRecordingsFromList(List<int> LicenseproductIds);
        LicenseProductRecording GetLicenseProductRecordingByLicenseProductRecordingId(int licenseProductRecordingId);

        bool IsAlreadyPresent(int trackId, int licneseProductId);

        LicenseProductRecording Add(LicenseProductRecording licenseProductRecording);

        void Update(LicenseProductRecording licenseProductRecording);

        List<LicenseProductRecording> GetLicenseRecordingsList(List<int> licenseProductIds);

        List<LicenseProductRecording> GetLicenseRecordingsByLicenseProductId(int licenseProductId);

        List<LicenseProductRecording> GetLicenseProductRecordingsFromIds(List<int> licenseRecordingIds);

        LicenseProductRecording GetLicenseRecordingsByRecsTrackId(int trackId);
    }
}