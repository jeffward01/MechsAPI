using System.Collections.Generic;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public interface ISnapshotOriginalPublisherRepository
    {
        List<Snapshot_OriginalPublisher> GetAllOriginalPublishersForCaeCode(int cloneContactId);

        Snapshot_OriginalPublisher SaveSnapshotOriginalPublisher(Snapshot_OriginalPublisher originalPublisher);

        List<Snapshot_OriginalPublisher> GetAllOriginalPublishersForSnapshotWriterId(int worksWriterSnapshotId);

        bool DeleteOriginalPublisherSnapshotBySnapshotId(int snapshotId);
    }
}