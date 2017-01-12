using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public class DataHarmonizationQueueRepository : IDataHarmonizationQueueRepository
    {
        public int GetPendingCountForAllActionRequests()
        {
            using (var context = new AuthContext())
            {
                return context.DataHarmonizationQueues.Count(_ => _.DataProcessorStatusId == 1);
            }
        }

        public int GetPendingCountForCreateRequests()
        {
            using (var context = new AuthContext())
            {
                return context.DataHarmonizationQueues.Where(_ => _.ActionTypeId == 1).Count(_ => _.DataProcessorStatusId == 1);
            }
        }

        public int GetPendingCountForDeleteRequests()
        {
            using (var context = new AuthContext())
            {
                return context.DataHarmonizationQueues.Where(_ => _.ActionTypeId == 2).Count(_ => _.DataProcessorStatusId == 1);
            }
        }

        public DataHarmonizationQueue GetFirstItemInQueue()
        {
            using (var context = new AuthContext())
            {
                return context.DataHarmonizationQueues.FirstOrDefault(_ => _.DataProcessorStatusId == 1);
            }
        }

        public bool ArePendingItemsInQueue()
        {
            using (var context = new AuthContext())
            {
                return context.DataHarmonizationQueues.Any(_ => _.DataProcessorStatusId == 1);
            }
        }

        public DataHarmonizationQueue EditDataHarmonizationQueue(DataHarmonizationQueue dataHarmonizationQueue)
        {
            using (var context = new AuthContext())
            {
                context.Entry(dataHarmonizationQueue).State = EntityState.Modified;
                context.SaveChanges();
                return dataHarmonizationQueue;
            }
        }

        public DataHarmonizationQueue CreateDataHarmonizationRequest(DataHarmonizationQueue dataHarmonizationQueueItem)
        {
            using (var context = new AuthContext())
            {
                context.DataHarmonizationQueues.Add(dataHarmonizationQueueItem);
                context.SaveChanges();
                return dataHarmonizationQueueItem;
            }
        }
    }
}
