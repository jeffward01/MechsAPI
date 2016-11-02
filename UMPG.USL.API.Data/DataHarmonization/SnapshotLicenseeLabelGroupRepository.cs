using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
   public class SnapshotLicenseeLabelGroupRepository : ISnapshotLicenseeLabelGroupRepository
   {
        public Snapshot_LicenseeLabelGroup SaveLicenseeLabelGroup(
        Snapshot_LicenseeLabelGroup snapshotLicenseeLabelGroup)
        {
            using (var context = new AuthContext())
            {
                context.Snapshot_LicenseeLabelGroups.Add(snapshotLicenseeLabelGroup);
                context.SaveChanges();
                return snapshotLicenseeLabelGroup;
            }
        }

        public Snapshot_LicenseeLabelGroup GetSnapshotLicenseeLabelGroupBySnapshotIdGroup(int snapshotLicenseeLabelGroupId)
        {
            using (var context = new AuthContext())
            {
                return context.Snapshot_LicenseeLabelGroups.Find(snapshotLicenseeLabelGroupId);
            }
        }

        public Snapshot_LicenseeLabelGroup GetLicenseeLabelGroupByCloneLicenseeLabelGroupId(int cloneLicenseeLabelGroupId)
        {
            using (var context = new AuthContext())
            {
                return context.Snapshot_LicenseeLabelGroups
                    .Include("LabelGroupLinksFiltered")
                    .Include("LabelGroupLinks")
                    .FirstOrDefault(_ => _.CloneLicenseeLabelGroupId == cloneLicenseeLabelGroupId);
            }
        }

        


        public bool DeleteSnapshotLicenseeLabelGroupBySnapshotId(int snapshotLicenseeLabelGroupId)
        {
            using (var context = new AuthContext())
            {
                var recording = context.Snapshot_LicenseeLabelGroups.Find(snapshotLicenseeLabelGroupId);
                context.Snapshot_LicenseeLabelGroups.Attach(recording);
                context.Snapshot_LicenseeLabelGroups.Remove(recording);
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
