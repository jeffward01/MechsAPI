using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Business.DataHarmonization
{
    public interface ISnapshotLicenseProductConfigurationManager
    {
        Snapshot_LicenseProductConfiguration SaveSnapshotLicenseProductConfiguration(
            Snapshot_LicenseProductConfiguration snapshotLicenseProductConfiguration);

        Snapshot_LicenseProductConfiguration GetSnapshotLicenseProductConfigurationByLicenseProductConfigurationId(
            int snapshotLicenseProductConfigurationId);
    }
}