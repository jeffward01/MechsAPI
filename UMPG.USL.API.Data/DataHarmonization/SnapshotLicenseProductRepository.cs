using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.Models.DataHarmonization;
using EntityState = System.Data.Entity.EntityState;


namespace UMPG.USL.API.Data.DataHarmonization
{
    public class SnapshotLicenseProductRepository : ISnapshotLicenseProductRepository
    {
        public Snapshot_LicenseProduct SaveSnapshotLicenseProduct(Snapshot_LicenseProduct licenseProductSnapshot)
        {
            using (var context = new AuthContext())
            {
                context.Entry(licenseProductSnapshot).State = (EntityState)System.Data.EntityState.Added;
                context.SaveChanges();
                return licenseProductSnapshot;
            }
        }

        public Snapshot_LicenseProduct GetLicenseProductSnapShotById(int id)
        {
            using (var context = new AuthContext())
            {
                return context.Snapshot_LicenseProducts.FirstOrDefault(sl => sl.ProductId == id);

            }
        }
    }
}
