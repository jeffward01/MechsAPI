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
    [RoutePrefix("api/LookUpCTRL/paidquarters")]
    public class PaidQuartersController : ApiController
    {
        private readonly IPaidQuarterManager _paidQuarterManager;
        public PaidQuartersController(IPaidQuarterManager paidQuarterManager)
        {
            _paidQuarterManager = paidQuarterManager;
        }
        //[Authorize]
        [Route("")]
        [HttpGet]
        [ActionName("GetAll")]
        public List<LU_PaidQuarter> Get()
        {
            return _paidQuarterManager.GetAll();
        }


        [Route("GetRolling10years")]
        [HttpGet]
        [ActionName("GetRolling10years")]
        public List<LU_PaidQuarter> GetRolling10years()
        {
            return _paidQuarterManager.GetRolling10years();
        }

        [Route("GetPaidQuarter/{paidQuarterId}")]
        [HttpGet]
        public LU_PaidQuarter GetPaidQuarter(int paidQuarterId)
        {
            return _paidQuarterManager.Get(paidQuarterId);
        }

        //[Authorize]
        [Route("Search")]
        [HttpPost]
        public List<LU_PaidQuarter> Search([FromBody]string query)
        {

            return _paidQuarterManager.Search(query);
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
