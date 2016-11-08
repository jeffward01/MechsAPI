using System;
using System.Collections.Generic;
using System.Linq;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public class SnapshotLocalClientCopyrightRepository : ISnapshotLocalClientCopyrightRepository
    {
        public List<Snapshot_LocalClientCopyright> GetAllLocalCopyrightsForTrackId(int trackId)
        {
            using (var context = new AuthContext())
            {
                return context.Snapshot_LocalClientCopyrights.Where(_ => _.CloneWorksTrackId == trackId).ToList();
            }
        }

        public bool DeleteLocalClientCopyrightBySnapshotId(int localClientCopyrightSnapshotId)
        {
            using (var context = new AuthContext())
            {
                var clientcopyRight = context.Snapshot_LocalClientCopyrights.Find(localClientCopyrightSnapshotId);
                context.Snapshot_LocalClientCopyrights.Attach(clientcopyRight);
                context.Snapshot_LocalClientCopyrights.Remove(clientcopyRight);
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

        public Snapshot_LocalClientCopyright SaveLocalClientCopyright(Snapshot_LocalClientCopyright snapshotLabel)
        {
            using (var context = new AuthContext())
            {
                context.Snapshot_LocalClientCopyrights.Add(snapshotLabel);
                context.SaveChanges();
                return snapshotLabel;
            }
        }

    }
}