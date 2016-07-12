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
using UMPG.USL.API.Business.Lookups;
using UMPG.USL.Models.LookupModel;
using UMPG.USL.API.Data.LookupData;


namespace UMPG.USL.API.Tests.Manager_Tests.LookUps
{
    [TestFixture]
    public class ScheduleManagerTests
    {
        [Test]
        public void Get_ReturnSchedule()
        {
            //Arrange
            var mockIScheduleRepository = A.Fake<IScheduleRepository>();

            //Build Expected
            LU_Schedule expected = new LU_Schedule { };

            A.CallTo(() => mockIScheduleRepository.Get(A<int>.Ignored)).WithAnyArguments().Returns(expected);     

            //Act
            ScheduleManager manager = new ScheduleManager(mockIScheduleRepository);
            var result = manager.Get(A<int>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetAll_ReturnListSchedule()
        {
            //Arrange
            var mockIScheduleRepository = A.Fake<IScheduleRepository>();

            //Build Expected
            List<LU_Schedule> expected = new List<LU_Schedule> { };

            A.CallTo(() => mockIScheduleRepository.GetAll()).WithAnyArguments().Returns(expected);

            //Act
            ScheduleManager manager = new ScheduleManager(mockIScheduleRepository);
            var result = manager.GetAll();

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Search_ReturnListSchedule()
        {
            //Arrange
            var mockIScheduleRepository = A.Fake<IScheduleRepository>();

            //Build Expected
            List<LU_Schedule> expected = new List<LU_Schedule> { };

            A.CallTo(() => mockIScheduleRepository.Search(A<string>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            ScheduleManager manager = new ScheduleManager(mockIScheduleRepository);
            var result = manager.Search(A<string>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }
    }
}
