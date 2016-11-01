using System;
using System.Linq;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public class SnapshotLicenseProductRecordingRepository : ISnapshotLicenseProductRecordingRepository
    {
        public Snapshot_LicenseProductRecording GetLicenseProductRecordingForCloneTrackId(int cloneTrackId)
        {
            using (var context = new AuthContext())
            {
                return context.Snapshot_LicenseRecordings.FirstOrDefault(_ => _.TrackId == cloneTrackId);
            }
        }

        public bool DeletePhoneBySnapshotPhoneId(int snapshotLicenseProductRecordingId)
        {
            using (var context = new AuthContext())
            {
                var licenseProductRecording = context.Snapshot_LicenseRecordings.Find(snapshotLicenseProductRecordingId);
                context.Snapshot_LicenseRecordings.Attach(licenseProductRecording);
                context.Snapshot_LicenseRecordings.Remove(licenseProductRecording);
                try
                {
                    context.SaveChanges();
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }
    }
}