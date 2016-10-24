using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Business.DataHarmonization
{
    public interface ISnapshotLabelGroupManager
    {
        Snapshot_LabelGroup SaveLabelGroupSnapshotLabelGroup(Snapshot_LabelGroup snapshotLabelGroup);
        Snapshot_LabelGroup GetSnapshotLabelGroupByLabelGroupId(int labelGroupId);
    }
}