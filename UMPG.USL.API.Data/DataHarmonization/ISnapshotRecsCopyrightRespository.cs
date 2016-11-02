using System.Collections.Generic;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public interface ISnapshotRecsCopyrightRespository
    {
        Snapshot_RecsCopyright SaveSnapshotWorksRecording(Snapshot_RecsCopyright snapshotRecsCopyright);
        List<Snapshot_RecsCopyright> GetAllRecsCopyrightsForCloneTrackId(int cloneTrackId);
        bool DeleteRecsCopyrightByRecsCopyrightSnapshotId(int recCopyrightSnapshotId);
    }
}