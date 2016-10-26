using System.Collections.Generic;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public interface ISnapshotAddressRepository
    {
        List<Snapshot_Address> GetAllAddressesForCloneContactId(int cloneContactId);
        bool DeleteAddressBySnapshotAddressId(int snapshotAddressId);
    }
}