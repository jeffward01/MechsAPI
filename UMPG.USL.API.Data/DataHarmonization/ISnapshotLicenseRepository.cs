using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public interface ISnapshotLicenseRepository
    {
        Snapshot_License SaveSnapshotLicense(Snapshot_License licenseSnapshot);

        Snapshot_License GetLicenseSnapShotById(int id);

        Snapshot_License GetSnapshotLicenseByCloneLicenseId(int licenseId);
        bool DoesExistAndComplete(int licenseId);

        bool DoesLicenseSnapshotExist(int licenseId);

        bool DeleteSnapshotLicense(Snapshot_License licenseId);
    }
}