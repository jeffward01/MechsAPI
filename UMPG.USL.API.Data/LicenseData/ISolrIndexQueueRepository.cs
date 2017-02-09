using System.Collections.Generic;
using UMPG.USL.Models;

namespace UMPG.USL.API.Data.LicenseData
{
    public interface ISolrIndexQueueRepository
    {
        SolrIndexQueueItem Add(SolrIndexQueueItem indexQueueItem);

        SolrIndexQueueItem Update(SolrIndexQueueItem indexQueueItem);
        SolrIndexQueueItem GetSolrIndexQueueItemById(int id);

        IList<SolrIndexQueueItem> GetAllFailed();
        void Delete(SolrIndexQueueItem indexQueueItem);

        SolrIndexQueueItem ProcessItemFromQueue();

        IList<SolrIndexQueueItem> GetAllUnProcessIndexQueueItems();

        IList<SolrIndexQueueItem> GetAllInProcessIndexQueueItems();

        int GetQueueSize();

        int GetFailedCount();
    }
}