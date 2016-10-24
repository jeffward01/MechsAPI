using UMPG.USL.API.Data.DataHarmonization;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Business.DataHarmonization
{
    public class SnapshotLicenseProductConfigurationManager : ISnapshotLicenseProductConfigurationManager
    {
        private readonly ISnapshotLicenseProductConfigurationRepository _snapshotLicenseProductConfigurationRepository;

        public SnapshotLicenseProductConfigurationManager(ISnapshotLicenseProductConfigurationRepository snapshotLicenseProductConfigurationRepository)
        {
            _snapshotLicenseProductConfigurationRepository = snapshotLicenseProductConfigurationRepository;
        }

        public Snapshot_LicenseProductConfiguration SaveSnapshotLicenseProductConfiguration(
            Snapshot_LicenseProductConfiguration snapshotLicenseProductConfiguration)
        {
            return
                _snapshotLicenseProductConfigurationRepository.SaveSnapshotLicenseProductConfiguration(
                    snapshotLicenseProductConfiguration);
        }

        public Snapshot_LicenseProductConfiguration GetSnapshotLicenseProductConfigurationByLicenseProductConfigurationId(
            int snapshotLicenseProductConfigurationId)
        {
            return
                _snapshotLicenseProductConfigurationRepository.GetSnapshotLicenseProductConfigurationByLicenseProductConfigurationId(
                    snapshotLicenseProductConfigurationId);
        }
    }
}