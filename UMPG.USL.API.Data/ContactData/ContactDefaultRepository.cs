using System;
using System.Collections.Generic;
using System.Linq;
using UMPG.USL.Models.ContactModel;

namespace UMPG.USL.API.Data.ContactData
{
    public class ContactDefaultRepository : IContactDefaultRepository
    {

        public ContactDefault Save(ContactDefault contactDefault)
        {
            using (var context = new AuthContext())
            {
                // temporary - make sure we use the same contactdefault record for now

                var query =
                    context.ContactDefaults.FirstOrDefault(cd => cd.ContactId == contactDefault.ContactId);
                if (query == null)
                {
                    contactDefault.CreatedDate = DateTime.Now;
                    context.ContactDefaults.Add(contactDefault);
                }
                else
                {
                    query.ModifiedDate = DateTime.Now;
                    query.UserSetting = contactDefault.UserSetting;
                    context.Entry(query).State = System.Data.Entity.EntityState.Modified;
                }
                //context.ContactDefaults.Add(contactDefault);
                context.SaveChanges();

                return contactDefault;
            }
        }

        public ContactDefault Add(ContactDefault contactDefault)
        {
            using (var context = new AuthContext())
            {
                context.ContactDefaults.Add(contactDefault);
                context.SaveChanges();

                return contactDefault;
            }
        }

        public ContactDefault Get(int id)
        {
            using (var context = new AuthContext())
            {
                return context.ContactDefaults.FirstOrDefault(c => c.ContactId == id);
                //return context.ContactDefaults.FirstOrDefault(c => c.co == id);
            }
        }

        public List<ContactDefault> GetAll()
        {
            using (var context = new AuthContext())
            {
                return context.ContactDefaults.ToList();
            }
        }

    }
}
