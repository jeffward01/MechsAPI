using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMPG.USL.API.Data
{
    public class ProcessorRepository : IProcessorRepository
    {
        //Get unprocessed item count for LicenseProcessor
        public int GetUnProcessedLicenseProcessorCount()
        {
            using (var context = new AuthContext())
            {
                return context.GenerateLicenseQueue.Count(_ => _.GenerateLicenseStatusId == 1 || _.GenerateLicenseStatusId == 2);
            }
        }

        //Get unprocessed item count for DataHarmonizationProcessor
        public int GetUnProcessedDataHarmonizationCount()
        {
            using (var context = new AuthContext())
            {
                return context.DataHarmonizationQueues.Count(_ => _.DataProcessorStatusId == 1 || _.DataProcessorStatusId == 2);
            }
        }

        public int GetUnProcessedDataHarmonizationErrorCount()
        {
            using (var context = new AuthContext())
            {
                return context.DataHarmonizationQueues.Count(_ => _.DataProcessorStatusId == 4);
            }
        }

        public int GetUnProcessedSolrProcessorErrorCount()
        {
            using (var context = new AuthContext())
            {
                return context.SolrIndexQueues.Count(_ => _.SolrQueueStatus == 4);
            }
        }

        public int GetUnProcessedLicenseProcessorErrorCount()
        {
            using (var context = new AuthContext())
            {
                return context.GenerateLicenseQueue.Count(_ => _.GenerateLicenseStatusId == 4);
            }
        }
        //Get unprocessed item count for SolrProcessor
        public int GetUnProcessedSolrProcessorCount()
        {
            using (var context = new AuthContext())
            {
                return context.SolrIndexQueues.Count(_ => _.SolrQueueStatus == 1 || _.SolrQueueStatus == 2);
            }
        }


        public bool IsLicenseProcessorProcessing()
        {
            using (var context = new AuthContext())
            {
                return context.GenerateLicenseQueue.Any(_ => _.GenerateLicenseStatusId == 2);
            }
        }

        public bool IsSolrProcessorProcessing()
        {
            using (var context = new AuthContext())
            {
                return context.SolrIndexQueues.Any(_ => _.SolrQueueStatus == 2);
            }
        }
        public bool IsDataHarmonizingProcessorProcessing()
        {
            using (var context = new AuthContext())
            {
                return context.DataHarmonizationQueues.Any(_ => _.DataProcessorStatusId == 2);
            }
        }
    }
}
