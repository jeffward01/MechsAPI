using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using UMPG.USL.API.Business.LookUps;
using UMPG.USL.Models.LookupModel;

namespace UMPG.USL.API.Controllers.LookUpCTRL
{
    [RoutePrefix("api/LookUpCTRL/LicenseTypes")]
    public class LicenseTypeController:ApiController
    {
        private readonly ILicenseTypeManager _licenseTypeManager;
        public LicenseTypeController(ILicenseTypeManager licenseTypeManager)
        {
            _licenseTypeManager = licenseTypeManager;
        }
        //[Authorize]
        [Route("")]
        [HttpGet]
        [ActionName("GetAll")]
        public List<LU_LicenseType> Get()
        {
            return _licenseTypeManager.GetAll();
        }
    }
}