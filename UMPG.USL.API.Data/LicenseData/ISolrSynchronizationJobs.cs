using UMPG.USL.Models;

namespace UMPG.USL.API.Data.LicenseData
{
    public interface ISolrSynchronizationJobs
    {
        SolrSynchronizationJob Add(SolrSynchronizationJob job);
        SolrSynchronizationJob Update(SolrSynchronizationJob job);
        void Delete(SolrSynchronizationJob job);
        SolrSynchronizationJob GetItemFromQueue();
    }
}