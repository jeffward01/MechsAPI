using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public interface ISnapshotAdministratorRepository
    {
        Snapshot_Administrator SaveSnapshotAdministrator(Snapshot_Administrator administratorSnapshot);
        Snapshot_Administrator GetSnapshotAdministratorByAdministratorId(int adminSnapshotId);
        bool DeleteConfigurationSnapshot(int adminSnapshotId);
    }
}