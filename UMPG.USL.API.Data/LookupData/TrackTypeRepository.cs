using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.Models.LookupModel;

namespace UMPG.USL.API.Data.LookupData
{
    public class TrackTypeRepository:ITrackTypeRepository
    {
        public List<LU_TrackType> GetAll()
        {
            using (var context = new AuthContext())
            {
                return context.Lu_TrackTypes.ToList();
            }
        }
    }
}
