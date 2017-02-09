using System.Collections.Generic;
using UMPG.USL.Models;
using UMPG.USL.Models.DataHarmonization;
using UMPG.USL.Models.LicenseGenerate;

namespace UMPG.USL.API.Business
{
    public interface IServiceManager
    {
        bool RestartService(string serviceName);
        void SetIndividualDHItemToPending(int id);
        void DeleteSingleDhItem(int id);
        void DeleteSingleLicenseItem(int id);

        bool StopService(string serviceName);

        bool StartService(string serviceName);
        bool ClearRestartInProcess();
        List<ServiceInformationDTO> GetAllProcessorStatus();
        bool TestStartRemoteService(string serviceName);
        IList<ServiceInformationDTO> GetRestartInProgress();

        ServiceInformationDTO GetServiceInformation(string serviceName);
        bool IsAdmin(int contactId);
        IList<GenerateLicenseQueue> GetAllFailedLicenseItems();
        IList<GenerateLicenseQueue> GetUnProcessedLicenseItems();
        IList<SolrIndexQueueItem> GetAllFailedSolrItems();
        IList<SolrIndexQueueItem> GetAllUnProcessedSolrItems();
        IList<DataHarmonizationQueue> GetAllUnProcessedDataHarmonizationItems();
        IList<DataHarmonizationQueue> GetAllFailedDataHarmonizationItems();

        //Processor manager
        void DeleteIndividualSolrItem(int solrIndexId);
        void SetIndividualSolrItemToPending(int solrIndexId);
        void SetAllFailedSolrItemsToPending();
        void SetAllUnProcessedSolrItemsToPending();
        void DeleteAllUnProcessedSolrItems();
        void DeleteAllFailedSolrItems();
        void DeleteAllFailedLicenseItems();
        void DeleteAllUnProcessedLicenseItems();
        void DeleteAllUnProcessedDhItems();
        void SetAllUnProcessedDhItemsToPending();
        void SetAllFailedDhItemsToPending();
        void DeleteAllFailedDhItems();
    }
}