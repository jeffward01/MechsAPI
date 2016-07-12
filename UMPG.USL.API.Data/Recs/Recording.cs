using System;
using System.Collections.Generic;
using System.Linq;
using UMPG.USL.Models;
using UMPG.USL.Models.Recs;

namespace UMPG.USL.API.Data.Recs
{
    public class RecordingyRepository : IRecordingRepository
    {
        public Recording Get(int Id)
        {
            throw new NotImplementedException();
        }

        public List<Recording> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<Recording> Search(string query)
        {
            throw new NotImplementedException();
        }

        public List<Recording> GetRecordingsByIds(List<int> ids)
        {
            using (var context = new AuthContext())
            {
                var recordings = context.Recordings
                    .Where(x => ids.Contains((int)x.track_id));
                    return recordings.ToList();
            }
        }
    }
}
