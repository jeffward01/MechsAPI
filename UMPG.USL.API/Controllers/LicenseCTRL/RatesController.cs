using System.Collections.Generic;
using System.Web.Http;
using UMPG.USL.API.Business.Licenses;
using UMPG.USL.Models.LicenseModel;

namespace UMPG.USL.API.Controllers.LicenseCTRL
{
    [RoutePrefix("api/licenseCTRL/Rates")]
    public class RatesController : ApiController
    {
        private readonly ILicensePRWriterRateManager _ratesManager ;
        public RatesController(ILicensePRWriterRateManager ratesManager)
        {
            _ratesManager = ratesManager;
        }
        //[Authorize]
        [Route("GetRates")]
        [HttpPost]
        [ActionName("GetRates")]
        public List<LicenseProductRecordingWriterRate> GetRates(GetWritersRatesRequest request)
        {
            return _ratesManager.GetEditWriterRates(request);
        }

 
    }


    #region Helpers



    #endregion
}
