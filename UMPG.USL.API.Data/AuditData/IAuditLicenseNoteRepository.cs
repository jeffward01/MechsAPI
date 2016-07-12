using System.Collections.Generic;
using UMPG.USL.Models;
using UMPG.USL.Models.AuditModel;

namespace UMPG.USL.API.Data.AuditData
{
    public interface IAuditLicenseNoteRepository
    {

        AuditLicenseNote Get(int Id);

        List<AuditLicenseNote> GetAll();


        
    }
}
