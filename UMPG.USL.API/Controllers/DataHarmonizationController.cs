using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Tracing;
using UMPG.USL.API.Business.DataHarmonization;
using UMPG.USL.API.Business.Recs;
using UMPG.USL.Models.DataHarmonization;
using UMPG.USL.Models.LicenseModel;
using UMPG.USL.Models.Security;

namespace UMPG.USL.API.Controllers
{
    [RoutePrefix("api/dataHarmonCTRL/methods")]
    public class DataHarmonizationController : ApiController
    {
        private readonly ITraceWriter _tracer;
        private readonly IDataHarmonizationManager _dataHarmonizationManager;
        private readonly IProductManager _productManager;

        public DataHarmonizationController(IDataHarmonizationManager dataHarmonizationManager, IProductManager productManager)
        {
            _productManager = productManager;
            _dataHarmonizationManager = dataHarmonizationManager;
            _tracer = GlobalConfiguration.Configuration.Services.GetTraceWriter();

        }

        [HttpGet]
        [Route("GetLicenseSnapshot/{licenseId}")]
        public IHttpActionResult GetLicenseSnapshot(int licenseId)
        {
            var exists = _dataHarmonizationManager.DoesSnapshotExist(licenseId);
            if (exists)
            {
                return Ok(_dataHarmonizationManager.GetLicenseSnapshot(licenseId));

            }
            return NotFound();
        }

        [HttpGet]
        [Route("IsSnapshotInProcess/{licenseId}")]
        public IHttpActionResult IsSnapshotInProcess(int licenseId)
        {
            return Ok(_dataHarmonizationManager.IsSnapshotInProcess(licenseId));
        }

        [Route("GetTrackDifferences/{licenseId}")]
        [HttpPost]
        public IList<RecsProductChanges> GetTrackDifferences(List<LicenseProduct> products, int licenseId)
        {
            return _productManager.GetTrackDifferences(products, licenseId);
        }

        [HttpGet]
        [Route("DoesSnapshotExistAndComplete/{licenseId}")]
        public IHttpActionResult DoesSnapshotExistAndComplete(int licenseId)
        {
            return Ok(_dataHarmonizationManager.DoesSnapshotExistAndComplete(licenseId));
        }


        [Route("FindOutOfSyncRecItems/{licenseId}")]
        [HttpPost]
        public IList<RecsProductChanges> FindOutOfSyncRecItems(List<LicenseProduct> products, int licenseId)
        {
            return _productManager.FindOutOfSyncRecItems(products, licenseId);
        }
    }
}
