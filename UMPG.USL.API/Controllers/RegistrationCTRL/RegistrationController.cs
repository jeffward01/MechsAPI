using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Cors;
using UMPG.USL.API.Business.Contacts;
using UMPG.USL.Models;
using UMPG.USL.Models.ContactModel;
using UMPG.USL.Models.RegistrationModel;
using UMPG.USL.API;

namespace UMPG.USL.API.Controllers.RegistrationCTRL
{
    [RoutePrefix("api/RegistrationCTRL/registration")]
    public class RegistrationController : ApiController
    {

        private readonly IContactManager _contactManager;
        public RegistrationController(IContactManager contactManager) 
        {
            _contactManager = contactManager;
        }

        [Route("Register")]
        [HttpPost]
        public RegistrationResult Register(ContactRegistration contactRegistration)
        {
            return _contactManager.Register(contactRegistration);
        }

    }


    #region Helpers



    #endregion
}

