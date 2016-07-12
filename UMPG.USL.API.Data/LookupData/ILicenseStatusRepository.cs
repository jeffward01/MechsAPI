using System.Collections.Generic;
using UMPG.USL.Models;
using UMPG.USL.Models.LookupModel;

namespace UMPG.USL.API.Data.LookupData
{
    public interface ILicenseStatusRepository
    {
        LU_LicenseStatus Add(LU_LicenseStatus licensemethod);

        LU_LicenseStatus Get(int Id);

        List<LU_LicenseStatus> GetAll();


    }
}
