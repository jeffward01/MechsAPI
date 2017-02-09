using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.Models.ProcessorModel;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Security.Principal;
using Microsoft.Win32.SafeHandles;
using UMPG.USL.Security.Safe;
using AuthenticationLevel = System.Net.Security.AuthenticationLevel;
using ObjectQuery = System.Data.Entity.Core.Objects.ObjectQuery;

namespace UMPG.USL.API.Business.ProcessorManagers
{
    public class RemoteServiceManager : IRemoteServiceManager
    {
        private static readonly TimeSpan _timeout = TimeSpan.FromSeconds(35);
        private const int _sleepTime = 15000;


        public bool StartRemoteService(string serviceName)
        {

            try
            {
              
               var service = new ServiceController(serviceName, EnvironmentInformation.SisterEnvironmentName);
                // Wait Timeout to get stopped status
                if (DoesRemoteServiceExist(serviceName))
                {

                    if (service.Status != ServiceControllerStatus.Running)
                    {
                        // Start Service
                        service.Start();
                        service.WaitForStatus(ServiceControllerStatus.Running, _timeout);
                        var result = _getRemoteServiceStatus(serviceName);
                        //iu.Undo();
                        return result == "Running" ? true : false;
                    }
                    else
                    {
                        var result = _getRemoteServiceStatus(serviceName);
                        //iu.Undo();
                        return result == "Running" ? true : false;
                    }

                }
                else
                {
                    _serviceDoesntExist(serviceName);
                    return true;
                }



            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
           
        }

        public bool StopRemoteService(string serviceName)
        {
            var service = new ServiceController(serviceName, EnvironmentInformation.SisterEnvironmentName);
            var result = false;
            // Wait Timeout to get stopped status
            if (DoesRemoteServiceExist(serviceName))
            {
                if (service.Status != ServiceControllerStatus.Stopped)
                {
                    // Stop Service
                    service.Stop();
                    service.WaitForStatus(ServiceControllerStatus.Stopped, _timeout);
                    var serviceResult = _getRemoteServiceStatus(serviceName);
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
                    var serviceResult = _getRemoteServiceStatus(serviceName);
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


        private string _getRemoteServiceStatus(string serviceName)
        {
            var sc = new ServiceController(serviceName, EnvironmentInformation.SisterEnvironmentName);

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

        private static bool DoesRemoteServiceExist(string serviceName)
        {
            ServiceController[] services = ServiceController.GetServices(EnvironmentInformation.SisterEnvironmentName);
            var service = services.FirstOrDefault(s => s.ServiceName == serviceName);
            return service != null;
        }

        private void _serviceDoesntExist(string serviceName)
        {
            Console.WriteLine(serviceName + " does not exist on Machine: " + EnvironmentInformation.EnvironmentName);
        }


    }
}
