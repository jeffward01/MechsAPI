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
using UMPG.USL.Security;
using UMPG.USL.Models.Security;
using UMPG.USL.API.Controllers.AuthenticateCTRL;
using System.Net.Http;
using System.Net;

namespace UMPG.USL.API.Tests.Controller_Tests
{
    [TestFixture]
    public class AuthenticateControllerTests
    {
        [Test]
        public void Login_ReturnHttpResponseMessage()
        {
            //Arrange
            var mockAuthenticator = A.Fake<IAuthenticator>();
            var mockContactContextResolver = A.Fake<IContactContextResolver>();

            AuthenticateResponse expected = new AuthenticateResponse { Success = true };

            //Build request
            UserCredentials request = new UserCredentials { IsInternal = true };

            //Build Authentication Result
            AuthenticationResult authResult = new AuthenticationResult { Success = true, UserId = "51019079bd2afb8727b1e76e", SiteLocationCode = "UK1" };

            A.CallTo(() => mockAuthenticator.AuthenticateInternal(A<string>.Ignored)).WithAnyArguments().Returns(authResult);

            //Act
            AuthenticateController authController = new AuthenticateController(mockAuthenticator, mockContactContextResolver);
            authController.Login(request);
            var returned = mockAuthenticator.AuthenticateInternal(A<string>.Ignored);

            //Assert
            Assert.AreEqual(expected.Success, returned.Success);
        }

        [Test]
        public void AuthenticateUserTest_ReturnHttpResponseMessage()
        {
            //Arrange
            var mockAuthenticator = A.Fake<IAuthenticator>();
            var mockContactContextResolver = A.Fake<IContactContextResolver>();

            //Build expected
            AuthenticateResponse expected = new AuthenticateResponse { Success = true };

            UserCredentials request = new UserCredentials
            {
                Username = "test",
                Password = "test",
                IsInternal = true,
                
            };

            //Build Authentication Result
            AuthenticationResult authResult = new AuthenticationResult { Success = true, UserId = "51019079bd2afb8727b1e76e", SiteLocationCode = "UK1" };

            A.CallTo(() => mockAuthenticator.AuthenticateInternal(A<string>.Ignored)).WithAnyArguments().Returns(authResult);

            AuthenticateResponse AResponse = new AuthenticateResponse{};

            AuthenticationResult response = new AuthenticationResult { };
            A.CallTo(() => mockAuthenticator.AuthenticateExternal(A<string>.Ignored, A<string>.Ignored)).WithAnyArguments().Returns(response);     
                     
            //Act
            AuthenticateController authController = new AuthenticateController(mockAuthenticator, mockContactContextResolver);
            var returned = mockAuthenticator.AuthenticateInternal(A<string>.Ignored);

            //Assert
            Assert.AreEqual(expected.Success, returned.Success);
        }

        [Test]
        public void ValidateSafeId_ReturnHttpResponseMessage()
        {
            //Arrange
            var mockAuthenticator = A.Fake<IAuthenticator>();
            var mockContactContextResolver = A.Fake<IContactContextResolver>();

            AuthenticateResponse expected = new AuthenticateResponse { Success = true };

            //Build request
            SafeIdCredentials request = new SafeIdCredentials { };

            //Build Authentication Result
            AuthenticationResult authResult = new AuthenticationResult { Success = true, UserId = "51019079bd2afb8727b1e76e", SiteLocationCode = "UK1" };

            A.CallTo(() => mockAuthenticator.AuthenticateSafeId(A<string>.Ignored)).WithAnyArguments().Returns(authResult);

            //Act
            AuthenticateController authController = new AuthenticateController(mockAuthenticator, mockContactContextResolver);
            authController.ValidateSafeId(request);
            var returned = mockAuthenticator.AuthenticateSafeId(A<string>.Ignored);

            //Assert
            A.CallTo(() => mockAuthenticator.AuthenticateSafeId(A<string>.Ignored)).WithAnyArguments().MustHaveHappened();
        }
    }
}
