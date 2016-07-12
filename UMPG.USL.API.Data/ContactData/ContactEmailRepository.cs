using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using UMPG.USL.Models.ContactModel;

namespace UMPG.USL.API.Data.ContactData
{
    public class ContactEmailRepository : IContactEmailRepository
    {

        public ContactEmail Add(ContactEmail contactEmail)
        {
            using (var context = new AuthContext())
            {
                context.ContactEmails.Add(contactEmail);
                context.SaveChanges();

                return contactEmail;
            }
        }


        //public ContactEmail Get(string email)
        public Contact Get(string email)
        {
            using (var context = new AuthContext())
            {
                var contactId = context.ContactEmails.Where(c => c.EmailAddress == email).Select(c => c.ContactId).FirstOrDefault();

                return context.Contacts.FirstOrDefault(b => b.ContactId == contactId);
                //return context.ContactEmails
                //.FirstOrDefault(c => c.EmailAddress == email && c.Deleted == null);
            }
        }

        public ContactEmail GetContactEmail(int contactId)
        {
            using (var context = new AuthContext())
            {
                return context.ContactEmails.FirstOrDefault(c => c.ContactId == contactId);
                
            }
        }

        public void Update(ContactEmail email)
        {
            using (var context = new AuthContext())
            {
                context.Entry(email).State = (EntityState)System.Data.EntityState.Modified;
                context.SaveChanges();

            }
        }
    }
}
