using System.Collections.Generic;
using System.Web.Http;
using UMPG.USL.API.Business.Licenses;
using UMPG.USL.API.Data.ContactData;
using UMPG.USL.Models;
using UMPG.USL.Models.LicenseModel;
using UMPG.USL.Models.Recs;

namespace UMPG.USL.API.Controllers.LicenseCTRL
{
    [RoutePrefix("api/licenseCTRL/licensees")]
    public class LicenseeController : ApiController
    {
        private readonly ILicenseeManager _licenseeManager;
        public LicenseeController(ILicenseeManager licenseeManager)
        {
            _licenseeManager = licenseeManager;
        }
        //[Authorize]
        [Route("")]
        [HttpGet]
        [ActionName("GetAll")]
        public List<Licensee> Get()
        {
            return _licenseeManager.GetAll();
        }

        //[Authorize]
        [Route("Search")]
        [HttpPost]
        public List<Licensee> Search([FromBody]string query)
        {

            return _licenseeManager.Search(query);
        }

        [Route("PagedLicenees")]
        [HttpPost]
        public PagedResponse<Licensee> GetLicensees(LicenseeAdminRequest request)
        {

            return _licenseeManager.GetLicensees(request);
        }

        [Route("AddLicensee")]
        [HttpPost]
        public Licensee AddLicensee(AddLicenseeRequest request)
        {
            return _licenseeManager.AddLicensee(request);
        }

        [Route("EditLicensee")]
        [HttpPost]
        public Licensee AddLicensee(Licensee request)
        {
            return _licenseeManager.EditLicensee(request);
        }

        [Route("DeleteLicensee")]
        [HttpPost]
        public bool DeleteLicensee(Licensee request)
        {
            return _licenseeManager.DeleteLicensee(request);
        }


    }


    #region Helpers



    #endregion
}
