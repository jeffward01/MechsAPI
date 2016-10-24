using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Business.DataHarmonization
{
    public interface ISnapshotLicenseNoteManager
    {
        Snapshot_LicenseNote SaveSnapshotLicenseNote(Snapshot_LicenseNote snapshotLicenseNote);
        Snapshot_LicenseNote GSnapshotLicenseNoteByLicenseNoteId(int snapshotLicenseNoteId);
    }
}