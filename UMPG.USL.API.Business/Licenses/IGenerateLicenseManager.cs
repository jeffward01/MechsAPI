using System.Collections.Generic;
using UMPG.USL.Models.LicenseGenerate;
namespace UMPG.USL.API.Business.Licenses
{
    using UMPG.USL.Models.LicenseModel;

    public interface IGenerateLicenseManager
    {
        void UpdateGenerateLicenseStatus(LicenseUserAction licenseId);
        List<GenerateLicenseQueue> GetByLicenseId(int licenseId);
        void Update(GenerateLicenseQueue generateLicenseQueue);

    }
}