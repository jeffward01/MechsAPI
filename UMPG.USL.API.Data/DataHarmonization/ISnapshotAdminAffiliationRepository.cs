using System.Collections.Generic;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public interface ISnapshotAdminAffiliationRepository
    {
        List<Snapshot_AdminAffiliation> GetAllAdminAffiliationsForSnapshotAdminId(int adminSnapshotId);
        bool DeletePhoneBySnapshotPhoneId(int snapshotPhoneId);
        Snapshot_AdminAffiliation SaveSnapshotAdminAffiliation(Snapshot_AdminAffiliation snapshotAdminAffiliation);
    }
}