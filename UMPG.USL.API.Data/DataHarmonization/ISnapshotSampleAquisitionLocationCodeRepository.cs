using System.Collections.Generic;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public interface ISnapshotSampleAquisitionLocationCodeRepository
    {
        Snapshot_SampleAquisitionLocationCode SaveSampleAquisitionLocationCode(Snapshot_SampleAquisitionLocationCode composerSnapshot);
        List<Snapshot_SampleAquisitionLocationCode> GetAllSampleLocationCodesForSampleId(int sampleSnapshotId);
        bool DeleteSampleLocationCode(Snapshot_SampleAquisitionLocationCode composerToDelete);
    }
}