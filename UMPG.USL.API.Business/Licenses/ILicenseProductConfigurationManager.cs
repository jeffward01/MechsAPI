using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.Models.LicenseModel;
using UMPG.USL.Models.StaticDropdownsData;

namespace UMPG.USL.API.Business.Licenses
{
    public interface ILicenseProductConfigurationManager
    {
        List<LicenseProductConfiguration> GetLicenseProductConfigurations(int licenseproductId);

        List<LicenseProductConfiguration> GetLicenseConfigurationList(List<int> licenseProductIds);

        bool DeleteLicenseProductConfiguration(UpdateLicenseProductConfigurationRequest request);

        //LicenseProductConfiguration AddLicenseProductConfiguration(UpdateLicenseProductConfigurationRequest request);
        UpdateLicenseProductConfigurationResult AddLicenseProductConfiguration(UpdateLicenseProductConfigurationRequest request);

        List<UpdateLicenseProductConfigurationResult> UpdateLicenseProductConfiguration(List<UpdateLicenseProductConfigurationRequest> requests);

        bool UpdateAllLicensesConfiguration(int startLicenseIdIndex, int endLicenseIdIndex);
    }
}
