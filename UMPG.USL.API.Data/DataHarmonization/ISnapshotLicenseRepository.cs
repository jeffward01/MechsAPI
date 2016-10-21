using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public interface ISnapshotLicenseRepository
    {
        Snapshot_License SaveSnapshotLicense(Snapshot_License licenseSnapshot);
        Snapshot_License GetLicenseSnapShotById(int id);
    }
}