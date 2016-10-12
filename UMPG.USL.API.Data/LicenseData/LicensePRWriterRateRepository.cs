using System;
using System.Collections.Generic;
using System.Linq;
using UMPG.USL.Models.LicenseModel;
using EntityState = System.Data.Entity.EntityState;

namespace UMPG.USL.API.Data.LicenseData
{
    public class LicensePRWriterRateRepository : ILicensePRWriterRateRepository
    {
        public LicenseProductRecordingWriterRate Add(LicenseProductRecordingWriterRate licensePRwriterrate)
        {
            using (var context = new AuthContext())
            {
                context.LicenseProductRecordingWriterRates.Add(licensePRwriterrate);
                context.SaveChanges();

                return licensePRwriterrate;
            }
        }

        public LicenseProductRecordingWriterRate Get(int id)
        {
            using (var context = new AuthContext())
            {
                var licensePRwriterrate = context.LicenseProductRecordingWriterRates
                    .FirstOrDefault(c => c.LicenseWriterRateId == id);
                return licensePRwriterrate;
            }
        }

        public List<LicenseProductRecordingWriterRate> GetAll()
        {
            using (var context = new AuthContext())
            {
                var licensePRwriterrates = context.LicenseProductRecordingWriterRates.ToList();
                return licensePRwriterrates;
            }
        }

        public bool LicenseProductHasWriterRate(int product_configuration_id, int licneseWriterId)
        {
            using (var context = new AuthContext())
            {
                var result = context.LicenseProductRecordingWriterRates.FirstOrDefault(x => x.product_configuration_id == product_configuration_id && x.LicenseWriterId == licneseWriterId);
                if (result != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public List<LicenseProductRecordingWriterRate> GetWriterRatesWithNotes()
        {
            using (var context = new AuthContext())
            {
                var licensePRwriterrates = context.LicenseProductRecordingWriterRates

                    .ToList();

                return licensePRwriterrates;
            }
        }

        public List<LicenseProductRecordingWriterRate> Search(string query)
        {
            using (var context = new AuthContext())
            {
                var licensePRwriterrates = context.LicenseProductRecordingWriterRates
                    .AsQueryable();

                if (!String.IsNullOrEmpty(query))
                {
                    return licensePRwriterrates.Where(p => p.LongStatRate.ToString().Contains(query)).ToList();
                }
                else
                {
                    return licensePRwriterrates.ToList();
                }
            }
        }

        //public List<LicenseProductRecordingWriterRate> GetLicenseProductRecordingWriterRatesByWriterIdsConfig(List<int> licenseWriterIds, int configuration_id)
        public List<LicenseProductRecordingWriterRate> GetLicenseProductRecordingWriterRatesByWriterIdsConfig(List<int> licenseWriterIds, int configuration_id)
        {
            using (var context = new AuthContext())
            {
                //                return context.LicenseProductRecordingWriterRates.Where(x => licenseWriterIds.Contains((int)x.LicenseWriterId) && x.configuration_id == configuration_id && x.Deleted == null).ToList();
                return context.LicenseProductRecordingWriterRates.Where(x => licenseWriterIds.Contains((int)x.LicenseWriterId) && x.product_configuration_id == configuration_id && x.Deleted == null).ToList();
            }
        }

        //        public List<LicenseProductRecordingWriterRate> GetLicenseProductRecordingWriterRatesByWriterIdConfig(int licenseWriterId, int configurationId)
        public List<LicenseProductRecordingWriterRate> GetLicenseProductRecordingWriterRatesByWriterIdConfig(int licenseWriterId, int configurationId)
        {
            using (var context = new AuthContext())
            {
                //                return context.LicenseProductRecordingWriterRates.Where(x => x.LicenseWriterId == licenseWriterId && x.configuration_id == configurationId && x.Deleted == null).ToList();
                return context.LicenseProductRecordingWriterRates.Where(x => x.LicenseWriterId == licenseWriterId && x.product_configuration_id == configurationId && x.Deleted == null).ToList();
            }
        }

        public List<LicenseProductRecordingWriterRate> GetLicenseProductRecordingWriterRates(int licenseWriterId)
        {
            using (var context = new AuthContext())
            {
                return context.LicenseProductRecordingWriterRates.Where(x => x.LicenseWriterId == licenseWriterId && !x.Deleted.HasValue).ToList();
            }
        }

        public List<LicenseProductRecordingWriterRate> GetLicenseRecordingWriterRatesFromIds(List<int> licenseWriterIds, List<int> configIds)
        {
            using (var context = new AuthContext())
            {
                var test = context.LicenseProductRecordingWriterRates
                    .Where(x => licenseWriterIds.Contains(x.LicenseWriterId));

                if (configIds.Count > 0)
                {
                    //test = test.Where(x => configIds.Contains((int)x.configuration_id) && !x.Deleted.HasValue);
                    test = test.Where(x => configIds.Contains((int)x.product_configuration_id) && !x.Deleted.HasValue);
                };

                return test.ToList();
            }
        }

        public List<LicenseProductRecordingWriterRate> GetLicenseRecordingWriterRatesFromIdsFull(List<int> licenseWriterIds, List<int> configIds)
        {
            using (var context = new AuthContext())
            {
                var test = context.LicenseProductRecordingWriterRates
                    .Where(x => licenseWriterIds.Contains(x.LicenseWriterId));

                if (configIds.Count > 0)
                {
                    //test = test.Where(x => configIds.Contains((int)x.configuration_id) && !x.Deleted.HasValue);
                    test = test.Where(x => configIds.Contains((int)x.product_configuration_id) && !x.Deleted.HasValue);
                };

                return test.ToList();
            }
        }

        public List<LicenseProductRecordingWriterRate> GetLicenseRecordingWriterRateIds(List<int> licenseWriterIds)
        {
            using (var context = new AuthContext())
            {
                var test = context.LicenseProductRecordingWriterRates
                    .Where(x => licenseWriterIds.Contains(x.LicenseWriterId));

                return test.ToList();
            }
        }

        public List<LicenseProductRecordingWriterRate> GetRatesByRatesIds(List<int> rateIds)
        {
            using (var context = new AuthContext())
            {
                var test = context.LicenseProductRecordingWriterRates
                    .Where(x => rateIds.Contains(x.LicenseWriterRateId));

                return test.ToList();
            }
        }

        public void Update(LicenseProductRecordingWriterRate licenseProductRecordingWriterRate)
        {
            using (var context = new AuthContext())
            {
                context.Entry(licenseProductRecordingWriterRate).State = (EntityState)System.Data.EntityState.Modified;
                context.SaveChanges();
            }
        }

        public List<int> GetLicensedConfigIds(int licenseWriterId)
        {
            using (var context = new AuthContext())
            {
                var configIds = context.LicenseProductRecordingWriterRates
                    .Where(w => w.LicenseWriterId == licenseWriterId && !w.Deleted.HasValue && w.licenseDate.HasValue)
                    .Select(w => w.configuration_id).Distinct().ToList();
                //.GroupBy(g => g.configuration_id)

                //.ToList();
                //.Count();
                return configIds;
            }
        }

        public List<int?> GetLicensedProductConfigIds(int licenseWriterId)
        {
            using (var context = new AuthContext())
            {
                var configIds = context.LicenseProductRecordingWriterRates
                    .Where(w => w.LicenseWriterId == licenseWriterId && !w.Deleted.HasValue && w.licenseDate.HasValue)
                    .Select(w => w.product_configuration_id).Distinct().ToList();

                return configIds;
            }
        }
    }
}