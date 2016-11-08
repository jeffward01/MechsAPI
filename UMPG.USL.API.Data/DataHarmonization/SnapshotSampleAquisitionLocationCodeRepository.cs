using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public class SnapshotSampleAquisitionLocationCodeRepository : ISnapshotSampleAquisitionLocationCodeRepository
    {
        public Snapshot_SampleAquisitionLocationCode SaveSampleAquisitionLocationCode(Snapshot_SampleAquisitionLocationCode composerSnapshot)
        {
            using (var context = new AuthContext())
            {
                context.Snapshot_SampleAquisitionLocationCodes.Add(composerSnapshot);
                context.SaveChanges();
                return composerSnapshot;
            }
        }

        public List<Snapshot_SampleAquisitionLocationCode> GetAllSampleLocationCodesForSampleId(int sampleSnapshotId)
        {
            using (var context = new AuthContext())
            {
                return context.Snapshot_SampleAquisitionLocationCodes.Where(sl => sl.SnapshotSampleId == sampleSnapshotId).ToList();
            }
        }

        public bool DeleteSampleLocationCode(Snapshot_SampleAquisitionLocationCode composerToDelete)
        {
            using (var context = new AuthContext())
            {
                var composer =
                    context.Snapshot_SampleAquisitionLocationCodes
                        .Find(composerToDelete.SnapshotSampleAquisitionLocationCode);

                context.Snapshot_SampleAquisitionLocationCodes.Attach(composer);
                context.Snapshot_SampleAquisitionLocationCodes.Remove(composer);
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
