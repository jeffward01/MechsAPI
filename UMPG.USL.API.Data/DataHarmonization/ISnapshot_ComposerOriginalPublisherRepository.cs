using System.Collections.Generic;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public interface ISnapshot_ComposerOriginalPublisherRepository
    {
        Snapshot_ComposerOriginalPublisher SaveComposerOriginalPublisher(Snapshot_ComposerOriginalPublisher composerSnapshot);
        List<Snapshot_ComposerOriginalPublisher> GetAllComposerOriginalPublishersForComposerId(int composerSnapshotId);
        bool DeleteComposerOriginalPublisher(Snapshot_ComposerOriginalPublisher composerToDelete);
    }
}