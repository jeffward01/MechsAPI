using UMPG.USL.API.Data.DataHarmonization;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Business.DataHarmonization
{
    public class SnapshotLicenseManager : ISnapshotLicenseManager
    {
        private readonly ISnapshotLicenseRepository _snapshotLicenseRepository;

        public SnapshotLicenseManager(ISnapshotLicenseRepository snapshotLicenseRepository)
        {
            _snapshotLicenseRepository = snapshotLicenseRepository;
        }

        public Snapshot_License SaveSnapshotLicense(Snapshot_License snapshotLicense)
        {
            return _snapshotLicenseRepository.SaveSnapshotLicense(snapshotLicense);
        }

        public Snapshot_License GetSnapshotLicenseBySnapshotLicenseId(int snapshotLicenseId)
        {
            return _snapshotLicenseRepository.GetLicenseSnapShotById(snapshotLicenseId);
        }
    }
}