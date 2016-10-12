using System;
using System.Collections.Generic;
using System.Linq;
using UMPG.USL.Models;
using UMPG.USL.Models.Recs;

namespace UMPG.USL.API.Data.Recs
{
    public class WriterRepository : IWriterRepository
    {

        public List<Writer> GetWriterNames(List<string> ipcodes)
        {
            using (var context = new AuthContext())
            {

                var writers =
                    context.Writers.AsNoTracking()
                    .Where(x => ipcodes.Contains((string)x.code))
                    .ToList();

                return writers;
            }
        }

        public Writer Get(string caeCode)
        {
            using (var context = new AuthContext())
            {
                return context.Writers.Where(x => x.cae == caeCode && x.type == "WRITER").FirstOrDefault();
            }
        }

        public List<Writer> GetAll()
        {
            using (var context = new AuthContext())
            {
                return context.Writers.ToList();
            }
        }

        public List<Writer> Search(string query)
        {
            using (var context = new AuthContext())
            {
                var Writers = context.Writers.Where(c => c.code == query).AsQueryable();

                if (!String.IsNullOrEmpty(query))
                {
                    return Writers.Where(c => c.code.ToLower().Contains(query.ToLower())).ToList();
                }
                else
                {
                    return Writers.ToList();
                }
            }
        }


    }
}
