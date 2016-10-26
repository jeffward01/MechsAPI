using System;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public class SnapshotRoleRepository : ISnapshotRoleRepository
    {
        public Snapshot_Role SaveSnapshotRole(Snapshot_Role snapshotRole)
        {
            using (var context = new AuthContext())
            {
                context.Snapshot_Roles.Add(snapshotRole);
                context.SaveChanges();
                return snapshotRole;
            }
        }

        public Snapshot_Role GetSnapshotRoleById(int snapShotRoleId)
        {
            using (var context = new AuthContext())
            {
                return context.Snapshot_Roles.Find(snapShotRoleId);
            }
        }

        public bool DeleteRoleSnapshotByRoleId(int snapshotRoleId)
        {
            using (var context = new AuthContext())
            {
                var role = context.Snapshot_Roles.Find(snapshotRoleId);
                context.Snapshot_Roles.Attach(role);
                context.Snapshot_Roles.Remove(role);
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