using System.Collections.Generic;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public interface ISnapshotKnownAsRepository
    {
        List<Snapshot_KnownAs> GetAllKnownAsForWriterCaeCode(int caeCode);
        bool DeleteKnownAsBySnapshotId(int snapshotId);
        Snapshot_KnownAs SaveKnownAs(Snapshot_KnownAs knownAs);
    }
}