using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.Models.LicenseModel;
using UMPG.USL.Models.StaticDropdownsData;
using EntityState = System.Data.Entity.EntityState;

namespace UMPG.USL.API.Data.LicenseData
{
    public class LicenseProductConfigurationRepository : ILicenseProductConfigurationRepository
    {

        public LicenseProductConfiguration GetLicenseProductConfiguration(int licenseproductId, int product_configuration_id)
        {
            using (var context = new AuthContext())
            {
                return context.LicenseProductConfigurations.Where(x => x.LicenseProductId == licenseproductId && x.product_configuration_id == product_configuration_id && !x.Deleted.HasValue).FirstOrDefault();
            }
        }

        public List<LicenseProductConfiguration> GetLicenseProductConfigurations(int licenseproductId)
        {
            using (var context = new AuthContext())
            {
                return context.LicenseProductConfigurations.Where(x => x.LicenseProductId == licenseproductId && !x.Deleted.HasValue).ToList();

            }
        }

        public LicenseProductConfiguration Get(int licenseProductConfigurationId)
        {
            using (var context = new AuthContext())
            {
                return context.LicenseProductConfigurations.Where(x => x.LicenseProductConfigurationId == licenseProductConfigurationId && !x.Deleted.HasValue).FirstOrDefault();
            }
        }

        public LicenseProductConfiguration Add(LicenseProductConfiguration licenseProductConfiguration)
        {
            using (var context = new AuthContext())
            {
                context.LicenseProductConfigurations.Add(licenseProductConfiguration);
                context.SaveChanges();
                return licenseProductConfiguration;
            }
        }

        public void Update(LicenseProductConfiguration licenseProductConfiguration)
        {
            using (var context = new AuthContext())
            {
                context.Entry(licenseProductConfiguration).State = (EntityState)System.Data.EntityState.Modified;
                context.SaveChanges();
            }
        }

        public List<LicenseProductConfiguration> GetLicenseConfigurationList(List<int> licenseProductIds)
        {

            using (var context = new AuthContext())
            {
                var test = context.LicenseProductConfigurations
                    .Where(x => licenseProductIds.Contains((int)x.LicenseProductId) && !x.Deleted.HasValue);

                return test.ToList();

            }

        }

        public List<int> GetProductIdsWithConfiguration(List<int> licenseProductIds, List<int> configurationIds )
        {

            using (var context = new AuthContext())
            {
                var test = context.LicenseProductConfigurations
                    .Where(x => licenseProductIds.Contains((int)x.LicenseProductId) && configurationIds.Contains(x.LicenseProductConfigurationId));

                return test.Select(x=>x.LicenseProductId).ToList();

            }

        }

        public List<int> GetLicenseProductConfigurationIds(int licenseproductId)
        {
            using (var context = new AuthContext())
            {
                var configIds = context.LicenseProductConfigurations.Where(x => x.LicenseProductId == licenseproductId && !x.Deleted.HasValue).Select(y=>(int)y.configuration_id).ToList();
                return configIds;
            }
        }
        
    }
}
