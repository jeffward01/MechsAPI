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
using UMPG.USL.API;
using UMPG.USL.API.ActionFilters;

namespace UMPG.USL.API.Controllers.ContactCTRL
{
    [RoutePrefix("api/ContactCTRL/ContactDefaults")]
    [AuthorizationRequired]
    public class ContactDefaultController : ApiController
    {

        private readonly IContactDefaultManager _contactDefaultManager;
        public ContactDefaultController(IContactDefaultManager contactDefaultManager)
        {
            _contactDefaultManager = contactDefaultManager;
        }
        //[Authorize]
        [Route("")]
        [HttpGet]
        [ActionName("GetAll")]
        public List<ContactDefault> Get()
        {
            return _contactDefaultManager.GetAll();
        }

        [Route("GetDefault/{contactId}")]
        [HttpGet]
        public ContactDefault GetDefault(int contactId)
        {
            return _contactDefaultManager.Get(contactId);
        }

        [Route("Add")]
        [HttpPost]
        [ActionName("Add")]
        public ContactDefault Add(ContactDefault contactDefault)
        {
            return _contactDefaultManager.Add(contactDefault);
        }

        [Route("Save")]
        [HttpPost]
        public ContactDefault Save(ContactDefault contactDefault)
        {
            return _contactDefaultManager.Save(contactDefault);
        }

    }


    #region Helpers



    #endregion
}
