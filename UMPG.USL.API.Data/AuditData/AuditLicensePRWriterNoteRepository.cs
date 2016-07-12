using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.Models.AuditModel;
using EntityState = System.Data.Entity.EntityState;

namespace UMPG.USL.API.Data.AuditData
{
    public class AuditLicensePRWriterNoteRepository : IAuditLicensePRWriterNoteRepository
    {

        public AuditLicenseProductRecordingWriterNote Get(int id)
        {
            using (var context = new AuditContext())
            {
                var licensenote = context.AuditLicenseProductRecordingWriterNotes.FirstOrDefault(c => c.LicenseWriterNoteId == id);
                return licensenote;
            }
        }

        public List<AuditLicenseProductRecordingWriterNote> GetAll(int id)
        {
            using (var context = new AuditContext())
            {
                var licensePrWriterNotesList = context.AuditLicenseProductRecordingWriterNotes.Where((c => c.LicenseWriterNoteId == id)).ToList();
                return licensePrWriterNotesList;

            }
        }


        public List<AuditLicenseProductRecordingWriterNote> GetAuditLicenseProductRecordingWriterNotes(List<int> licenseWriterIds)
        {
            using (var context = new AuditContext())
            {
                return context.AuditLicenseProductRecordingWriterNotes.Where(x => licenseWriterIds.Contains((int)x.LicenseWriterId) && x.Deleted == null).ToList();
            }
        }

        

    }
}
