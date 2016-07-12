using System.Collections.Generic;
using System.Web.Http;
using System.Web.Script.Serialization;
using UMPG.USL.API.Business.Recs;
using UMPG.USL.API.Business.Licenses;
using UMPG.USL.Models;
using UMPG.USL.Models.Recs;
using UMPG.USL.Models.LicenseModel;

namespace UMPG.USL.API.Controllers.RECsCTRL
{
    [RoutePrefix("api/RECsCTRL/Configurations")]
    public class ConfigurationsController : ApiController
    {
        private readonly IConfigurationManager _configurationManager ;
        public ConfigurationsController(IConfigurationManager configurationManager)
        {
            _configurationManager = configurationManager;
        }

        /// <summary>
        /// New method to get all recs_configurations from recs
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [HttpGet]
        [ActionName("GetConfigurations")]
        public List<RecsConfigurations> Get()
        {
            return _configurationManager.GetConfigurations();
        }

    }
}