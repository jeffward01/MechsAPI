using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.Models.LicenseModel;
using EntityState = System.Data.Entity.EntityState;

namespace UMPG.USL.API.Data.LicenseData
{
    public class LicensePRWriterRateNoteRepository : ILicensePRWriterRateNoteRepository
    {


        public List<LicenseProductRecordingWriterRateNote> GetAll()
        {
            using (var context = new AuthContext())
            {
                var licensePRwriterRateNotes = context.LicenseProductRecordingWriterRateNotes.ToList();
                return licensePRwriterRateNotes;

            }
        }

        public List<LicenseProductRecordingWriterRateNote> GetLicenseProductRecordingWriterRateNotes(List<int> licenseWriterRateIds)
        {
            using (var context = new AuthContext())
            {
                return context.LicenseProductRecordingWriterRateNotes.Where(x => licenseWriterRateIds.Contains((int)x.LicenseWriterRateId) && x.Deleted == null).ToList();
            }
        }

        public LicenseProductRecordingWriterRateNote Get(int licenseWriterRateNoteId)
        {
            using (var context = new AuthContext())
            {
                var licensePRWriterRateNote = context.LicenseProductRecordingWriterRateNotes
                    .FirstOrDefault(c => c.LicenseWriterRateNoteId == licenseWriterRateNoteId);
                return licensePRWriterRateNote;
            }

        }

        public LicenseProductRecordingWriterRateNote GetByLicenseWriterRateIdConfig(int licenseWriterRateId, int configuration_id)
        {
            using (var context = new AuthContext())
            {
                var licensePRWriterRateNote = context.LicenseProductRecordingWriterRateNotes
                    .FirstOrDefault(c => c.LicenseWriterRateId == licenseWriterRateId && c.configuration_id == configuration_id && c.Deleted == null);
                return licensePRWriterRateNote;
            }

        }


        public void Update(LicenseProductRecordingWriterRateNote licenseProductRecordingWriterRateNote)
        {
            using (var context = new AuthContext())
            {
                context.Entry(licenseProductRecordingWriterRateNote).State = (EntityState)System.Data.EntityState.Modified;
                context.SaveChanges();
            }
        }


        public LicenseProductRecordingWriterRateNote Add(LicenseProductRecordingWriterRateNote licenseProductRecordingWriterRateNote)
        {
            using (var context = new AuthContext())
            {

                context.LicenseProductRecordingWriterRateNotes.Add(licenseProductRecordingWriterRateNote);
                context.SaveChanges();

                return licenseProductRecordingWriterRateNote;
            }
        }

    }
}
