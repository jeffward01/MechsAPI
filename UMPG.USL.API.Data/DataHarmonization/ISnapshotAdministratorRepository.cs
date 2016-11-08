using System.Collections.Generic;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public interface ISnapshotAdministratorRepository
    {
        Snapshot_Administrator SaveSnapshotAdministrator(Snapshot_Administrator administratorSnapshot);
        Snapshot_Administrator GetSnapshotAdministratorByAdministratorId(int adminSnapshotId);
        List<Snapshot_Administrator> GetAllAdministratorsForOriginalPublisherId(int id);
        bool DeleteConfigurationSnapshot(int adminSnapshotId);
    }
}