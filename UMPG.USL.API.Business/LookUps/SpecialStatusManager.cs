using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.API.Data.LookupData;
using UMPG.USL.Models;
using UMPG.USL.Models.LookupModel;


namespace UMPG.USL.API.Business.Lookups
{
    public class SpecialStatusManager : ISpecialStatusManager
    {

        private readonly ISpecialStatusRepository _specialstatusRepository;

        public SpecialStatusManager(ISpecialStatusRepository specialstatusRepository)
        {
            _specialstatusRepository = specialstatusRepository;
        }

        public LU_SpecialStatus Get(int id)
        {
            return _specialstatusRepository.Get(id);
        }

        public List<LU_SpecialStatus> GetAll()
        {
            return _specialstatusRepository.GetAll();
        }

        //public LU_SpecialStatus Add(LU_SpecialStatus specialstatus)
        //{
        //    return _specialstatusRepository.Add(specialstatus);
        //}

        public List<LU_SpecialStatus> Search(string query)
        {
            return _specialstatusRepository.Search(query);
            
        }

    }
}
