using System.Collections.Generic;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public interface ISnapshotLicenseNoteRepository
    {
        Snapshot_LicenseNote SaveSnapshotLicenseNote(Snapshot_LicenseNote snapshotLicenseNote);
        Snapshot_LicenseNote GetSnapshotLicenseNoteByNoteId(int licenseNoteId);
        bool DeleteLicenseNoteSnapshotByLicenseNoteId(int snapshotNoteId);
        List<int> GetAllLicenseNoteIdsForLicenseId(int licenseId);
        List<Snapshot_LicenseNote> GetAllLicenseNoteForLicenseId(int licenseId);
        List<int> GetAllContactIdsRelatedToNote(int licneseId);
    }
}