using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMPG.USL.API.Business.Licenses
{
    using UMPG.USL.API.Data.LicenseData;
    using UMPG.USL.Models.LicenseModel;

    public class LicenseProductWriterNoteManager : ILicenseProductWriterNoteManager
    {

        private readonly ILicensePRWriterNoteRepository _licensePRWriterNoteRepository;
        

        public LicenseProductWriterNoteManager(ILicensePRWriterNoteRepository licensePRWriterNoteRepository)
        {
            _licensePRWriterNoteRepository = licensePRWriterNoteRepository;
        }

        public LicenseProductRecordingWriterNote Add(LicenseWriterNoteRequest noteRequest)
        {
            var lLicenseWriteNote = new LicenseProductRecordingWriterNote
            {

                LicenseWriterId = noteRequest.LicenseWriterId,
                Configuration_Id = noteRequest.Configuration_id,
                CreatedDate = DateTime.Now,
                Note = noteRequest.Note
                
            };
            var newNote = _licensePRWriterNoteRepository.Add(lLicenseWriteNote);
            return _licensePRWriterNoteRepository.Get(newNote.LicenseWriterNoteId);
        }

        public LicenseProductRecordingWriterNote Edit(LicenseWriterNoteRequest noteRequest)
        {
            var lLicenseWriteNote = new LicenseProductRecordingWriterNote
            {

                LicenseWriterId = noteRequest.LicenseWriterId,
                LicenseWriterNoteId = noteRequest.LicenseWriterNoteId,
                Configuration_Id = noteRequest.Configuration_id,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,

                Note = noteRequest.Note

            };

            _licensePRWriterNoteRepository.Update(lLicenseWriteNote);
            return lLicenseWriteNote;

        }


        public LicenseProductRecordingWriterNote Get(int id)
        {
            return _licensePRWriterNoteRepository.Get(id);
        }

        public List<LicenseProductRecordingWriterNote> GetAll(int id)
        {
            return _licensePRWriterNoteRepository.GetAll(id);
        }

        public LicenseProductRecordingWriterNote Remove(int id)
        {
            var writerNote = _licensePRWriterNoteRepository.Get(id);
            writerNote.Deleted = DateTime.Now;
            writerNote.ModifiedDate = DateTime.Now;

            _licensePRWriterNoteRepository.Update(writerNote);
            return writerNote;
        }
    }
}
