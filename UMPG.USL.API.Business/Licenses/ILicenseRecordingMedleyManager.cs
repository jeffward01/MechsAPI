using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.Models.LicenseModel;

namespace UMPG.USL.API.Business.Licenses
{
    public interface ILicenseRecordingMedleyManager
    {
        void AddMedleys(List<LicenseRecordingMedley> medleys);
        List<LicenseRecordingMedley> GetMedleysByTrackId(long trackId);
    }
}
