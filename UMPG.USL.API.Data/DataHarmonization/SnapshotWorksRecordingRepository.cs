using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public class SnapshotWorksRecordingRepository : ISnapshotWorksRecordingRepository
    {
        public Snapshot_WorksRecording SaveSnapshotWorksRecording(Snapshot_WorksRecording snapshotWorksRecording)
        {
            using (var context = new AuthContext())
            {
                context.Snapshot_WorksRecordings.Add(snapshotWorksRecording);
                context.SaveChanges();
                return snapshotWorksRecording;
            }
        }

        public Snapshot_WorksRecording GetSnapshotWorksRecordingByWorksRecordingId(int worksRecordingId)
        {
            using (var context = new AuthContext())
            {
                return context.Snapshot_WorksRecordings.Find(worksRecordingId);
            }
        }
    }
}