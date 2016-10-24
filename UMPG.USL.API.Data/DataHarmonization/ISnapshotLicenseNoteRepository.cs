using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public interface ISnapshotLicenseNoteRepository
    {
        Snapshot_LicenseNote SaveSnapshotLicenseNote(Snapshot_LicenseNote snapshotLicenseNote);
        Snapshot_LicenseNote GetSnapshotLicenseNoteByNoteId(int licenseNoteId);
    }
}