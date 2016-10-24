using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public class SnapshotLicenseNoteRepository : ISnapshotLicenseNoteRepository
    {
        public Snapshot_LicenseNote SaveSnapshotLicenseNote(Snapshot_LicenseNote snapshotLicenseNote)
        {
            using (var context = new AuthContext())
            {
                context.Snapshot_LicenseNotes.Add(snapshotLicenseNote);
                context.SaveChanges();
                return snapshotLicenseNote;
            }
        }

        public Snapshot_LicenseNote GetSnapshotLicenseNoteByNoteId(int licenseNoteId)
        {
            using (var context = new AuthContext())
            {
                return context.Snapshot_LicenseNotes.Find(licenseNoteId);
            }
        }
    }
}