using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Internal;
using UMPG.USL.Models.AuditModel;
using UMPG.USL.Models.Recs; // added for recs configurtion model in

using EntityState = System.Data.Entity.EntityState;
using UMPG.USL.Models.StaticDropdownsData;

namespace UMPG.USL.API.Data.AuditData
{
    public class AuditLicenseProductRepository : IAuditLicenseProductRepository
    {


        public List<AuditLicenseProduct> GetAuditLicenseProducts(int licenseId)
        {
            using (var context = new AuditContext())
            {
                return context.AuditLicenseProducts
                    .Include("Schedule").Where(x => x.LicenseId == licenseId && x.Deleted == null).ToList();
            }
        }

        public AuditLicenseProduct GetAuditLicenseProduct(int licenseProductId)
        {
            using (var context = new AuditContext())
            {
                return context.AuditLicenseProducts.FirstOrDefault(x => x.LicenseProductId == licenseProductId && x.Deleted == null);
            }
        }

        public List<AuditProductProcedureResult> GetAuditForProducts(AuditGenericRequest request)
        {
            using (var context = new AuthContext())
            {
                var param1 = new SqlParameter("@strTableName", request.Table);
                var param2 = new SqlParameter("@strPKName", "LicenseID");
                var param3 = new SqlParameter("@strPKValue", request.Id.ToString());
                var result =
                    context.Database.SqlQuery<AuditProductProcedureResult>("usp_AuditLicenseProduct @strTableName, @strPKName, @strPKValue",
                        param1, param2, param3).ToList();
                return result;

            }
        }
    }
}
