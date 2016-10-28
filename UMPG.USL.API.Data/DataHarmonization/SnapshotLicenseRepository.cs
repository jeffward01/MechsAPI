using NLog;
using System;
using System.Linq;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public class SnapshotLicenseRepository : ISnapshotLicenseRepository
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public Snapshot_License SaveSnapshotLicense(Snapshot_License licenseSnapshot)
        {
            using (var context = new AuthContext())
            {
                context.Snapshot_Licenses.Add(licenseSnapshot);
                try
                {
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    Logger.Debug(e.ToString);
                    throw new Exception("Error saving snapshot_licnese:     " + e.ToString());
                }
                return licenseSnapshot;
            }
        }

        public bool DoesLicenseSnapshotExist(int licenseId)
        {
            using (var context = new AuthContext())
            {
                var exist = context.Snapshot_Licenses.FirstOrDefault(_ => _.CloneLicenseId == licenseId);
                if (exist != null)
                {
                    return true;
                }
            }
            return false;
        }

        public Snapshot_License GetLicenseSnapShotById(int id)
        {
            using (var context = new AuthContext())
            {
                //License Details
                var response = context.Snapshot_Licenses
                    .Include("LicenseType")
                    .Include("LicensePriority")
                    .Include("LicenseStatus")
                  //  .Include("Licensee")
                    //.Include("Licensee.LicenseeLabelGroup")
                    .Include("LicenseMethod")
                    //.Include("Contact") // Contacts is throwing erorr.  not set to instance of an obj
                    //.Include("Contact2") // Contacts is throwing erorr.  not set to instance of an obj
                    .Include("LicenseNoteList")
                    .Include("LicenseProducts")
                    //.Include("LicenseeContact")
                    //.Include("LicenseeContact.Address")
                    .Include("LicenseMethod")
                    .Include("LicenseNoteList")
                    .FirstOrDefault(sl => sl.CloneLicenseId == id);

                //LicenseProduct and Product header
                var licenseProduct = context.Snapshot_LicenseProducts
                    .Include("ProductHeader")
                  .Include("ProductHeader.Artist")
                  .Include("ProductHeader.Label")
//TEST            //  .Include("ProductHeader.Configurations")
                  //  .Include("ProductHeader.Configurations.Configuration") // Error here
                  //.Include("ProductHeader.Configurations.LicenseProductConfiguration")
                  .Include("ProductHeader.Label.RecordLabelGroups")
                   .Include("ProductConfigurations") //comes back null, i think in the test license case its supposed
                    .Include("Schedule")
                      .Include("Recordings") 
                      .Include("Recordings.Writers")  //Add to database
                     .Include("Recordings.Track")  //Add to database
                     .Include("Recordings.Track.Copyrights")
                     .Include("Recordings.Track.Copyrights.Composers")
                     .Include("Recordings.Track.Copyrights.Samples")
                     .Include("Recordings.Track.Copyrights.LocalClients")
                     .Include("Recordings.Track.Copyrights.AquisitionLocationCodes")
                     .Include("Recordings.Track.Artist")
                    //    .Include("Recordings.LicenseRecording") //Add to database??

                    .Where(_ => _.LicenseId == response.CloneLicenseId).ToList();

                
        
                response.LicenseProducts = licenseProduct;
                return response;
            }
        }

        public bool DeleteSnapshotLicense(int licenseId)
        {
            using (var context = new AuthContext())
            {
                var licenseToBeDeleted = context.Snapshot_Licenses
                    .Include("LicenseType")
                    .Include("LicensePriority")
                    .Include("LicenseStatus")
                 //   .Include("Licensee")
                  //  .Include("Licensee.LicenseeLabelGroup")
                    .Include("LicenseMethod")
                    //.Include("Contact") // Contacts is throwing erorr.  not set to instance of an obj
                    //.Include("Contact2") // Contacts is throwing erorr.  not set to instance of an obj
                    .Include("LicenseNoteList")
                    .Include("LicenseProducts")
                    //.Include("LicenseeContact")
                    //.Include("LicenseeContact.Address")
                    .Include("LicenseMethod")
                    .Include("LicenseNoteList")

                    .FirstOrDefault(sl => sl.CloneLicenseId == licenseId);

                context.Snapshot_Licenses.Attach(licenseToBeDeleted);
                context.Snapshot_Licenses.Remove(licenseToBeDeleted);
                try
                {
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    return false;
                }
            }
            return true;
        }
    }
}