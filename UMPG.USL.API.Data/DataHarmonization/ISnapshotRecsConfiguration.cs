using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public interface ISnapshotRecsConfiguration
    {
        Snapshot_RecsConfiguration SaveSnapshotRecsConfiguration(
            Snapshot_RecsConfiguration snapshotRecsConfiguration);

        Snapshot_RecsConfiguration GetSnapshotRecsConfigurationByRecsConfigurationId(int recConfigurationId);
    }
}