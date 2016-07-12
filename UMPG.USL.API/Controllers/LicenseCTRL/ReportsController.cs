using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using UMPG.USL.API.Business.Reports;
using UMPG.USL.Models.Reports;

namespace UMPG.USL.API.Controllers.LicenseCTRL
{
    [RoutePrefix("api/licenseCTRL/Reports")]
    public class ReportsController:ApiController
    {
        private readonly IReportQueueManager _reportQueueManager ;
        public ReportsController(IReportQueueManager reportQueueManager)
        {
            _reportQueueManager = reportQueueManager;
        }

        [Route("AddReport")]
        [HttpPost]
        public ReportQueue Add(ReportQueue request)
        {
            return _reportQueueManager.Add(request);
        }
    }
}