using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using NUnit;
using NUnit.Framework;
using UMPG.USL.API.Business.Audits;
using UMPG.USL.Models;
using UMPG.USL.Models.AuditModel;
using UMPG.USL.Models.Recs;
using UMPG.USL.API.Business.Licenses;
using UMPG.USL.Models.LicenseModel;
using UMPG.USL.API.Controllers.LicenseCTRL;
using UMPG.USL.Common;
using System.Net.Http;
using System.Web.Http;
 
using System.Net;
using UMPG.USL.Models.ContactModel;
using UMPG.USL.API.Data.AuditData;
using UMPG.USL.API.Data.LicenseData;
using UMPG.USL.Models.LicenseGenerate;
using UMPG.USL.API.Data.Recs;
using UMPG.USL.API.Business.Recs;
using UMPG.USL.API.Data.Reports;
using UMPG.USL.Models.Reports;
using UMPG.USL.API.Business.Reports;

namespace UMPG.USL.API.Tests.Manager_Tests.Reports
{
    [TestFixture]
    public class ReportQueueManagerTests
    {
        [Test]
       public void AddReportQueue_ReturnReportQueue()
        {
           //Arrange
            var mockIReportQueueRepository = A.Fake<IReportQueueRepository>();

           //Build expected
           ReportQueue expected = new ReportQueue{};

           //Build Request
           ReportQueue request = new ReportQueue
           {
               CreatedDate = DateTime.Now
           };

           A.CallTo(() => mockIReportQueueRepository.Add(request)).WithAnyArguments();

           //Act
           ReportQueueManager manager = new ReportQueueManager(mockIReportQueueRepository);
           manager.Add(request);

           //Assert
           A.CallTo(() => mockIReportQueueRepository.Add(request)).WithAnyArguments().MustHaveHappened();

        }
    }
}
