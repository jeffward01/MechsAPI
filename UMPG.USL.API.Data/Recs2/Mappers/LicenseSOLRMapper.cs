using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq.Expressions;
using UMPG.USL.Models.ContactModel;
using UMPG.USL.Models.LicenseModel;
using UMPG.USL.Models.LicenseSearchModel;
using UMPG.USL.Models.LookupModel;
using UMPG.USL.Models.Recs;

namespace UMPG.USL.Common.Mappers
{
    public class LicenseSOLRMapper : IMapper<License, LicenseSOLR>
    {

        public License Map(LicenseSOLR source)
        {
            var license = new License();
            license.ArtistRollup = source.Artists;
            license.LicenseConfigurationRollup = source.RolledUpConfiguration;
            license.LicenseId = source.Id;
            if (source.Assignee != null)
            {
                license.Contact = new Contact
                {
                    FullName = source.Assignee.Name,
                    ContactId = source.Assignee.Id.Value
                };
            }
            license.LicenseMethod = new LU_LicenseMethod
            {
                LicenseMethod = source.LicenseMethod.Name,
                LicenseMethodId = source.LicenseMethod.Id.HasValue ? source.LicenseMethod.Id.Value : 0
            };
            license.LicenseMethodId = license.LicenseMethod.LicenseMethodId;
            license.LicenseName = source.LicenseName;
            license.LicenseNumber = source.LicenseNumber;
            license.LicensePriority = new LU_Priority
            {
                Priority = source.Priority.Name,
                PriorityId = source.Priority.Id.HasValue ? source.Priority.Id.Value : 0
            };
            license.PriorityId = source.Priority.Id.HasValue ? source.Priority.Id.Value : 0;
            license.LicenseStatus = new LU_LicenseStatus
            {
                LicenseStatus = source.Status.Name,
                LicenseStatusId = source.Status.Id.HasValue ? source.Status.Id.Value : 0
            };
            license.LicenseStatusId = source.Status.Id.HasValue ? source.Status.Id.Value : 0;
            license.LicenseType = new LU_LicenseType
            {
                LicenseTypeId = source.Type.Id.HasValue ? source.Type.Id.Value : 0,
                LicenseType = source.Type.Name
            };
             license.LicenseTypeId = source.Type.Id.HasValue ? source.Type.Id.Value : 0;
            license.Licensee = new Licensee
            {
                LicenseeId = source.Licensee.Id.HasValue ? source.Licensee.Id.Value : 0,
                Name = source.Licensee.Name
            };
            license.LicenseeId = source.Licensee.Id.HasValue ? source.Licensee.Id.Value : 0;
            license.ProductsNo = source.ProductCount;
            license.CreatedDate = source.CreatedDate;
            license.ModifiedDate = source.ModifiedDate;
            if(source.SignedDate.HasValue)
            license.SignedDate = source.SignedDate.Value;
            license.Contact2 = new Contact
            {
                FullName = source.CreatedBy.Name,
                ContactId = source.CreatedBy.Id.HasValue ? source.CreatedBy.Id.Value : 0
            };
            return license;
        }
    }
}