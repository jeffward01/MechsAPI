using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.Models.LicenseModel;

namespace UMPG.USL.API.Data.LicenseData
{


    public interface ILicensePRWriterNoteRepository
    {
        LicenseProductRecordingWriterNote Add(LicenseProductRecordingWriterNote licenseProductRecordingWriterNote);

        LicenseProductRecordingWriterNote Get(int id);

        List<LicenseProductRecordingWriterNote> GetAll(int id);

        void Update(LicenseProductRecordingWriterNote licenseProductRecordingWriterNote);

        List<LicenseProductRecordingWriterNote> GetLicenseProductRecordingWriterNotes(List<int> licenseWriterIds);
    
    }
}
