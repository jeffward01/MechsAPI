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



        public IList<GenerateLicenseQueue> GetAllUnProcessLicenses()
        {
            using (var context = new AuthContext())
            {
                return context.GenerateLicenseQueue.Where(_ => _.GenerateLicenseStatusId == 2 || _.GenerateLicenseStatusId == 1).ToList();
            }
        }
        public void Delete(GenerateLicenseQueue indexQueueItem)
        {
            using (var context = new AuthContext())
            {
                context.GenerateLicenseQueue.Attach(indexQueueItem);
                context.GenerateLicenseQueue.Remove(indexQueueItem);
                context.SaveChanges();
            }
        }
        public IList<GenerateLicenseQueue> GetAllFailed()
        {
            using (var context = new AuthContext())
            {
                return context.GenerateLicenseQueue.Where(_ => _.GenerateLicenseStatusId == 4).ToList();
            }
        }

        public IList<GenerateLicenseQueue> GetAllInProcessLicenses()
        {
            using (var context = new AuthContext())
            {
                return context.GenerateLicenseQueue.Where(_ => _.GenerateLicenseStatusId == 2).ToList();
            }
        }

        public GenerateLicenseQueue GetGenerateLicenseQueueById(int id)
        {
            using (var context = new AuthContext())
            {
                return context.GenerateLicenseQueue.FirstOrDefault(_ => _.GenerateLicenseQueueId == id);
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