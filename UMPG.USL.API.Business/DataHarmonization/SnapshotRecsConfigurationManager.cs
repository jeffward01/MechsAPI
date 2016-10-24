using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.API.Data.DataHarmonization;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Business.DataHarmonization
{
    public class SnapshotRecsConfigurationManager : ISnapshotRecsConfigurationManager
    {
        private readonly ISnapshotConfigurationRepository _snapshotConfigurationRepository;

        public SnapshotRecsConfigurationManager(ISnapshotConfigurationRepository snapshotConfigurationRepository)
        {
            _snapshotConfigurationRepository = snapshotConfigurationRepository;
        }

        public Snapshot_Configuration SaveSnapshotConfiguration(Snapshot_Configuration snapshotConfiguration)
        {
            return _snapshotConfigurationRepository.SaveSnapshotConfiguration(snapshotConfiguration);
        }

        public Snapshot_Configuration GetSnapshotConfigurationByConfigurationId(int configurationId)
        {
            return _snapshotConfigurationRepository.GetSnapshotConfigurationByConfigurationId(configurationId);
        }
    }
}
