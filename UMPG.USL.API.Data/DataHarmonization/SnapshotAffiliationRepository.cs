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

        public List<Snapshot_Affiliation> GetAllAffiliationsForWriterSnapshotId(int worksWriterSnapshotId)
        {
            using (var context = new AuthContext())
            {
                return context.Snapshot_Affiliations.Where(_ => _.SnapshotWorksWriterId == worksWriterSnapshotId).ToList();
            }
        }
        public Snapshot_Affiliation SaveSnapshotAffiliation(Snapshot_Affiliation snapshotAffiliation)
        {
            using (var context = new AuthContext())
            {
                context.Snapshot_Affiliations.Add(snapshotAffiliation);
                context.SaveChanges();
                return snapshotAffiliation;
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