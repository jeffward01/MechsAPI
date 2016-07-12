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
    [RoutePrefix("api/LookUpCTRL/SpecialStatuses")]
    public class SpecialStatusesController : ApiController
    {
        private readonly ISpecialStatusManager _specialstatusManager;
        public SpecialStatusesController(ISpecialStatusManager specialstatusManager)
        {
            _specialstatusManager = specialstatusManager;
        }
        //[Authorize]
        [Route("")]
        [HttpGet]
        [ActionName("GetAll")]
        public List<LU_SpecialStatus> Get()
        {
            return _specialstatusManager.GetAll();
        }

        [Route("GetSpecialStatus/{specialstatusId}")]
        [HttpGet]
        public LU_SpecialStatus GetSpecialStatus(int specialstatusId)
        {
            return _specialstatusManager.Get(specialstatusId);
        }

        //[Authorize]
        [Route("Search")]
        [HttpPost]
        public List<LU_SpecialStatus> Search([FromBody]string query)
        {

            return _specialstatusManager.Search(query);
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
