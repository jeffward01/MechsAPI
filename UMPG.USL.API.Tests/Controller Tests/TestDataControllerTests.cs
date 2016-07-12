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
using UMPG.USL.API.Business.Recs;
using UMPG.USL.API.Controllers.RECsCTRL;
using UMPG.USL.API.Controllers;
using System.Web;
using System.Web.Http;
using System.Net.Http;
using System.Web.Http.Results;


namespace UMPG.USL.API.Tests.Controller_Tests
{
    //____Notes for two tests below:____
    // No service/manager/interface is used in controller methods so standard method mocking cannot be used. 
    // Testing for return result type.  Cannot (without further research) mock 'Ok()' to return particular
    // results using A.CallTo(() => mockService.MockMethod()).Returns(expected);

    [TestFixture]
    public class TestDataControllerTests
    {
        [Test]
        public void Get_ReturnIHttpActionResult()
        {
            //Arrange
            var controller = new TestDataController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            //Act
            var response = controller.Get();

            //Assert
            Assert.IsInstanceOf(typeof(IHttpActionResult), response);
        }

        [Test]
        public void UpdateSong_ReturnIHttpActionResult()
        {
            //Arrange
            var controller = new TestDataController();
            var mockSongHelper = A.Fake<SongHelper>();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            //Act
            var response = controller.Update(A<Song>.Ignored);
                        
            OkNegotiatedContentResult<Song> contentResult = response as OkNegotiatedContentResult<Song>;

            //Assert
            Assert.IsInstanceOf(typeof(IHttpActionResult), response);
        //    Assert.IsNotNull(contentResult);
            //var content = contentResult.Content as Song;
            // Assert.IsNotNull(content);
        }
    }
}
