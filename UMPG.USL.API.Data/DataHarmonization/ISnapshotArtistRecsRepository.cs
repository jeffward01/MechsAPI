using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public interface ISnapshotArtistRecsRepository
    {
        Snapshot_ArtistRecs SaveSnapshotArtistRecs(Snapshot_ArtistRecs artistRecsSnapshot);
        Snapshot_ArtistRecs GetSnapshotArtistRecsByArtistId(int artistId);
        bool DeleteRecsArtistByProductHeaderSnapshotId(int snapshotLicenseProductId);
        bool DeleteRecsArtisByArtistSnapshotId(int artstSnapshotId);
    }
}