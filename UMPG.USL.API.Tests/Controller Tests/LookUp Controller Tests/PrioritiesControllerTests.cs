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
    public class PrioritiesControllerTests
    {
        [Test]
        public void GetAllLULicenseStatus_ReturnListLU_Priority()
        {
            //Arrange
            var mockPriorityManager = A.Fake<IPriorityManager>();

            //Build expected
            List<LU_Priority> expected = new List<LU_Priority> { };

            A.CallTo(() => mockPriorityManager.GetAll()).Returns(expected);

            //Call  
            PrioritiesController controller = new PrioritiesController(mockPriorityManager);
            var result = controller.Get();

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetPriority_ReturLU_Priority()
        {
            //Arrange
            var mockPriorityManager = A.Fake<IPriorityManager>();

            //Build expected
            LU_Priority expected = new LU_Priority { };

            A.CallTo(() => mockPriorityManager.Get(A<int>.Ignored)).Returns(expected);

            //Call  
            PrioritiesController controller = new PrioritiesController(mockPriorityManager);
            var result = controller.GetPriority(A<int>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Search_ReturnListLU_Priority()
        {
            //Arrange
            var mockPriorityManager = A.Fake<IPriorityManager>();

            //Build expected
            List<LU_Priority> expected = new List<LU_Priority> { };

            A.CallTo(() => mockPriorityManager.Search(A<string>.Ignored)).Returns(expected);

            //Call  
            PrioritiesController controller = new PrioritiesController(mockPriorityManager);
            var result = controller.Search(A<string>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }
    }
}
