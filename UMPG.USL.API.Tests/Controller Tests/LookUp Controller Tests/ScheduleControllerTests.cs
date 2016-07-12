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
using UMPG.USL.API.Business.Lookups;
using UMPG.USL.Models.LookupModel;
using UMPG.USL.API.Controllers.LookUpCTRL;

namespace UMPG.USL.API.Tests.Controller_Tests.LookUp_Controller_Tests
{
    [TestFixture]
    public class ScheduleControllerTests
    {
        [Test]
        public void GetAllLU_Schedule_ReturnListLU_Schedule()
        {
            //Arrange
            var mockScheduleManager = A.Fake<IScheduleManager>();

            //Build expected
            List<LU_Schedule> expected = new List<LU_Schedule> { };

            A.CallTo(() => mockScheduleManager.GetAll()).Returns(expected);

            //Call  
            ScheduleController controller = new ScheduleController(mockScheduleManager);
            var result = controller.Get();

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetScheduleLU_Schedule_ReturnLU_Schedule()
        {
            //Arrange
            var mockScheduleManager = A.Fake<IScheduleManager>();

            //Build expected
            LU_Schedule expected = new LU_Schedule { };

            A.CallTo(() => mockScheduleManager.Get(A<int>.Ignored)).Returns(expected);

            //Call  
            ScheduleController controller = new ScheduleController(mockScheduleManager);
            var result = controller.GetSchedule(A<int>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void SearchLU_Schedule_ReturnListLU_Schedule()
        {
            //Arrange
            var mockScheduleManager = A.Fake<IScheduleManager>();

            //Build expected
            List<LU_Schedule> expected = new List<LU_Schedule> { };

            A.CallTo(() => mockScheduleManager.Search(A<string>.Ignored)).Returns(expected);

            //Call  
            ScheduleController controller = new ScheduleController(mockScheduleManager);
            var result = controller.Search(A<string>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }
    }
}
