using System.Collections.Generic;
using UMPG.USL.Models;
using UMPG.USL.Models.LicenseModel;
using UMPG.USL.Models.ProductSearchModel;
using UMPG.USL.Models.Recs;

namespace UMPG.USL.API.Data.Recs
{
    public interface ISearchProvider
    {
        PagedResponse<Product> SearchProducts(ProductRequest searchCriteria, int contactId);
        PagedResponse<License> SearchLicenses(LicenseRequest searchCriteria);
    }
}