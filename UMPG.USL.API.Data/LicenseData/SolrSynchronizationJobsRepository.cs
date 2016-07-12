using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.Models;

namespace UMPG.USL.API.Data.LicenseData
{
    public class SolrSynchronizationJobsRepository : ISolrSynchronizationJobs
    {
        public SolrSynchronizationJob Add(SolrSynchronizationJob job)
        {
            using (var context = new AuthContext())
            {
                context.SolrSynchronizationJobs.Add(job);
                context.SaveChanges();
                return job;
            }
        }

        public SolrSynchronizationJob Update(SolrSynchronizationJob job)
        {
            using (var context = new AuthContext())
            {
                job.ModifiedBy = 1;
                job.ModifiedDate = DateTime.Now;
                context.Entry(job).State = (EntityState)System.Data.EntityState.Modified;
                context.SaveChanges();
                return job;
            }
        }


        public void Delete(SolrSynchronizationJob job)
        {
            using (var context = new AuthContext())
            {
                context.SolrSynchronizationJobs.Attach(job);
                context.SolrSynchronizationJobs.Remove(job);
                context.SaveChanges();
            }
        }

        public SolrSynchronizationJob GetItemFromQueue()
        {
            using (var context = new AuthContext())
            {
                var job =
                    context.SolrSynchronizationJobs.Where(i => i.Status == (int) SolrIndexQueueState.Pending)
                        .OrderBy(i => i.JobId)
                        .FirstOrDefault();
                return job;
            }
        }
    }
}
