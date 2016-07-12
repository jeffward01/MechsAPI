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
    public class LicenseStatusManager : ILicenseStatusManager
    {

        private readonly ILicenseStatusRepository _licenseStatusRepository;

        public LicenseStatusManager(ILicenseStatusRepository licenseStatusRepository)
        {
            _licenseStatusRepository = licenseStatusRepository;
        }

        public LU_LicenseStatus Get(int id)
        {
            return _licenseStatusRepository.Get(id);
        }


        public LU_LicenseStatus Add(LU_LicenseStatus status)
        {
            return _licenseStatusRepository.Add(status);
        }

        public List<LU_LicenseStatus> GetAll()
        {
            return _licenseStatusRepository.GetAll();
        }


    }
}
