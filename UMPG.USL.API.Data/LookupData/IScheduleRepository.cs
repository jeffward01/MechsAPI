using System.Collections.Generic;
using UMPG.USL.Models;
using UMPG.USL.Models.LookupModel;

namespace UMPG.USL.API.Data.LookupData
{
    public interface IScheduleRepository
    {
        int Add(LU_Schedule schedule);

        LU_Schedule Get(int Id);

        List<LU_Schedule> GetAll();

        List<LU_Schedule> Search(string query);

    }
}
