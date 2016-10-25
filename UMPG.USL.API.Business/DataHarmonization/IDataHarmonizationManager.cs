using System.Collections.Generic;
using UMPG.USL.Models.DataHarmonization;
using UMPG.USL.Models.LicenseModel;

namespace UMPG.USL.API.Business.DataHarmonization
{
    public interface IDataHarmonizationManager
    {
        bool TakeLicenseSnapshot(License licenseToBeSnapshotted);
        bool SaveLocalLicenseProductSnapshot(List<LicenseProduct> localLicenseProducts);
        Snapshot_License GetLicenseSnapshot(int licenseId);
    }
}