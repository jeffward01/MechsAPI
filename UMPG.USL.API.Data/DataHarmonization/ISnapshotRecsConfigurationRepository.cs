using System.Collections.Generic;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public interface ISnapshotRecsConfigurationRepository
    {
        Snapshot_RecsConfiguration SaveSnapshotRecsConfiguration(
            Snapshot_RecsConfiguration snapshotRecsConfiguration);

        Snapshot_RecsConfiguration GetSnapshotRecsConfigurationByRecsConfigurationId(int recConfigurationId);
        List<Snapshot_RecsConfiguration> GetAllRecsConfigurationsRecordingsForLicenseProductId(int? licenseProductId);
        bool DeleteWorkRecordingByRecordignSnapshotId(int recordingSnapshotIdea);
        bool DoesRecConfigurationrecordignsExistForProductHeaderSnapshotId(int productHeaderSnapshotId);
        List<Snapshot_RecsConfiguration> GetAllRecsConfigurationsRecordingsForProductHeaderId(int productHeaderId);
        List<Snapshot_RecsConfiguration> GetAllRecsConfigurationsRecordingsForProductHeaderSnapshotId(
            int productHeaderSnapshotId);
    }
}