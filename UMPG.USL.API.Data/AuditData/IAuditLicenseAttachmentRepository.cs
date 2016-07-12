using System.Collections.Generic;
using UMPG.USL.Models;
using UMPG.USL.Models.AuditModel;

namespace UMPG.USL.API.Data.AuditData
{
    public interface IAuditLicenseAttachmentRepository
    {

        AuditLicenseAttachment Get(int Id);

        AuditLicenseAttachment GetByLicenseId(string fileName, int? licenseId);

        List<AuditLicenseAttachment> GetAll();

        List<AuditLicenseAttachment> Search(string query);

    }
}
