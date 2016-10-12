using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using UMPG.USL.Models.LicenseModel;

namespace UMPG.USL.API.Data.LicenseData
{
    public class LicenseProductRecordingRepository : ILicenseProductRecordingRepository
    {
        public int GetLicenseProductRecordingsNo(int licenseproductId)
        {
            using (var context = new AuthContext())
            {
                return context.LicenseProductRecordings.Count(x => x.LicenseProductId == licenseproductId && x.Deleted == null);
            }
        }

        public List<int> GetLicenseProductRecordingsIds(int licenseproductId)
        {
            using (var context = new AuthContext())
            {
                return context.LicenseProductRecordings.Where(x => x.LicenseProductId == licenseproductId)
                    .Select(x => x.TrackId).DefaultIfEmpty(0).ToList();
            }
        }

        public bool IsAlreadyPresent(int trackId, int licneseProductId)
        {
            using (var context = new AuthContext())
            {
                var result =
                    context.LicenseProductRecordings.FirstOrDefault(
                        lp => lp.LicenseProductId == licneseProductId && lp.TrackId == trackId);
                if (result != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public List<int> GetLicenseRecordingsIdsByLicenseProductId(int licenseproductId)
        {
            using (var context = new AuthContext())
            {
                return context.LicenseProductRecordings.Where(x => x.LicenseProductId == licenseproductId)
                    .Select(x => x.LicenseRecordingId).DefaultIfEmpty(0).ToList();
            }
        }

        public LicenseProductRecording GetLicenseRecordingsByRecsTrackId(int trackId)
        {
            using (var context = new AuthContext())
            {
                return context.LicenseProductRecordings.Where(x => x.TrackId == trackId).FirstOrDefault();
            }
        }

        public List<LicenseProductRecording> GetLicenseProductRecordingsBrief(int LicenseproductId)
        {
            using (var context = new AuthContext())
            {
                return context.LicenseProductRecordings.Where(x => x.LicenseProductId == LicenseproductId && x.Deleted == null).ToList();
            }
        }

        public List<LicenseProductRecording> GetLicenseRecordingsList(List<int> licenseProductIds)
        {
            using (var context = new AuthContext())
            {
                var recordings = context.LicenseProductRecordings
                    .Where(x => x.Deleted == null && licenseProductIds.Contains((int)x.LicenseProductId));

                return recordings.ToList();
            }
        }

        public List<LicenseProductRecording> GetLicenseProductRecordingsFromList(List<int> LicenseproductIds)
        {
            using (var context = new AuthContext())
            {
                return context.LicenseProductRecordings.Where(x => LicenseproductIds.Contains(x.LicenseProductId) && x.Deleted == null).ToList();
            }
        }

        public LicenseProductRecording Add(LicenseProductRecording licenseProductRecording)
        {
            using (var context = new AuthContext())
            {
                context.LicenseProductRecordings.Add(licenseProductRecording);
                context.SaveChanges();
                return licenseProductRecording;
            }
        }

        public void Update(LicenseProductRecording licenseProductRecording)
        {
            using (var context = new AuthContext())
            {
                context.Entry(licenseProductRecording).State = (EntityState)System.Data.EntityState.Modified;
                context.SaveChanges();
            }
        }

        public List<LicenseProductRecording> GetLicenseRecordingsByLicenseProductId(int licenseProductId)
        {
            using (var context = new AuthContext())
            {
                return context.LicenseProductRecordings.Where(x => x.LicenseProductId == licenseProductId && x.Deleted == null).ToList();
            }
        }

        public List<LicenseProductRecording> GetLicenseProductRecordingsFromIds(List<int> licenseRecordingIds)
        {
            using (var context = new AuthContext())
            {
                return context.LicenseProductRecordings.Where(x => licenseRecordingIds.Contains(x.LicenseRecordingId) && x.Deleted == null).ToList();
            }
        }
    }
}