using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMPG.USL.API.Data.LicenseData
{
    using UMPG.USL.Models.LicenseGenerate;

    public class LicenseUploadPreviewLicenseRepository : ILicenseUploadPreviewLicenseRepository
    {
        public int AddOnLicenseQueue(GenerateLicenseQueue license)
        {
            using (var context = new AuthContext())
            {
                context.GenerateLicenseQueue.Add(license);
                context.SaveChanges();
            }

           return license.GenerateLicenseQueueId;
        }

        public void AddOnLicenseAttachment(GenerateLicenseAttachment license)
        {
            using (var context = new AuthContext())
            {
                context.GenerateLicenseAttachment.Add(license);
                context.SaveChanges();
            }
        }


    }
}
