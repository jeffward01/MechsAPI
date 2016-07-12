using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.Models.LookupModel;

namespace UMPG.USL.API.Business.Lookups
{
    public interface IWritersConsentTypeManager
    {
        //LU_LicenseMethod Add(LU_LicenseMethod licensemethod);

        LU_WritersConsentType Get(int id);

        List<LU_WritersConsentType> GetAll();

        List<LU_WritersConsentType> Search(string query);

        List<LU_WritersConsentType> GetWritersConsentForLookup();

        List<LU_PaidQuarterType> GetPaidQuarterForLookup();

        List<LU_WritersIncludeExcludeType> GetWritersIncludeExcludeForLookup();
    }
}
