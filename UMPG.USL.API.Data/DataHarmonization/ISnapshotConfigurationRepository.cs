using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public interface ISnapshotConfigurationRepository
    {
        Snapshot_Configuration SaveSnapshotConfiguration(Snapshot_Configuration snapshotConfiguration);
        Snapshot_Configuration GetSnapshotConfigurationByConfigurationId(int configurationId);
    }
}