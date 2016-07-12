using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.Models.LicenseModel;

namespace UMPG.USL.API.Data.LicenseData
{
    public interface ILicensePRWriterRateNoteRepository
    {

        List<LicenseProductRecordingWriterRateNote> GetAll();

        LicenseProductRecordingWriterRateNote Get(int licenseWriterRateNoteId);

        LicenseProductRecordingWriterRateNote GetByLicenseWriterRateIdConfig(int licenseWriterRateId, int configuration_id);

        List<LicenseProductRecordingWriterRateNote> GetLicenseProductRecordingWriterRateNotes(List<int> licenseWriterRateIds);

        void Update(LicenseProductRecordingWriterRateNote licenseProductRecordingWriterRateNote);

        LicenseProductRecordingWriterRateNote Add(LicenseProductRecordingWriterRateNote licenseProductRecordingWriterRateNote);
 
    }
}
