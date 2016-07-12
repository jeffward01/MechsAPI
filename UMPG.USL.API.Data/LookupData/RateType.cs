using System;
using System.Collections.Generic;
using System.Linq;
using UMPG.USL.Models;
using UMPG.USL.Models.LookupModel;

namespace UMPG.USL.API.Data.LookupData
{
    public class RateTypeRepository : IRateTypeRepository
    {

        public int Add(LU_RateType rateType)
        {
            using (var context = new AuthContext())
            {
                context.LU_RateTypes.Add(rateType);
                context.SaveChanges();

                return rateType.RateTypeId;
            }
        }

        public LU_RateType Get(int id)
        {
            using (var context = new AuthContext())
            {
                return context.LU_RateTypes.FirstOrDefault(c => c.RateTypeId == id);
            }
        }

        public List<LU_RateType> GetAll()
        {
            using (var context = new AuthContext())
            {
                return context.LU_RateTypes.OrderBy(c => c.RateType).ToList();
            }
        }

        public List<LU_RateType> Search(string query)
        {
            using (var context = new AuthContext())
            {
                var RateTypes = context.LU_RateTypes.Where(c => c.RateType == query).AsQueryable();

                if (!String.IsNullOrEmpty(query))
                {
                    return RateTypes.Where(c => c.RateType.ToLower().Contains(query.ToLower())).ToList();
                }
                else
                {
                    return RateTypes.ToList();
                }
            }
        }

    }
}
