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
        private readonly ISnapshotWorkTrackRepository _snapshotWorkTrackRepository;
        private readonly ISnapshotLicenseProductManager _snapshotLicenseProductManager;
        private readonly ISnapshotProductHeaderRepository _snapshotProductHeaderRepository;
        public SnapshotWorksRecordingManager(ISnapshotWorksRecordingRepository snapshotWorksRecordingRepository, ISnapshotWorkTrackRepository snapshotWorkTrackRepository, ISnapshotLicenseProductManager snapshotLicenseProductManager, ISnapshotProductHeaderRepository snapshotProductHeaderRepository)
        {
            _snapshotProductHeaderRepository = snapshotProductHeaderRepository;
            _snapshotLicenseProductManager = snapshotLicenseProductManager;
            _snapshotWorkTrackRepository = snapshotWorkTrackRepository;
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

        public RecordingInfo GetRecordingInfoForSnapshotRecordingId(Snapshot_WorksRecording snapshotWorksRecording)
        {
            var trackId = snapshotWorksRecording.SnapshotWorkTrackId;
            var track = _snapshotWorkTrackRepository.GetTrackBySnapshotWorksTrackId(trackId);
            var snapshotLicenseProductId = snapshotWorksRecording.SnapshotLicenseProductId;
            var productHeaderId =
                _snapshotLicenseProductManager.GetProductHeaderIdForSnapshotLicenseProductId(snapshotLicenseProductId);
            var productHeader = _snapshotProductHeaderRepository.GetProductHeaderByProductHeaderId(productHeaderId);

            return new RecordingInfo
            {
                SnapshotProductHeader = productHeader,
                SnapshotWorksTrack = track
            };
        }

        public RecordingInfo GetRecordingInfoForSnapshotTrackId(int snapshotWorksTrackId)
        {

            var snapshotWorksRecording = _snapshotWorksRecordingRepository.GetWorksRecordingForSnapshotTrackId(snapshotWorksTrackId);
            var track = _snapshotWorkTrackRepository.GetTrackBySnapshotWorksTrackId(snapshotWorksRecording.SnapshotWorkTrackId);
            var snapshotLicenseProductId = snapshotWorksRecording.SnapshotLicenseProductId;
            var productHeaderId =
                _snapshotLicenseProductManager.GetProductHeaderIdForSnapshotLicenseProductId(snapshotLicenseProductId);
            var productHeader = _snapshotProductHeaderRepository.GetProductHeaderByProductHeaderId(productHeaderId);

            return new RecordingInfo
            {
                SnapshotProductHeader = productHeader,
                SnapshotWorksTrack = track
            };
        }
    }
}
