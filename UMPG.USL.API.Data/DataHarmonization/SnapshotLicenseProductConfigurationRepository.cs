using System;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public class SnapshotLicenseProductConfigurationRepository : ISnapshotLicenseProductConfigurationRepository
    {
        public Snapshot_LicenseProductConfiguration SaveSnapshotLicenseProductConfiguration(
            Snapshot_LicenseProductConfiguration snapshotLicenseProductConfiguration)
        {
            using (var context = new AuthContext())
            {
                context.Snapshot_LicenseProductConfigurations.Add(snapshotLicenseProductConfiguration);
                context.SaveChanges();
                return snapshotLicenseProductConfiguration;
            }
        }

        public Snapshot_LicenseProductConfiguration
            GetSnapshotLicenseProductConfigurationByLicenseProductConfigurationId(int licenseProductConfigurationId)
        {
            using (var context = new AuthContext())
            {
                return context.Snapshot_LicenseProductConfigurations.Find(licenseProductConfigurationId);
            }
        }

        public bool
            DeleteLicenseProductConfigurationBySnapshot(Snapshot_LicenseProductConfiguration licenseProductConfigurationId)
        {
            using (var context = new AuthContext())
            {
                var licenseProduct =
                    context.Snapshot_LicenseProductConfigurations.Find(licenseProductConfigurationId.LicenseProductConfigurationId);
                context.Snapshot_LicenseProductConfigurations.Attach(licenseProduct);
                context.Snapshot_LicenseProductConfigurations.Remove(licenseProduct);
                try
                {
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    return false;
                }
                return true;
            }
        }
    }
}