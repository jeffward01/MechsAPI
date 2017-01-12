using System.Collections.Generic;
using UMPG.USL.Models;
using UMPG.USL.Models.ProductSearchModel;
using UMPG.USL.Models.Recs;
using UMPG.USL.Models.Security;

namespace UMPG.USL.API.Data.Recs
{
    public interface IRecsDataProvider
    {
        List<WorksRecording> RetrieveProductRecordings(int productId);
        HttpWebResponseWithStream AddProductWithHeader(object updateProductObject, string header);
        List<WorksWriter> RetrieveTrackWriters(string pipsCode);
        ProductHeader RetrieveProductHeader(int productId);
        List<RecsConfigurations> RetrieveConfigurations();
        ProductHeader UpdateProduct(object updateProductObject);
        HttpWebResponseWithStream AddProduct(object updateProductObject);
        ListResult<ArtistRecs> ArtistAutosuggest(string query);
        ListResult<AlbumSkinny> AlbumAutosuggest(AlbumAutosuggestRequest request);
        ListResult<TrackRecs> TrackAutosuggest(TrackAutosuggestRequest request);
        List<WorksRecording> RetrieveTracks(int productId);
        List<RecordLabel> RetrieveLabels();
        ListResult<WorksSearchResult> WorksSearch(WorksSearchRequest request);
        SingleResult<Track> RetrieveTrack(long trackId, CallerInfo callerInfo);
        HttpWebResponseWithStream SaveProductLink(ProductLink productLink);
        HttpWebResponseWithStream SaveProductLinkWithHeader(ProductLink productLink, string header);
        List<GetProductLink> GetProductLinks(int productId);
        HttpWebResponseWithStream RemoveProductLink(ProductLink productLink);
        List<Label> GetLabels();
        List<Publisher> GetPublshers(string query);
        List<Models.Recs.Configuration> GetRecsConfigurations();
        ListResult<Track> RetrieveTracks(List<long> tracksIds, string safeId);
        List<LabelGroup> RetrieveLabelGroups(string query);

        List<WorksRecording> RetrieveTracksWithWriters(int productId);
        bool UpdateProductPriority(UpdatePriorityRequest request, string safeIdHeader);
        List<VersionType> GetVersionTypes();

    }
}