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
    [RoutePrefix("api/LookUpCTRL/priorities")]
    public class PrioritiesController : ApiController
    {
        private readonly IPriorityManager _priorityManager;
        public PrioritiesController(IPriorityManager priorityManager)
        {
            _priorityManager = priorityManager;
        }
        //[Authorize]
        [Route("")]
        [HttpGet]
        [ActionName("GetAll")]
        public List<LU_Priority> Get()
        {
           return _priorityManager.GetAll();
        }

        [Route("GetPriority/{priorityId}")]
        [HttpGet]
        public LU_Priority GetPriority(int priorityId)
        {
            return _priorityManager.Get(priorityId);
        }

        //[Authorize]
        [Route("Search")]
        [HttpPost]
        public List<LU_Priority> Search([FromBody]string query)
        {
            
            return _priorityManager.Search(query);
        }

        //[HttpPost]
        //[ActionName("Add")]
        //public LU_Priority Add(LU_Priority priority)
        //{
        //    return _priorityManager.Add(priority);
        //}

    }


    #region Helpers



    #endregion
}
