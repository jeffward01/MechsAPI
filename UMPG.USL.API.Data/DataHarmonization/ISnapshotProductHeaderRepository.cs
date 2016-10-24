using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public interface ISnapshotProductHeaderRepository
    {
        Snapshot_ProductHeader SaveSnapshotProductHeader(Snapshot_ProductHeader snapshotProductHeader);
        Snapshot_ProductHeader GetSnapshotProductHeaderByProductHeaderId(int productHeaderId);
    }
}