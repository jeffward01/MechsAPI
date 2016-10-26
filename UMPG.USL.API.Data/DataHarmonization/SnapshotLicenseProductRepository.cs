using System;
using System.Collections.Generic;
using System.Linq;
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
                context.Snapshot_LicenseProducts.Add(licenseProductSnapshot);
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

        public List<int> GetLicenseProductIds(int licenseId)
        {
            using (var context = new AuthContext())
            {
                return
                    context.Snapshot_LicenseProducts.Where(_ => _.LicenseId == licenseId)
                        .Select(_ => _.SnapshotLicenseProductId)
                        .ToList();
            }
        }

        public bool DeleteLicenseProductSnapshot(int snapshotLicenseProductId)
        {
            using (var context = new AuthContext())
            {
                var licenseProduct =
                    context.Snapshot_LicenseProducts.First(_ => _.SnapshotLicenseProductId == snapshotLicenseProductId);
                context.Snapshot_LicenseProducts.Attach(licenseProduct);
                context.Snapshot_LicenseProducts.Remove(licenseProduct);
                try
                {
                    context.SaveChanges();
                }
                catch (Exception)
                {
                    return false;
                }
                return true;
            }
        }
    }
}