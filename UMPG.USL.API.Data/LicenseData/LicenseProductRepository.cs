using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Internal;
using UMPG.USL.Models.LicenseModel;
using UMPG.USL.Models.Recs; // added for recs configurtion model in

using EntityState = System.Data.Entity.EntityState;
using UMPG.USL.Models.StaticDropdownsData;

namespace UMPG.USL.API.Data.LicenseData
{
    public class LicenseProductRepository : ILicenseProductRepository
    {
        public int GetProductsNo(int licenseId)
        {
            using (var context = new AuthContext())
            {
                return context.LicenseProducts.Count(x => x.LicenseId == licenseId && x.Deleted == null);
            }
        }

        public int GetLicensesNo(int productId)
        {
            using (var context = new AuthContext())
            {
                return context.LicenseProducts.Count(x => x.ProductId == productId && x.Deleted == null);
            }
        }

        public List<int> GetProductsIds(int licenseId)
        {
            using (var context = new AuthContext())
            {
                return context.LicenseProducts.Where(x => x.LicenseId == licenseId && x.Deleted == null)
                    .Select(x => x.ProductId)   //tomh changed from LicenseProductId for myview page
                    .ToList();
            }
        }

        public List<int> GetLicenseIds(int productId)
        {
            using (var context = new AuthContext())
            {
                return context.LicenseProducts.Where(x => x.ProductId == productId && x.Deleted == null)
                    .Select(x => x.LicenseId)
                    .ToList();
            }
        }

        public List<LicenseProduct> GetLicenseProductsByRecsProductId(int productId)
        {
            using (var context = new AuthContext())
            {
                return context.LicenseProducts.Where(x => x.ProductId == productId && x.Deleted == null).ToList();
            }
        }

        public List<LicenseProduct> GetLicenseProducts(int licenseId)
        {
            using (var context = new AuthContext())
            {
                return context.LicenseProducts.Include("Schedule").Where(x => x.LicenseId == licenseId && x.Deleted == null).ToList();
            }
        }

        public LicenseProduct GetLicenseProduct(int licenseProductId)
        {
            using (var context = new AuthContext())
            {
                return context.LicenseProducts.Include("Schedule").Where(x => x.LicenseProductId == licenseProductId && x.Deleted == null).FirstOrDefault();
            }
        }

        public List<string> GetLicenseWriterIpCodes(int licenseRecordingId)
        {
            using (var context = new AuthContext())
            {
                return context.LicenseProductRecordingWriters.Where(x => x.LicenseRecordingId == licenseRecordingId)
                    .Select(x => x.IpCode)
                    .ToList();
            }
        }

        public List<LicenseProductConfiguration> GetLicenseProductConfigurations(int licenseId)
        {
            using (var context = new AuthContext())
            {
                return context.LicenseProductConfigurations
                    .Include("LicenseProduct")
                    .Where(x => x.LicenseProduct.LicenseId == licenseId && x.Deleted == null)
                    .ToList();
            }

        }

        public LicenseProductConfiguration GetLicenseProductConfigurationById(int licenseProductConfigurationId)
        {
            using (var context = new AuthContext())
            {
                return context.LicenseProductConfigurations
                    .Include("LicenseProduct")
                    .FirstOrDefault(x => x.LicenseProductConfigurationId == licenseProductConfigurationId && x.Deleted == null);
            }

        }

        public List<LicenseProductConfiguration> GetProductConfigurations(int licenseProductId)
        {
            using (var context = new AuthContext())
            {
                return context.LicenseProductConfigurations
                    .Where(x => x.LicenseProductId == licenseProductId && x.Deleted == null).ToList();
            }
        }

        public void UpdateLicenseProductConfiguration(int licenseProductConfigurationId, string fieldName, bool fieldValue)
        {
            using (var context = new AuthContext())
            {
                var lpc = context.LicenseProductConfigurations.SingleOrDefault(x => x.LicenseProductConfigurationId == licenseProductConfigurationId);
                context.Entry(lpc).State = (EntityState)System.Data.EntityState.Modified;
                if (fieldName == "S")
                {
                    //fieldValue contains current value, need to flip it
                    lpc.StatusReport = !fieldValue;
                }
                else if (fieldName == "P")
                {
                    //fieldValue contains current value, need to flip it 
                    lpc.PriorityReport = !fieldValue;
                }
                context.SaveChanges();
            }
        }

        public LicenseProduct GetLicenseProduct(int licenseId, int productId)
        {
            using (var context = new AuthContext())
            {
                return context.LicenseProducts.Where(x => x.LicenseId == licenseId && x.ProductId == productId && x.Deleted == null).FirstOrDefault();
            }
        }
        public List<LicenseProduct> GetAllLicenseProducts(long productId)
        {
            using (var context = new AuthContext())
            {
                return context.LicenseProducts.Include("Schedule").Where(x => x.ProductId == productId && x.Deleted == null).ToList();
            }
        }

        public LicenseProduct Add(LicenseProduct licenseProduct)
        {
            using (var context = new AuthContext())
            {
                context.LicenseProducts.Add(licenseProduct);
                context.SaveChanges();
                return licenseProduct;
            }
        }

        public void Update(LicenseProduct licenseProduct)
        {
            using (var context = new AuthContext())
            {
                context.Entry(licenseProduct).State = (EntityState) System.Data.EntityState.Modified;
                context.SaveChanges();
            }
        }


        public void RemoveLicenseProducts(int licenseId)
        {
             using (var context = new AuthContext())
             {
                 //var existingLicenseProducts = context.LicenseProducts.Where(lp => lp.Deleted == null && lp.LicenseId == licenseId);
                 //foreach (var licenseProduct in existingLicenseProducts)
                 //{
                 //    licenseProduct.Deleted = DateTime.Now;
                 //}
                 //var existingProductsIds =  existingLicenseProducts.Select(lp => lp.LicenseProductId).ToList();
                 //var existingProductConfigurations =
                 //    context.LicenseProductConfigurations.Where(
                 //        lpc => lpc.Deleted == null && existingProductsIds.Contains(lpc.LicenseProductId));
                 //var productConfigurationList = existingProductConfigurations.ToList();
                 //var productConfigurationsIds =
                 //    productConfigurationList.Select(pc => pc.LicenseProductConfigurationId).ToList();
                 //foreach (var productConfiguration in productConfigurationList)
                 //{
                 //    productConfiguration.Deleted = DateTime.Now;
                 //}
                 //var existingLicenseRecording =
                 //    context.LicenseProductRecordings.Where(
                 //        lpr => lpr.Deleted == null && existingProductsIds.Contains(lpr.LicenseProductId));
                 //var licenseRecordingList = existingLicenseRecording.ToList();
                 //var licenseRecordingIds = licenseRecordingList.Select(lr => lr.LicenseRecordingId).ToList();
                 //foreach (var licenseRecording in existingLicenseRecording)
                 //{
                 //    licenseRecording.Deleted = DateTime.Now;
                 //}
                 //var existingWriters =
                 //    context.LicenseProductRecordingWriters.Where(
                 //        lw => lw.Deleted == null && licenseRecordingIds.Contains(lw.LicenseRecordingId));
                 //foreach (var licenseProductRecordingWriter in existingWriters)
                 //{
                 //    licenseProductRecordingWriter.Deleted = DateTime.Now;
                 //}
                 //var writersList = existingWriters.ToList();
                 //var writersIds = writersList.Select(w => w.LicenseWriterId).ToList();
                 //var existingWriterRates =
                 //    context.LicenseProductRecordingWriterRates.
                 //        wr =>
                 //            wr.Deleted == null && existingWriters.FirstOrDefault(w => w.LicenseWriterId == wr.LicenseWriterId));
                 //foreach (var licenseProductRecordingWriterRate in existingWriterRates)
                 //{
                 //    licenseProductRecordingWriterRate.Deleted = DateTime.Now;
                 //}
                 //var existingWriterRateNotes =
                 //    context.LicenseProductRecordingWriterRateNotes.Where(
                 //        wrn =>
                 //            wrn.Deleted == null &&
                 //            existingWriterRates.Contains(wr => wr.LicenseWriterRateId == wrn.LicenseWriterRateId))
                 //        .ToList();
                 //foreach (var writerRateNote in existingWriterRateNotes)
                 //{
                 //    writerRateNote.Deleted = DateTime.Now;
                 //}
                 //var existingWriterStatuses =
                 //    context.LicenseProductRecordingWriterStatuses.Where(
                 //        ws =>
                 //            ws.Deleted == null && existingWriters.Contains(w => w.LicenseWriterId == ws.LicenseWriterId))
                 //        .ToList();
                 //foreach (var licenseProductRecordingWriterStatus in existingWriterStatuses)
                 //{
                 //    licenseProductRecordingWriterStatus.Deleted = DateTime.Now;
                 //}

                 //context.SaveChanges();
             }
        }

        public List<LicenseProductDropdown> GetLicenseProductDropDown(int licenseId)
        {
            using (var context = new AuthContext())
            {
                var licenseProducts = context.LicenseProducts.Where(x => x.LicenseId == licenseId);
                return licenseProducts.Select(x => new LicenseProductDropdown { LicenseProductId = x.LicenseProductId, ProductId = x.ProductId,LicenseId=licenseId }).ToList();
            }
        }

        public List<LicenseProduct> GetAllLicenseProductsForLicenseId(int licenseId)
        {
            using (var context = new AuthContext())
            {
                return context.LicenseProducts.Where(x => x.LicenseId == licenseId && !x.Deleted.HasValue).ToList();
            }
        }


        public bool DoeseLicenseProductHaveRecordings(int licenseProductId)
        {
            using (var context = new AuthContext())
            {
                return context.Snapshot_WorksRecordings.Any(_ => _.LicenseProductId == licenseProductId);
            }
        }


        public LicenseProduct Get(int licenseProductId)
        {
            using (var context = new AuthContext())
            {
                return context.LicenseProducts.Where(x => x.LicenseProductId == licenseProductId && !x.Deleted.HasValue).FirstOrDefault();
            }
        }

        public LicenseProduct GetMechLicenseProduct(int recsProductId) // New method
        {
            using (var context = new AuthContext())
            {
             return context.LicenseProducts.Where(x => x.ProductId == recsProductId && !x.Deleted.HasValue).FirstOrDefault();
                
            }
        }


    }
}
