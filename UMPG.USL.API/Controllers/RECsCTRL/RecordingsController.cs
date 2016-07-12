using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UMPG.USL.API.Business.Recs;

namespace UMPG.USL.API.Controllers.RECsCTRL
{
    public class RecordingsController
    {
        private readonly IRecordingManager _recordingManager;

        public RecordingsController(IRecordingManager recordingManager)
        {
            _recordingManager = recordingManager;
        }
    }
}