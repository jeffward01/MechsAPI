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
    [RoutePrefix("api/LookUpCTRL/schedules")]
    public class ScheduleController : ApiController
    {
        private readonly IScheduleManager _scheduleManager;
        public ScheduleController(IScheduleManager scheduleManager)
        {
            _scheduleManager = scheduleManager;
        }
        //[Authorize]
        [Route("")]
        [HttpGet]
        [ActionName("GetAll")]
        public List<LU_Schedule> Get()
        {
            return _scheduleManager.GetAll();
        }

        [Route("GetRateType/{scheduleId}")]
        [HttpGet]
        public LU_Schedule GetSchedule(int scheduleId)
        {
            return _scheduleManager.Get(scheduleId);
        }

        //[Authorize]
        [Route("Search")]
        [HttpPost]
        public List<LU_Schedule> Search([FromBody]string query)
        {

            return _scheduleManager.Search(query);
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
