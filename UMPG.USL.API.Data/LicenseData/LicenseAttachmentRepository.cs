using System;
using System.Collections.Generic;
using System.Linq;
using UMPG.USL.Models;
using UMPG.USL.Models.LicenseModel;
using System.Data.Entity.Migrations;

namespace UMPG.USL.API.Data.LicenseData
{
    using System.Data.Entity;
    using System.Net.Mime;

    public class LicenseAttachmentRepository : ILicenseAttachmentRepository
    {

        public void Add(LicenseAttachment licenseAttachment)
        {
            using (var context = new AuthContext())
            {
                context.LicenseAttachments.Add(licenseAttachment);    
                context.SaveChanges();
            }
        }

        public LicenseAttachment Get(int id)
        {
            using (var context = new AuthContext())
            {
                return context.LicenseAttachments.FirstOrDefault(c => c.licenseAttachmentId == id);
            }
        }

        public LicenseAttachment Get(string fileName, int? licenseId)
        {
            using (var context = new AuthContext())
            {
                return context.LicenseAttachments.FirstOrDefault(c => c.fileName == fileName && c.licenseId == licenseId && c.Deleted == null);
            }
        }

        public List<LicenseAttachment> GetAll()
        {
            using (var context = new AuthContext())
            {
                var attachments = context.LicenseAttachments
                    .Include("Contact")
                    .Where(c => c.Deleted == null).OrderBy(c => c.CreatedDate).ToList();
                return attachments;
            }
        }

        public List<LicenseAttachment> Search(string query)
        {
            using (var context = new AuthContext())
            {
                var licenseAttachments = context.LicenseAttachments.Where(c => c.fileName == query).AsQueryable();

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
        public void Update(LicenseAttachment licenseAttachment)
        {
            using (var context = new AuthContext())
            {
                context.Entry(licenseAttachment).State = (EntityState)System.Data.EntityState.Modified;
                context.SaveChanges();
            }
        }

    }
}
