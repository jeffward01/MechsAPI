using System;
using System.Collections.Generic;
using System.Linq;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public class SnapshotAffiliationBaseRepository : ISnapshotAffiliationBaseRepository
    {
        public List<Snapshot_AffiliationBase> GetAllAffiliationBasesForAffilationId(int affiliationId)
        {
            using (var context = new AuthContext())
            {
                return context.Snapshot_AffiliationBases.Where(_ => _.SnapshotAffiliationId == affiliationId).ToList();
            }
        }

        public Snapshot_AffiliationBase SaveSnapshotAffiliationBase(Snapshot_AffiliationBase snapshotAffiliation)
        {
            using (var context = new AuthContext())
            {
                context.Snapshot_AffiliationBases.Add(snapshotAffiliation);
                context.SaveChanges();
                return snapshotAffiliation;
            }
        }

        public bool DeleteAffilationByAffiliationBaseSnapshotId(int affiliationSnapshotId)
        {
            using (var context = new AuthContext())
            {
                var affilation = context.Snapshot_AffiliationBases.Find(affiliationSnapshotId);
                context.Snapshot_AffiliationBases.Attach(affilation);
                context.Snapshot_AffiliationBases.Remove(affilation);
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