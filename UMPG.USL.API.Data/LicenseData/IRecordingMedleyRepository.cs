using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.Models.LicenseModel;

namespace UMPG.USL.API.Data.LicenseData
{
    public interface IRecordingMedleyRepository
    {
        RecordingMedley GetByLicenseRecordingId(int licenseRecordingId);
        List<RecordingMedley> GetByLicenseRecordingIds(List<int> licenseRecordingIds);
        RecordingMedley Add(RecordingMedley recordingMedley);
    }
}
