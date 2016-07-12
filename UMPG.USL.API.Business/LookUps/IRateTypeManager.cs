using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.Models.LookupModel;

namespace UMPG.USL.API.Business.Lookups
{
    public interface IRateTypeManager
    {
        //LU_LicenseMethod Add(LU_LicenseMethod licensemethod);

        LU_RateType Get(int id);

        List<LU_RateType> GetAll();

        List<LU_RateType> Search(string query);


    }
}
