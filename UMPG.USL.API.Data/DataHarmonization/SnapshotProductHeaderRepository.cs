using System;
using System.Data.Entity;
using System.Linq;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public class SnapshotProductHeaderRepository : ISnapshotProductHeaderRepository
    {
        public Snapshot_ProductHeader SaveSnapshotProductHeader(Snapshot_ProductHeader snapshotProductHeader)
        {
            using (var context = new AuthContext())
            {
                context.Snapshot_ProductHeaders.Add(snapshotProductHeader);
                context.SaveChanges();
                return snapshotProductHeader;
            }
        }

        public Snapshot_ProductHeader GetSnapshotProductHeaderBySnapshotProductHeaderId(int snapshotProductHeaderId)
        {
            using (var context = new AuthContext())
            {
                return context.Snapshot_ProductHeaders.
                           Include("Configurations")
                    .Include("Configurations.Configuration")
                       .Include("Artist")
                    .Include("Label").

                    FirstOrDefault(_ => _.SnapshotProductHeaderId == snapshotProductHeaderId);
            }
        }

        public Snapshot_ProductHeader GetSnapshotProductHeaderByLicenseId(int licenseId)
        {
            using (var context = new AuthContext())
            {
                var licenseProduct =  context.Snapshot_LicenseProducts.FirstOrDefault(_ => _.LicenseId == licenseId && _.Deleted == null);
                var result =
                    context.Snapshot_ProductHeaders
                    .Include("Configurations")
                //    .Include("Configurations.Configuration")
                    //.Include("Configurations.LicenseProductConfiguration")
                    .Include("Artist")
                    .Include("Label").
                    FirstOrDefault(
                        _ => _.SnapshotProductHeaderId == licenseProduct.SnapshotProductHeaderId);


                return result;
            }
        }


        public void UpdateSnapshotProductHeader(Snapshot_ProductHeader snapshotProductHeader)
        {
            using (var context = new AuthContext())
            {
                context.Entry(snapshotProductHeader).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
        public Snapshot_ProductHeader GetProductHeaderByProductHeaderId(int productHeaderId)
        {
            using (var context = new AuthContext())
            {
                return context.Snapshot_ProductHeaders.FirstOrDefault(_ => _.CloneProductHeaderId == productHeaderId);
            }
        }

        public Snapshot_ProductHeader GetSnapshotProductHeaderByLabelSnapshotId(int labelSnapshotId)
        {
            using (var context = new AuthContext())
            {
                return context.Snapshot_ProductHeaders.FirstOrDefault(_ => _.SnapshotLabelId == labelSnapshotId);
            }
        }

        public int GetSnapshotProductHeaderBySnapshotLicenseProductId(int snapshotLicenseProductId)
        {
            using (var context = new AuthContext())
            {
                var licenseProduct = context.Snapshot_LicenseProducts.Find(snapshotLicenseProductId);
                return licenseProduct.SnapshotLicenseProductId;
            }
        }

        public Snapshot_ProductHeader GetProductHeaderLite(int productHeaderSnapshotId)
        {
            using (var context = new AuthContext())
            {
                return context.Snapshot_ProductHeaders.
                    Include("Configurations").
                    FirstOrDefault(_ => _.SnapshotProductHeaderId == productHeaderSnapshotId);
            }

        }

        public bool DeleteProductHeaderSnapshotBySnapshotId(int snapshotProductHeaderId)
        {
            using (var context = new AuthContext())
            {
                var productHeader =
                    context.Snapshot_ProductHeaders.Find(snapshotProductHeaderId);
                if (productHeader == null)
                {
                    return false;
                }
                context.Snapshot_ProductHeaders.Attach(productHeader);
                context.Snapshot_ProductHeaders.Remove(productHeader);
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