using UMPG.USL.Models.Reports;

namespace UMPG.USL.API.Data.Reports
{
    public class ReportQueueRepository:IReportQueueRepository
    {
        public ReportQueue Add(ReportQueue reportQueue)
        {
            using (var context = new AuthContext())
            {
                context.ReportQueues.Add(reportQueue);
                context.SaveChanges();
                return reportQueue;
            }
        }
    }
}
