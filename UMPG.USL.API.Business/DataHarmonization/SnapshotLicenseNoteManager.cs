using UMPG.USL.API.Data.DataHarmonization;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Business.DataHarmonization
{
    public class SnapshotLicenseNoteManager : ISnapshotLicenseNoteManager
    {
        private readonly ISnapshotLicenseNoteRepository _snapshotLicenseNoteRepository;

        public SnapshotLicenseNoteManager(ISnapshotLicenseNoteRepository snapshotLicenseNoteRepository)
        {
            _snapshotLicenseNoteRepository = snapshotLicenseNoteRepository;
        }

        public Snapshot_LicenseNote SaveSnapshotLicenseNote(Snapshot_LicenseNote snapshotLicenseNote)
        {
            return _snapshotLicenseNoteRepository.SaveSnapshotLicenseNote(snapshotLicenseNote);
        }

        public Snapshot_LicenseNote GSnapshotLicenseNoteByLicenseNoteId(int snapshotLicenseNoteId)
        {
            return _snapshotLicenseNoteRepository.GetSnapshotLicenseNoteByNoteId(snapshotLicenseNoteId);
        }
    }
}