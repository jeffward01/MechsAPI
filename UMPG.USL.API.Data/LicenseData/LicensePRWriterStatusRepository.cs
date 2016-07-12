using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.Models.LicenseModel;
using UMPG.USL.Models.LookupModel;
using UMPG.USL.Models.StaticDropdownsData;
using EntityState = System.Data.Entity.EntityState;

namespace UMPG.USL.API.Data.LicenseData
{
    public class LicensePRWriterRateStatusRepository : ILicensePRWriterRateStatusRepository
    {
        public LicenseProductRecordingWriterRateStatus Add(LicenseProductRecordingWriterRateStatus licensePRwriterRateStatus)
        {
            using (var context = new AuthContext())
            {
                context.LicenseProductRecordingWriterRateStatuses.Add(licensePRwriterRateStatus);
                context.SaveChanges();

                return licensePRwriterRateStatus;
            }
        }

        public LicenseProductRecordingWriterRateStatus Get(int id)
        {
            using (var context = new AuthContext())
            {
                var licensePRwriterRateStatus = context.LicenseProductRecordingWriterRateStatuses
                    .FirstOrDefault(c => c.LicenseWriterRateStatusId == id);
                return licensePRwriterRateStatus;
            }
        }

        public List<LicenseProductRecordingWriterRateStatus> GetAll()
        {
            using (var context = new AuthContext())
            {
                var licensePRwriterRateStatuses = context.LicenseProductRecordingWriterRateStatuses.ToList();
                return licensePRwriterRateStatuses;

            }
        }


        public List<LicenseProductRecordingWriterRateStatus> Search(string query)
        {
            using (var context = new AuthContext())
            {
                var licensePRwriterRateStatuses = context.LicenseProductRecordingWriterRateStatuses
                    .AsQueryable();

                if (!String.IsNullOrEmpty(query))
                {
                    return licensePRwriterRateStatuses.Where(p => p.LU_SpecialStatuses.ToString().Contains(query)).ToList();
                }
                else
                {
                    return licensePRwriterRateStatuses.ToList();
                }
            }
        }

        public List<LicenseProductRecordingWritersRateStatusDropdown> GetLicenseWriterRateStatusList(List<int> licenseWriterRateIds)
        {

            using (var context = new AuthContext())
            {
                // for performance, going to build the collection and its sub collections (if present)
                //var licensewriterRates = context.LicenseProductRecordingWriterRateStatuses
                //    .Where(lw => lw.Deleted ==null && licenseWriterRateIds.Contains(lw.LicenseWriterRateId))
                //    .ToList();

                var licensewriterRateStatuses = context.LicenseProductRecordingWriterRateStatuses
                  .Where(lw => lw.Deleted == null && licenseWriterRateIds.Contains(lw.LicenseWriterRateId)).ToList();

                List<LicenseProductRecordingWritersRateStatusDropdown> lrdd = new List<LicenseProductRecordingWritersRateStatusDropdown>();

                foreach(var lr in licensewriterRateStatuses)
                {
                   var item = new LicenseProductRecordingWritersRateStatusDropdown { LicenseWriterRateId = lr.LicenseWriterRateId, SpecialStatusId = lr.SpecialStatusId};
                   lrdd.Add(item);
                };


                return lrdd;

            }

        }

        public List<LicenseProductRecordingWriterRateStatus> GetLicenseWriterRateStatus(List<int> licenseWriterRateIds)
        {
            using (var context = new AuthContext())
            {
                List<LU_SpecialStatus> lu_specialstatuses = context.LU_SpecialStatuses.ToList();
                var licensewriterRates = context.LicenseProductRecordingWriterRateStatuses
                    //.Include("LU_SpecialStatuses")
                    .Where(lw => licenseWriterRateIds.Contains(lw.LicenseWriterRateId) && lw.Deleted == null)
                    .ToList();

                foreach (var lr in licensewriterRates)
                {

                    lr.LU_SpecialStatuses = lu_specialstatuses.Where(x => x.SpecialStatusId == lr.SpecialStatusId).FirstOrDefault();
                };


                return licensewriterRates;
            }
        }

        public List<int> GetLicenseWriterRatesWithOutStatus(List<int> licenseWriterRateIds)
        {
            using (var context = new AuthContext())
            {
                var licensewriterRatesWithHolds = context.LicenseProductRecordingWriterRateStatuses
                    .Where(lw => licenseWriterRateIds.Contains(lw.LicenseWriterRateId) && lw.Deleted == null)
                    .Select(lw=>lw.LicenseWriterRateId)
                    .ToList();

                // Now remove the LicenseWriterRateIds from the original list
                licenseWriterRateIds.RemoveAll(licensewriterRatesWithHolds.Contains);


                return licenseWriterRateIds;
            }
        }

        public List<int> GetLicenseWriterRatesIdsWithStatus(List<int> licenseWriterRateIds)
        {
            using (var context = new AuthContext())
            {
                var licensewriterRatesWithHolds = context.LicenseProductRecordingWriterRateStatuses
                    .Where(lw => licenseWriterRateIds.Contains(lw.LicenseWriterRateId) && lw.Deleted == null)
                    .Select(lw => lw.LicenseWriterRateId)
                    .ToList();

                return licensewriterRatesWithHolds;
            }
        }

        public void Update(LicenseProductRecordingWriterRateStatus licenseRecordingWriterRateStatus)
        {
            using (var context = new AuthContext())
            {
                context.Entry(licenseRecordingWriterRateStatus).State = (EntityState)System.Data.EntityState.Modified;
                context.SaveChanges();
            }
        }

        public bool StatusExists(int rateId, int statusId)
        {

            using (var context = new AuthContext())
            {
                return context.LicenseProductRecordingWriterRateStatuses.Any(x => x.LicenseWriterRateId == rateId && x.SpecialStatusId == statusId);
            }
        }
    }
}
