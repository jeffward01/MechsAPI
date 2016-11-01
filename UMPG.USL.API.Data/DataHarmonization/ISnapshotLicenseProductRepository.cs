using System.Collections.Generic;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public interface ISnapshotLicenseProductRepository
    {
        Snapshot_LicenseProduct SaveSnapshotLicenseProduct(Snapshot_LicenseProduct licenseProductSnapshot);

        Snapshot_LicenseProduct GetLicenseProductSnapShotById(int id);

        bool DeleteLicenseProductSnapshot(int snapshotLicenseProductId);
        int? GetProductIdFromSnapshotLicenseProductId(int snapshotLicenseProductId);

        List<int> GetLicenseProductIds(int licenseId);
        List<Snapshot_LicenseProduct> GetAllLicenseProductsForLicenseId(int licenseId);
        int? GetLicenseProductIdFromSnapshotLicenseProductId(int snapshotLicenseProductId);
    }
}