using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Web.SessionState;

namespace UMPG.USL.API.Business.Providers
{
    public class ProcessorDataProvider : IProcessorDataProvider
    {
        private List<ProcessorStatus> _processorStatuses = new List<ProcessorStatus>();
        
        private Timer timer = new Timer();


        public void CreateRestartEntry(string processorName)
        {
            var newRestartEntry = new ProcessorStatus
            {
                ProcessorName = processorName,
                RestartStatus = 1
            };
            _processorStatuses.Add(newRestartEntry);
            StartTimer(timer);
        }

        public bool FinalizeRestartEntry(string processorName)
        {
            var restartEntryToFinalize = _getProcessorStatusFromList(processorName);
            _processorStatuses.Remove(restartEntryToFinalize);
            return true;
        }

        public void ClearAllRestarts()
        {
            _processorStatuses = new List<ProcessorStatus>();
        }

        public IList<ProcessorStatus> GetRestartsInProgress()
        {
            return _processorStatuses;
        }


        public bool IsRestartInProcess(string processorName)
        {
            var result = _getProcessorStatusFromList(processorName);
            if (result.ProcessorName != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }        

        private ProcessorStatus _getProcessorStatusFromList(string processorName)
        {
           ProcessorStatus processorStatus = new ProcessorStatus();
            
                foreach (var status in _processorStatuses)
                {
                    if (status.ProcessorName == processorName)
                    {
                    processorStatus = status;
                    }
                }
            return processorStatus;
        }

        private void StartTimer(Timer timer)
        {
            timer.Interval = 60000;
            timer.AutoReset = false;
            timer.Start();
            timer.Elapsed += ResetRestartFlag;
        }

        private void ResetRestartFlag(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            _processorStatuses = new List<ProcessorStatus>();
        }

 
    }

    public class ProcessorStatus
    {
        public string ProcessorName { get; set; }
        public int RestartStatus = 0; //0 means restart is NOT in progress, 1 means it IS in progress
    }

   
}
