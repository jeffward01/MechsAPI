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
    }
}