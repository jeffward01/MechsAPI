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
    public class PaidQuarterManager : IPaidQuarterManager
    {

        private readonly IPaidQuarterRepository _paidQuarterRepository;

        public PaidQuarterManager(IPaidQuarterRepository paidQuarterRepository)
        {
            _paidQuarterRepository = paidQuarterRepository;
        }

        public LU_PaidQuarter Get(int id)
        {
            return _paidQuarterRepository.Get(id);
        }

        public List<LU_PaidQuarter> GetAll()
        {
            return _paidQuarterRepository.GetAll();
        }

        public List<LU_PaidQuarter> GetRolling10years()
        {
            return _paidQuarterRepository.GetRolling10years();
        }

        //public LU_LicenseMethod Add(LU_LicenseMethod licensemethod)
        //{
        //    return _licensemethodRepository.Add(licensemethod);
        //}

        public List<LU_PaidQuarter> Search(string query)
        {
            return _paidQuarterRepository.Search(query);
            
        }

    }
}
