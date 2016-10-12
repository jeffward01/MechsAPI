using System.Collections.Generic;
using System.Web.Http;
using UMPG.USL.API.ActionFilters;
using UMPG.USL.API.Business.Recs;
using UMPG.USL.Models.Recs;

namespace UMPG.USL.API.Controllers.RECsCTRL
{
    [RoutePrefix("api/RECsCTRL/Labels")]
    [AuthorizationRequired]
    public class LabelController : ApiController
    {
        private readonly ILabelManager _labelManager ;
        public LabelController(ILabelManager labelManager)
        {
            _labelManager = labelManager;
        }
        //[Authorize]
        [Route("")]
        [HttpGet]
        [ActionName("GetAll")]
        public List<Label> Get()
        {
           return _labelManager.GetAll();
        }

        [Route("GetPublishers")]
        [HttpGet]
        [ActionName("GetPublishers")]
        public List<Publisher> GetPublishers(string query)
        {
            return _labelManager.GetPublishers(query);
        }
        [Route("GetRecsConfigurations")]
        [HttpGet]
        [ActionName("GetRecsConfigurations")]
        public List<Configuration> GetRecsConfigurations()
        {
            return _labelManager.GetRecsConfigurations();
        }
       
    }


    #region Helpers



    #endregion
}
