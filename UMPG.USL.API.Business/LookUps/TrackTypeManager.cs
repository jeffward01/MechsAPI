using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.API.Data.LookupData;
using UMPG.USL.Models.LookupModel;

namespace UMPG.USL.API.Business.LookUps
{
    public class TrackTypeManager:ITrackTypeManager
    {
        private readonly ITrackTypeRepository _trackTypeRepository;
        public TrackTypeManager(ITrackTypeRepository trackTypeRepository)
        {
            _trackTypeRepository = trackTypeRepository;
        }
        public List<LU_TrackType> GetAll()
        {
            return _trackTypeRepository.GetAll();
        }
    }
}
