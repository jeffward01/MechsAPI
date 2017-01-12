using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using UMPG.USL.API.Business;

namespace UMPG.USL.API.Controllers
{
    [RoutePrefix("api/serviceController")]
    public class ServiceController : ApiController
    {

         private readonly IServiceManager _repo;

        public ServiceController(IServiceManager serviceManager)
        {
            _repo = serviceManager;
        }

   
        [Route("RestartDataHarmonizingProcessor")]
        [HttpGet]
        public IHttpActionResult RestartDataHarmonizingProcessor()
        {
            return Ok(_repo.RestartService("DataHarmonizationProcessor"));
        }


        [Route("RestartSolrProcessor")]
        [HttpGet]
        public IHttpActionResult RestartSolrProcessor()
        {
            return Ok(_repo.RestartService("SolrProcessor"));
        }

        [Route("RestartLicenseProcessor")]
        [HttpGet]
        public IHttpActionResult RestartLicenseProcessor()
        {
            return Ok(_repo.RestartService("LicenseProcessor"));
        }

        [Route("GetAllProcessorStatus")]
        [HttpGet]
        public IHttpActionResult GetAllProcessorStatus()
        {
            return Ok(_repo.GetAllProcessorStatus());
        }

        




        //TODO: Possibly remove impersonate from web.config and put in application:https://weblogs.asp.net/kaushal/start-stop-window-service-from-asp-net-page
        //Reasons: No need to give permissions ALL the time.  We can give permissions for lifetime of request instead.

    }
}
