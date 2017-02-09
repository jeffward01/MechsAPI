using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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

        public void Delete(DataHarmonizationQueue indexQueueItem)
        {
            using (var context = new AuthContext())
            {
                context.DataHarmonizationQueues.Attach(indexQueueItem);
                context.DataHarmonizationQueues.Remove(indexQueueItem);
                context.SaveChanges();
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

        public IList<DataHarmonizationQueue> GetAllInProcessItems()
        {
            using (var context = new AuthContext())
            {
                return context.DataHarmonizationQueues.Where(_ => _.DataProcessorStatusId == 2).ToList();
            }
        }

        public DataHarmonizationQueue GetDhItemById(int id)
        {
            using (var context = new AuthContext())
            {
                return context.DataHarmonizationQueues.FirstOrDefault(_ => _.DataHarmonizationQueueId == id);
            }
        }

        public IList<DataHarmonizationQueue> GetAllFailedItems()
        {
            using (var context = new AuthContext())
            {
                return context.DataHarmonizationQueues.Where(_ => _.DataProcessorStatusId == 4).ToList();
            }
        }

        public IList<DataHarmonizationQueue> GetAllUnProcessItems()
        {
            using (var context = new AuthContext())
            {
                return context.DataHarmonizationQueues.Where(_ => _.DataProcessorStatusId == 2|| _.DataProcessorStatusId == 1).ToList();
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