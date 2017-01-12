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
        int? GetProductIdFromSnapshotLicenseProductId(int snapshotLicenseProductId);
        Snapshot_LicenseProduct GetLicenseProductSnapShotByLPSnapId(int id);

        List<int> GetLicenseProductIds(int licenseId);
        List<Snapshot_LicenseProduct> GetAllLicenseProductsForLicenseId(int licenseId);
        Snapshot_LicenseProduct SaveMassiveSnapshotLicenseProduct(Snapshot_LicenseProduct licenseProductSnapshot);
        int? GetLicenseProductIdFromSnapshotLicenseProductId(int snapshotLicenseProductId);
    }
}