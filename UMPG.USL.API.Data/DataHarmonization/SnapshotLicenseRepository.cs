using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                context.Entry(licenseSnapshot).State = (EntityState)System.Data.EntityState.Added;
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
