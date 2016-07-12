using System;
using System.Collections.Generic;
using System.Linq;

namespace UMPG.USL.API.Data.LookupData
{
    public class StatRateRepository:IStatRateRepository
    {
        public float GetStatRate(float durationn, DateTime date)
        {
            using (var context = new AuthContext())
            {
                double duration = durationn;
                var lreturn = context.StatRate
                       .Include("StatRateDate")
                       .Include("StatRateTime").Where(x => x.StatRateTime.EndTime == duration && DateTime.Compare(date, x.StatRateDate.BeginDate) >= 0 && DateTime.Compare(date, x.StatRateDate.EndDate)<=0).Select(x => x.Rate).FirstOrDefault();
                return (float)lreturn;

            }
        }
        
    }
}
