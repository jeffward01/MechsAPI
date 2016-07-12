using System.Collections.Generic;
using UMPG.USL.Models.ContactModel;

namespace UMPG.USL.API.Data.ContactData
{
    public interface IContactEmailRepository
    {
        ContactEmail Add(ContactEmail contactEmail);

        Contact Get(string Email);

        ContactEmail GetContactEmail(int contactId);

        void Update(ContactEmail email);

    }
}
