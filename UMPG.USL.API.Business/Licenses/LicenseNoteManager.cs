using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.API.Data.LicenseData;
using UMPG.USL.API.Data.LookupData;
using UMPG.USL.API.Data.Recs;
using UMPG.USL.Models;
using UMPG.USL.Models.LicenseModel;
using UMPG.USL.Models.LookupModel;
using UMPG.USL.Models.Recs;


namespace UMPG.USL.API.Business.Licenses
{
    public class LicenseNoteManager : ILicenseNoteManager
    {

        private readonly ILicenseNoteRepository _licenseNoteRepository;
        private readonly INoteTypeRepository _noteTypeRepository;

        public LicenseNoteManager(ILicenseNoteRepository licenseNoteRepository, INoteTypeRepository noteTypeRepository)
        {
            _licenseNoteRepository = licenseNoteRepository;
            _noteTypeRepository = noteTypeRepository;
        }

        public LicenseNote Get(int id)
        {
            return _licenseNoteRepository.Get(id);
        }

        public List<LicenseNote> GetAll()
        {
            return _licenseNoteRepository.GetAll();
        }

        public LicenseNote Add(LicenseNoteRequest noteRequest)
        {
            var lLicenseNote = new LicenseNote
            {
                Note = noteRequest.LicenseNote,
                licenseId = noteRequest.LicenseId,
                CreatedBy = noteRequest.ContactId,
                CreatedDate = DateTime.Now,
                NoteTypeId = noteRequest.NoteTypeId
                
            };
            var newNote= _licenseNoteRepository.Add(lLicenseNote);
            return _licenseNoteRepository.Get(newNote.licenseNoteId);

        }

        public List<LicenseNote> Search(string query)
        {
            return _licenseNoteRepository.Search(query);

        }

        public List<LicenseNote> GetLicenseNotes(int licenseid)
        {
            return _licenseNoteRepository.GetLicenseNotes(licenseid);
        }

        public List<LU_NoteType> GetLicenseNoteTypes()
        {
            return _noteTypeRepository.GetLicenseNoteTypes();
        }

        public bool UpdateLicenseNote(LicenseNote licenseNote)
        {
            licenseNote.ModifiedDate = DateTime.Now;
            _licenseNoteRepository.UpdateLicenseNote(licenseNote);
            return true;
        }

        public bool DeleteLicenseNotes(List<int> licenseNotesIds)
        {
            foreach (var licenseNotesId in licenseNotesIds)
            {
                var lLicenseNote = _licenseNoteRepository.Get(licenseNotesId);
                lLicenseNote.Deleted = DateTime.Now;
                _licenseNoteRepository.UpdateLicenseNote(lLicenseNote);
            }
            return true;
        }

        public LicenseNote GetLicenseNote(int licenseNoteid)
        {
            return _licenseNoteRepository.GetLicenseNote(licenseNoteid);
        }


    }
}
