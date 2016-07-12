using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.API.Data.LookupData;
using UMPG.USL.Models.LookupModel;

namespace UMPG.USL.API.Business.LookUps
{
    public class LicenseTypeManager: ILicenseTypeManager
    {
        private readonly ILicenseTypeRepository _licenseTypeRepository;

        public LicenseTypeManager(ILicenseTypeRepository licenseTypeRepository)
        {
            _licenseTypeRepository = licenseTypeRepository;
        }
        public List<LU_LicenseType> GetAll()
        {
            return _licenseTypeRepository.GetAll();
        }
    }
}
