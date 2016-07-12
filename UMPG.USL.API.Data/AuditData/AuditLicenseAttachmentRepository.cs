using System;
using System.Collections.Generic;
using System.Linq;
using UMPG.USL.Models;
using UMPG.USL.Models.AuditModel;
using System.Data.Entity.Migrations;

namespace UMPG.USL.API.Data.AuditData
{
    using System.Data.Entity;
    using System.Net.Mime;

    public class AuditLicenseAttachmentRepository : IAuditLicenseAttachmentRepository
    {
        public AuditLicenseAttachment Get(int id)
        {
            using (var context = new AuditContext())
            {
                return context.AuditLicenseAttachments.FirstOrDefault(c => c.licenseAttachmentId == id);
            }
        }

        public AuditLicenseAttachment GetByLicenseId(string fileName, int? licenseId)
        {
            using (var context = new AuditContext())
            {
                return context.AuditLicenseAttachments.FirstOrDefault(c => c.fileName == fileName && c.licenseId == licenseId && c.Deleted == null);
            }
        }

        public List<AuditLicenseAttachment> GetAll()
        {
            using (var context = new AuditContext())
            {
                var attachments = context.AuditLicenseAttachments
                    //.Include("Contact")  - tom removed returning no results.. need to think through this
                    .Where(c => c.Deleted == null).OrderBy(c => c.CreatedDate).ToList();
                return attachments;
            }
        }

        public List<AuditLicenseAttachment> Search(string query)
        {
            using (var context = new AuditContext())
            {
                var licenseAttachments = context.AuditLicenseAttachments.Where(c => c.fileName == query).AsQueryable();

                if (!String.IsNullOrEmpty(query))
                {
                    return licenseAttachments.Where(c => c.fileName.ToLower().Contains(query.ToLower())).ToList();
                }
                else
                {
                    return licenseAttachments.ToList();
                }
            }
        }

    }
}
