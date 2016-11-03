using System.Collections.Generic;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public interface ISnapshotOriginalPubAffiliationBaseRepository
    {
        List<Snapshot_OriginalPubAffiliationBase> GetAllOriginalPubAffiliationBasesByAffilationId(int opAffiliationId);
        bool DeletePhoneBySnapshotPhoneId(int snapshotPhoneId);
        Snapshot_OriginalPubAffiliationBase SaveSnapshotAdminAffiliation(Snapshot_OriginalPubAffiliationBase snapshotAdminAffiliation);
    }
}