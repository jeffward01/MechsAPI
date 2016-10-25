using NLog;
using System;
using System.Linq;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public class SnapshotLicenseRepository : ISnapshotLicenseRepository
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public Snapshot_License SaveSnapshotLicense(Snapshot_License licenseSnapshot)
        {
            using (var context = new AuthContext())
            {
                context.Snapshot_Licenses.Add(licenseSnapshot);
                try
                {
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    Logger.Debug(e.ToString);
                    throw new Exception("Error saving snapshot_licnese:     " + e.ToString());
                }
                return licenseSnapshot;
            }
        }

        public Snapshot_License GetLicenseSnapShotById(int id)
        {
            using (var context = new AuthContext())
            {
                return context.Snapshot_Licenses.FirstOrDefault(sl => sl.CloneLicenseId == id);
            }
        }
    }
}