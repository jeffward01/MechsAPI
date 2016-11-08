using System.Collections.Generic;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public interface ISnapshotComposerOriginalPublisherAdminAffiliationBaseRepository
    {
        Snapshot_ComposerOriginalPublisherAdminAffiliationBase SaveComposerOriginalPublisherAdminAffiliationBase(Snapshot_ComposerOriginalPublisherAdminAffiliationBase sampleSnapshot);
        List<Snapshot_ComposerOriginalPublisherAdminAffiliationBase> GetAllComposerOriginalPublisherAdminAffiliationBasesForAffiliationId(int adminAffiliationId);
        bool DeleteComposerOriginalPublisherAdminAffiliationBase(Snapshot_ComposerOriginalPublisherAdminAffiliationBase composerToDelete);
    }
}