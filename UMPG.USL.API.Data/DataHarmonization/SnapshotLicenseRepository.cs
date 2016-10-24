using System.Linq;
using UMPG.USL.Models.DataHarmonization;
using EntityState = System.Data.Entity.EntityState;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public class SnapshotLicenseRepository : ISnapshotLicenseRepository
    {
        public Snapshot_License SaveSnapshotLicense(Snapshot_License licenseSnapshot)
        {
            using (var context = new AuthContext())
            {
                context.Snapshot_Licenses.Add(licenseSnapshot);
                context.SaveChanges();
                return licenseSnapshot;
            }
        }

        public Snapshot_License GetLicenseSnapShotById(int id)
        {
            using (var context = new AuthContext())
            {
                return context.Snapshot_Licenses.FirstOrDefault(sl => sl.LicenseId == id);
            }
        }

    }
}