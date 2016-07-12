using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.Models.AuditModel;
using UMPG.USL.Models.LookupModel;
using UMPG.USL.Models.StaticDropdownsData;
using EntityState = System.Data.Entity.EntityState;

namespace UMPG.USL.API.Data.AuditData
{
    public class AuditLicensePRWriterStatusRepository : IAuditLicensePRWriterStatusRepository
    {

        public AuditLicenseProductRecordingWriterRateStatus Get(int id)
        {
            using (var context = new AuditContext())
            {
                var licensePRwriterRateStatus = context.AuditLicenseProductRecordingWriterRateStatuses
                    .FirstOrDefault(c => c.LicenseWriterRateStatusId == id);
                return licensePRwriterRateStatus;
            }
        }
        
        public List<AuditLicenseProductRecordingWriterRateStatus> GetAuditLicenseWriterRateStatus(List<int> licenseWriterRateIds)
        {
            using (var context = new AuditContext())
            {
                
                var licensewriterRates = context.AuditLicenseProductRecordingWriterRateStatuses
                    .Where(lw => licenseWriterRateIds.Contains(lw.LicenseWriterRateId) && lw.Deleted == null)
                   .ToList();
                
                return licensewriterRates;
            }
        }

       
    }
}
