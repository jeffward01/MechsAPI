using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.Models.Recs;

namespace UMPG.USL.API.Data.Recs
{
    public class RecordingWorkLinkRepository : IRecordingWorkLinkRepository
    {
        public int GetWritersNo(long trackId)
        {
            using (var context = new AuthContext())
            {

                var writerscount = (from a in context.RecordingWorkLinks
                    join w in context.Works on a.work_code equals w.primaryworkcode
                    join wo in context.WorkOriginalInterestedParties on w.alternateworkcode equals wo.primaryworkcode
                    join ip in context.InterestedParties on wo.ipcode equals ip.code
                    where (a.track_id == trackId && ip.type == "WRITER")
                    select new {ipcode = ip.code})
                    .Count();

                return writerscount;
            }
            
        }

        public IEnumerable<RecsWriter> GetWorkWriters(long trackId)
        {
            using (var context = new AuthContext())
            {

                return (from a in context.RecordingWorkLinks
                                        join w in context.Works on a.work_code equals w.primaryworkcode
                                        join wo in context.WorkOriginalInterestedParties on w.alternateworkcode equals wo.primaryworkcode
                                        join ip in context.InterestedParties on wo.ipcode equals ip.code
                                        where (a.track_id == trackId && ip.type=="WRITER")
                        select new RecsWriter
                        { 
                                            fullName = ip.fullname,
                                            ipcode = wo.ipcode,
                                            cae = ip.cae,
                                            controlled = wo.controlled,
                                            sequence =wo.sequence,
                                            percentage =wo.percentage,
                                            affiliation =wo.affiliation,
                                            usregistrationpreference = wo.usregistrationpreference
                                            
                                        }).ToList();

            }
        }
    }
}
