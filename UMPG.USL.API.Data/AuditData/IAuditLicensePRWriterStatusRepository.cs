using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.Models.AuditModel;
using UMPG.USL.Models.StaticDropdownsData;

namespace UMPG.USL.API.Data.AuditData
{
    public interface IAuditLicensePRWriterStatusRepository
    {

        AuditLicenseProductRecordingWriterRateStatus Get(int id);
        List<AuditLicenseProductRecordingWriterRateStatus> GetAuditLicenseWriterRateStatus(List<int> licenseWriterRateIds);

    }
}
