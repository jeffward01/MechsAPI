using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.API.Data.AuditData;
using UMPG.USL.API.Data.LicenseData;
using UMPG.USL.Models;
using UMPG.USL.Models.AuditModel;
using UMPG.USL.Models.Recs;


namespace UMPG.USL.API.Business.Audits
{
    public class AuditManager : IAuditManager
    {

        private readonly IAuditLicenseRepository _auditLicenseRepository;
        private readonly IAuditLicenseProductRepository _auditlicenseProductRepository;
        private readonly IAuditLicenseNoteRepository _auditlicenseNoteRepository;
        private readonly IAuditLicensePRWriterRateRepository _auditlicensePRWriterRateRepository;
        private readonly IAuditLicenseProductRecordingRepository _auditlicenseProductRecordingRepository;
        private readonly IAuditLicensePRWriterRepository _auditlicensePRWriterRepository;
        private readonly IAuditLicensePRWriterNoteRepository _auditLicensePRWriterNoteRepository;
        private readonly IAuditLicensePRWriterStatusRepository _auditlicensePRWriterStatusRepository;

        public AuditManager(IAuditLicenseRepository auditLicenseRepository, IAuditLicenseProductRepository auditlicenseProductRepository)
        {
            _auditLicenseRepository = auditLicenseRepository;
            _auditlicenseProductRepository = auditlicenseProductRepository;

        }

       public List<AuditLicenseProcedureResult> GetAuditForLicense(AuditGenericRequest request)
        {
            return _auditLicenseRepository.GetAuditForLicense(request);
        }

        public List<AuditProductProcedureResult> GetAuditForProducts(AuditGenericRequest request)
        {
            return _auditlicenseProductRepository.GetAuditForProducts(request);
        }
    }
}
