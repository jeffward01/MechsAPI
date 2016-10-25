using System.Linq;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public class SnapshotArtistRecsRepository : ISnapshotArtistRecsRepository
    {
        public Snapshot_ArtistRecs SaveSnapshotArtistRecs(Snapshot_ArtistRecs artistRecsSnapshot)
        {
            using (var context = new AuthContext())
            {
                context.Snapshot_ArtistRecs.Add(artistRecsSnapshot);
                context.SaveChanges();
                return artistRecsSnapshot;
            }
        }

        public Snapshot_ArtistRecs GetSnapshotArtistRecsByArtistId(int artistId)
        {
            using (var context = new AuthContext())
            {
                return context.Snapshot_ArtistRecs.FirstOrDefault(sl => sl.SnapshotArtistRecsId == artistId);
            }
        }
    }
}