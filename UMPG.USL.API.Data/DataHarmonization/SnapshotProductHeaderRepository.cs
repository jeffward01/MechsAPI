using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public class SnapshotProductHeaderRepository : ISnapshotProductHeaderRepository
    {
        public Snapshot_ProductHeader SaveSnapshotProductHeader(Snapshot_ProductHeader snapshotProductHeader)
        {
            using (var context = new AuthContext())
            {
                context.Snapshot_ProductHeaders.Add(snapshotProductHeader);
                context.SaveChanges();
                return snapshotProductHeader;
            }
        }

        public Snapshot_ProductHeader GetSnapshotProductHeaderByProductHeaderId(int productHeaderId)
        {
            using (var context = new AuthContext())
            {
                return context.Snapshot_ProductHeaders.Find(productHeaderId);
            }
        }
    }
}