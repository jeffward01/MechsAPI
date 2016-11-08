using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public class SnapshotComposerAffiliationBaseRepository : ISnapshotComposerAffiliationBaseRepository
    {
        public Snapshot_ComposerAffiliationBase SaveComposerAffiliatioBasenSnapshot(Snapshot_ComposerAffiliationBase composerSnapshot)
        {
            using (var context = new AuthContext())
            {
                context.Snapshot_ComposerAffiliationBases.Add(composerSnapshot);
                context.SaveChanges();
                return composerSnapshot;
            }
        }

        public List<Snapshot_ComposerAffiliationBase> GetAllComposersAffiliationBasesByComposerAffiliationSnapshotId(int composerAffiliationSnapshotId)
        {
            using (var context = new AuthContext())
            {
                return context.Snapshot_ComposerAffiliationBases.Where(sl => sl.SnapshotComposerAffiliationId == composerAffiliationSnapshotId).ToList();
            }
        }

        public bool DeleteComposerAffiliationBaseSnapshotByComposer(Snapshot_ComposerAffiliationBase composerToDelete)
        {
            using (var context = new AuthContext())
            {
                var composer =
                    context.Snapshot_ComposerAffiliationBases
                        .Find(composerToDelete.SnapshotComposerAffiliationBaseId);

                context.Snapshot_ComposerAffiliationBases.Attach(composer);
                context.Snapshot_ComposerAffiliationBases.Remove(composer);
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
