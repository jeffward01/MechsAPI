using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Win32;
using UMPG.USL.Models.LicenseModel;

namespace UMPG.USL.API.Data.LicenseData
{
    public interface ILicenseRecordingMedleyRepository
    {
        LicenseRecordingMedley Add(LicenseRecordingMedley medleyTrack);
        List<LicenseRecordingMedley> GetMedleysByTrackId(long trackId);
        void Update(LicenseRecordingMedley medley);
     }
}
