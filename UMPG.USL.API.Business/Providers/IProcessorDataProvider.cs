using System.Collections.Generic;

namespace UMPG.USL.API.Business.Providers
{
    public interface IProcessorDataProvider
    {
        void CreateRestartEntry(string processorName);
        bool FinalizeRestartEntry(string processorName);
        bool IsRestartInProcess(string processorName);
        void ClearAllRestarts();
        IList<ProcessorStatus> GetRestartsInProgress();
    }
}