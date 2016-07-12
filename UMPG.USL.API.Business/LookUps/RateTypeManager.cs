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
    public class RateTypeManager : IRateTypeManager
    {

        private readonly IRateTypeRepository _ratetypeRepository;

        public RateTypeManager(IRateTypeRepository ratetypeRepository)
        {
            _ratetypeRepository = ratetypeRepository;
        }

        public LU_RateType Get(int id)
        {
            return _ratetypeRepository.Get(id);
        }

        public List<LU_RateType> GetAll()
        {
            return _ratetypeRepository.GetAll();
        }

        //public LU_LicenseMethod Add(LU_LicenseMethod licensemethod)
        //{
        //    return _licensemethodRepository.Add(licensemethod);
        //}

        public List<LU_RateType> Search(string query)
        {
            return _ratetypeRepository.Search(query);
            
        }

    }
}
