using System.Collections.Generic;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public interface ISnapshotAffiliationRepository
    {
        List<Snapshot_Affiliation> GetAllAFfiliationsForCAENumber(int cloneCaeNumber);

        bool DeleteAffilationByAffiliationSnapshotId(int affiliationSnapshotId);
        List<Snapshot_Affiliation> GetAllAffiliationsForWriterSnapshotId(int worksWriterSnapshotId);

        Snapshot_Affiliation SaveSnapshotAffiliation(Snapshot_Affiliation snapshotAffiliation);
    }
}