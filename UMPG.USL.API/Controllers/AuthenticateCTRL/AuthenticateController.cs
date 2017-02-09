using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UMPG.USL.Common;
using UMPG.USL.Common.Transport;
using UMPG.USL.Models;
using UMPG.USL.Models.Security;
using UMPG.USL.Security;
using System.Linq;
using UMPG.USL.Security.Hashing;
using Castle.Core.Logging;
using System.Web;
using System.Security.Principal;
using UMPG.USL.API.Business.Token;


namespace UMPG.USL.API.Controllers.AuthenticateCTRL
{
    using System.Configuration;

    [RoutePrefix("api/authenticateCTRL/authenticate")]
    public class AuthenticateController : ApiController
    {

        private readonly IAuthenticator _authenticator;
        private readonly IContactContextResolver _contactContextResolver;
        private readonly ITokenServices _tokenServices;

        public AuthenticateController(IAuthenticator authenticator, IContactContextResolver contactContextResolver, ITokenServices tokenServices)
        {
            _authenticator = authenticator;
            _contactContextResolver = contactContextResolver;
            _tokenServices = tokenServices;
        }


        [Route("GetUser")]
        [HttpPost]
        public IHttpActionResult GetUser(string safeId)
        {
            return Ok(_authenticator.AuthenticateSafeId(safeId));
        }

        [Route("Login")]
        [HttpPost]
        public HttpResponseMessage Login(UserCredentials userCredentials)
        {

            AuthenticationResult result = new AuthenticationResult();
            if (userCredentials.IsInternal)
            {
                if (string.IsNullOrEmpty(userCredentials.Username))
                    return new HttpResponseMessage(HttpStatusCode.BadRequest);

                result = _authenticator.AuthenticateInternal(userCredentials.Username);
            }
            else
            {
                if (string.IsNullOrEmpty(userCredentials.Username) || string.IsNullOrEmpty(userCredentials.Password))
                    return new HttpResponseMessage(HttpStatusCode.BadRequest);

                result = _authenticator.AuthenticateExternal(userCredentials.Username, userCredentials.Password);
            }

            //AuthenticationResult result = new AuthenticationResult { Success = true, UserId = "51019079bd2afb8727b1e76e", SiteLocationCode = "UK1" };
            //var result = new AuthenticationResult { Success = true, UserId = "51019079bd2afb8727b1e76e", SiteLocationCode = "UK1" };
            //var result = new AuthenticationResult { Success = true, UserId = "4feca43ff038dfe313ee14b4", SiteLocationCode = "UK1" };      
            //var siteLocationCode = GetContext().SiteLocationCode;

            var response = new AuthenticateResponse
            {
                FieldErrors = result.FieldErrors,
                Success = result.Success,
                GlobalErrors = result.GlobalErrors,
                ErrorList = result.FieldErrors.Values
                     .SelectMany(x => x)
                     .ToList()
            };


            ContactContext contact = null;
            if(result.Success) contact = _contactContextResolver.Resolve(result.Safe.Id);
            if (contact!=null)
            {
                //check for contact record with safeid
              
                //  temp use safe user name
                contact.Contact.FullName = result.Safe.Name;
                contact.Contact.SafeId = result.Safe.Id;
                response.ContactContext = contact;
                response.UserApps = result.Safe.UserApps;

                if (!string.IsNullOrEmpty(userCredentials.Password))
                {

                    if (!HashUtility.ValidatePassword(userCredentials.Password, contact.Contact.Password))
                    {
                        response.Success = false;
                        response.ErrorList.Add("Login failed");
                    }
                    else
                    {
                        _tokenServices.DeleteByUserId(contact.Contact.ContactId);
                        var token = _tokenServices.GenerateToken(contact.Contact.ContactId);
                        response.GeneratedToken = token.AuthToken;
                    }

                }

            }
            else
            {

                response.Success = false;
            }
            

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [Route("Test/{username}/{password}")]
        [HttpGet]
        public HttpResponseMessage AuthenticateUserTest(string username, string password)
        {

            var userCredentials = new UserCredentials();
            userCredentials.Username = username;
            userCredentials.Password = password;
            if (password == "internal")
            {
                userCredentials.IsInternal = true;
            }
            else
            {
                userCredentials.IsInternal = false;
            }

            return Login(userCredentials);
            /*
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return new HttpResponseMessage(HttpStatusCode.BadRequest);

            AuthenticationResult result = _authenticator.AuthenticateExternal(username, password);
            //AuthenticationResult result = new AuthenticationResult { Success = true, UserId = "51019079bd2afb8727b1e76e", SiteLocationCode = "UK1" };
            //var result = new AuthenticationResult { Success = true, UserId = "51019079bd2afb8727b1e76e", SiteLocationCode = "UK1" };
            //var result = new AuthenticationResult { Success = true, UserId = "4feca43ff038dfe313ee14b4", SiteLocationCode = "UK1" };      

            //var siteLocationCode = GetContext().SiteLocationCode;

            var response = new AuthenticateResponse
            {
                FieldErrors = result.FieldErrors,
                Success = result.Success,
                GlobalErrors = result.GlobalErrors,
                ErrorList = result.FieldErrors.Values
                     .SelectMany(x => x)
                     .ToList()
            };

            if (result.Success)
            {
                //  temp force contactid to 1
                var contact = _contactContextResolver.Resolve(1);
                //  temp use safe user name
                contact.Contact.FullName = result.Safe.Name;
                response.ContactContext = contact;
            }

            return Request.CreateResponse(HttpStatusCode.OK, response);
            */
        }




        [Route("ValidateSafeId")]
        [HttpPost]
        public HttpResponseMessage ValidateSafeId(SafeIdCredentials safeIdCredentials)
        {
            if (string.IsNullOrEmpty(safeIdCredentials.SafeId))
                return new HttpResponseMessage(HttpStatusCode.BadRequest);

            AuthenticationResult result = _authenticator.AuthenticateSafeId(safeIdCredentials.SafeId);

            var response = new AuthenticateResponse
            {
                FieldErrors = result.FieldErrors,
                Success = result.Success,
                GlobalErrors = result.GlobalErrors
                //ErrorList = result.FieldErrors.Values
                //     .SelectMany(x => x)
                //     .ToList()
            };
            var contact = _contactContextResolver.Resolve(safeIdCredentials.SafeId);
            if (result.Success && contact.Contact.Password == safeIdCredentials.Password)
            {
                //  temp force contactid to 1

                //  temp use safe user name
                contact.Contact.FullName = result.Safe.Name;
                response.ContactContext = contact;
                response.UserApps = result.Safe.UserApps;
                var token = _tokenServices.GenerateToken(contact.Contact.ContactId);
                response.GeneratedToken = token.AuthToken;

            }
            else
            {
                response.Success = false;
            }

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [Route("GenerateHash/{passwordString}")]
        [HttpGet]
        public HttpResponseMessage GenerateHash(string passwordString)
        {
           var hashedPassword = HashUtility.HashPassword(passwordString);
            return Request.CreateResponse(HttpStatusCode.OK, hashedPassword);
        }


    }

}