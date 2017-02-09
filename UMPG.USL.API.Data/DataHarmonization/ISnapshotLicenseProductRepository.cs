using System.Collections.Generic;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public interface ISnapshotLicenseProductRepository
    {
        Snapshot_LicenseProduct SaveSnapshotLicenseProduct(Snapshot_LicenseProduct licenseProductSnapshot);

        Snapshot_LicenseProduct GetLicenseProductSnapShotById(int id);
        Snapshot_LicenseProduct GetLicenseProductByLicenseProductId(int id);

        bool DeleteLicenseProductSnapshot(int snapshotLicenseProductId);
        bool DoesLicenseProductSnapshotExist(int licenseProductId);
        Snapshot_LicenseProduct GetSnapshotLicenseProductBySnapshotProductHeaderId(int snapshotProductHeaderID);
        int? GetProductIdFromSnapshotLicenseProductId(int snapshotLicenseProductId);
        Snapshot_LicenseProduct GetLicenseProductSnapShotByLPSnapId(int id);
        bool DoesProductHeaderSnapshotExist(int licenseProductId);
        int GetSnapshotProductHeaderId(int licenseProductId);
        bool DoesProductHeaderSnapshotExistById(int cloneProductHeaderId);
        List<int> GetLicenseProductIds(int licenseId);
        List<Snapshot_LicenseProduct> GetAllLicenseProductsForLicenseId(int licenseId);
        Snapshot_LicenseProduct SaveMassiveSnapshotLicenseProduct(Snapshot_LicenseProduct licenseProductSnapshot);
        Snapshot_LicenseProduct UpdateSnapshotLicenseProduct(Snapshot_LicenseProduct snapshotLicenseProduct);
        int? GetLicenseProductIdFromSnapshotLicenseProductId(int snapshotLicenseProductId);
        int GetSnapshotCloneProductHeaderId(int snapshotProductHeaderId);
        Snapshot_LicenseProduct GetSnapshotLicenseProductByLicenseProductId(int licenseProductId);
    }
}