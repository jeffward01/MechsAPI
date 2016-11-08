using System;
using System.Collections.Generic;
using System.Linq;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public class SnapshotAquisitionLocationCodeRepository : ISnapshotAquisitionLocationCodeRepository
    {
        public List<Snapshot_AquisitionLocationCode> GetAllAquisitionLocationCodesForTrackId(int trackId)
        {
            using (var context = new AuthContext())
            {
                return context.Snapshot_AquisitionLocationCodes.Where(_ => _.CloneWorksTrackId == trackId).ToList();
            }
        }

        public bool DeleteAquisitionLocationCodeBySnashotId(int aquisitonLocationCodeSnapshotId)
        {
            using (var context = new AuthContext())
            {
                var aquisitonLocationCode = context.Snapshot_AquisitionLocationCodes.Find(aquisitonLocationCodeSnapshotId);
                context.Snapshot_AquisitionLocationCodes.Attach(aquisitonLocationCode);
                context.Snapshot_AquisitionLocationCodes.Remove(aquisitonLocationCode);
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

        public Snapshot_AquisitionLocationCode SaveAquisitionLocationCode(Snapshot_AquisitionLocationCode snapshotLabel)
        {
            using (var context = new AuthContext())
            {
                context.Snapshot_AquisitionLocationCodes.Add(snapshotLabel);
                context.SaveChanges();
                return snapshotLabel;
            }
        }
    }
}