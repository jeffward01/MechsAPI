using UMPG.USL.Models;

namespace UMPG.USL.API.Data.LicenseData
{
    public interface ISolrIndexQueueRepository
    {
        SolrIndexQueueItem Add(SolrIndexQueueItem indexQueueItem);
        SolrIndexQueueItem Update(SolrIndexQueueItem indexQueueItem);
        SolrIndexQueueItem ProcessItemFromQueue();
        int GetQueueSize();

        int GetFailedCount();
    }
}