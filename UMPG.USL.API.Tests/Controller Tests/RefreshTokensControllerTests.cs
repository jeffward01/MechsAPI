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
using UMPG.USL.API.Business;
using System.Web.Http;
using UMPG.USL.API.Controllers;
using System.Net.Http;

namespace UMPG.USL.API.Tests.Controller_Tests
{
    [TestFixture]
    public class RefreshTokensControllerTests
    {
        [Test]
        public void Get_ReturnIHttpActionResult()
        {
            //Arrange
            var mockAuthManager = A.Fake<IAuthManager>();

            //Build expected
            
            List<RefreshToken> expected = new List<RefreshToken> { };

            A.CallTo(() => mockAuthManager.GetAllRefreshTokens()).Returns(expected);

            RefreshTokensController controller = new RefreshTokensController(mockAuthManager);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            var response = controller.Get();

            //Assert
            Assert.IsInstanceOf(typeof(IHttpActionResult), response);
        }

        [Test]
        public void Delete_ReturnIHttpActionResult()
        {
            //Arrange
            var mockAuthManager = A.Fake<IAuthManager>();

            A.CallTo(() => mockAuthManager.RemoveRefreshToken(A<string>.Ignored));

            RefreshTokensController controller = new RefreshTokensController(mockAuthManager);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            var response = controller.Delete(A<string>.Ignored);

            //Assert
            Assert.IsInstanceOf(typeof(Task<IHttpActionResult>), response);
        }
    }
}
