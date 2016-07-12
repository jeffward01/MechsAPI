using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.Models.LicenseModel;

namespace UMPG.USL.API.Data.LicenseData
{
    public class RecordingMedleyRepository:IRecordingMedleyRepository
    {
        public RecordingMedley GetByLicenseRecordingId(int licenseRecordingId)
        {
            using (var context = new AuthContext())
            {
                return context.RecordingMedleys.Include("LicenseRecordingMedley").Include("LicenseProductRecording")
                    .FirstOrDefault(x => x.LicenseRecordingId == licenseRecordingId && !x.Deleted.HasValue);
            }
        }
        public List<RecordingMedley> GetByLicenseRecordingIds(List<int> licenseRecordingIds)
        {
            using (var context = new AuthContext())
            {
                return context.RecordingMedleys.Include("LicenseRecordingMedley").Include("LicenseProductRecording")
                    .Where(x => licenseRecordingIds.Contains(x.LicenseRecordingId) && !x.Deleted.HasValue).ToList();
            }
        }

        public RecordingMedley Add(RecordingMedley recordingMedley)
        {
            using (var context = new AuthContext())
            {
                context.RecordingMedleys.Add(recordingMedley);
                context.SaveChanges();
                return recordingMedley;
            }
        }
    }
}
