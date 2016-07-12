using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.API.Data.LicenseData;
using UMPG.USL.API.Data.LookupData;
using UMPG.USL.Models;
using UMPG.USL.Models.LicenseModel;
using UMPG.USL.Models.LookupModel;


namespace UMPG.USL.API.Business.Lookups
{
    public class ScheduleManager : IScheduleManager
    {

        private readonly IScheduleRepository _scheduleRepository;

        public ScheduleManager(IScheduleRepository scheduleRepository)
        {
            _scheduleRepository = scheduleRepository;
        }

        public LU_Schedule Get(int id)
        {
            return _scheduleRepository.Get(id);
        }

        public List<LU_Schedule> GetAll()
        {
            return _scheduleRepository.GetAll();
        }

        //public LU_LicenseMethod Add(LU_LicenseMethod licensemethod)
        //{
        //    return _licensemethodRepository.Add(licensemethod);
        //}

        public List<LU_Schedule> Search(string query)
        {
            return _scheduleRepository.Search(query);
            
        }

    }
}
