using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.Models.Recs;

namespace UMPG.USL.API.Business.Recs
{
    public interface IArtistManager
    {
        //Artist Add(LU_Priority priority);

        ArtistSOLR Get(int id);

        List<ArtistSOLR> GetAll();

        List<ArtistSOLR> Search(string query);


    }
}
