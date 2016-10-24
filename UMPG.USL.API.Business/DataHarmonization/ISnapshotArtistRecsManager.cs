using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Business.DataHarmonization
{
    public interface ISnapshotArtistRecsManager
    {
        Snapshot_ArtistRecs SaveSnapshotArtistRecs(Snapshot_ArtistRecs snapshotArtistRecs);
        Snapshot_ArtistRecs GetSnapshotArtistRecsByArtistRecsId(int snapshotArtistRecsId);
    }
}