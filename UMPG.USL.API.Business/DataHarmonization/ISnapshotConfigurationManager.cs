using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Business.DataHarmonization
{
    public interface ISnapshotConfigurationManager
    {
        Snapshot_Configuration SaveSnapshotConfiguration(Snapshot_Configuration snapshotConfiguration);
        Snapshot_Configuration GetSnapshotConfigurationByConfigurationId(int snapshotConfigurationId);
    }
}