using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Cors;
using UMPG.USL.API.Business.Licenses;
using UMPG.USL.Models.LookupModel;
using UMPG.USL.API;
using UMPG.USL.API.ActionFilters;
using UMPG.USL.API.Business.Lookups;

namespace UMPG.USL.API.Controllers.LookUpCTRL
{
    [RoutePrefix("api/LookUpCTRL/WritersConsentTypes")]
    [AuthorizationRequired]
    public class WritersConsentTypesController : ApiController
    {
        private readonly IWritersConsentTypeManager _writersConsentTypeManager;
        public WritersConsentTypesController(IWritersConsentTypeManager writersConsentTypeManager)
        {
            _writersConsentTypeManager = writersConsentTypeManager;
        }
        //[Authorize]
        [Route("")]
        [HttpGet]
        [ActionName("GetAll")]
        public List<LU_WritersConsentType> Get()
        {
            return _writersConsentTypeManager.GetAll();
        }

        [Route("GetWritersConsentType/{writersConsentTypeId}")]
        [HttpGet]
        public LU_WritersConsentType GetWritersConsentType(int writersConsentTypeId)
        {
            return _writersConsentTypeManager.Get(writersConsentTypeId);
        }

        [Route("GetWritersConsentForLookup")]
        [HttpGet]
        public List<LU_WritersConsentType> GetWritersConsentForLookup()
        {
            return _writersConsentTypeManager.GetWritersConsentForLookup();
        }



        [Route("GetPaidQuarterForLookup")]
        [HttpGet]
        public List<LU_PaidQuarterType> GetPaidQuarterForLookup()
        {
            return _writersConsentTypeManager.GetPaidQuarterForLookup();
        }


        [Route("GetWritersIncludeExcludeForLookup")]
        [HttpGet]
        public List<LU_WritersIncludeExcludeType> GetWritersIncludeExcludeForLookup()
        {
            return _writersConsentTypeManager.GetWritersIncludeExcludeForLookup();
        }


        //[Authorize]
        [Route("Search")]
        [HttpPost]
        public List<LU_WritersConsentType> Search([FromBody]string query)
        {

            return _writersConsentTypeManager.Search(query);
        }

        //[HttpPost]
        //[ActionName("Add")]
        //public License Add(LU_LicenseMethod licensemethod)
        //{
        //    return _licensemethodManager.Add(licensemethod);
        //}

    }


    #region Helpers



    #endregion
}
