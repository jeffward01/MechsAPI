using System.Collections.Generic;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public interface ISnapshotLicenseProductRepository
    {
        Snapshot_LicenseProduct SaveSnapshotLicenseProduct(Snapshot_LicenseProduct licenseProductSnapshot);

        Snapshot_LicenseProduct GetLicenseProductSnapShotById(int id);

        bool DeleteLicenseProductSnapshot(int snapshotLicenseProductId);

        List<int> GetLicenseProductIds(int licenseId);
    }
}