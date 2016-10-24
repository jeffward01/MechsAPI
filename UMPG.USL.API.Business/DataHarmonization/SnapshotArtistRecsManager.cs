using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.API.Data.DataHarmonization;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Business.DataHarmonization
{
    public class SnapshotArtistRecsManager : ISnapshotArtistRecsManager
    {
        private readonly ISnapshotArtistRecsRepository _snapshotArtistRecsRepository;

        public SnapshotArtistRecsManager(ISnapshotArtistRecsRepository snapshotArtistRecsRepository)
        {
            _snapshotArtistRecsRepository = snapshotArtistRecsRepository;
        }

        public Snapshot_ArtistRecs SaveSnapshotArtistRecs(Snapshot_ArtistRecs snapshotArtistRecs)
        {
            return _snapshotArtistRecsRepository.SaveSnapshotArtistRecs(snapshotArtistRecs);
        }
        public Snapshot_ArtistRecs GetSnapshotArtistRecsByArtistRecsId(int snapshotArtistRecsId)
        {
            return _snapshotArtistRecsRepository.GetSnapshotArtistRecsByArtistId(snapshotArtistRecsId);
        }

    }
}
