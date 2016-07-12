using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.Models.Recs;

namespace UMPG.USL.API.Business.Recs
{
    public interface IAutosuggestManager
    {
        ListResult<ArtistRecs> Artist(string query);
        ListResult<AlbumSkinny> Product(AlbumAutosuggestRequest request);
        ListResult<TrackRecs> Track(TrackAutosuggestRequest request);
        ListResult<WorksSearchResult> Work(WorksSearchRequest request);
        List<LabelGroup> RetrieveLabelGroups(string query);
        List<VersionType> GetVersionTypes();


    }
}
