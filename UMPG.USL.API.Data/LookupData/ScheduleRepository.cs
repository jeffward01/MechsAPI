using System;
using System.Collections.Generic;
using System.Linq;
using UMPG.USL.Models;
using UMPG.USL.Models.LookupModel;

namespace UMPG.USL.API.Data.LookupData
{
    public class ScheduleRepository : IScheduleRepository
    {

        public int Add(LU_Schedule schedule)
        {
            using (var context = new AuthContext())
            {
                context.LU_Schedules.Add(schedule);
                context.SaveChanges();

                return schedule.ScheduleId;
            }
        }

        public LU_Schedule Get(int id)
        {
            using (var context = new AuthContext())
            {
                return context.LU_Schedules.FirstOrDefault(c => c.ScheduleId == id);
            }
        }

        public List<LU_Schedule> GetAll()
        {
            using (var context = new AuthContext())
            {
                return context.LU_Schedules.OrderBy(c => c.ScheduleId).ToList();
            }
        }

        public List<LU_Schedule> Search(string query)
        {
            using (var context = new AuthContext())
            {
                var Schedules = context.LU_Schedules.Where(c => c.ScheduleName == query).AsQueryable();

                if (!String.IsNullOrEmpty(query))
                {
                    return Schedules.Where(c => c.ScheduleName.ToLower().Contains(query.ToLower())).ToList();
                }
                else
                {
                    return Schedules.ToList();
                }
            }
        }

    }
}
