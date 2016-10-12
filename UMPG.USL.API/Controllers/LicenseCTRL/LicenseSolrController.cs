using System.Collections.Generic;
using System.Web.Http;
using UMPG.USL.API.ActionFilters;
using UMPG.USL.API.Business.Licenses;
using UMPG.USL.Models;
using UMPG.USL.Models.LicenseModel;

namespace UMPG.USL.API.Controllers.LicenseCTRL
{
    using System;
    using System.Configuration;
    using System.IO;
    using System.Text;
    using System.Web;
    using System.Web.UI;

    using UMPG.USL.API.Business.Contacts;
    using UMPG.USL.Models.ContactModel;

    [RoutePrefix("api/licenseCTRL/licenseSolr")]
    [AuthorizationRequired]
    public class LicenseSolrController : BaseController
    {
        private readonly ILicenseSolrManager _licenseSolrManager;
        
        public LicenseSolrController(ILicenseSolrManager licenseSolrManager)
        {
            _licenseSolrManager = licenseSolrManager;
        }


        [Route("SolrUpdateLicenseStatus/{licenseId}")]
        [HttpGet]
        public bool SolrUpdateLicenseStatus(int licenseId)
        {
            return _licenseSolrManager.UpdateLicenseStatus(licenseId);
        }

    }


    #region Helpers

    #endregion
}
