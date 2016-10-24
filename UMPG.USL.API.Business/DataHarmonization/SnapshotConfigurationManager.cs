using UMPG.USL.API.Data.DataHarmonization;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Business.DataHarmonization
{
    public class SnapshotConfigurationManager : ISnapshotConfigurationManager
    {
        private readonly ISnapshotConfigurationRepository _snapshotConfigurationRepository;

        public SnapshotConfigurationManager(ISnapshotConfigurationRepository snapshotConfigurationRepository)
        {
            _snapshotConfigurationRepository = snapshotConfigurationRepository;
        }

        public Snapshot_Configuration SaveSnapshotConfiguration(Snapshot_Configuration snapshotConfiguration)
        {
            return _snapshotConfigurationRepository.SaveSnapshotConfiguration(snapshotConfiguration);
        }

        public Snapshot_Configuration GetSnapshotConfigurationByConfigurationId(int snapshotConfigurationId)
        {
            return _snapshotConfigurationRepository.GetSnapshotConfigurationByConfigurationId(snapshotConfigurationId);
        }
    }
}