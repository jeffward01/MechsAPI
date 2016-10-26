using System.Collections.Generic;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public interface ISnapshotContactEmailRepository
    {
        List<Snapshot_ContactEmail> GetAllContactEmailsForCloneContactId(int cloneContactId);
        bool DeleteContactEmailBySnapshotContactEmailId(int snapshotAddressId);
    }
}