using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.API.Data.DataHarmonization;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Business.DataHarmonization
{
    public class SnapshotWorksRecordingManager : ISnapshotWorksRecordingManager
    {
        private readonly ISnapshotWorksRecordingRepository _snapshotWorksRecordingRepository;
        public SnapshotWorksRecordingManager(ISnapshotWorksRecordingRepository snapshotWorksRecordingRepository)
        {
            _snapshotWorksRecordingRepository = snapshotWorksRecordingRepository;
        }

        public Snapshot_WorksRecording SaveSnapshotWorksRecording(Snapshot_WorksRecording snapshotWorksRecording)
        {
            return _snapshotWorksRecordingRepository.SaveSnapshotWorksRecording(snapshotWorksRecording);
        }

        public Snapshot_WorksRecording GetSnapshotWorksRecordingByWorksRecordingId(int worksRecordingId)
        {
            return _snapshotWorksRecordingRepository.GetSnapshotWorksRecordingByWorksRecordingId(worksRecordingId);
        }
    }
}
