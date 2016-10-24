using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public interface ISnapshotRoleRepository
    {
        Snapshot_Role SaveSnapshotRole(Snapshot_Role snapshotRole);
        Snapshot_Role GetSnapshotRoleById(int snapShotRoleId);
    }
}