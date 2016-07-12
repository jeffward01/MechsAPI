using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using UMPG.USL.API.Data.LookupData;
using UMPG.USL.Models.AuditModel;

namespace UMPG.USL.API.Data.AuditData
{
    public class AuditLicenseRecordingMedleyRepository : IAuditLicenseRecordingMedleyRepository
    {

        public List<AuditLicenseRecordingMedley> GetMedleysByTrackId(long trackId)
        {
            return new List<AuditLicenseRecordingMedley>();
        }



      }
}
