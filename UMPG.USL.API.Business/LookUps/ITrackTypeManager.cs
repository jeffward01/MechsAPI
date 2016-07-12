using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.Models.LookupModel;

namespace UMPG.USL.API.Business.LookUps
{
    public interface ITrackTypeManager
    {
        List<LU_TrackType> GetAll();
    }
}
