using System;
using System.Collections.Generic;
using System.Linq;
using UMPG.USL.Models;
using UMPG.USL.Models.AuditModel;

namespace UMPG.USL.API.Data.AuditData
{

    /// <summary>
    /// 
    /// THIS IS OBSOLETE - 10-10-2015 - TH
    /// </summary>
    public class AuditRepository : IAuditRepository
    {

        public int Add(Audit audit)
        {
            using (var context = new AuthContext())
            {
                context.Audits.Add(audit);
                context.SaveChanges();

                return audit.auditID;
            }
        }

        public Audit Get(int id)
        {
            using (var context = new AuthContext())
            {
                return context.Audits.FirstOrDefault(c => c.auditID == id);
            }
        }

        public List<Audit> GetAll()
        {
            using (var context = new AuthContext())
            {
                var test = context.Audits.ToList();
                return test;
            }
        }

        public List<Audit> Search(string query)
        {
            using (var context = new AuthContext())
            {
                var audits = context.Audits.Where(c => c.tableName == query).AsQueryable();

                if (!String.IsNullOrEmpty(query))
                {
                    return audits.Where(c => c.tableName.ToLower().Contains(query.ToLower())).ToList();
                }
                else
                {
                    return audits.ToList();
                }
            }
        }

    }
}
