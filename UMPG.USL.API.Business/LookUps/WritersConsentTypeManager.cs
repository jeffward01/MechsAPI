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
    public class WritersConsentTypeManager : IWritersConsentTypeManager
    {

        private readonly IWritersConsentTypeRepository _writersConsentTypeRepository;

        public WritersConsentTypeManager(IWritersConsentTypeRepository writersConsentTypeRepository)
        {
            _writersConsentTypeRepository = writersConsentTypeRepository;
        }

        public LU_WritersConsentType Get(int id)
        {
            return _writersConsentTypeRepository.Get(id);
        }

        public List<LU_WritersConsentType> GetAll()
        {
            return _writersConsentTypeRepository.GetAll();
        }

        public List<LU_WritersConsentType> GetWritersConsentForLookup()
        {
            return _writersConsentTypeRepository.GetWritersConsentForLookup();
        }

        public List<LU_PaidQuarterType> GetPaidQuarterForLookup()
        {
            return _writersConsentTypeRepository.GetPaidQuarterForLookup();
        }

        public List<LU_WritersIncludeExcludeType> GetWritersIncludeExcludeForLookup()
        {
            return _writersConsentTypeRepository.GetWritersIncludeExcludeForLookup();
        }

        

        //public LU_LicenseMethod Add(LU_LicenseMethod licensemethod)
        //{
        //    return _licensemethodRepository.Add(licensemethod);
        //}

        public List<LU_WritersConsentType> Search(string query)
        {
            return _writersConsentTypeRepository.Search(query);
            
        }

    }
}
