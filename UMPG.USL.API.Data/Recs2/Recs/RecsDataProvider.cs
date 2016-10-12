using Castle.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using UMPG.USL.API.Data;
using UMPG.USL.Common.Mappers;
using UMPG.USL.Common;
using UMPG.USL.Models;
using UMPG.USL.Models.ProductSearchModel;
using UMPG.USL.Models.Recs;
using UMPG.USL.Models.Security;

namespace UMPG.USL.API.Data.Recs
{    
    public class RecsDataProvider: IRecsDataProvider
    {
        private readonly IRecs _recs;
        private readonly IMappingsManager _mappingsManager;
        public RecsDataProvider(IRecs recs, IMappingsManager mappingsManager)
        {
            _recs = recs;
            _mappingsManager = mappingsManager;
        }

        public List<WorksWriter> RetrieveTrackWriters(string pipsCode)
        {
            return _recs.RetrieveWriters(pipsCode);
            //return worksWriters.Select(w => _mappingsManager.Map<Writer, WorksWriter>(w)).ToList();
        }

        public ProductHeader RetrieveProductHeader(int productId)
        {
            var product = _recs.RetrieveProductHeader(productId);
            return product;
        }

        public List<WorksRecording> RetrieveProductRecordings(int productId)
        {
            return _recs.RetrieveTracks(productId);
            //return tracks.Select(t => _mappingsManager.Map<Recording, WorksRecording>(t)).ToList();
        }

        public List<RecsConfigurations> RetrieveConfigurations()
        {
            var configs = _recs.RetrieveConfigurations();
            return configs;
        }

        public ProductHeader UpdateProduct(object updateProductObject)
        {
            return _recs.UpdateProduct(updateProductObject);
        }

        public HttpWebResponseWithStream AddProduct(object updateProductObject)
        {
            return _recs.AddProduct(updateProductObject);
        }

        public ListResult<ArtistRecs> ArtistAutosuggest(string query)
        {
            return _recs.ArtistAutosuggest(StringHelper.EncodeSpecialCharactersOnly(query));
        }

        public ListResult<AlbumSkinny> AlbumAutosuggest(AlbumAutosuggestRequest request)
        {
            request.query = StringHelper.EncodeSpecialCharactersOnly(request.query);
            return _recs.AlbumAutosuggest(request);
        }

        public ListResult<TrackRecs> TrackAutosuggest(TrackAutosuggestRequest request)
        {
            request.query = StringHelper.EncodeSpecialCharactersOnly(request.query);
            return _recs.TrackAutosuggest(request);
        }

        public List<WorksRecording> RetrieveTracks(int productId)
        {
            return _recs.RetrieveTracks(productId);
        }

        public List<RecordLabel> RetrieveLabels()
        {
            return _recs.RetrieveLabels();
        }

        public ListResult<WorksSearchResult> WorksSearch(WorksSearchRequest request)
        {
            return _recs.WorksSearch(request);
        }

        public SingleResult<Track> RetrieveTrack(long trackId, CallerInfo callerInfo)
        {
            return _recs.RetrieveTrack(trackId, callerInfo);
        }    

        public HttpWebResponseWithStream SaveProductLink(ProductLink productLink)
        {
            return _recs.SaveProductLink(productLink);
        }

        public List<GetProductLink> GetProductLinks(int productId)
        {
            return _recs.GetProductLinks(productId);
        }

        public HttpWebResponseWithStream RemoveProductLink(ProductLink productLink)
        {
            return _recs.RemoveProductLink(productLink);
        }

        public List<Label> GetLabels()
        {
            return _recs.GetLabels();
        }

        public List<Publisher> GetPublshers(string query)
        {
            return _recs.GetPublishers(query);
        }

        public List<Models.Recs.Configuration> GetRecsConfigurations()
        {
            return _recs.GetRecsConfigurations();
        }

        public ListResult<Track> RetrieveTracks(List<long> tracksIds, string safeId)
        {
            return _recs.RetrieveTracks(tracksIds, safeId);
        }

        public List<LabelGroup> RetrieveLabelGroups(string query)
        {
            return _recs.RetrieveLabelGroups(query);
        }

        public List<WorksRecording> RetrieveTracksWithWriters(int productId)
        {
            return _recs.RetrieveTracksWithWriters(productId);
        }

        public bool UpdateProductPriority(UpdatePriorityRequest request)
        {
            return _recs.UpdateProductPriority(request);
        }

        public List<VersionType> GetVersionTypes()
        {
            return _recs.GetVersionTypes();
        }
    }

}