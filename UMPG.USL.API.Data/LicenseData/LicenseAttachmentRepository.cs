using System;
using System.Collections.Generic;
using System.Linq;
using UMPG.USL.Models.LicenseModel;

namespace UMPG.USL.API.Data.LicenseData
{
    using System.Data.Entity;

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
                return
                    context.LicenseAttachments.FirstOrDefault(
                        c => c.fileName == fileName && c.licenseId == licenseId && c.Deleted == null);
            }
        }

        public List<LicenseAttachment> GetAll()
        {
            using (var context = new AuthContext())
            {
                var attachments = context.LicenseAttachments
                    .Include("Contact")
                    .Include("AttachmentType")
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

        public bool UpdateLicenseAttachment(LicenseAttachment licenseAttachment)
        {
            using (var context = new AuthContext())
            {
                var currentEntry =
                    context.LicenseAttachments.FirstOrDefault(
                        x => x.licenseAttachmentId == licenseAttachment.licenseAttachmentId);

                if (currentEntry == null)
                {
                    return false;
                }
                //currentEntry = licenseAttachment;

                currentEntry.licenseAttachmentId = licenseAttachment.licenseAttachmentId;
                currentEntry.licenseId = licenseAttachment.licenseId;
                currentEntry.Contact = licenseAttachment.Contact;
                currentEntry.fileName = licenseAttachment.fileName;
                currentEntry.AttachmentTypeId = licenseAttachment.AttachmentTypeId;
                currentEntry.fileType = licenseAttachment.fileType;
                currentEntry.virtualFilePath = licenseAttachment.virtualFilePath;
                currentEntry.uploaddedDate = licenseAttachment.uploaddedDate;
                currentEntry.includeInLicense = licenseAttachment.includeInLicense;
                //context.Entry(currentEntry).CurrentValues.SetValues(licenseAttachment);
                context.Entry(currentEntry).State = (EntityState)System.Data.EntityState.Modified;
                context.SaveChanges();
                return true;
            }
        }
    }
}