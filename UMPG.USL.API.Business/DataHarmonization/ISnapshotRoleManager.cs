using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Business.DataHarmonization
{
    public interface ISnapshotRoleManager
    {
        Snapshot_Role SaveSnapshotRole(Snapshot_Role snapshotRole);
        Snapshot_Role GetSnapshotRoleByRoleId(int roleId);
    }
}