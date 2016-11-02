using System;
using System.Collections.Generic;
using System.Linq;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public class SnapshotSampleRepository : ISnapshotSampleRepository
    {
        public List<Snapshot_RecsCopyright> GetAllSamplesForRecsCopyrightByCloneTrackId(int cloneTrackId)
        {
            using (var context = new AuthContext())
            {
                return context.Snapshot_RecsCopyrights.Where(_ => _.CloneWorksTrackId == cloneTrackId).ToList();
            }
        }

        public bool DeleteSampleBySampleSnapshotId(int sampleSnapshotId)
        {
            using (var context = new AuthContext())
            {
                var sample = context.Snapshot_RecsCopyrights.Find(sampleSnapshotId);
                context.Snapshot_RecsCopyrights.Attach(sample);
                context.Snapshot_RecsCopyrights.Remove(sample);
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