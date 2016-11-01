using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public interface ISnapshotLicenseProductRecordingRepository
    {
        Snapshot_LicenseProductRecording GetLicenseProductRecordingForCloneTrackId(int cloneTrackId);
        bool DeletePhoneBySnapshotPhoneId(int snapshotLicenseProductRecordingId);
    }
}