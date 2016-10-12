using System.Collections.Generic;
using UMPG.USL.Models;
using UMPG.USL.Models.LicenseModel;

namespace UMPG.USL.API.Data.LicenseData
{
    public interface ILicenseAttachmentRepository
    {
        void Add(LicenseAttachment licensee);

        LicenseAttachment Get(int Id);

        LicenseAttachment Get(string fileName, int? licenseId);

        List<LicenseAttachment> GetAll();

        List<LicenseAttachment> Search(string query);


        void Update(LicenseAttachment licenseAttachment);
        bool UpdateLicenseAttachment(LicenseAttachment licenseAttachment);
    }
}
