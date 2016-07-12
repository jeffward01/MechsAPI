using System.Collections.Generic;
using System.Data;
using System.Linq;
using UMPG.USL.Models;
using UMPG.USL.Models.LicenseGenerate;
using UMPG.USL.Models.Enums;
//using UMPG.USL.Models.Enums;
//using UMPG.USL.Models.LicenseGenerate;
using EntityState = System.Data.Entity.EntityState;

namespace UMPG.USL.API.Data.LicenseData
{


    public class GenerateLicenseQueueRepository : IGenerateLicenseQueueRepository
    {
        public List<GenerateLicenseQueue> GetByLicenseId(int licenseId)
        {
            using (var context = new AuthContext())
            {
                var licenses =
                    context.GenerateLicenseQueue
                        .Where(x => x.LicenseId == licenseId && x.GenerateLicenseStatusId == (int)GenerateLicenseStatus.Completed && x.UserAction == null)
                        .OrderByDescending(x => x.GenerateLicenseQueueId)
                        .ToList();
                
                return licenses;
            }
        }


        public void Update(GenerateLicenseQueue generateLicenseQueue)
        {

            using (var context = new AuthContext())
            {
                context.Entry(generateLicenseQueue).State = (EntityState)System.Data.EntityState.Modified;
                context.SaveChanges();
            }

        }
    }
}