using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public Snapshot_LicenseProduct GetLicenseProductByLicenseProductId(int id)
        {
            using (var context = new AuthContext())
            {
                return context.Snapshot_LicenseProducts.FirstOrDefault(sl => sl.CloneLicenseProductId == id);
            }
        }

        public Snapshot_LicenseProduct SaveMassiveSnapshotLicenseProduct(Snapshot_LicenseProduct licenseProductSnapshot)
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

        //private bool InsertChildren(Snapshot_LicenseProduct licenseProductSnapshot)
        //{
        //    using (var context = new AuthContext())
        //    {
        //        context.Configuration.AutoDetectChangesEnabled = false;
        //        context.Configuration.ValidateOnSaveEnabled = false;

        //        context.Configuration.AutoDetectChangesEnabled = true;
        //        context.Configuration.ValidateOnSaveEnabled =  true;
        //    }
        //}
        public bool DoesLicenseProductSnapshotExist(int licenseProductId)
        {
            using (var context = new AuthContext())
            {
                return context.Snapshot_LicenseProducts.Any(sl => sl.CloneLicenseProductId == licenseProductId);
            }
        }

        public int GetSnapshotProductHeaderId(int licenseProductId)
        {
            using (var context = new AuthContext())
            {
                return context.Snapshot_LicenseProducts.FirstOrDefault(sl => sl.CloneLicenseProductId == licenseProductId).SnapshotProductHeaderId;
            }
        }

        public int GetSnapshotCloneProductHeaderId(int snapshotProductHeaderId)
        {
            using (var context = new AuthContext())
            {
                return context.Snapshot_ProductHeaders.FirstOrDefault(sl => sl.SnapshotProductHeaderId == snapshotProductHeaderId).CloneProductHeaderId;
            }
        }

        public bool DoesProductHeaderSnapshotExist(int licenseProductId)
        {
            using (var context = new AuthContext())
            {
                var result =context.Snapshot_LicenseProducts.FirstOrDefault(sl => sl.CloneLicenseProductId == licenseProductId);
                if (result != null)
                {
                    return result.SnapshotProductHeaderId != 0;
                }
                return false;
            }
        }

        public bool DoesProductHeaderSnapshotExistById(int cloneProductHeaderId)
        {
            using (var context = new AuthContext())
            {
                var result = context.Snapshot_ProductHeaders.FirstOrDefault(sl => sl.CloneProductHeaderId == cloneProductHeaderId);
                if (result != null)
                {
                    return result.SnapshotProductHeaderId != 0;
                }
                return false;
            }
        }


        public Snapshot_LicenseProduct GetLicenseProductSnapShotById(int id)
        {
            using (var context = new AuthContext())
            {
                return context.Snapshot_LicenseProducts.FirstOrDefault(sl => sl.ProductId == id);
            }
        }

        public Snapshot_LicenseProduct GetSnapshotLicenseProductByLicenseProductId(int licenseProductId)
        {
            using (var context = new AuthContext())
            {
                return context.Snapshot_LicenseProducts.FirstOrDefault(sl => sl.CloneLicenseProductId == licenseProductId);
            }
        }

        public Snapshot_LicenseProduct GetSnapshotLicenseProductBySnapshotProductHeaderId(int snapshotProductHeaderID)
        {
            using (var context = new AuthContext())
            {
                return context.Snapshot_LicenseProducts.FirstOrDefault(sl => sl.SnapshotProductHeaderId == snapshotProductHeaderID);
            }
        }

        public Snapshot_LicenseProduct GetLicenseProductSnapShotByLPSnapId(int id)
        {
            using (var context = new AuthContext())
            {
                return context.Snapshot_LicenseProducts.FirstOrDefault(sl => sl.SnapshotLicenseProductId == id);
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

                        .Include("ProductHeader")
                        .Include("ProductHeader.Artist")
                         // .Include("ProductHeader.Label")
                         // .Include("ProductHeader.Label.RecordLabelGroups")
                         /*
                       .Include("ProductHeader.Configurations") //TEST
                       .Include("ProductHeader.Configurations.Configuration")
                       .Include("ProductHeader.Configurations.LicenseProductConfiguration")
                         */
                         // Not needed .Include("ProductConfigurations") //comes back null, i think in the test license case its supposed
                         .Include("Schedule")
                         .Include("Recordings")
                         .Include("Recordings.Writers")  //Add to database
                         .Include("Recordings.Writers.KnownAs")
                         .Include("Recordings.Writers.Affiliation")
                         .Include("Recordings.Writers.Affiliation.Affiliations")
                         .Include("Recordings.Writers.OriginalPublishers")
                         .Include("Recordings.Writers.OriginalPublishers.Affiliation")
                         .Include("Recordings.Writers.OriginalPublishers.Affiliation.Affiliations")
                         .Include("Recordings.Writers.OriginalPublishers.KnownAs")
                         .Include("Recordings.Writers.OriginalPublishers.Administrator")
                         .Include("Recordings.Writers.OriginalPublishers.Administrator.Affiliation")
                         .Include("Recordings.Writers.OriginalPublishers.Administrator.Affiliation.Affiliations")
                         .Include("Recordings.Writers.OriginalPublishers.Administrator.KnownAs")
                         .Include("Recordings.Track")
                         .Include("Recordings.Track.Artist")
                         .Include("Recordings.Track.Copyrights")
                         .Include("Recordings.Track.Copyrights.Composers")
                         .Include("Recordings.Track.Copyrights.Composers.KnownAs")
                         .Include("Recordings.Track.Copyrights.Composers.OriginalPublishers")
                         .Include("Recordings.Track.Copyrights.Composers.OriginalPublishers.Affiliation")
                         .Include("Recordings.Track.Copyrights.Composers.OriginalPublishers.Affiliation.Affiliations")
                         .Include("Recordings.Track.Copyrights.Composers.OriginalPublishers.KnownAs")
                         .Include("Recordings.Track.Copyrights.Composers.OriginalPublishers.Administrator")
                         .Include("Recordings.Track.Copyrights.Composers.OriginalPublishers.Administrator.KnownAs")
                         .Include("Recordings.Track.Copyrights.Composers.OriginalPublishers.Administrator.Affiliation")
                         .Include("Recordings.Track.Copyrights.Composers.OriginalPublishers.Administrator.Affiliation.Affiliations")
                         // .Include("Recordings.Track.Copyrights.Composers.LicenseProductRecordingWriter")
                         .Include("Recordings.Track.Copyrights.Composers.Affiliation.Affiliations")
                         .Include("Recordings.Track.Copyrights.Samples")
                         .Include("Recordings.Track.Copyrights.Samples.LocalClients")
                         .Include("Recordings.Track.Copyrights.Samples.AquisitionLocationCodes")
                         .Include("Recordings.Track.Copyrights.LocalClients")
                         .Include("Recordings.Track.Copyrights.AquisitionLocationCodes")
                         .Include("Recordings.LicenseRecording")
                          .AsNoTracking()
                    //   .Include("Recordings.LicenseRecording") //Add to database??  BROKEN, its in database, but when its 'on' recordings are not returned

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

        public Snapshot_LicenseProduct UpdateSnapshotLicenseProduct(Snapshot_LicenseProduct snapshotLicenseProduct)
        {
            using (var context = new AuthContext())
            {
                context.Entry(snapshotLicenseProduct).State = (EntityState)System.Data.EntityState.Modified;
                context.SaveChanges();
                return snapshotLicenseProduct;
            }
        }
    }
}