using System.Collections.Generic;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public interface ISnapshotPhoneRepository
    {
        List<Snapshot_Phone> GetAllPhonesForCloneContactId(int cloneContactId);
        bool DeletePhoneBySnapshotPhoneId(int snapshotPhoneId);
    }
}