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
using UMPG.USL.API.Business.Lookups;

namespace UMPG.USL.API.Controllers.LookUpCTRL
{
    [RoutePrefix("api/LookUpCTRL/licensemethods")]
    public class LicenseMethodsController : ApiController
    {
        private readonly ILicenseMethodManager _licensemethodManager;
        public LicenseMethodsController(ILicenseMethodManager licensemethodManager)
        {
            _licensemethodManager = licensemethodManager;
        }
        //[Authorize]
        [Route("")]
        [HttpGet]
        [ActionName("GetAll")]
        public List<LU_LicenseMethod> Get()
        {
           return _licensemethodManager.GetAll();
        }

        [Route("GetLicenseMethod/{licenseMethodId}")]
        [HttpGet]
        public LU_LicenseMethod GetLicenseMethod(int licensemethodId)
        {
            return _licensemethodManager.Get(licensemethodId);
        }

        //[Authorize]
        [Route("Search")]
        [HttpPost]
        public List<LU_LicenseMethod> Search([FromBody]string query)
        {
            
            return _licensemethodManager.Search(query);
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
