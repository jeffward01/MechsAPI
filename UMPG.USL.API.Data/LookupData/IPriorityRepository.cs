using System.Collections.Generic;
using UMPG.USL.Models;
using UMPG.USL.Models.LookupModel;

namespace UMPG.USL.API.Data.LookupData
{
    public interface IPriorityRepository
    {
        int Add(LU_Priority priority);

        LU_Priority Get(int Id);

        List<LU_Priority> GetAll();

        List<LU_Priority> Search(string query);

    }
}
