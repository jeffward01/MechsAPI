using System.Collections.Generic;
using UMPG.USL.Models.ContactModel;

namespace UMPG.USL.API.Business.Contacts
{
    public interface IContactDefaultManager
    {
        List<ContactDefault> GetAll();

        ContactDefault Add(ContactDefault contactDefault);

        ContactDefault Save(ContactDefault contactDefault);

        ContactDefault Get(int id);


    }
}
