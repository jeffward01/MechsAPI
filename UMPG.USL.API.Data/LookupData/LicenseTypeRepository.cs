using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.Models.LookupModel;

namespace UMPG.USL.API.Data.LookupData
{
    public class LicenseTypeRepository: ILicenseTypeRepository
    {
        public List<LU_LicenseType> GetAll()
        {

            using (var context = new AuthContext())
            {
                return context.LU_LicenseTypes.ToList();
            }
        }
    }
}
