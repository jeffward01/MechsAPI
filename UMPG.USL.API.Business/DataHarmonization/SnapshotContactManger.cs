using UMPG.USL.API.Data.DataHarmonization;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Business.DataHarmonization
{
    public class SnapshotContactManger : ISnapshotContactManger
    {
        private readonly ISnapshotContactRepository _snapshotContactRepository;

        public SnapshotContactManger(ISnapshotContactRepository snapshotContactRepository)
        {
            _snapshotContactRepository = snapshotContactRepository;
        }

        public Snapshot_Contact SaveSnapshotContact(Snapshot_Contact snapshotContact)
        {
            return _snapshotContactRepository.SaveSnapshotContact(snapshotContact);
        }

        public Snapshot_Contact GetSnapshotContactByContactId(int snapshotContactId)
        {
            return _snapshotContactRepository.GetSnapshotContactByContactId(snapshotContactId);
        }

  
    }
}