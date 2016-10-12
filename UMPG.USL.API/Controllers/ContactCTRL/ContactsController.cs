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
using UMPG.USL.Models.LicenseModel;

namespace UMPG.USL.API.Controllers.ContactCTRL
{
    [RoutePrefix("api/ContactCTRL/Contacts")]
    //[AuthorizationRequired]
    public class ContactController : ApiController
    {
        private readonly IContactManager _contactManager ;
        public ContactController(IContactManager contactManager)
        {
            _contactManager = contactManager;
        }
        //[Authorize]
        [Route("")]
        [HttpGet]
        [ActionName("GetAll")]
        public List<Contact> Get()
        {
           return _contactManager.GetAll();
        }

        [Route("GetAssignees")]
        [HttpGet]
        [ActionName("GetAssignees")]
        public List<Contact> GetAssignees()
        {
            return _contactManager.GetAssignees();
        }

        //[Authorize]
        [Route("Search")]
        [HttpPost]
        public List<Contact> Search([FromBody]string query)
        {
            
            return _contactManager.Search(query);
        }

        [HttpPost]
        [ActionName("Add")]
        public Contact Add(Contact contact)
        {
            return _contactManager.Add(contact);
        }

        [Route("GetContactsForLicensee/{licenseeId}")]
        [HttpGet]
        public List<Contact> GetProductLicenses(int licenseeId)
        {
            return _contactManager.GetContactsForLicensee(licenseeId);
        }

        [Route("GetContactsWithRole/{roleId}")]
        [HttpPost]
        public List<Contact> GetContactsWithRole(int roleId)
        {
            return _contactManager.GetContactsWithRole(roleId);
        }

        [Route("GetLabelsForLicensee/{licenseeId}")]
        [HttpGet]
        public List<LicenseeLabelGroup> GetLabelsForLicensee(int licenseeId)
        {
            return _contactManager.GetLabelsForLicensee(licenseeId);
        }

        [Route("GetContactsForLicenseeLabel/{licenseeLabelGroupId}")]
        [HttpGet]
        public List<LicenseeLabelGroupLink> GetContactsForLicenseeLabel(int licenseeLabelGroupId)
        {
            return _contactManager.GetContactsForLicenseeLabel(licenseeLabelGroupId);
        }

        [Route("GetAllLabelGroups")]
        [HttpGet]
        public List<LicenseeLabelGroup> GetAllLabelGroups()
        {
            return _contactManager.GetAllLabelGroups();
        }

        [Route("EditContact")]
        [HttpPost]
        public Contact EditContact(Contact contact)
        {
            return _contactManager.EditContact(contact);
        }

        [Route("AddLabelGroup")]
        [HttpPost]
        public LicenseeLabelGroup AddLabelGroup(LicenseeLabelGroup labelGroup)
        {
            return _contactManager.AddLabelGroup(labelGroup);
        }

        [Route("EditLabelGroup")]
        [HttpPost]
        public LicenseeLabelGroup EditLabelGroup(LicenseeLabelGroup labelGroup)
        {
            return _contactManager.EditLabelGroup(labelGroup);
        }
        [Route("DeleteLabelGroup")]
        [HttpPost]
        public LicenseeLabelGroup DeleteLabelGroup(LicenseeLabelGroup labelGroup)
        {
            return _contactManager.DeleteLabelGroup(labelGroup);
        }

        [Route("AddContactAndLink")]
        [HttpPost]
        public Contact AddContactAndLink(AddContactAndLinqRequest request)
        {
            return _contactManager.AddContactAndLink(request);
        }


        [Route("AddLicenseeContactAndLink")]
        [HttpPost]
        public Contact AddContactAndLink(AddLicenseeContactAndLinqRequest request)
        {
            return _contactManager.AddLicenseeContactAndLink(request);
        }

        [Route("DeleteContactAndLink")]
        [HttpPost]
        public bool DeleteContactAndLink(DeleteContactRequest contact)
        {
            return _contactManager.DeleteContactandLink(contact);
        }

        [Route("DeleteContactFromLabelGroup")]
        [HttpPost]
        public bool DeleteContactFromLabelGroup(DeleteContactFromLabelGroupRequest request)
        {
            return _contactManager.DeleteContactFromLabelGroup(request);
        }
        


        [Route("DeleteLicenseeContactAndLink")]
        [HttpPost]
        public bool DeleteLicenseeContactAndLink(DeleteContactRequest contact)
        {
            return _contactManager.DeleteLicenseeContactAndLink(contact);
        }

        [Route("EmailExists/{licenseeId}")]
        [HttpPost]
        public bool EmailExists([FromBody]string email, int licenseeId)
        {
            return _contactManager.EmailExists(email, licenseeId);
        }

    }


    #region Helpers



    #endregion
}
