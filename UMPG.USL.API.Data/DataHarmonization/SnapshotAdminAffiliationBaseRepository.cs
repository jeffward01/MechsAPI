using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public class SnapshotAdminAffiliationBaseRepository : ISnapshotAdminAffiliationBaseRepository
    {
        public Snapshot_AdminAffiliationBase SaveSnapshotAdministrator(Snapshot_AdminAffiliationBase administratorSnapshot)
        {
            using (var context = new AuthContext())
            {
                context.Snapshot_AdminAffiliationBases.Add(administratorSnapshot);
                context.SaveChanges();
                return administratorSnapshot;
            }
        }

        public Snapshot_AdminAffiliationBase GetSnapshotAdministratorByAdminAffiliationBaseId(int adminSnapshotId)
        {
            using (var context = new AuthContext())
            {
                return context.Snapshot_AdminAffiliationBases.Find(adminSnapshotId);
            }
        }

        public bool DeleteConfigurationSnapshot(int adminSnapshotId)
        {
            using (var context = new AuthContext())
            {
                var adminSnapshot =
                    context.Snapshot_AdminAffiliationBases.Find(adminSnapshotId);
                if (adminSnapshot == null)
                {
                    return false;
                }
                context.Snapshot_AdminAffiliationBases.Attach(adminSnapshot);
                context.Snapshot_AdminAffiliationBases.Remove(adminSnapshot);
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
