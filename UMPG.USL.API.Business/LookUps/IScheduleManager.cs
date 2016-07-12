using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.Models.LookupModel;

namespace UMPG.USL.API.Business.Lookups
{
    public interface IScheduleManager
    {
        //LU_LicenseMethod Add(LU_LicenseMethod licensemethod);

        LU_Schedule Get(int id);

        List<LU_Schedule> GetAll();

        List<LU_Schedule> Search(string query);


    }
}
