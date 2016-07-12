using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.Models;
using UMPG.USL.Models.LicenseModel;
using UMPG.USL.Models.LookupModel;
using UMPG.USL.Models.Recs;

namespace UMPG.USL.API.Business.Licenses
{
    public interface ILicenseNoteManager
    {
        LicenseNote Add(LicenseNoteRequest noteRequest);

        LicenseNote Get(int id);

        List<LicenseNote> GetAll();

        List<LicenseNote> Search(string query);

        LicenseNote GetLicenseNote(int licenseNoteId);

        List<LicenseNote> GetLicenseNotes(int licenseId);

        List<LU_NoteType> GetLicenseNoteTypes();

        bool UpdateLicenseNote(LicenseNote licenseNote);

        bool DeleteLicenseNotes(List<int> licenseNotesIds);

    }
}
