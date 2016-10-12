using System.Collections.Generic;
using System.Web.Http;
using UMPG.USL.API.ActionFilters;
using UMPG.USL.API.Business.Licenses;
using UMPG.USL.Models;
using UMPG.USL.Models.LicenseModel;
using UMPG.USL.Models.LookupModel;
using UMPG.USL.Models.Recs;

namespace UMPG.USL.API.Controllers.LicenseCTRL
{

    [RoutePrefix("api/licenseCTRL/licensePRWriterNote")]
    [AuthorizationRequired]
    public class LicenseProductWriterNoteController : ApiController
    {
        private readonly ILicenseProductWriterNoteManager _licenseProductWriterNoteManager ;
        public LicenseProductWriterNoteController(ILicenseProductWriterNoteManager licenseProductWriterNoteManager)
        {
            _licenseProductWriterNoteManager = licenseProductWriterNoteManager;
        }
        //[Authorize]

        //[Authorize]
        //[Route("Search")]
        //[HttpPost]
        //public List<LicenseNote> Search([FromBody]string query)
        //{

        //    return _licenseProductWriterNoteManager.Search(query);
        //}


        [Route("addLicenseNote")]
        [HttpPost]
        
        public LicenseProductRecordingWriterNote AddLicenseNote(LicenseWriterNoteRequest noteRequest)
        {
            return _licenseProductWriterNoteManager.Add(noteRequest);
        }

        [Route("editLicenseNote")]
        [HttpPost]

        public LicenseProductRecordingWriterNote EditLicenseNote([FromBody]LicenseWriterNoteRequest noteRequest)
        {
            return _licenseProductWriterNoteManager.Edit(noteRequest);
        }

        [Route("getAllLicenseNote")]
        [HttpPost]

        public List<LicenseProductRecordingWriterNote> GetAllLicenseNote(int licenseWriterId)
        {
            return _licenseProductWriterNoteManager.GetAll(licenseWriterId);
        }

        [Route("removeLicenseNote")]
        [HttpPost]
        public LicenseProductRecordingWriterNote RemoveNote([FromBody]int licenseWriterNoteId)
        {
            return _licenseProductWriterNoteManager.Remove(licenseWriterNoteId);
        }
    }
}