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
    [RoutePrefix("api/LookUpCTRL/rateTypes")]
    public class RateTypesController : ApiController
    {
        private readonly IRateTypeManager _ratetypeManager;
        public RateTypesController(IRateTypeManager ratetypeManager)
        {
            _ratetypeManager = ratetypeManager;
        }
        //[Authorize]
        [Route("")]
        [HttpGet]
        [ActionName("GetAll")]
        public List<LU_RateType> Get()
        {
            return _ratetypeManager.GetAll();
        }

        [Route("GetRateType/{ratetypeId}")]
        [HttpGet]
        public LU_RateType GetRateType(int ratetypeId)
        {
            return _ratetypeManager.Get(ratetypeId);
        }

        //[Authorize]
        [Route("Search")]
        [HttpPost]
        public List<LU_RateType> Search([FromBody]string query)
        {

            return _ratetypeManager.Search(query);
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
