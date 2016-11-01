using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public interface ISnapshotWorkTrackRepository
    {
        Snapshot_WorksTrack GetTrackForCloneTrackId(int cloneTrackId);
        bool DeleteTrackBySnapshotTrackId(int snapshotTrackId);
    }
}