using System.Collections.Generic;
using System.Web.Http;
using UMPG.USL.API.Business.Audits;
using UMPG.USL.Models;
using UMPG.USL.Models.AuditModel;
using UMPG.USL.Models.Recs;

namespace UMPG.USL.API.Controllers.AuditCTRL
{
    [RoutePrefix("api/auditCTRL/audits")]
    public class AuditController : ApiController
    {
        private readonly IAuditManager _auditManager;
        public AuditController(IAuditManager auditManager)
        {
            _auditManager = auditManager;
        }
        

        [Route("GetLicenseAudit")]
        [HttpPost]
        public List<AuditLicenseProcedureResult> GetAuditForLicense(AuditGenericRequest request)
        {
            return _auditManager.GetAuditForLicense(request);
        }

        [Route("GetProductAudit")]
        [HttpPost]
        public List<AuditProductProcedureResult> GetProductAudit(AuditGenericRequest request)
        {
            return _auditManager.GetAuditForProducts(request);
        }
    }


    #region Helpers



    #endregion
}
