using UMPG.USL.API.Data.DataHarmonization;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Business.DataHarmonization
{
    public class SnapshotLicenseeLabelGroupManager : ISnapshotLicenseeLabelGroupManager
    {
        private readonly ISnapshotLicenseeLabelGroupRepository _licenseeLabelGroupRepository;

        public SnapshotLicenseeLabelGroupManager(ISnapshotLicenseeLabelGroupRepository licenseeLabelGroupRepository)
        {
            _licenseeLabelGroupRepository = licenseeLabelGroupRepository;
        }

        public Snapshot_LicenseeLabelGroup SaveLicenseeLabelGroup(
     Snapshot_LicenseeLabelGroup snapshotLicenseeLabelGroup)
        {
            return _licenseeLabelGroupRepository.SaveLicenseeLabelGroup(snapshotLicenseeLabelGroup);
        }

        public Snapshot_LicenseeLabelGroup GetSnapshotLicenseeLabelGroupBySnapshotIdGroup(int snapshotLicenseeLabelGroupId)
        {
            return _licenseeLabelGroupRepository.GetSnapshotLicenseeLabelGroupBySnapshotIdGroup(snapshotLicenseeLabelGroupId);
        }

        public Snapshot_LicenseeLabelGroup GetLicenseeLabelGroupByCloneLicenseeLabelGroupId(int cloneLicenseeLabelGroupId)
        {
            return _licenseeLabelGroupRepository.GetLicenseeLabelGroupByCloneLicenseeLabelGroupId(cloneLicenseeLabelGroupId);
        }

        public bool DeleteSnapshotLicenseeLabelGroupBySnapshotId(int snapshotLicenseeLabelGroupId)
        {
            return
                _licenseeLabelGroupRepository.DeleteSnapshotLicenseeLabelGroupBySnapshotId(snapshotLicenseeLabelGroupId);
        }
    }
}
