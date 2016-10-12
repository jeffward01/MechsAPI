using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using UMPG.USL.API.ActionFilters;
using UMPG.USL.API.Business.Licenses;
using UMPG.USL.Models.LicenseModel;

namespace UMPG.USL.API.Controllers.LicenseCTRL
{
    [RoutePrefix("api/licenseCTRL/LicenseRecordingMedley")]
    [AuthorizationRequired]
    public class LicenseRecordingMedleyController : ApiController
    {
        private readonly ILicenseRecordingMedleyManager _medleyManager;

        public LicenseRecordingMedleyController(ILicenseRecordingMedleyManager medleyManager)
        {
            _medleyManager = medleyManager;
        }
        
        [Route("AddRecordingMedley")]
        [HttpPost]
        public bool AddRecordingMedley(List<LicenseRecordingMedley> medleysOrSamples)
        {
            _medleyManager.AddMedleys(medleysOrSamples);
            return true;
            
        }

        [Route("GetMedleysByTrackId/{trackId}")]
        [HttpGet]
        public List<LicenseRecordingMedley> GetMedleysByTrackId(int trackId)
        {
            return _medleyManager.GetMedleysByTrackId((long)trackId);
            

        }
    }
}