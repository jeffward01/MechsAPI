using System;
using System.Collections.Generic;
using System.Linq;
using UMPG.USL.Models;
using UMPG.USL.Models.AuditModel;

namespace UMPG.USL.API.Data.AuditData
{
    public class AuditLicenseeRepository : IAuditLicenseeRepository
    {


        public AuditLicensee Get(int id)
        {
            using (var context = new AuditContext())
            {
                return context.AuditLicensees.FirstOrDefault(c => c.LicenseeId == id);
            }
        }

        public List<AuditLicensee> GetAll()
        {
            using (var context = new AuditContext())
            {
                var test = context.AuditLicensees
                    //.Include("Contact")  - tom removed returning no results.. need to think through this
                    .OrderBy(c=>c.Name).ToList();
                return test;
            }
        }



    }
}
