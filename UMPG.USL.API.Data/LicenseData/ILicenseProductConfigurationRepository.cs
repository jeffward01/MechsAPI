using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.Models.LicenseModel;
using UMPG.USL.Models.StaticDropdownsData;

namespace UMPG.USL.API.Data.LicenseData
{
    public interface ILicenseProductConfigurationRepository
    {
        List<LicenseProductConfiguration> GetLicenseProductConfigurations(int licenseproductId);

        LicenseProductConfiguration Get(int licenseProductConfigurationId);

        LicenseProductConfiguration Add(LicenseProductConfiguration licenseProductConfiguration);

        void Update(LicenseProductConfiguration licenseProductConfiguration);

        LicenseProductConfiguration GetLicenseProductConfiguration(int licenseproductId, int product_configuration_id);

        List<LicenseProductConfiguration> GetLicenseConfigurationList(List<int> licenseProductIds);

        List<int> GetProductIdsWithConfiguration(List<int> licenseProductIds, List<int> configurationIds);

        List<int> GetLicenseProductConfigurationIds(int licenseproductId);

    }
}
