using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.Models.Recs;

namespace UMPG.USL.API.Business.Recs
{
    public interface IWriterManager
    {
        //Writer Add(Writer writer);

        Writer Get(string id);

        List<Writer> GetAll();

        List<Writer> Search(string query);


    }
}
