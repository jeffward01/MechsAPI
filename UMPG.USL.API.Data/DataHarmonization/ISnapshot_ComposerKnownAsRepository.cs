using System.Collections.Generic;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public interface ISnapshot_ComposerKnownAsRepository
    {
        Snapshot_ComposerKnownAs SaveComposerKnownAs(Snapshot_ComposerKnownAs composerSnapshot);
        List<Snapshot_ComposerKnownAs> GetAllComposerKnownAsByComposerSnapshotId(int composerAffiliationSnapshotId);
        bool DeleteComposerKnownAs(Snapshot_ComposerKnownAs composerToDelete);
    }
}