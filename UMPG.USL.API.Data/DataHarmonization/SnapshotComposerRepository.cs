using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public class SnapshotComposerRepository : ISnapshotComposerRepository
    {
        public Snapshot_Composer SaveComposerSnapshot(Snapshot_Composer composerSnapshot)
        {
            using (var context = new AuthContext())
            {
                context.Snapshot_Composers.Add(composerSnapshot);
                context.SaveChanges();
                return composerSnapshot;
            }
        }

        public List<Snapshot_Composer> GetAllComposersByRecsCopyrightid(int recsCopyrightId)
        {
            using (var context = new AuthContext())
            {
                return context.Snapshot_Composers.Where(sl => sl.SnapshotRecsCopyrightId == recsCopyrightId).ToList();
            }
        }

        public bool DeleteComposerSnapshotByComposer(Snapshot_Composer composerToDelete)
        {
            using (var context = new AuthContext())
            {
                var composer =
                    context.Snapshot_Composers
                        .Find(composerToDelete.SnapshotComposerId);

                context.Snapshot_Composers.Attach(composer);
                context.Snapshot_Composers.Remove(composer);
                try
                {
                    context.SaveChanges();
                }
                catch (Exception)
                {
                    return false;
                }
                return true;
            }
        }
    }
}
