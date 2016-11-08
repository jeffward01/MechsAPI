using System;
using System.Collections.Generic;
using System.Linq;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public class Snapshot_ComposerOriginalPublisherRepository : ISnapshot_ComposerOriginalPublisherRepository
    {
        public Snapshot_ComposerOriginalPublisher SaveComposerOriginalPublisher(Snapshot_ComposerOriginalPublisher composerSnapshot)
        {
            using (var context = new AuthContext())
            {
                context.Snapshot_ComposerOriginalPublishers.Add(composerSnapshot);
                context.SaveChanges();
                return composerSnapshot;
            }
        }

        public List<Snapshot_ComposerOriginalPublisher> GetAllComposerOriginalPublishersForComposerOd(int composerSnapshotId)
        {
            using (var context = new AuthContext())
            {
                return context.Snapshot_ComposerOriginalPublishers.Where(sl => sl.SnapshotComposerId == composerSnapshotId).ToList();
            }
        }

        public bool DeleteComposerOriginalPublisher(Snapshot_ComposerOriginalPublisher composerToDelete)
        {
            using (var context = new AuthContext())
            {
                var composer =
                    context.Snapshot_ComposerOriginalPublishers
                        .Find(composerToDelete.SnapshotComposerOriginalPublisherId);

                context.Snapshot_ComposerOriginalPublishers.Attach(composer);
                context.Snapshot_ComposerOriginalPublishers.Remove(composer);
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