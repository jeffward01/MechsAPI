using System;
using System.Collections.Generic;
using System.Linq;
using UMPG.USL.Models;
using UMPG.USL.Models.Recs;

namespace UMPG.USL.API.Data.Recs
{
    public class ArtistRepository : IArtistRepository
    {


        public ArtistSOLR Get(int id)
        {
            using (var context = new AuthContext())
            {
                return context.Artists.FirstOrDefault(c => c.artist_id == id);
            }
        }

        public List<ArtistSOLR> GetAll()
        {
            using (var context = new AuthContext())
            {
                return context.Artists.ToList();
            }
        }

        public List<ArtistSOLR> Search(string query)
        {
            using (var context = new AuthContext())
            {
                var Artists = context.Artists.Where(c => c.name== query).AsQueryable();

                if (!String.IsNullOrEmpty(query))
                {
                    return Artists.Where(c => c.name.ToLower().Contains(query.ToLower())).ToList();
                }
                else
                {
                    return Artists.ToList();
                }
            }
        }

    }
}
