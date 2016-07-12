using System.Collections.Generic;
using System.Web.Http;
using UMPG.USL.API.Business.Licenses;
using UMPG.USL.Models;
using UMPG.USL.Models.LicenseModel;
using UMPG.USL.Models.LookupModel;
using UMPG.USL.Models.Recs;

namespace UMPG.USL.API.Controllers.LicenseCTRL
{
    [RoutePrefix("api/licenseCTRL/licenseNote")]
    public class LicenseNoteController : ApiController
    {
        private readonly ILicenseNoteManager _licenseNoteManager ;
        public LicenseNoteController(ILicenseNoteManager licenseNoteManager)
        {
            _licenseNoteManager = licenseNoteManager;
        }
        //[Authorize]

        //[Authorize]
        [Route("Search")]
        [HttpPost]
        public List<LicenseNote> Search([FromBody]string query)
        {
            
            return _licenseNoteManager.Search(query);
        }


        [Route("addLicenseNote")]
        [HttpPost]
        
        public LicenseNote AddLicenseNote(LicenseNoteRequest noteRequest)
        {
            return _licenseNoteManager.Add(noteRequest);
        }


        [Route("GetLicenseNotes/{licenseId}")]
        [HttpGet]
        public List<LicenseNote> GetLicenseNotes(int licenseId)
        {
            return _licenseNoteManager.GetLicenseNotes(licenseId);
        }

        [Route("GetLicenseNote/{licenseNoteId}")]
        [HttpGet]
        public LicenseNote GetLicenseNote(int licenseNoteId)
        {
            return _licenseNoteManager.GetLicenseNote(licenseNoteId);
        }

        [Route("GetLicenseNoteTypes")]
        [HttpGet]
        public List<LU_NoteType> GetLicenseNoteTypes()
        {
            return _licenseNoteManager.GetLicenseNoteTypes();
        }

        [Route("DeleteLicenseNotes")]
        [HttpPost]
        public bool DeleteLicenseNotes(List<int> licenseNoteIds)
        {
            return _licenseNoteManager.DeleteLicenseNotes(licenseNoteIds);
        }

        [Route("EditLicenseNote")]
        [HttpPost]
        public bool EditLicenseNote(LicenseNote licenseNote)
        {
            return _licenseNoteManager.UpdateLicenseNote(licenseNote);
        }

    }


    #region Helpers



    #endregion
}
