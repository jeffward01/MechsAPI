using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Business.DataHarmonization
{
    public interface ISnapshotLicenseeLabelGroupManager
    {
        Snapshot_LicenseeLabelGroup SaveLicenseeLabelGroup(
            Snapshot_LicenseeLabelGroup snapshotLicenseeLabelGroup);

        Snapshot_LicenseeLabelGroup GetSnapshotLicenseeLabelGroupBySnapshotIdGroup(int snapshotLicenseeLabelGroupId);
        Snapshot_LicenseeLabelGroup GetLicenseeLabelGroupByCloneLicenseeLabelGroupId(int cloneLicenseeLabelGroupId);
        bool DeleteSnapshotLicenseeLabelGroupBySnapshotId(int snapshotLicenseeLabelGroupId);
    }
}