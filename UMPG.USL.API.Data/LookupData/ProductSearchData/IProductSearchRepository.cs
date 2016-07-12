using System;
using System.Collections.Generic;
using UMPG.USL.Models;
using UMPG.USL.Models.Recs;

namespace UMPG.USL.API.Data.Recs
{
    public interface IProductSearchRepository
    {
  
        Label Get(int Id);

        List<Label> GetAll();

        List<Label> Search(string query);

    }
}
