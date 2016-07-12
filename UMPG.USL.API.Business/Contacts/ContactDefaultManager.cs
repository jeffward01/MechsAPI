using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.API.Data.ContactData;
using UMPG.USL.Models.ContactModel;

namespace UMPG.USL.API.Business.Contacts
{
    public class ContactDefaultManager : IContactDefaultManager
    {

        private readonly IContactDefaultRepository _contactDefaultRepository;

        public ContactDefaultManager(IContactDefaultRepository contactDefaultRepository)
        {
            _contactDefaultRepository = contactDefaultRepository;
        }

        public List<ContactDefault> GetAll()
        {
            return _contactDefaultRepository.GetAll();
        }

        public ContactDefault Get(int id)
        {
            return _contactDefaultRepository.Get(id);
        }

        public ContactDefault Add(ContactDefault contactDefault)
        {
            return _contactDefaultRepository.Add(contactDefault);
        }

        public ContactDefault Save(ContactDefault contactDefault)
        {
            return _contactDefaultRepository.Save(contactDefault);
        }

    }
}
