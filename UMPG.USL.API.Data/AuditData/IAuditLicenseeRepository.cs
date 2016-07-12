using System.Collections.Generic;
using UMPG.USL.Models;
using UMPG.USL.Models.AuditModel;

namespace UMPG.USL.API.Data.AuditData
{
    public interface IAuditLicenseeRepository
    {


        AuditLicensee Get(int Id);

        List<AuditLicensee> GetAll();


    }
}
