using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Business.DataHarmonization
{
    public interface ISnapshotLicenseManager
    {
        Snapshot_License SaveSnapshotLicense(Snapshot_License snapshotLicense);

        Snapshot_License GetSnapshotLicenseBySnapshotLicenseId(int snapshotLicenseId);

        bool DoesSnapshotExists(int licenseId);

        bool DeleteLicenseSnapshot(int licenseId);
    }
}