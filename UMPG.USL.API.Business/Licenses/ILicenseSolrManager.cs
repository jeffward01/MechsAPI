using System.Collections.Generic;
namespace UMPG.USL.API.Business.Licenses
{
    public interface ILicenseSolrManager
    {
        bool UpdateLicense(int licenseId, bool updateProducts = true);
        bool UpdateProduct(long productId, bool updateRelatedLicenses = true);
        bool UpdateLicenseAssignee(List<int> licenseId);
        bool UpdateLicenseStatus(int licenseId);
        void ClearCache();
    }
}