using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.Models.LicenseModel;
using EntityState = System.Data.Entity.EntityState;

namespace UMPG.USL.API.Data.LicenseData
{
    public class LicensePRWriterNoteRepository : ILicensePRWriterNoteRepository
    {
        public LicenseProductRecordingWriterNote Add(LicenseProductRecordingWriterNote licensePRwriterNote)
        {
            using (var context = new AuthContext())
            {

                context.LicenseProductRecordingWriterNotes.Add(licensePRwriterNote);
                context.SaveChanges();

                return licensePRwriterNote;
            }
        }

        public LicenseProductRecordingWriterNote Get(int id)
        {
            using (var context = new AuthContext())
            {
                var licensenote = context.LicenseProductRecordingWriterNotes.FirstOrDefault(c => c.LicenseWriterNoteId == id);
                return licensenote;
            }
        }

        public List<LicenseProductRecordingWriterNote> GetAll(int id)
        {
            using (var context = new AuthContext())
            {
                var licensePrWriterNotesList = context.LicenseProductRecordingWriterNotes.Where((c => c.LicenseWriterNoteId == id)).ToList();
                return licensePrWriterNotesList;

            }
        }


        public void Update(LicenseProductRecordingWriterNote licenseProductRecordingWriterNote)
        {
            using (var context = new AuthContext())
            {
                context.Entry(licenseProductRecordingWriterNote).State = (EntityState)System.Data.EntityState.Modified;
                context.SaveChanges();
            }
        }

        public List<LicenseProductRecordingWriterNote> GetLicenseProductRecordingWriterNotes(List<int> licenseWriterIds)
        {
            using (var context = new AuthContext())
            {
                return context.LicenseProductRecordingWriterNotes.Where(x => licenseWriterIds.Contains((int)x.LicenseWriterId) && x.Deleted == null).ToList();
            }
        }

        //public LicenseProductRecordingWriterRate Get(int id)
        //{
        //    using (var context = new AuthContext())
        //    {
        //        var licensePRwriterrate = context.LicenseProductRecordingWriterRates
        //            .FirstOrDefault(c => c.LicenseWriterRateId == id);
        //        return licensePRwriterrate;
        //    }
        //}

        //public List<LicenseProductRecordingWriterRate> GetAll()
        //{
        //    using (var context = new AuthContext())
        //    {
        //        var licensePRwriterrates = context.LicenseProductRecordingWriterRates.ToList();
        //        return licensePRwriterrates;

        //    }
        //}

        //public List<LicenseProductRecordingWriterRate> GetWriterRatesWithNotes()
        //{
        //    using (var context = new AuthContext())
        //    {
        //        var licensePRwriterrates = context.LicenseProductRecordingWriterRates
                    
        //            .ToList();

        //        return licensePRwriterrates;

        //    }
        //}

        //public List<LicenseProductRecordingWriterRate> Search(string query)
        //{
        //    using (var context = new AuthContext())
        //    {
        //        var licensePRwriterrates = context.LicenseProductRecordingWriterRates
        //            .AsQueryable();

        //        if (!String.IsNullOrEmpty(query))
        //        {
        //            return licensePRwriterrates.Where(p => p.LongStatRate.ToString().Contains(query)).ToList();
        //        }
        //        else
        //        {
        //            return licensePRwriterrates.ToList();
        //        }
        //    }
        //}

        //public List<LicenseProductRecordingWriterRate> GetLicenseProductRecordingWriterRatesByWriterIdsConfig(List<int> licenseWriterIds, int configuration_id)
        //{
        //    using (var context = new AuthContext())
        //    {
        //        return context.LicenseProductRecordingWriterRates.Where(x => licenseWriterIds.Contains((int)x.LicenseWriterId) && x.configuration_id == configuration_id && x.Deleted == null).ToList();
        //    }
        //}

        //public List<LicenseProductRecordingWriterRate> GetLicenseProductRecordingWriterRates(int licenseWriterId)
        //{
        //    using (var context = new AuthContext())
        //    {

        //        return context.LicenseProductRecordingWriterRates.Where(x => x.LicenseWriterId == licenseWriterId).ToList();
        //    }
        //}

        //public List<LicenseProductRecordingWriterRate> GetLicenseRecordingWriterRatesFromIds(List<int> licenseWriterIds, List<int> configIds)
        //{

        //    using (var context = new AuthContext())
        //    {
        //        var test = context.LicenseProductRecordingWriterRates
        //            .Where(x => licenseWriterIds.Contains(x.LicenseWriterId));

        //        if (configIds.Count > 0 )
        //        {
        //            test = test.Where(x => configIds.Contains((int)x.configuration_id));
        //        };


        //        return test.ToList();
                    

        //    }

        //}

        //public void Update(LicenseProductRecordingWriterRate licenseProductRecordingWriterRate)
        //{
        //    using (var context = new AuthContext())
        //    {

                
        //        context.Entry(licenseProductRecordingWriterRate).State = (EntityState) System.Data.EntityState.Modified;
        //        context.SaveChanges();
        //    }

        //}

    }
}
