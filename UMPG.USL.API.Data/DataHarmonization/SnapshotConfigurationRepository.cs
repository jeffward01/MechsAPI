using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public class SnapshotConfigurationRepository : ISnapshotConfigurationRepository
    {
        public Snapshot_Configuration SaveSnapshotConfiguration(Snapshot_Configuration snapshotConfiguration)
        {
            using (var context = new AuthContext())
            {
                context.Snapshot_Configurations.Add(snapshotConfiguration);
                context.SaveChanges();
                return snapshotConfiguration;
            }
        }

        public Snapshot_Configuration GetSnapshotConfigurationByConfigurationId(int configurationId)
        {
            using (var context = new AuthContext())
            {
                return context.Snapshot_Configurations.Find(configurationId);
            }
        }
    }
}
