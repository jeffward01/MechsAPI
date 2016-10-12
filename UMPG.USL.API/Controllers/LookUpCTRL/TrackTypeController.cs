using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Cors;
using UMPG.USL.API.Business.Licenses;
using UMPG.USL.API.Business.LookUps;
using UMPG.USL.Models.LookupModel;
using UMPG.USL.API;
using UMPG.USL.API.ActionFilters;
using UMPG.USL.API.Business.Lookups;

namespace UMPG.USL.API.Controllers.LookUpCTRL
{
    [RoutePrefix("api/LookUpCTRL/TrackType")]
    [AuthorizationRequired]
    public class TrackTypeController : ApiController
    {
        private readonly ITrackTypeManager _trackTypeManager;
        public TrackTypeController(ITrackTypeManager trackTypeManager)
        {
            _trackTypeManager = trackTypeManager;
        }
        //[Authorize]
        [Route("")]
        [HttpGet]
        [ActionName("GetAll")]
        public List<LU_TrackType> Get()
        {
            return _trackTypeManager.GetAll();
        }

    }


    #region Helpers



    #endregion
}
