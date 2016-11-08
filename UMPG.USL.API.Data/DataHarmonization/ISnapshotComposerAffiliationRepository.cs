using System.Collections.Generic;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public interface ISnapshotComposerAffiliationRepository
    {
        Snapshot_ComposerAffiliation SaveComposerAffiliationSnapshot(Snapshot_ComposerAffiliation composerSnapshot);
        List<Snapshot_ComposerAffiliation> GetAllComposersAffiliationsByComposerSnapshotId(int composerSnapshotId);
        bool DeleteComposerAffiliationSnapshotByComposer(Snapshot_ComposerAffiliation composerToDelete);
    }
}