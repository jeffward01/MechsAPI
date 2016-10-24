using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Business.DataHarmonization
{
    public interface ISnapshotLabelManger
    {
        Snapshot_Label SaveSnapshotLabel(Snapshot_Label snapshotLabel);
        Snapshot_Label GetSnapshotLabelByLabelId(int snapshotLabelId);
    }
}