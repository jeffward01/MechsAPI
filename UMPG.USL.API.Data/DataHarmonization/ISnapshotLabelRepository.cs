using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public interface ISnapshotLabelRepository
    {
        Snapshot_Label SaveSnapshotLabel(Snapshot_Label snapshotLabel);
        Snapshot_Label GetSnapshotLabelByLabelId(int labelId);
    }
}