using System.Collections.Generic;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public interface IDataHarmonizationQueueRepository
    {
        int GetPendingCountForAllActionRequests();
        int GetPendingCountForCreateRequests();
        int GetPendingCountForDeleteRequests();
        DataHarmonizationQueue GetFirstItemInQueue();
        void Delete(DataHarmonizationQueue indexQueueItem);
        DataHarmonizationQueue GetDhItemById(int id);
        IList<DataHarmonizationQueue> GetAllInProcessItems();
        IList<DataHarmonizationQueue> GetAllUnProcessItems();
        IList<DataHarmonizationQueue> GetAllFailedItems();
        bool ArePendingItemsInQueue();
        DataHarmonizationQueue EditDataHarmonizationQueue(DataHarmonizationQueue dataHarmonizationQueue);
        DataHarmonizationQueue CreateDataHarmonizationRequest(DataHarmonizationQueue dataHarmonizationQueueItem);
    }
}