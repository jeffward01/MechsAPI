using System.Collections.Generic;
using UMPG.USL.Models.DataHarmonization;
using UMPG.USL.Models.LicenseModel;

namespace UMPG.USL.API.Business.DataHarmonization
{
    public interface IRecCongruencyCheckService
    {
        List<RecsProductChanges> CheckForLicenseProductChanges(List<LicenseProduct> recsLicenseProducts,
            List<Snapshot_LicenseProduct> licenseProductsSnapshots);
    }
}