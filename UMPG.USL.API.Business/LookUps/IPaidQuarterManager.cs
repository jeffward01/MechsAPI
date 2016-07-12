using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.Models.LookupModel;

namespace UMPG.USL.API.Business.Lookups
{
    public interface IPaidQuarterManager
    {
        //LU_LicenseMethod Add(LU_LicenseMethod licensemethod);

        LU_PaidQuarter Get(int id);

        List<LU_PaidQuarter> GetAll();

        List<LU_PaidQuarter> Search(string query);

        List<LU_PaidQuarter> GetRolling10years();
    }
}
