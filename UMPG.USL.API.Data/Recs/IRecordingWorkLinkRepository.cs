using System.Collections.Generic;
using UMPG.USL.Models;
using UMPG.USL.Models.Recs;


namespace UMPG.USL.API.Data.Recs
{
    public interface IRecordingWorkLinkRepository
    {
        int GetWritersNo(long trackId);

        IEnumerable<RecsWriter> GetWorkWriters(long trackId);

    }
}
