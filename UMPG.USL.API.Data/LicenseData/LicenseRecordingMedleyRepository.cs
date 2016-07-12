using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using UMPG.USL.API.Data.LookupData;
using UMPG.USL.Models.LicenseModel;

namespace UMPG.USL.API.Data.LicenseData
{
    public class LicenseRecordingMedleyRepository:ILicenseRecordingMedleyRepository
    {
        public LicenseRecordingMedley Add(LicenseRecordingMedley medleyTrack)
        {
            using (var context = new AuthContext())
            {
                context.LicenseRecordingMedleys.Add(medleyTrack);
                context.SaveChanges();

                return medleyTrack;
            }
        }

        public List<LicenseRecordingMedley> GetMedleysByTrackId(long trackId)
        {
            using (var context = new AuthContext())
            {
                return context.LicenseRecordingMedleys.Where(x => x.TrackId == trackId && !x.Deleted.HasValue).ToList();
            }
        }

        public void Update(LicenseRecordingMedley medley)
        {
            using (var context = new AuthContext())
            {
                context.Entry(medley).State = (EntityState)System.Data.EntityState.Modified;
                context.SaveChanges();
            }
        }

      }
}
