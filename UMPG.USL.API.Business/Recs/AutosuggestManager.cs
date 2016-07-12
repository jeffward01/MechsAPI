using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.API.Data.LicenseData;
using UMPG.USL.API.Data.Recs;
using UMPG.USL.Models;
using UMPG.USL.Models.LicenseModel;
using UMPG.USL.Models.Recs;


namespace UMPG.USL.API.Business.Recs
{
    public class AutosuggestManager : IAutosuggestManager
    {
        private readonly IRecsDataProvider _recsProvider;

        public AutosuggestManager(IRecsDataProvider recsProvider)
        {
            _recsProvider = recsProvider;
        }

        public ListResult<ArtistRecs> Artist(string query)
        {
            return _recsProvider.ArtistAutosuggest(query);
        }

        public ListResult<AlbumSkinny> Product(AlbumAutosuggestRequest request)
        {
            return _recsProvider.AlbumAutosuggest(request);
        }

        public ListResult<TrackRecs> Track(TrackAutosuggestRequest request)
        {
            return _recsProvider.TrackAutosuggest(request);
        }

        public ListResult<WorksSearchResult> Work(WorksSearchRequest request)
        {
            return _recsProvider.WorksSearch(request);
        }

        public List<LabelGroup> RetrieveLabelGroups(string query)
        {
            return _recsProvider.RetrieveLabelGroups(query);
        }
        public List<VersionType> GetVersionTypes()
        {
            return _recsProvider.GetVersionTypes();
        }
    }
}
