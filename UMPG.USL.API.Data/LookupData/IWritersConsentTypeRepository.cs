using System.Collections.Generic;
using UMPG.USL.Models;
using UMPG.USL.Models.LookupModel;

namespace UMPG.USL.API.Data.LookupData
{
    public interface IWritersConsentTypeRepository
    {
        int Add(LU_WritersConsentType ratetype);

        LU_WritersConsentType Get(int Id);

        List<LU_WritersConsentType> GetAll();

        List<LU_WritersConsentType> Search(string query);

        List<LU_WritersConsentType> GetWritersConsentForLookup();

        List<LU_PaidQuarterType> GetPaidQuarterForLookup();

        List<LU_WritersIncludeExcludeType> GetWritersIncludeExcludeForLookup();

    }
}
