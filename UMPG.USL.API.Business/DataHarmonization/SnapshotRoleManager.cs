using UMPG.USL.API.Data.DataHarmonization;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Business.DataHarmonization
{
    public class SnapshotRoleManager : ISnapshotRoleManager
    {
        private readonly ISnapshotRoleRepository _snapshotRoleRepository;

        public SnapshotRoleManager(ISnapshotRoleRepository snapshotRoleRepository)
        {
            _snapshotRoleRepository = snapshotRoleRepository;
        }

        public Snapshot_Role SaveSnapshotRole(Snapshot_Role snapshotRole)
        {
            return _snapshotRoleRepository.SaveSnapshotRole(snapshotRole);
        }

        public Snapshot_Role GetSnapshotRoleByRoleId(int roleId)
        {
            return _snapshotRoleRepository.GetSnapshotRoleById(roleId);
        }
    }
}