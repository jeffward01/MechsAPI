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
    public interface ILicenseProductWriterNoteManager
    {
        LicenseProductRecordingWriterNote Add(LicenseWriterNoteRequest noteRequest);
        LicenseProductRecordingWriterNote Edit(LicenseWriterNoteRequest noteRequest);
        LicenseProductRecordingWriterNote Get(int Id);
        List<LicenseProductRecordingWriterNote> GetAll(int Id);
        LicenseProductRecordingWriterNote Remove(int Id);
    }
}