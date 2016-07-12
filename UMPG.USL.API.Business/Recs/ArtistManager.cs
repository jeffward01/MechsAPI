using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.API.Data.LicenseData;
using UMPG.USL.API.Data.Recs;
using UMPG.USL.Models;
using UMPG.USL.Models.LicenseModel;
using UMPG.USL.Models.Recs;


namespace UMPG.USL.API.Business.Recs
{
    public class ArtistManager : IArtistManager
    {

        private readonly IArtistRepository _ArtistRepository;

        public ArtistManager(IArtistRepository ArtistRepository)
        {
            _ArtistRepository = ArtistRepository;
        }

        public ArtistSOLR Get(int id)
        {
            return _ArtistRepository.Get(id);
        }

        public List<ArtistSOLR> GetAll()
        {
            return _ArtistRepository.GetAll();
        }

        //public LU_Artist Add(LU_Artist Artist)
        //{
        //    return _ArtistRepository.Add(Artist);
        //}

        public List<ArtistSOLR> Search(string query)
        {
            return _ArtistRepository.Search(query);
            
        }

    }
}
