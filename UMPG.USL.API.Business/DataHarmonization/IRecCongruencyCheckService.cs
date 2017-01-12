using System.Collections.Generic;
using UMPG.USL.Models.DataHarmonization;
using UMPG.USL.Models.LicenseModel;

namespace UMPG.USL.API.Business.DataHarmonization
{
    public interface IRecCongruencyCheckService
    {


        List<RecsProductChanges> CheckSnapshotAgainstRecs(List<LicenseProduct> mechsLicenseProducts,
            List<Snapshot_LicenseProduct> licenseProductSnapshots);
    }
}