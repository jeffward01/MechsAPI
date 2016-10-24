using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public class SnapshotLabelRepository : ISnapshotLabelRepository
    {
        public Snapshot_Label SaveSnapshotLabel(Snapshot_Label snapshotLabel)
        {
            using (var context = new AuthContext())
            {
                context.Snapshot_Labels.Add(snapshotLabel);
                context.SaveChanges();
                return snapshotLabel;
            }
        }

        public Snapshot_Label GetSnapshotLabelByLabelId(int labelId)
        {
            using (var context = new AuthContext())
            {
                return context.Snapshot_Labels.Find(labelId);
            }
        }
    }
}