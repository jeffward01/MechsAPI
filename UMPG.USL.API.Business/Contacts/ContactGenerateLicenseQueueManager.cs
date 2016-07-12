using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.API.Data.ContactData;
using UMPG.USL.Models.ContactModel;

namespace UMPG.USL.API.Business.Contacts
{
    public class ContactGenerateLicenseQueueManager :IContactGenerateLicenseQueueManager
    {
        private readonly IContactGenerateLicenseQueueRepository _contactGeneratedLicenseQueueRepository;

        public ContactGenerateLicenseQueueManager(IContactGenerateLicenseQueueRepository contactGeneratedLicenseQueueRepository)
        {
            _contactGeneratedLicenseQueueRepository = contactGeneratedLicenseQueueRepository;
        }


        public ContactGeneratedLicenseQueue Add(ContactGeneratedLicenseQueue contact)
        {
            return _contactGeneratedLicenseQueueRepository.Add(contact);
        }
    }
}