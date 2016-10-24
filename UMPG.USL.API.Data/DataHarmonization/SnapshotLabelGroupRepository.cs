using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public class SnapshotLabelGroupRepository : ISnapshotLabelGroupRepository
    {
        public Snapshot_LabelGroup SaveSnapshotLabelGroup(Snapshot_LabelGroup snapshotLabelGroup)
        {
            using (var context = new AuthContext())
            {
                context.Snapshot_LabelGroups.Add(snapshotLabelGroup);
                context.SaveChanges();
                return snapshotLabelGroup;
            }
        }

        public Snapshot_LabelGroup GetSaSnapshotLabelGroupByLabelGroupId(int labelGroupId)
        {
            using (var context = new AuthContext())
            {
                return context.Snapshot_LabelGroups.Find(labelGroupId);
            }
        }
    }
}