using System.Collections.Generic;
using UMPG.USL.Models;
using UMPG.USL.Models.LookupModel;

namespace UMPG.USL.API.Data.LookupData
{
    public interface ISpecialStatusRepository
    {
        int Add(LU_SpecialStatus ratetype);

        LU_SpecialStatus Get(int Id);

        List<LU_SpecialStatus> GetAll();

        List<LU_SpecialStatus> Search(string query);

    }
}
