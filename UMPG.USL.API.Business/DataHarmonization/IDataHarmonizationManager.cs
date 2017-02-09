using System.Collections.Generic;
using UMPG.USL.Models.DataHarmonization;
using UMPG.USL.Models.LicenseModel;

namespace UMPG.USL.API.Business.DataHarmonization
{
    public interface IDataHarmonizationManager
    {
        bool TakeLicenseSnapshot(License licenseToBeSnapshotted, List<LicenseProduct> licenseProducts);

        Snapshot_License GetLicenseSnapshot(int licenseId);
        Snapshot_LicenseProduct TakeLicenseProductSnapshotLite(LicenseProduct licenseProductToBeSnapshotted);
        bool TakeLicenseSnapshotLite(License licenseToBeSnapshotted, bool snapshotComplete);
        Snapshot_License GetLicenseSnapshotFull(int licenseId);
        
        bool DoesSnapshotExistAndComplete(int licenseId);

        bool DoesSnapshotExist(int licenseId);
        bool IsSnapshotInProcess(int licenseId);
        bool RemoveLicenseProductFromSnapshot(int licenseId, int productId);

        bool DeleteLicenseSnapshot(int licenseSnapshotId);
        Snapshot_ProductHeader GetSnapshotProductHeaderForLicenseId(int licenseId);
    }
}