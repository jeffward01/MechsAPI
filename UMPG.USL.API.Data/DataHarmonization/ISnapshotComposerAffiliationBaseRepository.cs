using System.Collections.Generic;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public interface ISnapshotComposerAffiliationBaseRepository
    {
        Snapshot_ComposerAffiliationBase SaveComposerAffiliatioBasenSnapshot(Snapshot_ComposerAffiliationBase composerSnapshot);
        List<Snapshot_ComposerAffiliationBase> GetAllComposersAffiliationBasesByComposerAffiliationSnapshotId(int composerAffiliationSnapshotId);
        bool DeleteComposerAffiliationBaseSnapshotByComposer(Snapshot_ComposerAffiliationBase composerToDelete);
    }
}