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
using UMPG.USL.API.Business.Reports;
using UMPG.USL.Models.Reports;
using UMPG.USL.API.Controllers.LicenseCTRL;


namespace UMPG.USL.API.Tests.Controller_Tests.License_Controller_Tests
{
    public class ReportsControllerTests
    {
        [Test]
        public void AddReportQueue_ReturnReportQueue()
        {
            //Arrange
            var mockReportQueueManager = A.Fake<IReportQueueManager>();

            //Build expected
            ReportQueue expected = new ReportQueue { };

            A.CallTo(() => mockReportQueueManager.Add(A<ReportQueue>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            ReportsController controller = new ReportsController(mockReportQueueManager);
            var result = controller.Add(A<ReportQueue>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }
    }
}
