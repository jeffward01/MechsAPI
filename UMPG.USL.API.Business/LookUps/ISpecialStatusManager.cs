using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.Models.LookupModel;

namespace UMPG.USL.API.Business.Lookups
{
    public interface ISpecialStatusManager
    {
        //LU_SpecialStatus Add(LU_SpecialStatus specialstatus);

        LU_SpecialStatus Get(int id);

        List<LU_SpecialStatus> GetAll();

        List<LU_SpecialStatus> Search(string query);


    }
}
