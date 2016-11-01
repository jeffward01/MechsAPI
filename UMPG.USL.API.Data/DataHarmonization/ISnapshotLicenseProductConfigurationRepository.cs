using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public interface ISnapshotLicenseProductConfigurationRepository
    {
        Snapshot_LicenseProductConfiguration SaveSnapshotLicenseProductConfiguration(
            Snapshot_LicenseProductConfiguration snapshotLicenseProductConfiguration);

        Snapshot_LicenseProductConfiguration
            GetSnapshotLicenseProductConfigurationByLicenseProductConfigurationId(int licenseProductConfigurationId);

        bool
            DeleteLicenseProductConfigurationBySnapshot(
                Snapshot_LicenseProductConfiguration licenseProductConfigurationId);
    }
}