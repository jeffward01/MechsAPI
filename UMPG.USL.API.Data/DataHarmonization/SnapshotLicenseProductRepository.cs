using System;
using System.Collections.Generic;
using System.Linq;
using NLog;
using UMPG.USL.Models.DataHarmonization;
using EntityState = System.Data.Entity.EntityState;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public class SnapshotLicenseProductRepository : ISnapshotLicenseProductRepository
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        public Snapshot_LicenseProduct SaveSnapshotLicenseProduct(Snapshot_LicenseProduct licenseProductSnapshot)
        {
            using (var context = new AuthContext())
            {
                context.Snapshot_LicenseProducts.Add(licenseProductSnapshot);
                try
                {
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    Logger.Debug(e.ToString());
                    throw new Exception(e.ToString());
                }
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

        public List<Snapshot_LicenseProduct> GetAllLicenseProductsForLicenseId(int licenseId)
        {

            using (var context = new AuthContext())
            {
                return
                    context.Snapshot_LicenseProducts

                    //        .Include("ProductHeader")
                    //      .Include("ProductHeader.Artist")
                    //      .Include("ProductHeader.Label")
                    //                  .Include("ProductHeader.Configurations") //TEST
                    //       .Include("ProductHeader.Configurations.Configuration") // Error here, not showing yet
                    //   .Include("ProductHeader.Configurations.LicenseProductConfiguration")
                    //  .Include("ProductHeader.Label.RecordLabelGroups")
                    //   .Include("ProductConfigurations") //comes back null, i think in the test license case its supposed
                         .Include("Schedule")
                    //         .Include("Recordings")
                    //        .Include("Recordings.Writers")  //Add to database
                    //       .Include("Recordings.Track")  //Add to database  || works
                    //       .Include("Recordings.Track.Copyrights")
                    //       .Include("Recordings.Track.Copyrights.Composers")
                    //       .Include("Recordings.Track.Copyrights.Samples")
                    //       .Include("Recordings.Track.Copyrights.LocalClients")
                    //       .Include("Recordings.Track.Copyrights.AquisitionLocationCodes")
                    //       .Include("Recordings.Track.Artist")
                    // .Include("Recordings.LicenseRecording") //Add to database??  BROKEN, its in database, but when its 'on' recordings are not returned


                    .Where(_ => _.LicenseId == licenseId)
                        .ToList();
            }
        }

        public int? GetProductIdFromSnapshotLicenseProductId(int snapshotLicenseProductId)
        {
            using (var context = new AuthContext())
            {
                var licneseProduct = context.Snapshot_LicenseProducts.Find(snapshotLicenseProductId);
                return licneseProduct.ProductId;
            }
        }


        public int? GetLicenseProductIdFromSnapshotLicenseProductId(int snapshotLicenseProductId)
        {
            using (var context = new AuthContext())
            {
                var licneseProduct = context.Snapshot_LicenseProducts.Find(snapshotLicenseProductId);
                return licneseProduct.CloneLicenseProductId;
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