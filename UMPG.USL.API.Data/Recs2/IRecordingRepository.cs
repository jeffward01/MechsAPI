using System.Collections.Generic;
using UMPG.USL.Models;
using UMPG.USL.Models.Recs;

namespace UMPG.USL.API.Data.Recs
{
    public interface IRecordingRepository
    {
        //int Add(Recording recording);

        Recording Get(int Id);

        List<Recording> GetAll();

        List<Recording> Search(string query);

        List<Recording> GetRecordingsByIds(List<int> ids);

    }
}
