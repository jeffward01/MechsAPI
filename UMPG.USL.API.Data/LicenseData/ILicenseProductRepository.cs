using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.Models.LicenseModel;
using UMPG.USL.Models.StaticDropdownsData;

namespace UMPG.USL.API.Data.LicenseData
{
    public interface ILicenseProductRepository
    {
        LicenseProduct Get(int licenseProductId);
        int GetProductsNo(int licenseId);

        int GetLicensesNo(int productId);
        
        List<int> GetProductsIds(int licenseId);

        List<int> GetLicenseIds(int productId);

        List<LicenseProduct> GetLicenseProducts(int licenseId);

        List<LicenseProduct> GetLicenseProductsByRecsProductId(int productId);

        List<LicenseProduct> GetAllLicenseProducts(long productId);
        List<LicenseProductConfiguration> GetLicenseProductConfigurations(int licenseId);

        LicenseProductConfiguration GetLicenseProductConfigurationById(int licenseProductConfigurationId);
        List<LicenseProductConfiguration> GetProductConfigurations(int licenseProductId);
        void UpdateLicenseProductConfiguration(int licenseProductConfigurationId, string fieldName, bool fieldValue);
        LicenseProduct Add(LicenseProduct licentProduct);
        void Update(LicenseProduct licenseProduct);

        List<LicenseProductDropdown> GetLicenseProductDropDown(int licenseId);

        LicenseProduct GetLicenseProduct(int licenseId, int productId);
        LicenseProduct GetLicenseProduct(int licenseProductId);
    }
}
