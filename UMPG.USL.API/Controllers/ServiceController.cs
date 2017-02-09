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

        [Route("RestartProcessor/{processorName}")]
        [HttpGet]
        public IHttpActionResult RestartProcessor(string processorName)
        {
            return Ok(_repo.RestartService(processorName));
        }
        
        [Route("ClearRestartInProcess")]
        [HttpGet]
        public IHttpActionResult ClearRestartInProcess()
        {
            return Ok(_repo.ClearRestartInProcess());
        }

        [Route("GetAllProcessorStatus")]
        [HttpGet]
        public IHttpActionResult GetAllProcessorStatus()
        {
            return Ok(_repo.GetAllProcessorStatus());
        }
        
        [Route("GetProcessorStatus/{processorName}")]
        [HttpGet]
        public IHttpActionResult GetAllProcessorStatus(string processorName)
        {
            return Ok(_repo.GetServiceInformation(processorName));
        }

        [Route("GetRestartInProgress")]
        [HttpGet]
        public IHttpActionResult GetRestartInProgress()
        {
            return Ok(_repo.GetRestartInProgress());
        }

        [Route("TestRestartProcessor/{processorName}")]
        [HttpGet]
        public IHttpActionResult TestRestartProcessor(string processorName)
        {
            return Ok(_repo.TestStartRemoteService(processorName));
        }


        [Route("IsAdmin/{contactId}")]
        [HttpGet]
        public IHttpActionResult IsAdmin(int contactId)
        {
            return Ok(_repo.IsAdmin(contactId));
        }


        [Route("SolrFailedItems")]
        [HttpGet]
        public IHttpActionResult SolrFailedItems()
        {
            return Ok(_repo.GetAllFailedSolrItems());
        }


        [Route("SolrUnProcessedItems")]
        [HttpGet]
        public IHttpActionResult SolrUnProcessedItems()
        {
            return Ok(_repo.GetAllUnProcessedSolrItems());
        }


        [Route("LicenseUnProcessedItems")]
        [HttpGet]
        public IHttpActionResult LicenseUnProcessedItems()
        {
            return Ok(_repo.GetUnProcessedLicenseItems());
        }

        [Route("LicenseFailedItems")]
        [HttpGet]
        public IHttpActionResult LicenseFailedItems()
        {
            return Ok(_repo.GetAllFailedLicenseItems());
        }


        [Route("DataHarmonizingUnProcessedItems")]
        [HttpGet]
        public IHttpActionResult DataHarmonizingUnProcessedItems()
        {
            return Ok(_repo.GetAllUnProcessedDataHarmonizationItems());
        }


        [Route("DataHarmonizingFailedItems")]
        [HttpGet]
        public IHttpActionResult DataHarmonizingFailedItems()
        {
            return Ok(_repo.GetAllFailedDataHarmonizationItems());
        }



        [Route("SetAllUnProcessedSolrItemsToPending")]
        [HttpGet]
        public IHttpActionResult SetAllUnProcessedSolrItemsToPending()
        {
            _repo.SetAllUnProcessedSolrItemsToPending();
            return Ok();
        }


        [Route("DeleteAllUnProcessedSolrItems")]
        [HttpGet]
        public IHttpActionResult DeleteAllUnProcessedSolrItems()
        {
            _repo.DeleteAllUnProcessedSolrItems();
            return Ok();
        }


        [Route("SetSingleUnProcessedSolrItemToPending/{solrIndexId}")]
        [HttpPost]
        public IHttpActionResult SetSingleUnProcessedSolrItemToPending(int solrIndexId)
        {
            _repo.SetIndividualSolrItemToPending(solrIndexId);
            return Ok();
        }


        [Route("DeleteSingleUnProcessedSolrItem/{solrIndexId}")]
        [HttpPost]
        public IHttpActionResult DeleteSingleUnProcessedSolrItem(int solrIndexId)
        {
            _repo.DeleteIndividualSolrItem(solrIndexId);
            return Ok();
        }


        [Route("SetAllUnProcessedDHItemsToPending")]
        [HttpGet]
        public IHttpActionResult SetAllUnProcessedDHItemsToPending()
        {
            _repo.SetAllUnProcessedDhItemsToPending();
            return Ok();
        }


        [Route("DeleteAllUnProcessedDHItems")]
        [HttpGet]
        public IHttpActionResult DeleteAllUnProcessedDHItems()
        {
            _repo.DeleteAllUnProcessedDhItems();
            return Ok();
        }


        [Route("SetSingleUnProcessedDHItemToPending/{DHIndexId}")]
        [HttpPost]
        public IHttpActionResult SetSingleUnProcessedDHItemToPending(int DHIndexId)
        {
            _repo.SetIndividualDHItemToPending(DHIndexId);
            return Ok();
        }


        [Route("DeleteAllFailedLicenseItems")]
        [HttpGet]
        public IHttpActionResult DeleteAllFailedLicenseItems()
        {
            _repo.DeleteAllFailedLicenseItems();
            return Ok();
        }

        [Route("DeleteAllUnProcessedLicenseItems")]
        [HttpGet]
        public IHttpActionResult DeleteAllUnProcessedLicenseItems()
        {
            _repo.DeleteAllUnProcessedLicenseItems();
            return Ok();
        }


        [Route("SetSingleUnProcessedLicenseItemToPending/{LicenseIndexId}")]
        [HttpPost]
        public IHttpActionResult SetSingleUnProcessedLicenseItemToPending(int LicenseIndexId)
        {
            _repo.SetIndividualDHItemToPending(LicenseIndexId);
            return Ok();
        }


        [Route("DeleteSingleUnProcessedLicenseItem/{LicenseIndexId}")]
        [HttpPost]
        public IHttpActionResult DeleteSingleUnProcessedLicenseItem(int LicenseIndexId)
        {
            _repo.DeleteSingleLicenseItem(LicenseIndexId);
            return Ok();
        }


        [Route("DeleteAllFailedSolrItems")]
        [HttpPost]
        public IHttpActionResult DeleteAllFailedSolrItems()
        {
            _repo.DeleteAllFailedSolrItems();
            return Ok();
        }

        [Route("SetAllFailedSolrItemsToPending")]
        [HttpPost]
        public IHttpActionResult SetAllFailedSolrItemsToPending()
        {
            _repo.SetAllFailedSolrItemsToPending();
            return Ok();
        }


        [Route("DeleteSingleDhItem/{itemId}")]
        [HttpPost]
        public IHttpActionResult DeleteSingleDhItem(int itemId)
        {
            _repo.DeleteSingleDhItem(itemId);
            return Ok();
        }



        [Route("SetAllFailedDhItemsToPending")]
        [HttpPost]
        public IHttpActionResult DeleteSingleDhItem()
        {
            _repo.SetAllFailedDhItemsToPending();
            return Ok();
        }






        //TODO: Possibly remove impersonate from web.config and put in application:https://weblogs.asp.net/kaushal/start-stop-window-service-from-asp-net-page
        //Reasons: No need to give permissions ALL the time.  We can give permissions for lifetime of request instead.

    }
}
