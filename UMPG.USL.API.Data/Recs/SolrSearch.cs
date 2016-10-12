using Castle.Core;
using System;
using System.Collections.Generic;
using UMPG.USL.Common.Mappers;
using UMPG.USL.Common.Transport;
using UMPG.USL.Models;
using UMPG.USL.Models.LicenseSearchModel;
using UMPG.USL.Models.Security;
using System.Linq;
using UMPG.USL.Models.ProductSearchModel;
using UMPG.USL.API.Data.Configuration;
using UMPG.USL.Models.Recs;
using UMPG.USL.Models.LicenseModel;

namespace UMPG.USL.API.Data.Recs
{
    public class Solr : ISolrSearch
    {
        private readonly IRecsRequestHandler _recsRequestHandler;
        private readonly IMappingsManager _mappingsManager;
        private readonly ISolrConfigurationRetriever _solrConfigurationRetriever;

        public Solr(IRecsRequestHandler recsRequestHandler, IMappingsManager mappingsManager, ISolrConfigurationRetriever solrConfigurationRetriever)
        {
            _recsRequestHandler = recsRequestHandler;
            _mappingsManager = mappingsManager;
            _solrConfigurationRetriever = solrConfigurationRetriever;
        }

        public ProductSearchResult SearchProducts(ProductRequest searchCriteria)
        {
            var recsSearchCriteria = _mappingsManager.Map<string, ProductRequest>(searchCriteria);

            var url = string.Format("{0}/mechs_product/select?{1}", _solrConfigurationRetriever.RecsConfiguration.UnSecureUrl, recsSearchCriteria);

            return _recsRequestHandler.Get<ProductSearchResult>(url);
        }

        public LicenseSearchResult SearchLicenses(LicenseRequest searchCriteria)
        {
            var recsSearchCriteria = _mappingsManager.Map<string, LicenseRequest>(searchCriteria);
            //"q=7127&qf=trackTitle^30  trackTileExact^30  trackTitlePartial^30  productTitle^90  productTitleExact^90  productTitlePartial^90  licenseTitle^900  licenseTitleExact^900  licenseTitlePartial^900  artistName^270  artistNameExact^270 artistNamePartial^270  writer^10  writerExact^10  writerPartial^10  localClientCode pipsCode upc licenseNumber-writerRate:*&rows=10&start=0"
           // string recsSearchCriteria =
           //     "q=7127&qf=licenseNumber trackTitle^30  trackTileExact^30  trackTitlePartial^30  productTitle^90  productTitleExact^90  productTitlePartial^90  licenseTitle^900  licenseTitleExact^900  licenseTitlePartial^900  artistName^270  artistNameExact^270 artistNamePartial^270  writer^10  writerExact^10  writerPartial^10  localClientCode pipsCode upc -writerRate:*&rows=10&start=0";
            var url = string.Format("{0}/mechs_license/select?{1}", _solrConfigurationRetriever.RecsConfiguration.UnSecureUrl, recsSearchCriteria);
            return _recsRequestHandler.Get<LicenseSearchResult>(url);
        }

        public bool UpdateLicense(LicenseSOLRUpdateRequest request)
        {
            var url = string.Format("{0}/solr/mechs_license/update/json?commit=true", _solrConfigurationRetriever.RecsConfiguration.UnSecureUrl);
            _recsRequestHandler.PostJson<object>(url, request);
            return true;
        }



    }
}