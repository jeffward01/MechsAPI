using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.Models.AuditModel;
using UMPG.USL.Models.StaticDropdownsData;

namespace UMPG.USL.API.Data.AuditData
{
    public interface IAuditLicensePRWriterRepository
    {
        AuditLicenseProductRecordingWriter Get(int writerId);
        List<AuditLicenseProductRecordingWriter> GetLicenseWriters(int licenseRecordingId);
        List<AuditLicenseProductRecordingWriter> GetAuditLicenseWriterList(List<int> licenseRecordingIds);
        List<AuditLicenseProductRecordingWriter> GetAuditLicensePrWritersFromIds(List<int> writerIds);
        
    }
}
