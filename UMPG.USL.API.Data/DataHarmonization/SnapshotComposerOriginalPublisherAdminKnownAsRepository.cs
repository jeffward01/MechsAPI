using System;
using System.Collections.Generic;
using System.Linq;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public class SnapshotComposerOriginalPublisherAdminKnownAsRepository : ISnapshotComposerOriginalPublisherAdminKnownAsRepository
    {
        public Snapshot_ComposerOriginalPublisherAdminKnownAs SaveSnapshotComposerOriginalPublisherAdminKnownAs(Snapshot_ComposerOriginalPublisherAdminKnownAs sampleSnapshot)
        {
            using (var context = new AuthContext())
            {
                context.Snapshot_ComposerOriginalPublisherAdminKnownAs.Add(sampleSnapshot);
                context.SaveChanges();
                return sampleSnapshot;
            }
        }

        public List<Snapshot_ComposerOriginalPublisherAdminKnownAs> GetAllComposerOriginalPublisherAdminKnownAsForAdministratorId(int adminAffiliationId)
        {
            using (var context = new AuthContext())
            {
                return context.Snapshot_ComposerOriginalPublisherAdminKnownAs.Where(sl => sl.SnapshotComposerOriginalPublisherAdministratorId == adminAffiliationId).ToList();
            }
        }

        public bool DeleteSnapshotComposerOriginalPublisherAdminKnownAs(Snapshot_ComposerOriginalPublisherAdminKnownAs composerToDelete)
        {
            using (var context = new AuthContext())
            {
                var composer =
                    context.Snapshot_ComposerOriginalPublisherAdminKnownAs
                        .Find(composerToDelete.SnapshotComposerOriginalPublisherAdminKnownAsId);

                context.Snapshot_ComposerOriginalPublisherAdminKnownAs.Attach(composer);
                context.Snapshot_ComposerOriginalPublisherAdminKnownAs.Remove(composer);
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