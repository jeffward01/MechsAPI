using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.Models.ContactModel;

namespace UMPG.USL.API.Business.Contacts
{


    public interface IContactGenerateLicenseQueueManager
    {
        ContactGeneratedLicenseQueue Add(ContactGeneratedLicenseQueue contact);
    }
}
