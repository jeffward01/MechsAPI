using System;
using UMPG.USL.API.Data.Reports;
using UMPG.USL.Models.Reports;

namespace UMPG.USL.API.Business.Reports
{
    public class ReportQueueManager:IReportQueueManager
    {
        private readonly IReportQueueRepository _reportQueueRepository;

        public ReportQueueManager(IReportQueueRepository reportQueueRepository)
        {
            _reportQueueRepository = reportQueueRepository;
        }

        public ReportQueue Add(ReportQueue reportQueue)
        {
            reportQueue.CreatedDate = DateTime.Now;
            return _reportQueueRepository.Add(reportQueue);
        }
    }
}
