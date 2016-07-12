using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.Models.AuditModel;
using UMPG.USL.Models.StaticDropdownsData;
using EntityState = System.Data.Entity.EntityState;

namespace UMPG.USL.API.Data.AuditData
{
    public class AuditLicenseProductConfigurationRepository : IAuditLicenseProductConfigurationRepository
    {

        public AuditLicenseProductConfiguration GetLicenseProductConfiguration(int licenseproductId, int product_configuration_id)
        {
            using (var context = new AuditContext())
            {
                return context.AuditLicenseProductConfigurations.Where(x => x.LicenseProductId == licenseproductId && x.product_configuration_id == product_configuration_id && !x.Deleted.HasValue).FirstOrDefault();
            }
        }

        public List<AuditLicenseProductConfiguration> GetLicenseProductConfigurations(int licenseproductId)
        {
            using (var context = new AuditContext())
            {
                return context.AuditLicenseProductConfigurations.Where(x => x.LicenseProductId == licenseproductId && !x.Deleted.HasValue).ToList();

            }
        }

        public AuditLicenseProductConfiguration Get(int licenseProductConfigurationId)
        {
            using (var context = new AuditContext())
            {
                return context.AuditLicenseProductConfigurations.Where(x => x.LicenseProductConfigurationId == licenseProductConfigurationId && !x.Deleted.HasValue).FirstOrDefault();
            }
        }


        public List<AuditLicenseProductConfiguration> GetLicenseConfigurationList(List<int> licenseProductIds)
        {

            using (var context = new AuditContext())
            {
                var test = context.AuditLicenseProductConfigurations
                    .Where(x => licenseProductIds.Contains((int)x.LicenseProductId) && !x.Deleted.HasValue);

                return test.ToList();

            }

        }

        public List<int> GetProductIdsWithConfiguration(List<int> licenseProductIds, List<int> configurationIds )
        {

            using (var context = new AuditContext())
            {
                var test = context.AuditLicenseProductConfigurations
                    .Where(x => licenseProductIds.Contains((int)x.LicenseProductId) && configurationIds.Contains(x.LicenseProductConfigurationId));

                return test.Select(x=>x.LicenseProductId).ToList();

            }

        }

    }
}
