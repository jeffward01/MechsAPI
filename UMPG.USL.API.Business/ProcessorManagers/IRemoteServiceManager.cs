namespace UMPG.USL.API.Business.ProcessorManagers
{
    public interface IRemoteServiceManager
    {
        bool StartRemoteService(string serviceName);
        bool StopRemoteService(string serviceName);
    }
}