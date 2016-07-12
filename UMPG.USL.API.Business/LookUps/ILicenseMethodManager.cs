using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.Models.LookupModel;

namespace UMPG.USL.API.Business.Lookups
{
    public interface ILicenseMethodManager
    {
        //LU_LicenseMethod Add(LU_LicenseMethod licensemethod);

        LU_LicenseMethod Get(int id);

        List<LU_LicenseMethod> GetAll();

        List<LU_LicenseMethod> Search(string query);


    }
}
