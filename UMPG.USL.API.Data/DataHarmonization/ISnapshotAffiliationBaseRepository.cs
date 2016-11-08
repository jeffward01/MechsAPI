using System.Collections.Generic;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public interface ISnapshotAffiliationBaseRepository
    {
        List<Snapshot_AffiliationBase> GetAllAffiliationBasesForAffilationId(int affiliationId);
        Snapshot_AffiliationBase SaveSnapshotAffiliationBase(Snapshot_AffiliationBase snapshotAffiliation);
        bool DeleteAffilationByAffiliationBaseSnapshotId(int affiliationSnapshotId);
    }
}