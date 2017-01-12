using System.Collections.Generic;
using UMPG.USL.Models;

namespace UMPG.USL.API.Business
{
    public interface IServiceManager
    {
        bool RestartService(string serviceName);
        bool StopService(string serviceName);
        bool StartService(string serviceName);
        List<ServiceInformationDTO> GetAllProcessorStatus();
    }
}