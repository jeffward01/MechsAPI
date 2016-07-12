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
    public class SolrUpdate : ISolrUpdate
    {
        private readonly IRecsRequestHandler _recsRequestHandler;
        private readonly IMappingsManager _mappingsManager;
        private readonly ISolrUpdateServerConfigurationRetriever _solrUpdateServerConfigurationRetriever;

        public SolrUpdate(IRecsRequestHandler recsRequestHandler, IMappingsManager mappingsManager, ISolrUpdateServerConfigurationRetriever solrUpdateConfigurationRetriever)
        {
            _recsRequestHandler = recsRequestHandler;
            _mappingsManager = mappingsManager;
            _solrUpdateServerConfigurationRetriever = solrUpdateConfigurationRetriever;
        }

        public bool UpdateLicense(LicenseSOLRUpdateRequest request)
        {
            var updateListRequest = new List<LicenseSOLRUpdateRequest>();
            updateListRequest.Add(request);
            var url = string.Format("{0}/mechs_license/update/json?commit=true", _solrUpdateServerConfigurationRetriever.RecsConfiguration.UnSecureUrl);
            _recsRequestHandler.PostJson<object>(url, updateListRequest);
            return true;
        }

        public bool UpdateProduct(List<ProductSolrUpdateRequest> request)
        {
            var url = string.Format("{0}/mechs_product/update/json?commit=true", _solrUpdateServerConfigurationRetriever.RecsConfiguration.UnSecureUrl);
            _recsRequestHandler.PostJson<object>(url, request);
            return true;
        }

        public bool UpdateLicenseFields(List<object> request)
        {
            var url = string.Format("{0}/mechs_license/update/json?commit=true", _solrUpdateServerConfigurationRetriever.RecsConfiguration.UnSecureUrl);
            _recsRequestHandler.PostJson<object>(url, request);
            return true;
        }
        public bool UpdateProductFields(List<object> request)
        {
            var url = string.Format("{0}/mechs_product/update/json?commit=true", _solrUpdateServerConfigurationRetriever.RecsConfiguration.UnSecureUrl);
            _recsRequestHandler.PostJson<object>(url, request);
            return true;
        }
    }
}