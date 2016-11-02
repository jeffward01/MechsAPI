using System.Collections.Generic;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public interface ISnapshotAquisitionLocationCodeRepository
    {
        List<Snapshot_AquisitionLocationCode> GetAllAquisitionLocationCodesForTrackId(int trackId);
        bool DeleteAquisitionLocationCodeBySnashotId(int aquisitonLocationCodeSnapshotId);
    }
}