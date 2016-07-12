using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.Models.AuditModel;
using UMPG.USL.Models.StaticDropdownsData;

namespace UMPG.USL.API.Data.AuditData
{
    public interface IAuditLicenseProductConfigurationRepository
    {
        List<AuditLicenseProductConfiguration> GetLicenseProductConfigurations(int licenseproductId);

        AuditLicenseProductConfiguration Get(int licenseProductConfigurationId);

        AuditLicenseProductConfiguration GetLicenseProductConfiguration(int licenseproductId, int product_configuration_id);

        List<AuditLicenseProductConfiguration> GetLicenseConfigurationList(List<int> licenseProductIds);

        List<int> GetProductIdsWithConfiguration(List<int> licenseProductIds, List<int> configurationIds);

    }
}
