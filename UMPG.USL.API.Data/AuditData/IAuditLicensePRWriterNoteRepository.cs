using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.Models.AuditModel;

namespace UMPG.USL.API.Data.AuditData
{


    public interface IAuditLicensePRWriterNoteRepository
    {

        AuditLicenseProductRecordingWriterNote Get(int id);

        List<AuditLicenseProductRecordingWriterNote> GetAll(int id);

        List<AuditLicenseProductRecordingWriterNote> GetAuditLicenseProductRecordingWriterNotes(List<int> licenseWriterIds);
    
    }
}
