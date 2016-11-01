using System;
using System.Collections.Generic;
using System.Linq;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public class SnapshotWorksWriterRepository : ISnapshotWorksWriterRepository
    {
        public List<Snapshot_WorksWriter> GetAllWritersForCloneTrackId(int cloneTrackId)
        {
            using (var context = new AuthContext())
            {
                return context.Snapshot_WorksWriters.Where(_ => _.CloneWorksTrackId == cloneTrackId).ToList();
            }
        }

        public bool DeleteWorksWriterSnapshotBySnapshotId(int worksWriterSnapshotId)
        {
            using (var context = new AuthContext())
            {
                var worksWriter = context.Snapshot_WorksWriters.Find(worksWriterSnapshotId);
                context.Snapshot_WorksWriters.Attach(worksWriter);
                context.Snapshot_WorksWriters.Remove(worksWriter);
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