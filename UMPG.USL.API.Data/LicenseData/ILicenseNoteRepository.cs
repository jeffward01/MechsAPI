using System.Collections.Generic;
using UMPG.USL.Models;
using UMPG.USL.Models.LicenseModel;

namespace UMPG.USL.API.Data.LicenseData
{
    public interface ILicenseNoteRepository
    {
        LicenseNote Add(LicenseNote licensenote);

        LicenseNote Get(int Id);

        List<LicenseNote> GetAll();

        List<LicenseNote> Search(string query);

        List<LicenseNote> GetLicenseNotes(int licenseid);

        LicenseNote GetLicenseNote(int licenseNoteid);

        void UpdateLicenseNote(LicenseNote licensenote);
        
    }
}
