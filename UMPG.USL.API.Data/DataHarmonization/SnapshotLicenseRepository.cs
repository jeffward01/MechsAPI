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

        public Snapshot_License GetLicenseSnapShotById(int id)
        {
            using (var context = new AuthContext())
            {
                var response = context.Snapshot_Licenses
                    .Include("LicenseType")
                    .Include("LicensePriority")
                    .Include("LicenseStatus")
                   // .Include("Licensee.LicenseeLabelGroup")
                    //.Include("Licensee")
                    .Include("LicenseMethod")
                    //.Include("Contact")
                    //.Include("Contact2")
                    //.Include("LicenseNoteList")
                 //   .Include("LicenseProducts")
                    //.Include("LicenseeContact")
                    //.Include("LicenseeContact.Address")
                    .FirstOrDefault(sl => sl.CloneLicenseId == id);

                var licenseProduct = context.Snapshot_LicenseProducts

                        .Include("ProductHeader")

                    .Where(_ => _.LicenseId == response.CloneLicenseId).ToList();
                response.LicenseProducts = licenseProduct;

                return response;
            }
        }
    }
}