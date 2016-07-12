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
    public class LicenseMethodManager : ILicenseMethodManager
    {

        private readonly ILicenseMethodRepository _licensemethodRepository;

        public LicenseMethodManager(ILicenseMethodRepository licensemethodRepository)
        {
            _licensemethodRepository = licensemethodRepository;
        }

        public LU_LicenseMethod Get(int id)
        {
            return _licensemethodRepository.Get(id);
        }

        public List<LU_LicenseMethod> GetAll()
        {
            return _licensemethodRepository.GetAll();
        }

        //public LU_LicenseMethod Add(LU_LicenseMethod licensemethod)
        //{
        //    return _licensemethodRepository.Add(licensemethod);
        //}

        public List<LU_LicenseMethod> Search(string query)
        {
            return _licensemethodRepository.Search(query);
            
        }

    }
}
