using System.Data.SqlClient;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using System;
using UMPG.USL.API.Data.Utils;
using UMPG.USL.Models;
using UMPG.USL.Models.AuditModel;
using EntityState = System.Data.Entity.EntityState;

namespace UMPG.USL.API.Data.AuditData
{
    public class AuditLicenseRepository : IAuditLicenseRepository
    {


        public AuditLicense Get(int id)
        {
            using (var context = new AuditContext())
            {
                var license = context.AuditLicenses
                    .LastOrDefault(c => c.LicenseId == id);
                
                return license;
            }
            
        }

  
        public List<AuditLicense> GetAll()
        {
            using (var context = new AuditContext())
            {
                var licenses = context.AuditLicenses
                     
                    .ToList();
                return licenses;

            }
        }

        public List<AuditLicenseProcedureResult> GetAuditForLicense(AuditGenericRequest request)
        {
            using (var context = new AuthContext())
            {
                var param1 = new SqlParameter("@strTableName", request.Table);
                var param2 = new SqlParameter("@strPKName", "LicenseID");
                var param3 = new SqlParameter("@strPKValue", request.Id.ToString());
                var result =
                    context.Database.SqlQuery<AuditLicenseProcedureResult>("usp_AuditLicense @strTableName, @strPKName, @strPKValue",
                        param1, param2, param3).ToList();
                return result;

            }
        }
    }
}
