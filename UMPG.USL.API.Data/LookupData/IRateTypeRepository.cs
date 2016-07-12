using System.Collections.Generic;
using UMPG.USL.Models;
using UMPG.USL.Models.LookupModel;

namespace UMPG.USL.API.Data.LookupData
{
    public interface IRateTypeRepository
    {
        int Add(LU_RateType ratetype);

        LU_RateType Get(int Id);

        List<LU_RateType> GetAll();

        List<LU_RateType> Search(string query);

    }
}
