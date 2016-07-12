using System.Collections.Generic;
using UMPG.USL.Models;
using UMPG.USL.Models.AuditModel;

namespace UMPG.USL.API.Data.AuditData
{
    public interface IAuditLicenseRepository
    {

        AuditLicense Get(int id);
        List<AuditLicense> GetAll();
        List<AuditLicenseProcedureResult> GetAuditForLicense(AuditGenericRequest request);

    }
}
