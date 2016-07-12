using System.Collections.Generic;
using UMPG.USL.Models;
using UMPG.USL.Models.AuditModel;

namespace UMPG.USL.API.Data.AuditData
{
    public interface IAuditRepository
    {
        int Add(Audit audit);

        Audit Get(int Id);

        List<Audit> GetAll();

        List<Audit> Search(string query);

    }
}
