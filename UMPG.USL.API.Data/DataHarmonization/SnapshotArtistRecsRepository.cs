using System;
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

        public bool DeleteRecsArtistByProductHeaderSnapshotId(int snapshotLicenseProductId)
        {
            using (var context = new AuthContext())
            {
                var productHeader =
                    context.Snapshot_ProductHeaders
                    .Include("Artist")
                    .FirstOrDefault(_ => _.SnapshotProductHeaderId == snapshotLicenseProductId);

                context.Snapshot_ArtistRecs.Attach(productHeader.Artist);
                context.Snapshot_ArtistRecs.Remove(productHeader.Artist);
                try
                {
                    context.SaveChanges();
                }
                catch (Exception)
                {
                    return false;
                }
                return true;
            }
        }
    }
}