using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Business.DataHarmonization
{
    public interface ISnapshotProductHeaderManager
    {
        Snapshot_ProductHeader SaveSnapshotProductHeader(Snapshot_ProductHeader snapshotProductHeader);
        Snapshot_ProductHeader GetSnapshotProductHeaderByProductHeaderId(int snapshotProductHeader);
        Snapshot_ProductHeader GetSnapshotProductHeaderForLabelSnapshotId(int snapshotLabelId);
    }
}