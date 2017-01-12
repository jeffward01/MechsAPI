using System.Collections.Generic;
using UMPG.USL.Models;
using UMPG.USL.Models.Security;
using UMPG.USL.Models.ProductSearchModel;
using UMPG.USL.Models.Recs;

namespace UMPG.USL.API.Data.Recs
{
    public interface IRecs
    {
        List<WorksRecording> RetrieveTracks(int productId);
        List<WorksRecording> RetrieveTracksWithWriters(int productId);
        List<WorksWriter> RetrieveWriters(string pipsCode);
        ProductHeader RetrieveProductHeader(int productId);
        List<RecsConfigurations> RetrieveConfigurations();
        ProductHeader UpdateProduct(object request);
        HttpWebResponseWithStream AddProduct(object updateProductObject);
        HttpWebResponseWithStream AddProductWithHeader(object updateProductObject, string header);
        ListResult<ArtistRecs> ArtistAutosuggest(string query);
        ListResult<AlbumSkinny> AlbumAutosuggest(AlbumAutosuggestRequest request);
        HttpWebResponseWithStream SaveProductLinkWithHeader(ProductLink productLink, string header);
        ListResult<TrackRecs> TrackAutosuggest(TrackAutosuggestRequest request);
        List<RecordLabel> RetrieveLabels();
        ListResult<WorksSearchResult> WorksSearch(WorksSearchRequest request);
        SingleResult<Track> RetrieveTrack(long trackId, CallerInfo callerInfo);
        HttpWebResponseWithStream SaveProductLink(ProductLink productLink);
        List<GetProductLink> GetProductLinks(int productId);
        HttpWebResponseWithStream RemoveProductLink(ProductLink productLink);
        List<Label> GetLabels();
        List<Publisher> GetPublishers(string query);
        List<Models.Recs.Configuration> GetRecsConfigurations();
        ListResult<Track> RetrieveTracks(List<long> tracksIds, string safeId);
        List<RecsProductSummary> ProductSummary(List<long> productIds);
        List<LabelGroup> RetrieveLabelGroups(string query);
        bool UpdateProductPriority(UpdatePriorityRequest request, string safeIdHeader);
        List<Models.Recs.VersionType> GetVersionTypes();

    }
}