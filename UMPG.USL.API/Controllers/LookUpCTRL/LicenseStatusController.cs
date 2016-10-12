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
    [RoutePrefix("api/LookUpCTRL/LicenseStatuses")]
    [AuthorizationRequired]
    public class LicenseStatusesController : ApiController
    {
        private readonly ILicenseStatusManager _licenseStatusManager;
        public LicenseStatusesController(ILicenseStatusManager licenseStatusManager)
        {
            _licenseStatusManager = licenseStatusManager;
        }
        //[Authorize]
        [Route("")]
        [HttpGet]
        [ActionName("GetAll")]
        public List<LU_LicenseStatus> Get()
        {
            return _licenseStatusManager.GetAll();
        }

    }


    #region Helpers



    #endregion
}
