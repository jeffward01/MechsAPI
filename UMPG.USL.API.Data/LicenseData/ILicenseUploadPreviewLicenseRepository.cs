using System.Collections.Generic;
using UMPG.USL.Models;
using UMPG.USL.Models.LicenseModel;
using UMPG.USL.Models.LicenseGenerate;

namespace UMPG.USL.API.Data.LicenseData
{

    public interface ILicenseUploadPreviewLicenseRepository
    {
        int AddOnLicenseQueue(GenerateLicenseQueue license);
        void AddOnLicenseAttachment(GenerateLicenseAttachment license);
    }

}