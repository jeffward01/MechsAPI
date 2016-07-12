using System.Collections.Generic;
using UMPG.USL.Models;
using UMPG.USL.Models.LicenseModel;
using UMPG.USL.Models.LicenseSearchModel;
using UMPG.USL.Models.Security;
using UMPG.USL.Models.ProductSearchModel;
using UMPG.USL.Models.Recs;

namespace UMPG.USL.API.Data.Recs
{
    public interface ISolrSearch
    {
        ProductSearchResult SearchProducts(ProductRequest searchCriteria);
        LicenseSearchResult SearchLicenses(LicenseRequest searchCriteria);
        bool UpdateLicense(LicenseSOLRUpdateRequest request);
    }
}