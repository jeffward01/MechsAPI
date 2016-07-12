using System.Collections.Generic;
using UMPG.USL.Models.ContactModel;

namespace UMPG.USL.API.Data.ContactData
{
    public interface IPhoneRepository
    {
        int Add(Phone phone);

        Phone Get(int Id);

        List<Phone> GetAll();

        List<Phone> Search(string query);

        void Update(Phone phone);

    }
}
