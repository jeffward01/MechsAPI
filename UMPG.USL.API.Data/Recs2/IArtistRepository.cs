using System.Collections.Generic;
using UMPG.USL.Models;
using UMPG.USL.Models.Recs;

namespace UMPG.USL.API.Data.Recs
{
    public interface IArtistRepository
    {

        ArtistSOLR Get(int Id);

        List<ArtistSOLR> GetAll();

        List<ArtistSOLR> Search(string query);

    }
}
