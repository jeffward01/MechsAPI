using System.Collections.Generic;
using System.Web.Http;
using System.Web.Script.Serialization;
using UMPG.USL.API.Business.Recs;
using UMPG.USL.API.Business.Licenses;
using UMPG.USL.Models;
using UMPG.USL.Models.Recs;
using UMPG.USL.Models.LicenseModel;

namespace UMPG.USL.API.Controllers.RECsCTRL
{
    [RoutePrefix("api/RECsCTRL/Autosuggests")]
    public class AutosuggestsController : ApiController
    {
        private readonly IAutosuggestManager _autosuggestManager ;
        public AutosuggestsController(IAutosuggestManager autosuggestManager)
        {
            _autosuggestManager = autosuggestManager;
        }

        [Route("Artist")]
        [HttpPost]
        public ListResult<ArtistRecs> Artist([FromBody]string query)
        {
            return _autosuggestManager.Artist(query);
        }

        [Route("Product")]
        [HttpPost]
        public ListResult<AlbumSkinny> Product(AlbumAutosuggestRequest request)
        {
            return _autosuggestManager.Product(request);
        }

        [Route("Track")]
        [HttpPost]
        public ListResult<TrackRecs> Product(TrackAutosuggestRequest request)
        {
            return _autosuggestManager.Track(request);
        }
        
        
        [Route("Work")]
        [HttpPost]
        public ListResult<WorksSearchResult> Work(WorksSearchRequest request)
        {
            return _autosuggestManager.Work(request);
        }

        [Route("LabelGroups")]
        [HttpPost]
        public List<LabelGroup> GetLabelGroups([FromBody]string query)
        {
            return _autosuggestManager.RetrieveLabelGroups(query);
        }
        [Route("GetVersionTypes")]
        [HttpGet]
        public List<VersionType> GetVersionTypes()
        {
            return _autosuggestManager.GetVersionTypes();
        }

    }
}