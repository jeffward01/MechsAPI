using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.Models.AuditModel;

namespace UMPG.USL.API.Data.AuditData
{
    public interface IAuditLicensePRWriterRateRepository
    {

        AuditLicenseProductRecordingWriterRate Get(int id);
        List<AuditLicenseProductRecordingWriterRate> GetByWriterId(int id);
    }
}
