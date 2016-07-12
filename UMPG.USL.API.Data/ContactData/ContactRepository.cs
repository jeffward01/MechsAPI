using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using UMPG.USL.Models.ContactModel;
using UMPG.USL.Models.LicenseModel;

namespace UMPG.USL.API.Data.ContactData
{
    public class ContactRepository : IContactRepository
    {

        public Contact Add(Contact contact)
        {
            using (var context = new AuthContext())
            {
                context.Contacts.Add(contact);
                context.SaveChanges();

                return contact;
            }
        }


        public Contact Get(int contactId)
        {
            using (var context = new AuthContext())
            {
                return context.Contacts
                    .Include(x => x.Address)
                    .Include(x => x.Phone)
                    .Include(x => x.Email)
                    .FirstOrDefault(c => c.ContactId == contactId);
            }
        }


        public Contact Get(string safeId)
        {
            using (var context = new AuthContext())
            {
                return context.Contacts
                    //.Include("PreferedDownloadAudioFormat")
                    //.Include("PreferedStreamAudioFormat")
                    //.Include("Site")
                    //.Include("Account")
                    //.Include("Account.BusinessType")
                    //.Include("Account.Address")
                    //.Include("Account.Address.Country")
                    .Include("Role")
                    .Include("Role.Actions")
                    .FirstOrDefault(c => c.SafeId == safeId && c.IsActive && c.Deleted == null);
            }
        }


        public List<Contact> GetAll()
        {
            using (var context = new AuthContext())
            {
                return context.Contacts.Include(x=>x.Address).
                    Include(x=>x.Phone).
                    Include(x=>x.Email).Where(x=>!x.Deleted.HasValue)
                    .OrderBy(c => c.FullName).ToList();
            }
        }


        public List<Contact> GetContactsWithRole(int roleId)
        {
            using (var context = new AuthContext())
            {
                return context.Contacts.Where(c => c.RoleId == roleId).ToList();
            }
        }

        public bool EmailExists(string email, int licenseeId)
        {
            using (var context = new AuthContext())
            {
                var exists = context.Contacts.Include(x => x.Address).
                    Include(x => x.Phone).
                    Include(x => x.Email).FirstOrDefault(x => !x.Deleted.HasValue && x.LicenseeId != licenseeId &&
                                                              x.Email.FirstOrDefault().EmailAddress == email);
                if (exists==null)
                {
                    return false;
                }
                return true;
            }
        }

        public List<Contact> GetAssignees()
        {
            using (var context = new AuthContext())
            {
                return context.Contacts
                    .Where(c=>c.LicenseeId == null)
                    .OrderBy(c => c.FullName).ToList();
            }
        }
        
        public List<Contact> Search(string query)
        {
            using (var context = new AuthContext())
            {
               

                if (!String.IsNullOrEmpty(query))
                {
                    return context.Contacts.Where(c => c.FullName.ToLower().Contains(query.ToLower())).ToList();
                }
                else
                {
                    return context.Contacts.ToList();
                }
            }
        }

        public List<Contact> GetContactsForLicensee(int licenseeId)
        {
            using (var context = new AuthContext())
            {
                    return context.Contacts
                        .Include(x=>x.Address)
                        .Include(x=>x.Email)
                        .Include(x=>x.Phone)
                        .Where(c => c.LicenseeId==licenseeId).Where(c => c.Deleted == null).ToList();
               
            }
        }


        public List<LicenseeLabelGroup> GetLabelsForLicensee(int licenseeId)
        {
            using (var context = new AuthContext())
            {
                    return context.LicenseeLabelGroups.Where(c => c.LicenseeId==licenseeId && !c.Deleted.HasValue).ToList();
               
            }
        }
        public List<LicenseeLabelGroup> GetAlLabelGroups()
        {
            using (var context = new AuthContext())
            {
                return context.LicenseeLabelGroups.ToList();

            }
        }

        public Contact EditContact(Contact contact)
        {
            using (var context = new AuthContext())
            {
                context.Entry(contact).State = (EntityState)System.Data.EntityState.Modified;
                context.SaveChanges();
                return contact;
            }
        }

        public LicenseeLabelGroup AddLabelGroup(LicenseeLabelGroup labelGroup)
        {
            using (var context = new AuthContext())
            {
                context.LicenseeLabelGroups.Add(labelGroup);
                context.SaveChanges();

                return labelGroup;
            }
        }

        public LicenseeLabelGroup EditLabelGroup(LicenseeLabelGroup labelGroup)
        {
            using (var context = new AuthContext())
            {
                context.Entry(labelGroup).State = (EntityState)System.Data.EntityState.Modified;
                context.SaveChanges();
                return labelGroup;
            }
        }
        public LicenseeLabelGroupLink EditLabelGroupLink(LicenseeLabelGroupLink labelGroupLink)
        {
            using (var context = new AuthContext())
            {
                context.Entry(labelGroupLink).State = (EntityState)System.Data.EntityState.Modified;
                context.SaveChanges();
                return labelGroupLink;
            }
        }

        
        public List<LicenseeLabelGroupLink> GetContactsForLicenseeLabel(int licenseeLabelGroupId)
        {
            using (var context = new AuthContext())
            {
                return context.LicenseeLabelGroupLinks.Where(c => c.LicenseeLabelGroupId == licenseeLabelGroupId && c.Deleted==null).ToList();

            }
        }

        public void AddLabelGroupLink(LicenseeLabelGroupLink labelGroup)
        {
            using (var context = new AuthContext())
            {
                context.LicenseeLabelGroupLinks.Add(labelGroup);
                context.SaveChanges();

            }
        }

        public List<LicenseeLabelGroupLink> GetLinksForContact(int contactId)
        {
            using (var context = new AuthContext())
            {
                return context.LicenseeLabelGroupLinks.Where(c => c.ContactId == contactId && !c.Deleted.HasValue).ToList();

            }
        }
    }
}
