using UMPG.USL.Models.Reports;

namespace UMPG.USL.API.Business.Reports
{
    public interface IReportQueueManager
    {
        ReportQueue Add(ReportQueue reportQueue);
    }
}
