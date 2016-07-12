using System;
using System.Collections.Generic;
using System.Linq;
using UMPG.USL.Models.ContactModel;
namespace UMPG.USL.API.Data.ContactData
{
    public interface IContactGenerateLicenseQueueRepository
    {
        ContactGeneratedLicenseQueue Add(ContactGeneratedLicenseQueue contact);
    }
}