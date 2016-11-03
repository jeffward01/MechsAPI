using System.Collections.Generic;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public interface ISnapshotOriginalPublisherAffiliationRepository
    {
        List<Snapshot_OriginalPublisherAffiliation> GetAllOriginalPublisherAffiliationsByCaeCode(int caeCode);
        bool DeleteOriginalPublisherSnapshotById(int snapshotPhoneId);
        Snapshot_OriginalPublisherAffiliation SaveSnapshotOriginalPublisherAffiliation(Snapshot_OriginalPublisherAffiliation snapshotAdminAffiliation);
    }
}