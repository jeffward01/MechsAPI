using System;
using System.Collections.Generic;
using System.Linq;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public class SnapshotOriginalPublisherAffiliationRepository : ISnapshotOriginalPublisherAffiliationRepository
    {
        public List<Snapshot_OriginalPublisherAffiliation> GetAllOriginalPublisherAffiliationsByCaeCode(int caeCode)
        {
            using (var context = new AuthContext())
            {
                return context.Snapshot_OriginalPublisherAffiliations.Where(_ => _.CloneWriterCaeNumber == caeCode).ToList();
            }
        }

        public bool DeleteOriginalPublisherSnapshotById(int snapshotPhoneId)
        {
            using (var context = new AuthContext())
            {
                var address = context.Snapshot_OriginalPublisherAffiliations.Find(snapshotPhoneId);
                context.Snapshot_OriginalPublisherAffiliations.Attach(address);
                context.Snapshot_OriginalPublisherAffiliations.Remove(address);
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

        public Snapshot_OriginalPublisherAffiliation SaveSnapshotOriginalPublisherAffiliation(Snapshot_OriginalPublisherAffiliation snapshotAdminAffiliation)
        {
            using (var context = new AuthContext())
            {
                context.Snapshot_OriginalPublisherAffiliations.Add(snapshotAdminAffiliation);
                context.SaveChanges();
                //    int id = snapshotAdminAffiliation.SnapshotAdminAffiliationId;
                return snapshotAdminAffiliation;
            }
        }
    }
}