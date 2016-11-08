using System.Collections.Generic;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public interface ISnapshot_ComposerOriginalPublisherAffiliationRepository
    {
        Snapshot_ComposerOriginalPublisherAffiliation SaveComposerOriginalPublisherAffiliation(Snapshot_ComposerOriginalPublisherAffiliation composerSnapshot);
        List<Snapshot_ComposerOriginalPublisherAffiliation> GetAllComposerOriginalPublisherAffiliationsForComposerOriginalPublisherId(int composerOrignalPublisherId);
        bool DeleteComposerOriginalPubhliserAffiliation(Snapshot_ComposerOriginalPublisherAffiliation composerToDelete);
    }
}