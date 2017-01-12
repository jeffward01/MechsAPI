using System;
using System.Collections.Generic;
using System.ServiceProcess;
using System.Threading;
using System.Web.Configuration;
using UMPG.USL.API.Data;
using UMPG.USL.Models;

namespace UMPG.USL.API.Business
{
    public class ServiceManager : IServiceManager
    {
        private static readonly TimeSpan _timeout = TimeSpan.FromSeconds(35);
        private const int _sleepTime = 15000;
        private readonly IProcessorRepository _processorRepository;
        

        public ServiceManager(IProcessorRepository processorRepository)
        {
            _processorRepository = processorRepository;
        }

        public List<ServiceInformationDTO> GetAllProcessorStatus()
        {
            List<ServiceInformationDTO> listOfProcessorInformation = new List<ServiceInformationDTO>();
            var listOfProcessors = WebConfigurationManager.AppSettings["ProcessorNames"];
            var listOfProcessorsSplit = listOfProcessors.Split(';');
            foreach (var processor in listOfProcessorsSplit)
            {
                processor.Trim();
                listOfProcessorInformation.Add(_getServiceInformation(processor));
            }
            return listOfProcessorInformation;
        }
        

        public bool RestartService(string serviceName)
        {
            return _restartService(serviceName);
        }

        public bool StopService(string serviceName)
        {
            return _stopService(serviceName);
        }

        public bool StartService(string serviceName)
        {
            return _startService(serviceName);
        }

        

        private ServiceInformationDTO _getServiceInformation(string serviceName)
        {
            ServiceInformationDTO serviceInfo = new ServiceInformationDTO
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
                count = _processorRepository.GetUnProcessedSolrProcessorErrorCount();
            }
            else if (serviceName == "SolrProcessor")
            {
                count = _processorRepository.GetUnProcessedSolrProcessorCount();
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


        private bool _stopService(string serviceName)
        {
            var service = new ServiceController(serviceName);
            var result = false;
            // Wait Timeout to get stopped status

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

        private bool _startService(string serviceName)
        {
            var service = new ServiceController(serviceName);
            // Wait Timeout to get running status

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

        private bool _restartService(string serviceName)
        {
            var service = new ServiceController(serviceName);

            if (service.Status != ServiceControllerStatus.Stopped)
            {
                //Stop service
                _stopService(serviceName);
            }
            Thread.Sleep(_sleepTime);

            //Start service
            return _startService(serviceName);
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
    }
}