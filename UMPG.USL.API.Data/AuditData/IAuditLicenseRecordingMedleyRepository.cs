using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Win32;
using UMPG.USL.Models.AuditModel;

namespace UMPG.USL.API.Data.AuditData
{
    public interface IAuditLicenseRecordingMedleyRepository
    {
        List<AuditLicenseRecordingMedley> GetMedleysByTrackId(long trackId);
     }
}
