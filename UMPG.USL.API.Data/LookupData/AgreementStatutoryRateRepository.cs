using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.Models.LookupModel;

namespace UMPG.USL.API.Data.LookupData
{
    public class AgreementStatutoryRateRepository : IAgreementStatutoryRateRepository
    {
        public AgreementStatutoryRate Get(int? year)
        {
            using (var context = new AuthContext())
            {
                return context.AgreementStatutoryRate.FirstOrDefault(x => x.Year == year);
            }
        }

        public List<AgreementStatutoryRate> GetAll()
        {
            using (var context = new AuthContext())
            {
                return context.AgreementStatutoryRate.ToList();
            }
        }
    }
}
