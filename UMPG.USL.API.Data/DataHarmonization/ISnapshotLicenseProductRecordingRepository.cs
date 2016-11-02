using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public interface ISnapshotLicenseProductRecordingRepository
    {
        Snapshot_LicenseProductRecording GetLicenseProductRecordingForCloneTrackId(int cloneTrackId);

        Snapshot_LicenseProductRecording SaveLicenseProductRecording(Snapshot_LicenseProductRecording licenseProductRecording);

        bool DeletePhoneBySnapshotPhoneId(int snapshotLicenseProductRecordingId);
    }
}