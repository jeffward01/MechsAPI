using System.Collections.Generic;
using UMPG.USL.Models.ContactModel;

namespace UMPG.USL.API.Data.ContactData
{
    public interface IContactDefaultRepository
    {
        ContactDefault Add(ContactDefault contactDefault);

        ContactDefault Save(ContactDefault contactDefault);

        ContactDefault Get(int Id);

        List<ContactDefault> GetAll();

    }
}
