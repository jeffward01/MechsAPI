using System.Linq;
using System.Collections.Generic;
using System.Data;
using System;
using UMPG.USL.API.Data.Utils;
using UMPG.USL.Models;
using UMPG.USL.Models.AuditModel;
using EntityState = System.Data.Entity.EntityState;

namespace UMPG.USL.API.Data.AuditData
{
    public class AuditLicenseNoteRepository : IAuditLicenseNoteRepository
    {

        public AuditLicenseNote Get(int id)
        {
            using (var context = new AuditContext())
            {
                var licensenote = context.AuditLicenseNotes
                    
                    .FirstOrDefault(c => c.licenseNoteId == id);
                return licensenote;
            }
        }

        public List<AuditLicenseNote> GetAll()
        {
            using (var context = new AuditContext())
            {
                var licensenotes = context.AuditLicenseNotes.ToList();
                return licensenotes;

            }
        }






    }
}
