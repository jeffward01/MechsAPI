using UMPG.USL.Models.Reports;

namespace UMPG.USL.API.Data.Reports
{
    public interface IReportQueueRepository
    {
        ReportQueue Add(ReportQueue reportQueue);
    }
}
