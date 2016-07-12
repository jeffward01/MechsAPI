using System;
using System.Collections.Generic;
using System.Linq;
using UMPG.USL.Models;
using UMPG.USL.Models.LookupModel;

namespace UMPG.USL.API.Data.LookupData
{
    public class PriorityRepository : IPriorityRepository
    {

        public int Add(LU_Priority priority)
        {
            using (var context = new AuthContext())
            {
                context.LU_Priorities.Add(priority);
                context.SaveChanges();

                return priority.PriorityId;
            }
        }

        public LU_Priority Get(int id)
        {
            using (var context = new AuthContext())
            {
                return context.LU_Priorities.FirstOrDefault(c => c.PriorityId == id);
            }
        }

        public List<LU_Priority> GetAll()
        {
            using (var context = new AuthContext())
            {
                return context.LU_Priorities.ToList();
            }
        }

        public List<LU_Priority> Search(string query)
        {
            using (var context = new AuthContext())
            {
                var Prioritys = context.LU_Priorities.Where(c => c.Priority == query).AsQueryable();

                if (!String.IsNullOrEmpty(query))
                {
                    return Prioritys.Where(c => c.Priority.ToLower().Contains(query.ToLower())).ToList();
                }
                else
                {
                    return Prioritys.ToList();
                }
            }
        }

    }
}
