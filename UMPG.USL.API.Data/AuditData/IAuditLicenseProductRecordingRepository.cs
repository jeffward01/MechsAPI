using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.Models.AuditModel;
using UMPG.USL.Models.Recs;

namespace UMPG.USL.API.Data.AuditData
{
    public interface IAuditLicenseProductRecordingRepository
    {

        List<AuditLicenseProductRecording> GetAuditLicenseProductRecordingsBrief(int LicenseproductId);

        List<AuditLicenseProductRecording> GetAuditLicenseRecordingsList(List<int> licenseProductIds);

        List<AuditLicenseProductRecording> GetAuditLicenseProductRecordingsFromList(List<int> LicenseproductIds);

    }
}
