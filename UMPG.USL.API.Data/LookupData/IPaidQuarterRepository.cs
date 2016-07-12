using System.Collections.Generic;
using UMPG.USL.Models;
using UMPG.USL.Models.LookupModel;

namespace UMPG.USL.API.Data.LookupData
{
    public interface IPaidQuarterRepository
    {
        int Add(LU_PaidQuarter ratetype);

        LU_PaidQuarter Get(int Id);

        List<LU_PaidQuarter> GetAll();

        List<LU_PaidQuarter> Search(string query);

        List<LU_PaidQuarter> GetRolling10years();

    }
}
