using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public class SnapshotRecsConfiguration : ISnapshotRecsConfiguration
    {
        public Snapshot_RecsConfiguration SaveSnapshotRecsConfiguration(
            Snapshot_RecsConfiguration snapshotRecsConfiguration)
        {
            using (var context = new AuthContext())
            {
                context.Snapshot_RecsConfigurations.Add(snapshotRecsConfiguration);
                context.SaveChanges();
                return snapshotRecsConfiguration;
            }
        }

        public Snapshot_RecsConfiguration GetSnapshotRecsConfigurationByRecsConfigurationId(int recConfigurationId)
        {
            using (var context = new AuthContext())
            {
                return context.Snapshot_RecsConfigurations.Find(recConfigurationId);
            }
        }
    }
}
