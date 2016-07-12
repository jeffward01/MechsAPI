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
     
using System.Net;

namespace UMPG.USL.API.Tests.Controller_Tests
{
    [TestFixture]
    public class AccountControllerTests
    {
        [Test]
        public void RegisterUserModel_ReturnIHttpActionResult()
        {
            //Arrange
           var mockAuthManager = A.Fake<IAuthManager>();
                      
            //Call  
            AccountController controller = new AccountController(mockAuthManager);
            var result = controller.Register(A<UserModel>.Ignored);
      
            //Assert
            Assert.IsInstanceOf(typeof(Task<IHttpActionResult>), result);
        }

        [Test]
        public void GetExternalLogin_ReturnIHttpActionResult()
        {
           //Arrange
            var mockAuthManager = A.Fake<IAuthManager>();

            //Act 
            AccountController controller = new AccountController(mockAuthManager);
            var result = controller.GetExternalLogin(A<string>.Ignored, A<string>.Ignored);

            //Assert
            Assert.IsInstanceOf(typeof(Task<IHttpActionResult>), result);
        }

        [Test]
        public void RegisterExternal_ReturnIHttpActionResult()
        {
            //Arrange
            var mockAuthManager = A.Fake<IAuthManager>();

            //Act 
            AccountController controller = new AccountController(mockAuthManager);
            var result = controller.RegisterExternal(A<RegisterExternalBindingModel>.Ignored); 

            //Assert
            Assert.IsInstanceOf(typeof(Task<IHttpActionResult>), result);
        }


         [Test]
        public void ObtainLocalAccessToken_ReturnIHttpActionResult()
        {
            //Arrange
            var mockAuthManager = A.Fake<IAuthManager>();

            //Act 
            AccountController controller = new AccountController(mockAuthManager);
            var result = controller.ObtainLocalAccessToken(A<string>.Ignored, A<string>.Ignored);

            //Assert
            Assert.IsInstanceOf(typeof(Task<IHttpActionResult>), result);
        }

    }
}
