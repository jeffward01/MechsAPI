using System;
using System.Collections.Generic;
using System.Linq;
using UMPG.USL.Models;
using UMPG.USL.Models.LookupModel;

namespace UMPG.USL.API.Data.LookupData
{
    public class PaidQuarterRepository : IPaidQuarterRepository
    {

        public int Add(LU_PaidQuarter paidQuarter)
        {
            using (var context = new AuthContext())
            {
                context.LU_PaidQuarters.Add(paidQuarter);
                context.SaveChanges();

                return paidQuarter.lU_PaidQuarterId;
            }
        }

        public LU_PaidQuarter Get(int id)
        {
            using (var context = new AuthContext())
            {
                return context.LU_PaidQuarters.FirstOrDefault(c => c.lU_PaidQuarterId == id);
            }
        }

        public List<LU_PaidQuarter> GetAll()
        {
            using (var context = new AuthContext())
            {
                return context.LU_PaidQuarters.OrderByDescending(c => c.year).ThenByDescending(c => c.paidQuarter).ToList();
            }
        }

        public List<LU_PaidQuarter> GetRolling10years()
        {
            using (var context = new AuthContext())
            {
                var currentyear = DateTime.Today.Year;

                //now go back 10 years
                var yearlist = context.LU_PaidQuarters
                    .Where(c=>c.year >=currentyear - 10 && 
                              c.year <=currentyear) 
                    .OrderByDescending(c => c.year)
                    .ThenByDescending(c => c.paidQuarter).ToList();

                return yearlist;
            }
        }

        public List<LU_PaidQuarter> Search(string query)
        {
            using (var context = new AuthContext())
            {
                var PaidQuarters = context.LU_PaidQuarters.Where(c => c.paidQuarter == query).AsQueryable();

                if (!String.IsNullOrEmpty(query))
                {
                    return PaidQuarters.Where(c => c.paidQuarter.ToLower().Contains(query.ToLower())).ToList();
                }
                else
                {
                    return PaidQuarters.ToList();
                }
            }
        }

    }
}
