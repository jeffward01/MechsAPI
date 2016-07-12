using System.Collections.Generic;
using UMPG.USL.Models.AuditModel;

namespace UMPG.USL.API.Business.Audits
{
    public interface IAuditManager
    {
        List<AuditLicenseProcedureResult> GetAuditForLicense(AuditGenericRequest request);

        List<AuditProductProcedureResult> GetAuditForProducts(AuditGenericRequest request);

    }
}
