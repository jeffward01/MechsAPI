using System.Collections.Generic;
using UMPG.USL.Models;
using UMPG.USL.Models.Recs;

namespace UMPG.USL.API.Data.Recs
{
    public interface IWriterRepository
    {
        //int Add(Writer writer);

        Writer Get(string ipCode);

        List<Writer> GetAll();

        List<Writer> Search(string query);

        //List<Writer> GetWritersByIds(List<int> ids);

    }
}
