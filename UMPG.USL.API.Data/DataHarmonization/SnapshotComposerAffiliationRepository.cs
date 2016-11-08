using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public class SnapshotComposerAffiliationRepository : ISnapshotComposerAffiliationRepository
    {
        public Snapshot_ComposerAffiliation SaveComposerAffiliationSnapshot(Snapshot_ComposerAffiliation composerSnapshot)
        {
            using (var context = new AuthContext())
            {
                context.Snapshot_ComposerAffiliations.Add(composerSnapshot);
                context.SaveChanges();
                return composerSnapshot;
            }
        }

        public List<Snapshot_ComposerAffiliation> GetAllComposersAffiliationsByComposerSnapshotId(int composerSnapshotId)
        {
            using (var context = new AuthContext())
            {
                return context.Snapshot_ComposerAffiliations.Where(sl => sl.SnapshotComposerId == composerSnapshotId).ToList();
            }
        }

        public bool DeleteComposerAffiliationSnapshotByComposer(Snapshot_ComposerAffiliation composerToDelete)
        {
            using (var context = new AuthContext())
            {
                var composer =
                    context.Snapshot_ComposerAffiliations
                        .Find(composerToDelete.SnapshotComposerAffiliationId);

                context.Snapshot_ComposerAffiliations.Attach(composer);
                context.Snapshot_ComposerAffiliations.Remove(composer);
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
