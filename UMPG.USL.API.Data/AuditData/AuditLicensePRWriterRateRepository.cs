using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.Models.AuditModel;
using EntityState = System.Data.Entity.EntityState;

namespace UMPG.USL.API.Data.AuditData
{
    public class AuditLicensePRWriterRateRepository : IAuditLicensePRWriterRateRepository
    {

        public AuditLicenseProductRecordingWriterRate Get(int id)
        {
            using (var context = new AuditContext())
            {
                var licensePRwriterrate = context.AuditLicenseProductRecordingWriterRates
                    .FirstOrDefault(c => c.LicenseWriterRateId == id);
                return licensePRwriterrate;
            }
        }

        public List<AuditLicenseProductRecordingWriterRate> GetByWriterId(int id)
        {
            using (var context = new AuditContext())
            {
                var licensePRwriterrate = context.AuditLicenseProductRecordingWriterRates
                    .Where(c => c.LicenseWriterId == id).ToList();
                return licensePRwriterrate;
            }
        }
    }
}
