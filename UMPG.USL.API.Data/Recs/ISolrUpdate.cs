using System.Collections.Generic;
using UMPG.USL.Models.LicenseModel;
using UMPG.USL.Models.ProductSearchModel;

namespace UMPG.USL.API.Data.Recs
{
    public interface ISolrUpdate
    {
        bool UpdateLicense(LicenseSOLRUpdateRequest request);
        bool UpdateProduct(List<ProductSolrUpdateRequest> request);
        bool UpdateLicenseFields(List<object> request);
        bool UpdateProductFields(List<object> request);
    }
}