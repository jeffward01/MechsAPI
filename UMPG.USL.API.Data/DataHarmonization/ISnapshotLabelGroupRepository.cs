using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public interface ISnapshotLabelGroupRepository
    {
        Snapshot_LabelGroup SaveSnapshotLabelGroup(Snapshot_LabelGroup snapshotLabelGroup);
        Snapshot_LabelGroup GetSaSnapshotLabelGroupByLabelGroupId(int labelGroupId);
    }
}