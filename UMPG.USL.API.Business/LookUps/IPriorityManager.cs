using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.Models.LookupModel;

namespace UMPG.USL.API.Business.Lookups
{
    public interface IPriorityManager
    {
        //LU_Priority Add(LU_Priority priority);

        LU_Priority Get(int id);

        List<LU_Priority> GetAll();

        List<LU_Priority> Search(string query);


    }
}
