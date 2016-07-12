using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.API.Data.LookupData;
using UMPG.USL.Models.LookupModel;

namespace UMPG.USL.API.Data.LookupData
{
    public class LicenseStatusRepository : ILicenseStatusRepository
    {
        public List<LU_LicenseStatus> GetAll()
        {
            using (var context = new AuthContext())
            {
                return context.LU_LicenseStatuses.ToList();
            }
        }

        public LU_LicenseStatus Add(LU_LicenseStatus licenseStatus)
        {
            using (var context = new AuthContext())
            {
                return  context.LU_LicenseStatuses.Add(licenseStatus);
            }
        }

        public LU_LicenseStatus Get(int id)
        {
            using (var context = new AuthContext())
            {
                return context.LU_LicenseStatuses.Where(ls => ls.LicenseStatusId == id).FirstOrDefault();
            }
        }

    }
}
