using System.Collections.Generic;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public interface ISnapshotSampleRepository
    {
        List<Snapshot_RecsCopyright> GetAllSamplesForRecsCopyrightByCloneTrackId(int cloneTrackId);
        bool DeleteSampleBySampleSnapshotId(int sampleSnapshotId);
    }
}