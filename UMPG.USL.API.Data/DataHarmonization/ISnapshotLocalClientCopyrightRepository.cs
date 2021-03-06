﻿using System.Collections.Generic;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public interface ISnapshotLocalClientCopyrightRepository
    {
        List<Snapshot_LocalClientCopyright> GetAllLocalCopyrightsForTrackId(int trackId);
        bool DeleteLocalClientCopyrightBySnapshotId(int localClientCopyrightSnapshotId);
        Snapshot_LocalClientCopyright SaveLocalClientCopyright(Snapshot_LocalClientCopyright snapshotLabel);
        List<Snapshot_LocalClientCopyright> GetAllLocalCopyrightsForRecsCopyrightSnapshotId(int recsCopyrightSnapshotId);
    }
}