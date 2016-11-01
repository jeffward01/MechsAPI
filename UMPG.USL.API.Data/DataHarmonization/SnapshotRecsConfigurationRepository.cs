using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public class SnapshotRecsConfigurationRepository : ISnapshotRecsConfigurationRepository
    {
        public Snapshot_RecsConfiguration SaveSnapshotRecsConfiguration(
            Snapshot_RecsConfiguration snapshotRecsConfiguration)
        {
            using (var context = new AuthContext())
            {
                context.Snapshot_RecsConfigurations.Add(snapshotRecsConfiguration);
                context.SaveChanges();
                return snapshotRecsConfiguration;
            }
        }

        public Snapshot_RecsConfiguration GetSnapshotRecsConfigurationByRecsConfigurationId(int recConfigurationId)
        {
            using (var context = new AuthContext())
            {
                return context.Snapshot_RecsConfigurations.Find(recConfigurationId);
            }
        }

        public List<Snapshot_RecsConfiguration> GetAllRecsConfigurationsRecordingsForLicenseProductId(int? productId)
        {
            using (var context = new AuthContext())
            {
                return context.Snapshot_RecsConfigurations
                         .Include("Configuration").Where(_ => _.LicenseProductId == productId).ToList();
            }
        }


        public bool DoesRecConfigurationrecordignsExistForProductHeaderSnapshotId(int productHeaderSnapshotId)
        {
            using (var context = new AuthContext())
            {
                var result =
                    context.Snapshot_ProductHeaders.Include("Configurations")
                        .FirstOrDefault(_ => _.SnapshotProductHeaderId == productHeaderSnapshotId);
                if (result == null)
                {
                    return false;
                }
            }
            return true;
        }

        public List<Snapshot_RecsConfiguration> GetAllRecsConfigurationsRecordingsForProductHeaderSnapshotId(int productHeaderSnapshotId)
        {
            using (var context = new AuthContext())
            {
                var productHeader = context.Snapshot_ProductHeaders

                           .Include("Configurations")
                           .Include("Configurations.Configuration")
                           .Include("Configurations.LicenseProductConfiguration")
                           
                    .FirstOrDefault(_ => _.SnapshotProductHeaderId == productHeaderSnapshotId);
                return productHeader.Configurations;
            }
        }

        public List<Snapshot_RecsConfiguration> GetAllRecsConfigurationsRecordingsForProductHeaderId(int productHeaderId)
        {
            using (var context = new AuthContext())
            {
                var recConfig = context.Snapshot_RecsConfigurations

                           
                           .Include("Configuration")
                           .Include("LicenseProductConfiguration")

                    .Where(_ => _.ProductHeaderId == productHeaderId).ToList();
                return recConfig;
            }
        }


        public bool DeleteWorkRecordingByRecordignSnapshotId(int recordingSnapshotIdea)
        {
            using (var context = new AuthContext())
            {
                var recording = context.Snapshot_RecsConfigurations.Find(recordingSnapshotIdea);
                context.Snapshot_RecsConfigurations.Attach(recording);
                context.Snapshot_RecsConfigurations.Remove(recording);
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
