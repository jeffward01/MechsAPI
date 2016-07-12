using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.Models.Recs;

namespace UMPG.USL.API.Business.Recs
{
    public interface IRecordingManager
    {
        //Recording Add(Recording recording);

        Recording Get(int id);

        List<Recording> GetAll();

        List<Recording> Search(string query);


    }
}
