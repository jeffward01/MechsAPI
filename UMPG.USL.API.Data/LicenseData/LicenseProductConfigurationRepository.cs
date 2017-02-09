using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
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

        public LicenseProductConfiguration GetLicenseProductConfigurationByProductIdAndLicenseProductConfigurationId(int licenseproductId, int product_configuration_id)
        {
            using (var context = new AuthContext())
            {
                return context.LicenseProductConfigurations.FirstOrDefault(x => x.LicenseProductId == licenseproductId && x.product_configuration_id == product_configuration_id && x.Deleted == null);
            }
        }
        public LicenseProductConfiguration GetLicenseProductConfigurationBLicenseProductConfigurationId(int product_configuration_id)
        {
            using (var context = new AuthContext())
            {
                return context.LicenseProductConfigurations.FirstOrDefault(x => x.LicenseProductConfigurationId == product_configuration_id && x.Deleted == null);
            }
        }


        public List<LicenseProductConfiguration> GetLicenseProductConfigurations(int licenseproductId)
        {
            using (var context = new AuthContext())
            {
                return context.LicenseProductConfigurations.Where(x => x.LicenseProductId == licenseproductId && !x.Deleted.HasValue).ToList();

            }
        }

        public string GetCatalogNummber(int productConfigId)
        {
            using (var context = new AuthContext())
            {
                var result =  context.LicenseProductConfigurations.FirstOrDefault(x => x.product_configuration_id == productConfigId && x.CatalogNumber.Length > 2);
                if (result != null)
                {
                    return result.CatalogNumber;
                }
                return null;

            }
        }

        public LicenseProductConfiguration Get(int licenseProductConfigurationId)
        {
            using (var context = new AuthContext())
            {
                return
                    context.LicenseProductConfigurations.FirstOrDefault(
                        x => x.LicenseProductConfigurationId == licenseProductConfigurationId && x.Deleted == null);
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

        public List<LicenseProductProductConfigurationTotals> GetLicenseProductProductConfigurationIds(int licenseproductId)
        {
            using (var context = new AuthContext())
            {
                var configIds =
                    (from c in context.LicenseProductConfigurations
                        where c.LicenseProductId == licenseproductId && !c.Deleted.HasValue
                        select
                            new LicenseProductProductConfigurationTotals()
                            {
                                configuration_id = (int) c.configuration_id,
                                product_configuration_id = (int) c.product_configuration_id,
                                LicensedAmount = 0.0
                            });

                return configIds.ToList();
            }
        }

    }
}
