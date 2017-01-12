using System.Collections.Generic;
using UMPG.USL.Models.DataHarmonization;
using UMPG.USL.Models.LicenseModel;

namespace UMPG.USL.API.Business.DataHarmonization
{
    public interface ISnapshotManager
    {
        bool DoesLicenseSnapshotExist(int licenseId);
        Snapshot_License GeLicenseSnapshotByLicenseId(int licenseId);
        bool TakeLicenseSnapshot(License licenseToBeSnapshotted, List<LicenseProduct> licenseProducts);
        bool DoesLicenseSnapshotExistAndComplete(int licenseId);
        bool DeleteLicenseSnapshot(int licenseSnapshotId);

         
        
        
        
    }
}