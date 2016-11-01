using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public interface ISnapshotLicenseeLabelGroupRepository
    {
        Snapshot_LicenseeLabelGroup SaveLicenseeLabelGroup(
            Snapshot_LicenseeLabelGroup snapshotRecsConfiguration);

        Snapshot_LicenseeLabelGroup GetSnapshotLicenseeLabelGroupBySnapshotIdGroup(int snapshotLicenseeLabelGroupId);
        Snapshot_LicenseeLabelGroup GetLicenseeLabelGroupByCloneLicenseeLabelGroupId(int cloneLicenseeLabelGroupId);
        bool DeleteSnapshotLicenseeLabelGroupBySnapshotId(int snapshotLicenseeLabelGroupId);
    }
}