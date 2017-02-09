using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public interface ISnapshotProductHeaderRepository
    {
        Snapshot_ProductHeader SaveSnapshotProductHeader(Snapshot_ProductHeader snapshotProductHeader);

        Snapshot_ProductHeader GetSnapshotProductHeaderBySnapshotProductHeaderId(int productHeaderId);

        int GetSnapshotProductHeaderBySnapshotLicenseProductId(int snapshotLicenseProductId);

        bool DeleteProductHeaderSnapshotBySnapshotId(int snapshotProductHeaderId);
        Snapshot_ProductHeader GetProductHeaderLite(int productHeaderSnapshotId);

        Snapshot_ProductHeader GetProductHeaderByProductHeaderId(int productHeaderId);
        Snapshot_ProductHeader GetSnapshotProductHeaderByLabelSnapshotId(int labelSnapshotId);
        Snapshot_ProductHeader GetSnapshotProductHeaderByLicenseId(int licenseId);
        void UpdateSnapshotProductHeader(Snapshot_ProductHeader snapshotProductHeader);
    }
}