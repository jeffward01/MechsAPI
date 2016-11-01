using System.Collections.Generic;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public interface ISnapshotOriginalPublisherRepository
    {
        List<Snapshot_OriginalPublisher> GetAllOriginalPublishersForCaeCode(int cloneContactId);
        bool DeleteOriginalPublisherSnapshotBySnapshotId(int snapshotId);
    }
}