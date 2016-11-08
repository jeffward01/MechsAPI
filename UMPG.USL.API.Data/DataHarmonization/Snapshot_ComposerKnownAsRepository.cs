using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public class Snapshot_ComposerKnownAsRepository : ISnapshot_ComposerKnownAsRepository
    {
        public Snapshot_ComposerKnownAs SaveComposerKnownAs(Snapshot_ComposerKnownAs composerSnapshot)
        {
            using (var context = new AuthContext())
            {
                context.Snapshot_ComposerKnownAs.Add(composerSnapshot);
                context.SaveChanges();
                return composerSnapshot;
            }
        }

        public List<Snapshot_ComposerKnownAs> GetAllComposerKnownAsByComposerSnapshotId(int composerAffiliationSnapshotId)
        {
            using (var context = new AuthContext())
            {
                return context.Snapshot_ComposerKnownAs.Where(sl => sl.SnapshotComposerId == composerAffiliationSnapshotId).ToList();
            }
        }

        public bool DeleteComposerKnownAs(Snapshot_ComposerKnownAs composerToDelete)
        {
            using (var context = new AuthContext())
            {
                var composer =
                    context.Snapshot_ComposerKnownAs
                        .Find(composerToDelete.SnapshotComposerKnownAsId);

                context.Snapshot_ComposerKnownAs.Attach(composer);
                context.Snapshot_ComposerKnownAs.Remove(composer);
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
