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

        public bool DoesSnapshotExists(int licenseId)
        {
            return _snapshotLicenseRepository.DoesLicenseSnapshotExist(licenseId);
        }


        public Snapshot_License SaveSnapshotLicense(Snapshot_License snapshotLicense)
        {
            return _snapshotLicenseRepository.SaveSnapshotLicense(snapshotLicense);
        }

        public Snapshot_License GetSnapshotLicenseBySnapshotLicenseId(int snapshotLicenseId)
        {
            return _snapshotLicenseRepository.GetLicenseSnapShotById(snapshotLicenseId);
        }

        public bool DeleteLicenseSnapshot(int licenseId)
        {
            return _snapshotLicenseRepository.DeleteSnapshotLicense(licenseId);
        }
    }
}