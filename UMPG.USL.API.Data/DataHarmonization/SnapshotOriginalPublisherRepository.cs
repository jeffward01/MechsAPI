using System;
using System.Collections.Generic;
using System.Linq;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public class SnapshotOriginalPublisherRepository : ISnapshotOriginalPublisherRepository
    {
        public List<Snapshot_OriginalPublisher> GetAllOriginalPublishersForCaeCode(int cloneContactId)
        {
            using (var context = new AuthContext())
            {
                return context.Snapshot_OriginalPublishers.Where(_ => _.CloneCaeNumber == cloneContactId).ToList();
            }
        }

        public bool DeleteOriginalPublisherSnapshotBySnapshotId(int snapshotId)
        {
            using (var context = new AuthContext())
            {
                var originalPublisher = context.Snapshot_OriginalPublishers.Find(snapshotId);
                context.Snapshot_OriginalPublishers.Attach(originalPublisher);
                context.Snapshot_OriginalPublishers.Remove(originalPublisher);
                try
                {
                    context.SaveChanges();
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }
    }
}