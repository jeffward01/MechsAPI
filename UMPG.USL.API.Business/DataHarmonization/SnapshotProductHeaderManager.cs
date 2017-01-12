using UMPG.USL.API.Data.DataHarmonization;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Business.DataHarmonization
{
    public class SnapshotProductHeaderManager : ISnapshotProductHeaderManager
    {
        private readonly ISnapshotProductHeaderRepository _snapshotProductHeaderRepository;

        public SnapshotProductHeaderManager(ISnapshotProductHeaderRepository snapshotProductHeaderRepository)
        {
            _snapshotProductHeaderRepository = snapshotProductHeaderRepository;
        }

        public Snapshot_ProductHeader SaveSnapshotProductHeader(Snapshot_ProductHeader snapshotProductHeader)
        {
            return _snapshotProductHeaderRepository.SaveSnapshotProductHeader(snapshotProductHeader);
        }

        public Snapshot_ProductHeader GetSnapshotProductHeaderByProductHeaderId(int snapshotProductHeader)
        {
            return _snapshotProductHeaderRepository.GetSnapshotProductHeaderBySnapshotProductHeaderId(snapshotProductHeader);
        }

        public Snapshot_ProductHeader GetSnapshotProductHeaderForLabelSnapshotId(int snapshotLabelId)
        {
            return _snapshotProductHeaderRepository.GetSnapshotProductHeaderByLabelSnapshotId(snapshotLabelId);
        }
    }
}