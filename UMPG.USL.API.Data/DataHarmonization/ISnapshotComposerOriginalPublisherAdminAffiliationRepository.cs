using System.Collections.Generic;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public interface ISnapshotComposerOriginalPublisherAdminAffiliationRepository
    {
        Snapshot_ComposerOriginalPublisherAdminAffiliation SaveComposerOriginalPublisherAdminAffiliation(Snapshot_ComposerOriginalPublisherAdminAffiliation sampleSnapshot);
        List<Snapshot_ComposerOriginalPublisherAdminAffiliation> GetAllComposerOriginalPublisherAdminAffiliationsorAdminId(int composerOpAdminId);
        bool DeleteComposerOriginalPublisherAdminAffiliation(Snapshot_ComposerOriginalPublisherAdminAffiliation composerToDelete);
    }
}