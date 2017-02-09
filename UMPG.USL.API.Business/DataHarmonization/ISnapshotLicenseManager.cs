using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Business.DataHarmonization
{
    public interface ISnapshotLicenseManager
    {
        Snapshot_License SaveSnapshotLicense(Snapshot_License snapshotLicense);

        Snapshot_License GetSnapshotLicenseBySnapshotLicenseId(int snapshotLicenseId);

        bool DoesSnapshotExists(int licenseId);

        bool DoesSnapshotExistAndComplete(int licenseId);
        void DeleteLicenseProductAndChildEntities(Snapshot_License license, int productId);

        bool DeleteLicenseSnapshotAndAllChildren(int licenseId);

        void DeleteRecsConfigAndChildrenForProductHeader(Snapshot_ProductHeader productHeader,
            int cloneRecsConfigurationId);

        //bool DeleteLicenseSnapshot(int licenseId);
    }
}