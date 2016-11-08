using System.Collections.Generic;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public interface ISnapshotAdminAffiliationBaseRepository
    {
        Snapshot_AdminAffiliationBase SaveSnapshotAdministrator(Snapshot_AdminAffiliationBase administratorSnapshot);
        Snapshot_AdminAffiliationBase GetSnapshotAdministratorByAdminAffiliationBaseId(int adminSnapshotId);
        List<Snapshot_AdminAffiliationBase> GetAllAdminAffiliationBaseForSnapshotAdminId(int adminSnapshotId);
        bool DeleteConfigurationSnapshot(int adminSnapshotId);
    }
}