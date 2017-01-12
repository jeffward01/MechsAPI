using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.Models.LicenseModel;
using UMPG.USL.Models.StaticDropdownsData;

namespace UMPG.USL.API.Data.LicenseData
{
    public interface ILicensePRWriterRepository
    {
        int GetLicenseProductRecordingWritersNo(int trackId);
        int GetLicenseProductRecordingLicensedWritersNo(int trackId);
        LicenseProductRecordingWriter Get(int writerId);
        List<LicenseProductRecordingWriter> GetLicenseProductRecordingWriters(int licenseRecordingId);
        LicenseProductRecordingWriter GetByRecordingIdAndCaeNumber(int recordingId, int caeNumber, string ipCode);

        List<LicenseProductRecordingWriter> GetLicenseProductRecordingWritersBrief(int licenseRecordingId);
        void Update(LicenseProductRecordingWriter licenseRecordingWriter);
        LicenseProductRecordingWriter Add(LicenseProductRecordingWriter licenseProductRecording);

        List<int> GetLicenseRecordingWriterIds(List<int> licenseRecordingIds);

        List<LicenseProductRecordingWriter> GetLicenseWriters(int licenseRecordingId);

        List<LicenseProductRecordingWriter> GetLicenseWriterList(List<int> licenseRecordingIds);

        List<LicenseProductRecordingWriter> GetLicensePrWritersFromIds(List<int> writerIds);
        int GetUnLicenseProductRecordingLicensedWritersNo(int licenseRecordingId);
    }
}
