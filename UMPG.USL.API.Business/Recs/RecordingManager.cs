using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.API.Data.LicenseData;
using UMPG.USL.API.Data.Recs;
using UMPG.USL.Models;
using UMPG.USL.Models.LicenseModel;
using UMPG.USL.Models.Recs;


namespace UMPG.USL.API.Business.Recs
{
    public class RecordingManager : IRecordingManager
    {

        private readonly IRecordingRepository _recordingRepository;

        public RecordingManager(IRecordingRepository recordingRepository)
        {
            _recordingRepository = recordingRepository;
        }

        public Recording Get(int id)
        {
            return _recordingRepository.Get(id);
        }

        public List<Recording> GetAll()
        {
            return _recordingRepository.GetAll();
        }

        //public Recording Add(Recording recording)
        //{
        //    return _recordingRepository.Add(recording);
        //}

        public List<Recording> Search(string query)
        {
            return _recordingRepository.Search(query);
            
        }

    }
}
