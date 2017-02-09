using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Threading;
using System.Web.Configuration;
using UMPG.USL.API.Business.ProcessorManagers;
using UMPG.USL.API.Business.Providers;
using UMPG.USL.API.Data;
using UMPG.USL.API.Data.DataHarmonization;
using UMPG.USL.API.Data.LicenseData;
using UMPG.USL.Models;
using UMPG.USL.Models.DataHarmonization;
using UMPG.USL.Models.LicenseGenerate;
using UMPG.USL.Models.ProcessorModel;

namespace UMPG.USL.API.Business
{
    public class ServiceManager : IServiceManager
    {
        private static readonly TimeSpan _timeout = TimeSpan.FromSeconds(35);
        private const int _sleepTime = 15000;
        private readonly IProcessorRepository _processorRepository;
        private readonly IProcessorDataProvider _processorDataProvider;
        private readonly IEnvironmentManager _environmentManager;
        private readonly IRemoteServiceManager _remoteServiceManager;
        private readonly ISolrIndexQueueRepository _solrIndexQueueRepository;
        private readonly IDataHarmonizationQueueRepository _dataHarmonizationQueueRepository;
        private readonly IGenerateLicenseQueueRepository _generateLicenseQueueRepository;
        private const bool _remoteMachineCommunication = false;

        public ServiceManager(IProcessorRepository processorRepository, IProcessorDataProvider processorDataProvider, IEnvironmentManager environmentManager, IRemoteServiceManager remoteServiceManager, ISolrIndexQueueRepository solrIndexQueueRepository, IDataHarmonizationQueueRepository dataHarmonizationQueueRepository, IGenerateLicenseQueueRepository generateLicenseQueueRepository)
        {
            _generateLicenseQueueRepository = generateLicenseQueueRepository;
            _dataHarmonizationQueueRepository = dataHarmonizationQueueRepository;
            _solrIndexQueueRepository = solrIndexQueueRepository;
            _remoteServiceManager = remoteServiceManager;
            _environmentManager = environmentManager;
            _processorRepository = processorRepository;
            _processorDataProvider = processorDataProvider;
        }

        public void DeleteIndividualSolrItem(int solrIndexId)
        {
            var item = _solrIndexQueueRepository.GetSolrIndexQueueItemById(solrIndexId);
            _solrIndexQueueRepository.Delete(item);
        }

        public void SetIndividualSolrItemToPending(int solrIndexId)
        {
            var item = _solrIndexQueueRepository.GetSolrIndexQueueItemById(solrIndexId);
            item.SolrQueueStatus = 1;
            _solrIndexQueueRepository.Update(item);
        }

        public void SetAllFailedSolrItemsToPending()
        {
            var items = _solrIndexQueueRepository.GetAllFailed();
            foreach (var item in items)
            {
                item.SolrQueueStatus = 1;
                item.TimesFailed = null;
                _solrIndexQueueRepository.Update(item);
            }
        }

        public void SetAllUnProcessedSolrItemsToPending()
        {
            var items = _solrIndexQueueRepository.GetAllUnProcessIndexQueueItems();
            foreach (var item in items)
            {
                item.SolrQueueStatus = 1;
                item.TimesFailed = null;
                _solrIndexQueueRepository.Update(item);
            }
        }

        public void SetSingleLicenseItemToPending(int id)
        {
            var item = _generateLicenseQueueRepository.GetGenerateLicenseQueueById(id);
            item.GenerateLicenseStatusId = 1;
                _generateLicenseQueueRepository.Update(item);
            
        }

        public void DeleteSingleLicenseItem(int id)
        {
            var item = _generateLicenseQueueRepository.GetGenerateLicenseQueueById(id);
            _generateLicenseQueueRepository.Delete(item);
        }

        public void DeleteAllUnProcessedSolrItems()
        {
            var items = _solrIndexQueueRepository.GetAllUnProcessIndexQueueItems();
            foreach (var item in items)
            {
                _solrIndexQueueRepository.Delete(item);
            }
        }

        public void DeleteAllFailedSolrItems()
        {
            var items = _solrIndexQueueRepository.GetAllFailed();
            foreach (var item in items)
            {
                _solrIndexQueueRepository.Delete(item);
            }
        }

        public void DeleteAllFailedLicenseItems()
        {
            var items = _generateLicenseQueueRepository.GetAllFailed();
            foreach (var item in items)
            {
                _generateLicenseQueueRepository.Delete(item);
            }
        }

        public void DeleteAllUnProcessedLicenseItems()
        {
            var items = _generateLicenseQueueRepository.GetAllUnProcessLicenses();
            foreach (var item in items)
            {
                _generateLicenseQueueRepository.Delete(item);
            }
        }

        public void DeleteAllUnProcessedDhItems()
        {
            var items = _dataHarmonizationQueueRepository.GetAllUnProcessItems();
            foreach (var item in items)
            {
                _dataHarmonizationQueueRepository.Delete(item);
            }
        }

        public void SetAllUnProcessedDhItemsToPending()
        {
            var items = _dataHarmonizationQueueRepository.GetAllUnProcessItems();
            foreach (var item in items)
            {
                item.DataProcessorStatusId = 1;
                _dataHarmonizationQueueRepository.EditDataHarmonizationQueue(item);
            }
        }

        public void SetAllFailedDhItemsToPending()
        {
            var items = _dataHarmonizationQueueRepository.GetAllFailedItems();
            foreach (var item in items)
            {
                item.DataProcessorStatusId = 1;
                _dataHarmonizationQueueRepository.EditDataHarmonizationQueue(item);
            }
        }

        public void SetIndividualDHItemToPending(int id)
        {
            var item = _dataHarmonizationQueueRepository.GetDhItemById(id);
            item.DataProcessorStatusId = 1;
            _dataHarmonizationQueueRepository.EditDataHarmonizationQueue(item);
        }

        public void DeleteAllFailedDhItems()
        {
            var items = _dataHarmonizationQueueRepository.GetAllFailedItems();
            foreach (var item in items)
            {
                _dataHarmonizationQueueRepository.Delete(item);
            }
        }

        public void DeleteSingleDhItem(int id)
        {
            var item = _dataHarmonizationQueueRepository.GetDhItemById(id);
            _dataHarmonizationQueueRepository.Delete(item);
        }

        public IList<GenerateLicenseQueue> GetAllFailedLicenseItems()
        {
            return _generateLicenseQueueRepository.GetAllFailed();
        }

        public IList<GenerateLicenseQueue> GetUnProcessedLicenseItems()
        {
            return _generateLicenseQueueRepository.GetAllUnProcessLicenses();
        }

        public IList<SolrIndexQueueItem> GetAllFailedSolrItems()
        {
            return _solrIndexQueueRepository.GetAllFailed();
        }

        public IList<SolrIndexQueueItem> GetAllUnProcessedSolrItems()
        {
            return _solrIndexQueueRepository.GetAllUnProcessIndexQueueItems();
        }

        public IList<DataHarmonizationQueue> GetAllUnProcessedDataHarmonizationItems()
        {
            return _dataHarmonizationQueueRepository.GetAllUnProcessItems();
        }

        public IList<DataHarmonizationQueue> GetAllFailedDataHarmonizationItems()
        {
            return _dataHarmonizationQueueRepository.GetAllFailedItems();
        }

        public List<ServiceInformationDTO> GetAllProcessorStatus()
        {
            var listOfProcessorInformation = new List<ServiceInformationDTO>();
            var listOfProcessors = WebConfigurationManager.AppSettings["ProcessorNames"];
            var listOfProcessorsSplit = listOfProcessors.Split(';');
            foreach (var processor in listOfProcessorsSplit)
            {
                processor.Trim();
                listOfProcessorInformation.Add(_getServiceInformation(processor));
            }
            var result = _processorDataProvider.GetRestartsInProgress();
            if (result.Count > 0)
            {
                foreach (var processor in listOfProcessorInformation)
                {
                    if (_isPresentInList(processor, result))
                    {
                        processor.ServiceStatus = "Restarting";
                    }
                }
            }

            return listOfProcessorInformation;
        }



        private bool _isPresentInList(ServiceInformationDTO needle, IList<ProcessorStatus> haystack)
        {
            foreach (var entry in haystack)
            {
                if (entry.ProcessorName == needle.ServiceName)
                {
                    return true;
                }
            }
            return false;
        }

        public bool ClearRestartInProcess()
        {
            _processorDataProvider.ClearAllRestarts();
            var result = _processorDataProvider.GetRestartsInProgress();
            if (result.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool RestartService(string serviceName)
        {
            _environmentManager.SetEnvironmentInformation();
            //Set all 'in process' items to 'pending'
            _setAllInProcessItemsToPending(serviceName);
            return _restartService(serviceName);
        }

        public bool StopService(string serviceName)
        {
            _environmentManager.SetEnvironmentInformation();
            return _stopService(serviceName);
        }

        public bool IsAdmin(int contactId)
        {
            var adminIds = WebConfigurationManager.AppSettings["AdminContactIds"];
            var listOfAdminIds = adminIds.Split(';');
            foreach (var adminId in listOfAdminIds)
            {
                adminId.Trim();
                var adminIdInt = Convert.ToInt32(adminId);
                if (adminIdInt == contactId)
                {
                    return true;
                }
            }
            return false;
        }

        public bool StartService(string serviceName)
        {
            _environmentManager.SetEnvironmentInformation();
            return _startService(serviceName);
        }

        public bool TestStartRemoteService(string serviceName)
        {
            return _remoteServiceManager.StartRemoteService(serviceName);
        }

        public ServiceInformationDTO GetServiceInformation(string serviceName)
        {
            return _getServiceInformation(serviceName);
        }

        public IList<ServiceInformationDTO> GetRestartInProgress()
        {
            var result = _processorDataProvider.GetRestartsInProgress();
            if (result.Count > 0)
            {
                return _processData(result);
            }
            return new List<ServiceInformationDTO>();
        }

        private IList<ServiceInformationDTO> _processData(IList<ProcessorStatus> processorStatuses)
        {
            IList<ServiceInformationDTO> list = new List<ServiceInformationDTO>();
            foreach (var status in processorStatuses)
            {
                list.Add(new ServiceInformationDTO
                {
                    ServiceStatus = "Restarting",
                    ServiceName = status.ProcessorName
                });
            }
            return list;
        }

        private ServiceInformationDTO _getServiceInformation(string serviceName)
        {
            var serviceInfo = new ServiceInformationDTO
            {
                ServiceName = serviceName,
                ServiceStatus = _getServiceStatus(serviceName),
                NumberOfItemsInQueue = _getNumberOfItemsInQueue(serviceName),
                NumberOfErrorItems = _getNumberOfErrorItemsInQueue(serviceName)
            };
            return serviceInfo;
        }

        private int _getNumberOfErrorItemsInQueue(string serviceName)
        {
            var count = 0;
            if (serviceName == "LicenseProcessor")
            {
                count = _processorRepository.GetUnProcessedLicenseProcessorErrorCount();
            }
            else if (serviceName == "SolrProcessor")
            {
                count = _processorRepository.GetUnProcessedSolrProcessorErrorCount();
            }
            else if (serviceName == "DataHarmonizationProcessor")
            {
                count = _processorRepository.GetUnProcessedDataHarmonizationErrorCount();
            }
            else
            {
                return count;
            }
            return count;
        }

        private int _getNumberOfItemsInQueue(string serviceName)
        {
            var count = 0;
            if (serviceName == "LicenseProcessor")
            {
                count = _processorRepository.GetUnProcessedLicenseProcessorCount();
            }
            else if (serviceName == "SolrProcessor")
            {
                count = _processorRepository.GetUnProcessedSolrProcessorCount();
            }
            else if (serviceName == "DataHarmonizationProcessor")
            {
                count = _processorRepository.GetUnProcessedDataHarmonizationCount();
            }
            else
            {
                return count;
            }
            return count;
        }

        private void _setAllInProcessItemsToPending(string serviceName)
        {
            if (serviceName == "LicenseProcessor")
            {
                var inProcessItems = _generateLicenseQueueRepository.GetAllInProcessLicenses();
                if (inProcessItems.Count > 0)
                {
                    foreach (var item in inProcessItems)
                    {
                        item.GenerateLicenseStatusId = 1;
                        _generateLicenseQueueRepository.Update(item);
                    }
                }
            }
            else if (serviceName == "SolrProcessor")
            {
                var inProcessItems = _solrIndexQueueRepository.GetAllInProcessIndexQueueItems();
                if (inProcessItems.Count > 0)
                {
                    foreach (var item in inProcessItems)
                    {
                        item.SolrQueueStatus = 1;
                        _solrIndexQueueRepository.Update(item);
                    }
                }
            }
            else if (serviceName == "DataHarmonizationProcessor")
            {
                var inProcessItems = _dataHarmonizationQueueRepository.GetAllInProcessItems();
                if (inProcessItems.Count > 0)
                {
                    foreach (var item in inProcessItems)
                    {
                        item.DataProcessorStatusId = 1;
                        _dataHarmonizationQueueRepository.EditDataHarmonizationQueue(item);
                    }
                }
            }
        }

        private bool _stopService(string serviceName)
        {
            var service = new ServiceController(serviceName);
            if (_remoteMachineCommunication)
            {
                if (_isOnUatOrProd())
                {
                    _remoteServiceManager.StopRemoteService(serviceName);
                }
            }
            var result = false;
            // Wait Timeout to get stopped status
            if (DoesServiceExist(serviceName))
            {
                if (service.Status != ServiceControllerStatus.Stopped)
                {
                    // Stop Service
                    service.Stop();
                    service.WaitForStatus(ServiceControllerStatus.Stopped, _timeout);
                    var serviceResult = _getServiceStatus(serviceName);
                    if (serviceResult == "Stopped")
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                }
                else
                {
                    //confirmStopped
                    var serviceResult = _getServiceStatus(serviceName);
                    if (serviceResult == "Stopped")
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                }
                return result;
            }
            else
            {
                _serviceDoesntExist(serviceName);
                return true;
            }
        }

        private bool _startService(string serviceName)
        {
            var service = new ServiceController(serviceName);
            if (_remoteMachineCommunication)
            {
                if (_isOnUatOrProd())
                {
                    _remoteServiceManager.StartRemoteService(serviceName);
                }
            }
            // Wait Timeout to get running status
            if (DoesServiceExist(serviceName))
            {
                if (service.Status != ServiceControllerStatus.Running)
                {
                    // Start Service
                    service.Start();
                    service.WaitForStatus(ServiceControllerStatus.Running, _timeout);
                    var result = _getServiceStatus(serviceName);
                    return result == "Running" ? true : false;
                }
                else
                {
                    var result = _getServiceStatus(serviceName);
                    return result == "Running" ? true : false;
                }
            }
            else
            {
                _serviceDoesntExist(serviceName);
                return true;
            }
        }

        private bool _restartService(string serviceName)
        {
            var service = new ServiceController(serviceName);
            if (DoesServiceExist(serviceName))
            {
                _processorDataProvider.CreateRestartEntry(serviceName);
                if (service.Status != ServiceControllerStatus.Stopped)
                {
                    //Stop service
                    _stopService(serviceName);
                }
                Thread.Sleep(_sleepTime);

                //Start service
                var result = _startService(serviceName);
                if (result)
                {
                    var finailized = _processorDataProvider.FinalizeRestartEntry(serviceName);
                    return true;
                }
                return false;
            }
            else
            {
                _serviceDoesntExist(serviceName);
                return true;
            }
        }

        private void _serviceDoesntExist(string serviceName)
        {
            Console.WriteLine(serviceName + " does not exist on Machine: " + EnvironmentInformation.EnvironmentName);
        }

        private string _getServiceStatus(string serviceName)
        {
            var sc = new ServiceController(serviceName);

            switch (sc.Status)
            {
                case ServiceControllerStatus.Running:
                    return "Running";

                case ServiceControllerStatus.Stopped:
                    return "Stopped";

                case ServiceControllerStatus.Paused:
                    return "Paused";

                case ServiceControllerStatus.StopPending:
                    return "Stopping";

                case ServiceControllerStatus.StartPending:
                    return "Starting";

                default:
                    return "Status Changing";
            }
        }

        private static bool DoesServiceExist(string serviceName)
        {
            var services = ServiceController.GetServices(EnvironmentInformation.EnvironmentName);
            var service = services.FirstOrDefault(s => s.ServiceName == serviceName);
            return service != null;
        }

        private bool _isOnUatOrProd()
        {
            if (EnvironmentInformation.EnvironmentType == "PROD" || EnvironmentInformation.EnvironmentType == "UAT")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}