using System;
using System.Collections.Generic;
using System.Linq;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public class SnapshotSampleRepository : ISnapshotSampleRepository
    {
        public Snapshot_Sample SaveSampleSnapshot(Snapshot_Sample sampleSnapshot)
        {
            using (var context = new AuthContext())
            {
                context.Snapshot_Samples.Add(sampleSnapshot);
                context.SaveChanges();
                return sampleSnapshot;
            }
        }

        public List<Snapshot_Sample> GetAllSamplesForRecCopyrightId(int recsCopyrightId)
        {
            using (var context = new AuthContext())
            {
                return context.Snapshot_Samples.Where(sl => sl.SnapshotRecsCopyrightId == recsCopyrightId).ToList();
            }
        }

        public bool DeleteSampleSnapshot(Snapshot_Sample composerToDelete)
        {
            using (var context = new AuthContext())
            {
                var composer =
                    context.Snapshot_Samples
                        .Find(composerToDelete.SnapshotSampleId);

                context.Snapshot_Samples.Attach(composer);
                context.Snapshot_Samples.Remove(composer);
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