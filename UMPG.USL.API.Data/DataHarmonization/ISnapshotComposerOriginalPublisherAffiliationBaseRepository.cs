using System.Collections.Generic;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public interface ISnapshotComposerOriginalPublisherAffiliationBaseRepository
    {
        Snapshot_ComposerOriginalPublisherAffiliationBase SaveComposerOriginalPublisherAffiliationBase(Snapshot_ComposerOriginalPublisherAffiliationBase composerSnapshot);
        List<Snapshot_ComposerOriginalPublisherAffiliationBase> GetComposerOriginalPublisherAffiliationBasesFComposerOriginalPublisherAffiliationId(int composerOrignalPublisherAffiliationId);
        bool DeleteComposerOriginalPublisherAffiliationBase(Snapshot_ComposerOriginalPublisherAffiliationBase composerToDelete);
    }
}