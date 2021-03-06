﻿using System;
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
        string GetCatalogNummber(int productConfigId);

        LicenseProductConfiguration Add(LicenseProductConfiguration licenseProductConfiguration);

        LicenseProductConfiguration GetLicenseProductConfigurationByProductIdAndLicenseProductConfigurationId(
            int licenseproductId, int product_configuration_id);

        void Update(LicenseProductConfiguration licenseProductConfiguration);

        LicenseProductConfiguration GetLicenseProductConfigurationBLicenseProductConfigurationId(
            int product_configuration_id);

        LicenseProductConfiguration GetLicenseProductConfiguration(int licenseproductId, int product_configuration_id);

        List<LicenseProductConfiguration> GetLicenseConfigurationList(List<int> licenseProductIds);

        List<int> GetProductIdsWithConfiguration(List<int> licenseProductIds, List<int> configurationIds);

        List<int> GetLicenseProductConfigurationIds(int licenseproductId);

        List<LicenseProductProductConfigurationTotals> GetLicenseProductProductConfigurationIds(int licenseproductId);

    }
}
