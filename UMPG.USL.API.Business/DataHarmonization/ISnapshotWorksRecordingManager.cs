using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Business.DataHarmonization
{
    public interface ISnapshotWorksRecordingManager
    {
        Snapshot_WorksRecording SaveSnapshotWorksRecording(Snapshot_WorksRecording snapshotWorksRecording);
        Snapshot_WorksRecording GetSnapshotWorksRecordingByWorksRecordingId(int worksRecordingId);
        RecordingInfo GetRecordingInfoForSnapshotRecordingId(Snapshot_WorksRecording snapshotWorksRecording);
        RecordingInfo GetRecordingInfoForSnapshotTrackId(int snapshotWorksTrackId);
    }
}