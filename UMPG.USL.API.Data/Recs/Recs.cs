using Castle.Core;
using System;
using System.Collections.Generic;
using UMPG.USL.Common.Mappers;
using UMPG.USL.Common.Transport;
using UMPG.USL.Models;
using UMPG.USL.Models.Security;
using System.Linq;
using UMPG.USL.Models.ProductSearchModel;
using UMPG.USL.API.Data.Configuration;
using UMPG.USL.Models.Recs;

namespace UMPG.USL.API.Data.Recs
{
    public class Recs : IRecs
    {
        private readonly IRecsRequestHandler _recsRequestHandler;
        private readonly IMappingsManager _mappingsManager;
        private readonly IRecsConfigurationRetriever _recsConfigurationRetriever;

        public Recs(IRecsRequestHandler recsRequestHandler, IMappingsManager mappingsManager, IRecsConfigurationRetriever recsConfigurationRetriever)
        {
            _recsRequestHandler = recsRequestHandler;
            _mappingsManager = mappingsManager;
            _recsConfigurationRetriever = recsConfigurationRetriever;
        }

        public List<WorksWriter> RetrieveWriters(string pipsCode)
        {
            string countryCode = "US2";
            var url = string.Format("{0}/http/work/composers/{1}/{2}", _recsConfigurationRetriever.RecsConfiguration.UnSecureUrl, pipsCode, countryCode);
            var writers =  _recsRequestHandler.Get<List<WorksWriter>>(url);
            foreach (var worksWriter in writers)
            {
                worksWriter.Contribution = (float?) GetContribution(worksWriter);
            }
            return writers;
        }

        public ProductHeader RetrieveProductHeader(int productId)
        {
            var url = string.Format("{0}/http/recs/product/header/{1}", _recsConfigurationRetriever.RecsConfiguration.UnSecureUrl, productId);
            return _recsRequestHandler.Get<ProductHeader>(url);
        }

        public List<RecsProductSummary> ProductSummary(List<long> productIds)
        {
            var url = string.Format("{0}/http/recs/product/summary?productId={1}", _recsConfigurationRetriever.RecsConfiguration.UnSecureUrl, string.Join(",", productIds));
            return _recsRequestHandler.Get<List<RecsProductSummary>>(url);
        }

        public List<LabelGroup> RetrieveLabelGroups(string query)
        {
            var url = string.Format("{0}/http/labelGroupAutoSuggest", _recsConfigurationRetriever.RecsConfiguration.UnSecureUrl);
            var response =  _recsRequestHandler.Post<ListResult<LabelGroup>>(url, new Dictionary<string, object> { { "input", new { queryString = query } } });
            return response.Values;
        }

        public bool UpdateProductPriority(UpdatePriorityRequest request, string safeIdHeader)
        {
            var url = string.Format("{0}/http/recs/product/updateProductPriority", _recsConfigurationRetriever.RecsConfiguration.SecureUrl);
            
            var response = _recsRequestHandler.PostSecureGetResponseWithStreamWithHeader(url, request, safeIdHeader);
            //return response;
            return true;

        }

        public List<WorksRecording> RetrieveTracksWithWriters(int productId)
        {
            string countryCode = "US2";
            var url = string.Format("{0}/http/recs/product/trackListingWithWriters/{1}/{2}", _recsConfigurationRetriever.RecsConfiguration.UnSecureUrl, productId, countryCode);
            return _recsRequestHandler.Get<List<WorksRecording>>(url);
        }

        public List<WorksRecording> RetrieveTracks(int productId)
        {
            string countryCode = "US2";
            var url = string.Format("{0}/http/recs/product/trackListing/{1}/{2}", _recsConfigurationRetriever.RecsConfiguration.UnSecureUrl, productId, countryCode);

            var recs_tracks = _recsRequestHandler.Get<List<WorksRecording>>(url);

             return recs_tracks;
        }

        public List<RecsConfigurations> RetrieveConfigurations()
        {
            var url = string.Format("{0}/retrieveConfigurations", _recsConfigurationRetriever.RecsConfiguration.WorksUnSecureUrl);
            return _recsRequestHandler.Get<List<RecsConfigurations>>(url);
        }

        public ProductHeader UpdateProduct(object updateProductObject)
        {
            var url = string.Format("{0}/http/recs/product", _recsConfigurationRetriever.RecsConfiguration.SecureUrl);
            return _recsRequestHandler.PostSecure<ProductHeader>(url, updateProductObject);
        }

        public HttpWebResponseWithStream AddProduct(object updateProductObject)
        {
            var url = string.Format("{0}/http/recs/product", _recsConfigurationRetriever.RecsConfiguration.SecureUrl);
            return _recsRequestHandler.PostSecureGetResponseWithStream(url, updateProductObject);
        }

        public HttpWebResponseWithStream AddProductWithHeader(object updateProductObject, string safeIdHeader)
        {
            var url = string.Format("{0}/http/recs/product", _recsConfigurationRetriever.RecsConfiguration.SecureUrl);
            return _recsRequestHandler.PostSecureGetResponseWithStreamWithHeader(url, updateProductObject, safeIdHeader);
        }


        public ListResult<ArtistRecs> ArtistAutosuggest(string query)
        {
            var url = string.Format("{0}/http/artistAutoSuggest", _recsConfigurationRetriever.RecsConfiguration.UnSecureUrl);
            return _recsRequestHandler.Post<ListResult<ArtistRecs>>(url, new Dictionary<string, object> { { "input", new { query = query } } });
        }

        public ListResult<AlbumSkinny> AlbumAutosuggest(AlbumAutosuggestRequest request)
        {
            var url = string.Format("{0}/http/albumAutoSuggest", _recsConfigurationRetriever.RecsConfiguration.UnSecureUrl);
            return _recsRequestHandler.Post<ListResult<AlbumSkinny>>(url, new Dictionary<string, object> { { "input", new { query = request.query, artistId = request.artistId, locationCode = request.siteLocationCode, request.filterMusicOwner } } });
        }

        public ListResult<TrackRecs> TrackAutosuggest(TrackAutosuggestRequest request)
        {
            var url = string.Format("{0}/http/trackAutoSuggest", _recsConfigurationRetriever.RecsConfiguration.UnSecureUrl);
            return _recsRequestHandler.Post<ListResult<TrackRecs>>(url, new Dictionary<string, object> { { "input", new { query = request.query, artistId = request.artistId, albumId = request.albumId, locationCode = request.siteLocationCode, request.filterMusicOwner } } });
        }

        public List<RecordLabel> RetrieveLabels()
        {
            var url = string.Format("{0}/http/retrieveLabels", _recsConfigurationRetriever.RecsConfiguration.UnSecureUrl);
            return _recsRequestHandler.Get<List<RecordLabel>>(url);
        }

        public ListResult<WorksSearchResult> WorksSearch(WorksSearchRequest request)
        {
            var url = string.Format("{0}/http/worksSearch?q={1}&rows=40", _recsConfigurationRetriever.RecsConfiguration.UnSecureUrl,request.title);
            return _recsRequestHandler.Get<ListResult<WorksSearchResult>>(url);
        }

        public SingleResult<Track> RetrieveTrack(long trackId, CallerInfo callerInfo)
        {
            var url = string.Format("{0}/http/retrieveTrack", _recsConfigurationRetriever.RecsConfiguration.UnSecureUrl);
            SingleResult<Track> response = _recsRequestHandler
                .Post<SingleResult<Track>>(url, new Dictionary<string, object> { 
                    { "input", new { id = trackId, userId = string.IsNullOrEmpty(callerInfo.SafeUserId) ? "0" : callerInfo.SafeUserId,siteAcquisitionLocation= string.IsNullOrEmpty(callerInfo.SiteLocationCode)?"":callerInfo.SiteLocationCode} } });

            if (response != null && response.Success && response.Result != null && response.Result.Rights != null && response.Result.Rights.Contains(callerInfo.SiteLocationCode))
            {
                return response;
            }
            else
            {
                return null;
            }
        }

        public HttpWebResponseWithStream SaveProductLink(ProductLink productLink)
        {
            var url = string.Format("{0}/http/recs/product/saveProductLink", _recsConfigurationRetriever.RecsConfiguration.SecureUrl);
            return _recsRequestHandler.PostSecureGetResponseWithStream(url, productLink);
        }

        public HttpWebResponseWithStream SaveProductLinkWithHeader(ProductLink productLink, string header)
        {
            var url = string.Format("{0}/http/recs/product/saveProductLink", _recsConfigurationRetriever.RecsConfiguration.SecureUrl);
            return _recsRequestHandler.PostSecureGetResponseWithStreamWithHeader(url, productLink, header);
        }


        public List<GetProductLink> GetProductLinks(int productId)
        {

            var locationCode = "US2";  // need to add this as a parameter

            var url = string.Format("{0}/http/recs/product/trackListing/{1}/{2}", _recsConfigurationRetriever.RecsConfiguration.UnSecureUrl, productId, locationCode);
            return _recsRequestHandler.Get<List<GetProductLink>>(url);
        }

        public HttpWebResponseWithStream RemoveProductLink(ProductLink productLink)
        {
            var url = string.Format("{0}/http/recs/product/removeProductLink", _recsConfigurationRetriever.RecsConfiguration.SecureUrl);
            return _recsRequestHandler.PostSecureGetResponseWithStream(url, new { productLinkId = productLink.id, databaseVersion = productLink.databaseVersion });

        }

        public List<Label> GetLabels()
        {
            var url = string.Format("{0}/http/retrieveLabels", _recsConfigurationRetriever.RecsConfiguration.UnSecureUrl);
            return _recsRequestHandler.Get<List<Label>>(url);
        }

        public List<Publisher> GetPublishers(string query)
        {
            var url = string.Format("{0}/http/originalPublisherAutoSuggest", _recsConfigurationRetriever.RecsConfiguration.UnSecureUrl);
            var lreturn = _recsRequestHandler.Post<ListResult<Publisher>>(url, new Dictionary<string, object> { { "input", new { query = query } } });
            return lreturn.Values;
        }

        public List<Models.Recs.Configuration> GetRecsConfigurations()
        {
            var url = string.Format("{0}/http/retrieveConfigurations", _recsConfigurationRetriever.RecsConfiguration.UnSecureUrl);
            return _recsRequestHandler.Get<List<Models.Recs.Configuration>>(url);
        }

        public List<Models.Recs.VersionType> GetVersionTypes()
        {
            var url = string.Format("{0}/http/retrieveVersionTypes", _recsConfigurationRetriever.RecsConfiguration.UnSecureUrl);
            return _recsRequestHandler.Get<List<Models.Recs.VersionType>>(url);
        }

        public ListResult<Track> RetrieveTracks(List<long> tracksIds, string safeId)
        {
            var input = new TracksInput { TracksIds = tracksIds.Select(x => (int)x).ToList(), UserId = safeId, SiteAcquisitionLocation = "US2" };
            var url = string.Format("{0}/http/retrieveTracks", _recsConfigurationRetriever.RecsConfiguration.UnSecureUrl);

            ListResult<Track> response = _recsRequestHandler.Post<ListResult<Track>>(url, new Dictionary<string, object> { { "input", input } });

            if (response != null && response.Success && response.Values != null && response.Values.Count > 0)
            {
                List<Track> tracks = new List<Track>();

                foreach (var track in response.Values)
                {
                    if (track.Rights != null && track.Rights.Count > 0 && track.Rights.Contains("US2"))
                    {
                        tracks.Add(track);
                    }
                }

                response.Values = tracks;
            }
            return response;
        }

        private double GetContribution(WorksWriter writer)
        {
            double contribution = (from originalPublisher in writer.OriginalPublishers from writerBase in originalPublisher.Administrator where writerBase.Controlled select writerBase).Aggregate<WriterBase, double>(0, (current, writerBase) => current + (double)writerBase.MechanicalCollectablePercentage);
            return writer.OriginalPublishers.Where(originalPublisher => originalPublisher.Controlled).Aggregate(contribution, (current, originalPublisher) => current + (double)originalPublisher.MechanicalCollectablePercentage);
        }
    }
}