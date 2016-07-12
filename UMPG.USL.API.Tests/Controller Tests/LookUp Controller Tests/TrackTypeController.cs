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
using UMPG.USL.API.Business.LookUps;

namespace UMPG.USL.API.Tests.Controller_Tests.LookUp_Controller_Tests
{
    public class TrackTypeController
    {
        /*
        [Test]
        [Ignore("Constructor Error. Strange. Come back later")]
        public void GetAll_ReturnListLU_TrackType()
        {
            //Arrange
            var mockTrackTypeManager = A.Fake<ITrackTypeManager>();

            //Build expected
            List<LU_TrackType> expected = new List<LU_TrackType> { };

            A.CallTo(() => mockTrackTypeManager.GetAll()).Returns(expected);

            //Call  
          //  TrackTypeController controller = new TrackTypeController(mockTrackTypeManager);
         / / var result = controller.Get();

            //Assert
            Assert.AreEqual(expected, result);
        }
        */
    }
}
