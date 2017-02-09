using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.Models.LicenseModel;
using UMPG.USL.API.Data.LicenseData;
using UMPG.USL.API.Data.Recs;
using UMPG.USL.Models.StaticDropdownsData;
using UMPG.USL.Models.LookupModel;

namespace UMPG.USL.API.Data.LicenseData
{
    public class LicensePRWriterRepository:ILicensePRWriterRepository
    {

        public int GetLicenseProductRecordingWritersNo(int licenseRecordingId)
        {
            using (var context = new AuthContext())
            {
                return (from lw in context.LicenseProductRecordingWriters
                        where lw.LicenseRecordingId == licenseRecordingId 
                        select new
                        {
                            LicenseWriterId = lw.LicenseWriterId,

                        }
                    ).Count();
            }
        }
        
        public int GetLicenseProductRecordingLicensedWritersNo(int licenseRecordingId)
        {
            using (var context = new AuthContext())
            {
                return (from lw in context.LicenseProductRecordingWriters
                        where lw.LicenseRecordingId == licenseRecordingId && lw.isLicensed==true
                        select new
                        {
                            LicenseWriterId = lw.LicenseWriterId,

                        }
                    ).Count();
            }
        }

        public int GetUnLicenseProductRecordingLicensedWritersNo(int licenseRecordingId) //This is the new method
        {
            using (var context = new AuthContext())
            {
                return (from lw in context.LicenseProductRecordingWriters
                        where lw.LicenseRecordingId == licenseRecordingId && lw.isLicensed == false
                        select new
                        {
                            LicenseWriterId = lw.LicenseWriterId,

                        }
                    ).Count();
            }
        }

        public LicenseProductRecordingWriter Get(int writerId)
        {
            using (var context = new AuthContext())
            {
                var licensePRwriterrate = context.LicenseProductRecordingWriters
                    .FirstOrDefault(c => c.LicenseWriterId == writerId);
                return licensePRwriterrate;
            }
        }

        public LicenseProductRecordingWriter GetByRecordingIdAndCaeNumber(int recordingId, int caeNumber, string ipCode)
        {
            using (var context = new AuthContext())
            {
                var licensePRwriterrate = context.LicenseProductRecordingWriters
                    .FirstOrDefault(c => c.LicenseRecordingId == recordingId && c.CAECode == caeNumber);
                return licensePRwriterrate;
            }
        }


        //public List<LicenseProductRecordingWriter> GetTrackWriters(int trackId)
        //{
        //    //using (var context = new AuthContext())
        //    //{
        //    //    var licenseRecording =
        //    //        context.LicenseProductRecordings.FirstOrDefault(lpr => lpr.TrackId == trackId);
        //    //}
        //}
        public List<LicenseProductRecordingWriter> GetLicenseProductRecordingWriters(int licenseRecordingId)
        {
            
            using (var context = new AuthContext())
            {
                // for performance, going to build the collection and its sub collections (if present)
                var licensewriters = context.LicenseProductRecordingWriters.Where(lw => lw.LicenseRecordingId == licenseRecordingId && lw.Deleted == null).ToList();
                   
                // Now loop through the list, and gather the collections for SpecialStatus, Rates, and RateNotes                    
                foreach (var licensewriter in licensewriters) // TOMH move to busisness layer !!!
                {

                    //fill rates  --- TOMH move to LicnesePRWriterRate Repository
                    licensewriter.RateList = context.LicenseProductRecordingWriterRates
                        .Include("RateType")
                        .Include("WritersConsentType")
                        .Where(x => x.LicenseWriterId == licensewriter.LicenseWriterId && x.Deleted == null)
                        .ToList();


                    licensewriter.WriterNotes = context.LicenseProductRecordingWriterNotes
                            .Where(x => x.LicenseWriterId == licensewriter.LicenseWriterId && x.Deleted == null)
                            .OrderByDescending(x=>x.CreatedDate)
                            .ToList();

                    // if there is at least one note, grab the first one and populate
                    if (licensewriter.WriterNotes.Count > 0) {
                        licensewriter.MostRecentNote = licensewriter.WriterNotes.FirstOrDefault().Note;
                    }

                    licensewriter.WriterNoteCount = licensewriter.WriterNotes.Count();

                    // file rate notes TOMH move to LicnesePRWriterNote Repository
                    foreach (var rateitem in licensewriter.RateList)
                    {
                        //
                        // Note: Temporary added this code for Preview page for Ioan.  Needed to add a Type at the RateList level
                        //

                        // Set default to Physical 
                        rateitem.configuration_type = "P";

                        // change if digital - note: this may change at some point
                        if (rateitem.configuration_name.Contains("DPD"))
                        {
                            rateitem.configuration_type = "D";
                        }
                        else if (rateitem.configuration_name.Contains("streaming"))
                        {
                            rateitem.configuration_type = "D";
                        }
                        else if (rateitem.configuration_name.Contains("digit"))
                        {
                            rateitem.configuration_type = "D";
                        }

                        //
                        // Here we need to fill the LicenseWriterRateStatus
                        //
                        //fill specialstatus TOMH move to LicnesePRWriterStatus Repository
                        List<LU_SpecialStatus> lu_specialstatuses = context.LU_SpecialStatuses.ToList();
                        rateitem.SpecialStatusList = context.LicenseProductRecordingWriterRateStatuses
                            //.Include("LU_SpecialStatuses")
                            .Where(x => x.LicenseWriterRateId == rateitem.LicenseWriterRateId && x.Deleted == null)
                            .ToList();


                        foreach (var lr in rateitem.SpecialStatusList)
                        {
                            lr.LU_SpecialStatuses = lu_specialstatuses.Where(x => x.SpecialStatusId == lr.SpecialStatusId).FirstOrDefault();
                        };



                    }


                }

                return licensewriters;


            }

        }

        public List<LicenseProductRecordingWriter> GetLicenseProductRecordingWritersBrief(int licenseRecordingId)
        {

            using (var context = new AuthContext())
            {
                // for performance, going to build the collection and its sub collections (if present)
                var licensewriters = context.LicenseProductRecordingWriters.Where(lw => lw.LicenseRecordingId == licenseRecordingId).ToList();

                // Now loop through the list, and gather the collections for SpecialStatus, Rates, and RateNotes                    
                

                return licensewriters;

            }

        }

        public List<LicenseProductRecordingWriter> GetLicenseWriters(int licenseRecordingId)
        {

            using (var context = new AuthContext())
            {
                // for performance, going to build the collection and its sub collections (if present)
                var licensewriters = context.LicenseProductRecordingWriters
                    .Where(lw => lw.LicenseRecordingId ==licenseRecordingId )
                    .ToList();

                return licensewriters;

            }

        }

        public List<LicenseProductRecordingWriter> GetLicenseWriterList(List<int> licenseRecordingIds)
        {

            using (var context = new AuthContext())
            {
                // for performance, going to build the collection and its sub collections (if present)
                var licensewriters = context.LicenseProductRecordingWriters
                    .Where(lw => licenseRecordingIds.Contains(lw.LicenseRecordingId))
                    .ToList();

                return licensewriters;

            }

        }

        public List<LicenseProductRecordingWriter> GetLicensePrWritersFromIds(List<int> writerIds)
        {
            using (var context = new AuthContext())
            {
               var licensewriters = context.LicenseProductRecordingWriters
                    .Where(lw => writerIds.Contains(lw.LicenseWriterId))
                    .ToList();
                return licensewriters;

            }
        }


        public List<int> GetLicenseRecordingWriterIds(List<int> licenseRecordingIds)
        {

            using (var context = new AuthContext())
            {
                var writerIds =  context.LicenseProductRecordingWriters.Where(x => licenseRecordingIds.Contains((int)x.LicenseRecordingId))
                       .Select(x=>x.LicenseWriterId)
                       .DefaultIfEmpty(0);
                       
                return writerIds.ToList();

            }

        }

        

        public LicenseProductRecordingWriter Add(LicenseProductRecordingWriter writer)
        {
            using (var context = new AuthContext())
            {
                context.LicenseProductRecordingWriters.Add(writer);
                context.SaveChanges();
                return writer;
            }
            
        }

        public void Update(LicenseProductRecordingWriter writer)
        {
            using (var context = new AuthContext())
            {
                context.Entry(writer).State = (EntityState) System.Data.EntityState.Modified;
                context.SaveChanges();
            }
        }

    }
}
