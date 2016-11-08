using System;
using System.Collections.Generic;
using System.Linq;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public class SnapshotComposerOriginalPublisherAdministratorRepository : ISnapshotComposerOriginalPublisherAdministratorRepository
    {
        public Snapshot_ComposerOriginalPublisherAdministrator SaveComposerOriginalPublisherAdministrator(Snapshot_ComposerOriginalPublisherAdministrator sampleSnapshot)
        {
            using (var context = new AuthContext())
            {
                context.Snapshot_ComposerOriginalPublisherAdministrator.Add(sampleSnapshot);
                context.SaveChanges();
                return sampleSnapshot;
            }
        }

        public List<Snapshot_ComposerOriginalPublisherAdministrator> GetAllComposerOriginalPublisherAdministratorsForComposerOriginalPublisherId(int snapshotComposerOriginalPublisherId)
        {
            using (var context = new AuthContext())
            {
                return context.Snapshot_ComposerOriginalPublisherAdministrator.Where(sl => sl.SnapshotComposerOriginalPublisherId == snapshotComposerOriginalPublisherId).ToList();
            }
        }

        public bool DeleteComposerOriginalPublisherAdministrator(Snapshot_ComposerOriginalPublisherAdministrator composerToDelete)
        {
            using (var context = new AuthContext())
            {
                var composer =
                    context.Snapshot_ComposerOriginalPublisherAdministrator
                        .Find(composerToDelete.SnapshotComposerOriginalPublisherAdministratorId);

                context.Snapshot_ComposerOriginalPublisherAdministrator.Attach(composer);
                context.Snapshot_ComposerOriginalPublisherAdministrator.Remove(composer);
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