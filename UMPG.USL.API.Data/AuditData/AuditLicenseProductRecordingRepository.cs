using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.Models.AuditModel;
using UMPG.USL.Models.Recs;

namespace UMPG.USL.API.Data.AuditData
{
    public class AuditLicenseProductRecordingRepository : IAuditLicenseProductRecordingRepository
    {

      
        public List<AuditLicenseProductRecording> GetAuditLicenseProductRecordingsBrief(int LicenseproductId)
        {
            using (var context = new AuditContext())
            {
                return context.AuditLicenseProductRecordings.Where(x => x.LicenseProductId == LicenseproductId && x.Deleted == null).ToList();
               
            }
        }

        public List<AuditLicenseProductRecording> GetAuditLicenseRecordingsList(List<int> licenseProductIds)
        {
            using (var context = new AuditContext())
            {

                var recordings = context.AuditLicenseProductRecordings
                    .Where(x => x.Deleted == null && licenseProductIds.Contains((int)x.LicenseProductId));

                return recordings.ToList();

            }

        }

        public List<AuditLicenseProductRecording> GetAuditLicenseProductRecordingsFromList(List<int> LicenseproductIds)
        {
            using (var context = new AuditContext())
            {
                return context.AuditLicenseProductRecordings.Where(x => LicenseproductIds.Contains(x.LicenseProductId) && x.Deleted == null).ToList();

            }
        }







    }
}
