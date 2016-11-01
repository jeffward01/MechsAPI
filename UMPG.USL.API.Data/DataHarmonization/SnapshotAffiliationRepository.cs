using System;
using System.Collections.Generic;
using System.Linq;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public class SnapshotAffiliationRepository : ISnapshotAffiliationRepository
    {
        public List<Snapshot_Affiliation> GetAllAFfiliationsForCAENumber(int cloneCaeNumber)
        {
            using (var context = new AuthContext())
            {
                return context.Snapshot_Affiliations.Where(_ => _.CloneWriterCaeNumber == cloneCaeNumber).ToList();
            }
        }

        public bool DeleteAffilationByAffiliationSnapshotId(int affiliationSnapshotId)
        {
            using (var context = new AuthContext())
            {
                var affilation = context.Snapshot_Affiliations.Find(affiliationSnapshotId);
                context.Snapshot_Affiliations.Attach(affilation);
                context.Snapshot_Affiliations.Remove(affilation);
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