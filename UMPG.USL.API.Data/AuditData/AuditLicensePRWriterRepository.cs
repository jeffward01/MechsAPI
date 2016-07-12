using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.Models.AuditModel;
using UMPG.USL.API.Data.AuditData;
using UMPG.USL.API.Data.Recs;
using UMPG.USL.Models.StaticDropdownsData;
using UMPG.USL.Models.LookupModel;

namespace UMPG.USL.API.Data.AuditData
{
    public class AuditLicensePRWriterRepository : IAuditLicensePRWriterRepository
    {



        public AuditLicenseProductRecordingWriter Get(int writerId)
        {
            using (var context = new AuditContext())
            {
                var licensePRwriterrate = context.AuditLicenseProductRecordingWriters
                    .FirstOrDefault(c => c.LicenseWriterId == writerId);
                return licensePRwriterrate;
            }
        }


       
        public List<AuditLicenseProductRecordingWriter> GetLicenseWriters(int licenseRecordingId)
        {

            using (var context = new AuditContext())
            {
                // for performance, going to build the collection and its sub collections (if present)
                var licensewriters = context.AuditLicenseProductRecordingWriters
                    .Where(lw => lw.LicenseRecordingId ==licenseRecordingId )
                    .ToList();

                return licensewriters;

            }

        }

        public List<AuditLicenseProductRecordingWriter> GetAuditLicenseWriterList(List<int> licenseRecordingIds)
        {

            using (var context = new AuditContext())
            {
                // for performance, going to build the collection and its sub collections (if present)
                var licensewriters = context.AuditLicenseProductRecordingWriters
                    .Where(lw => licenseRecordingIds.Contains(lw.LicenseRecordingId))
                    .ToList();

                return licensewriters;

            }

        }

        public List<AuditLicenseProductRecordingWriter> GetAuditLicensePrWritersFromIds(List<int> writerIds)
        {
            using (var context = new AuditContext())
            {
                var licensewriters = context.AuditLicenseProductRecordingWriters
                    .Where(lw => writerIds.Contains(lw.LicenseWriterId))
                    .ToList();
                return licensewriters;

            }
        }
        
    }
}
