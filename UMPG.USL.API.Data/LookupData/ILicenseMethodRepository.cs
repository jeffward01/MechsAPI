using System.Collections.Generic;
using UMPG.USL.Models;
using UMPG.USL.Models.LookupModel;

namespace UMPG.USL.API.Data.LookupData
{
    public interface ILicenseMethodRepository
    {
        int Add(LU_LicenseMethod licensemethod);

        LU_LicenseMethod Get(int Id);

        List<LU_LicenseMethod> GetAll();

        List<LU_LicenseMethod> Search(string query);

    }
}
