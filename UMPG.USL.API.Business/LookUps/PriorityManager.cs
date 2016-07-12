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
    public class PriorityManager : IPriorityManager
    {

        private readonly IPriorityRepository _priorityRepository;

        public PriorityManager(IPriorityRepository priorityRepository)
        {
            _priorityRepository = priorityRepository;
        }

        public LU_Priority Get(int id)
        {
            return _priorityRepository.Get(id);
        }

        public List<LU_Priority> GetAll()
        {
            return _priorityRepository.GetAll();
        }

        //public LU_Priority Add(LU_Priority priority)
        //{
        //    return _priorityRepository.Add(priority);
        //}

        public List<LU_Priority> Search(string query)
        {
            return _priorityRepository.Search(query);
            
        }

    }
}
