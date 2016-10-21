using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public interface ISnapshotLicenseProductRepository
    {
        Snapshot_LicenseProduct SaveSnapshotLicenseProduct(Snapshot_LicenseProduct licenseProductSnapshot);
        Snapshot_LicenseProduct GetLicenseProductSnapShotById(int id);
    }
}