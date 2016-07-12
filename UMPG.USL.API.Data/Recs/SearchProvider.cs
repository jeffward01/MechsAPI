using Castle.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using UMPG.USL.API.Data;
using UMPG.USL.Common.Mappers;
using UMPG.USL.Models;
using UMPG.USL.Models.LicenseModel;
using UMPG.USL.Models.LicenseSearchModel;
using UMPG.USL.Models.ProductSearchModel;
using UMPG.USL.Models.Recs;

namespace UMPG.USL.API.Data.Recs
{    
    public class SearchProvider : ISearchProvider
    {
        private readonly ISolrSearch _solrSearch;
        private readonly IMappingsManager _mappingsManager;
        public SearchProvider(ISolrSearch solr, IMappingsManager mappingsManager)
        {
            _solrSearch = solr;
            _mappingsManager = mappingsManager;
        }

        public PagedResponse<Product> SearchProducts(ProductRequest searchCriteria, int contactId)
        {
            var result = _solrSearch.SearchProducts(searchCriteria);
            var response = new PagedResponse<Product>();
            response.Results = result.Products.Select(p => _mappingsManager.Map<Product, ProductSOLR>(p)).ToList();
            response.Total = result.NumFound;
            return response;
        }

        public PagedResponse<License> SearchLicenses(LicenseRequest searchCriteria)
        {
            var result = _solrSearch.SearchLicenses(searchCriteria);
            var response = new PagedResponse<License>();
            response.Results = result.Licenses.Select(p => _mappingsManager.Map<License, LicenseSOLR>(p)).ToList();
            response.Total = result.NumFound;
            return response;
        }
    }
}