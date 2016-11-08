using System;
using System.Collections.Generic;
using System.Linq;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public class SnapshotComposerOriginalPublisherAdminAffiliationBaseRepository : ISnapshotComposerOriginalPublisherAdminAffiliationBaseRepository
    {
        public Snapshot_ComposerOriginalPublisherAdminAffiliationBase SaveComposerOriginalPublisherAdminAffiliationBase(Snapshot_ComposerOriginalPublisherAdminAffiliationBase sampleSnapshot)
        {
            using (var context = new AuthContext())
            {
                context.Snapshot_ComposerOriginalPublisherAdminAffiliationBases.Add(sampleSnapshot);
                context.SaveChanges();
                return sampleSnapshot;
            }
        }

        public List<Snapshot_ComposerOriginalPublisherAdminAffiliationBase> GetAllComposerOriginalPublisherAdminAffiliationBasesForAffiliationId(int adminAffiliationId)
        {
            using (var context = new AuthContext())
            {
                return context.Snapshot_ComposerOriginalPublisherAdminAffiliationBases.Where(sl => sl.SnapshotComposerOriginalPublisherAdminAffiliationId == adminAffiliationId).ToList();
            }
        }

        public bool DeleteComposerOriginalPublisherAdminAffiliationBase(Snapshot_ComposerOriginalPublisherAdminAffiliationBase composerToDelete)
        {
            using (var context = new AuthContext())
            {
                var composer =
                    context.Snapshot_ComposerOriginalPublisherAdminAffiliationBases
                        .Find(composerToDelete.SnapshotComposerOriginalPublisherAdminAffiliationBaseId);

                context.Snapshot_ComposerOriginalPublisherAdminAffiliationBases.Attach(composer);
                context.Snapshot_ComposerOriginalPublisherAdminAffiliationBases.Remove(composer);
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