using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Business.DataHarmonization
{
    public interface ISnapshotLicenseProductManager
    {
        Snapshot_LicenseProduct SaveSnapshotLicenseProduct(Snapshot_LicenseProduct snapshotLicenseProduct);
        Snapshot_ProductHeader GetProductForTrackId(int snapshotTrackId);
        Snapshot_LicenseProduct GetSnapshotLicenseProductByLicenseProductId(int snapshotLicenseProductId);
        int GetProductHeaderIdForSnapshotLicenseProductId(int id);
    }
}