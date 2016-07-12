using System;
using System.Collections.Generic;
using System.Linq;
using UMPG.USL.Models;
using UMPG.USL.Models.LookupModel;

namespace UMPG.USL.API.Data.LookupData
{
    public class WritersConsentTypeRepository : IWritersConsentTypeRepository
    {

        public int Add(LU_WritersConsentType rateTypeType)
        {
            using (var context = new AuthContext())
            {
                context.LU_WritersConsentTypes.Add(rateTypeType);
                context.SaveChanges();

                return rateTypeType.WritersConsentTypeId;
            }
        }

        public LU_WritersConsentType Get(int id)
        {
            using (var context = new AuthContext())
            {
                return context.LU_WritersConsentTypes.FirstOrDefault(c => c.WritersConsentTypeId == id);
            }
        }

        public List<LU_WritersConsentType> GetAll()
        {
            using (var context = new AuthContext())
            {
                return context.LU_WritersConsentTypes.OrderBy(c => c.WritersConsentType).ToList();
            }
        }

        public List<LU_WritersConsentType> GetWritersConsentForLookup()
        {
            using (var context = new AuthContext())
            {
                return context.LU_WritersConsentTypes.ToList();
            }
        }

        public List<LU_PaidQuarterType> GetPaidQuarterForLookup()
        {
            using (var context = new AuthContext())
            {
                return context.LU_PaidQuarterTypes.ToList();
            }
        }

        public List<LU_WritersIncludeExcludeType> GetWritersIncludeExcludeForLookup()
        {
            using (var context = new AuthContext())
            {
                return context.LU_WritersIncludeExcludeTypes.ToList();
            }
        }


        

        public List<LU_WritersConsentType> Search(string query)
        {
            using (var context = new AuthContext())
            {
                var WritersConsentTypes = context.LU_WritersConsentTypes.Where(c => c.WritersConsentType == query).AsQueryable();

                if (!String.IsNullOrEmpty(query))
                {
                    return WritersConsentTypes.Where(c => c.WritersConsentType.ToLower().Contains(query.ToLower())).ToList();
                }
                else
                {
                    return WritersConsentTypes.ToList();
                }
            }
        }

    }
}
