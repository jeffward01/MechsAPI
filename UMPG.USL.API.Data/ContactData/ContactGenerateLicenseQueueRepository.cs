using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.Models.ContactModel;

namespace UMPG.USL.API.Data.ContactData
{


    public class ContactGenerateLicenseQueueRepository : IContactGenerateLicenseQueueRepository
    {
        public ContactGeneratedLicenseQueue Add(ContactGeneratedLicenseQueue contact)
        {
            using (var context = new AuthContext())
            {
                context.ContactGeneratedLicenseQueue.Add(contact);
                context.SaveChanges();

                return contact;
            }
        }
    }
}
