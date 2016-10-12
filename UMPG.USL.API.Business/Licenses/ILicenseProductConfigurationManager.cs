using System;
using System.Collections.Generic;
using UMPG.USL.Models.LicenseModel;

namespace UMPG.USL.API.Business.Licenses
{
    public interface ILicenseProductConfigurationManager
    {
        void AddLicenseRecordingForProductSafe(int productId, int licenseProductId);

        List<LicenseProductConfiguration> GetLicenseProductConfigurations(int licenseproductId);

        List<LicenseProductConfiguration> GetLicenseConfigurationList(List<int> licenseProductIds);

        bool DeleteLicenseProductConfiguration(UpdateLicenseProductConfigurationRequest request);

        //LicenseProductConfiguration AddLicenseProductConfiguration(UpdateLicenseProductConfigurationRequest request);
        UpdateLicenseProductConfigurationResult AddLicenseProductConfiguration(UpdateLicenseProductConfigurationRequest request);

        List<UpdateLicenseProductConfigurationResult> UpdateLicenseProductConfiguration(List<UpdateLicenseProductConfigurationRequest> requests);

        void AddLicenseRecordingForProduct(int productId, DateTime createdDate, int createdBy, int licenseProductId);

        bool UpdateAllLicensesConfiguration(int startLicenseIdIndex, int endLicenseIdIndex);
    }
}