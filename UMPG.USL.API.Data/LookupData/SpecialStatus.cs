using System;
using System.Collections.Generic;
using System.Linq;
using UMPG.USL.Models;
using UMPG.USL.Models.LookupModel;

namespace UMPG.USL.API.Data.LookupData
{
    public class SpecialStatusRepository : ISpecialStatusRepository
    {

        public int Add(LU_SpecialStatus specialstatus)
        {
            using (var context = new AuthContext())
            {
                context.LU_SpecialStatuses.Add(specialstatus);
                context.SaveChanges();

                return specialstatus.SpecialStatusId;
            }
        }

        public LU_SpecialStatus Get(int id)
        {
            using (var context = new AuthContext())
            {
                return context.LU_SpecialStatuses.FirstOrDefault(c => c.SpecialStatusId == id);
            }
        }

        public List<LU_SpecialStatus> GetAll()
        {
            using (var context = new AuthContext())
            {
                return context.LU_SpecialStatuses.OrderBy(c => c.SpecialStatus).ToList();
            }
        }

        public List<LU_SpecialStatus> Search(string query)
        {
            using (var context = new AuthContext())
            {
                var SpecialStatuses = context.LU_SpecialStatuses.Where(c => c.SpecialStatus == query).AsQueryable();

                if (!String.IsNullOrEmpty(query))
                {
                    return SpecialStatuses.Where(c => c.SpecialStatus.ToLower().Contains(query.ToLower())).ToList();
                }
                else
                {
                    return SpecialStatuses.ToList();
                }
            }
        }

    }
}
