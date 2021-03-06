﻿using System.Collections.Generic;
using UMPG.USL.Models.LicenseModel;

namespace UMPG.USL.API.Business.Licenses
{
    public interface ILicenseAttachmentManager
    {
        List<LicenseAttachment> GetAll();

        //Licensee Add(Licensee licensee);

        LicenseAttachment Get(int id);
        
        List<LicenseAttachment> Search(string query);
        bool DoesLicenseHaveLicenseAttachments(int licenseId);

        List<LicenseAttachment> GetAllAttachmentsByLicenseId(int licenseId);

        void AddLicenseAttachment(LicenseAttachment licenseAttachment);

        void RemoveLicenseAttachment(LicenseAttachment licenseAttachment);
        LicenseAttachment GetLicenseAttachement(int licenseAttachmentId);
        bool UpdateLicenseAttachement(LicenseAttachment licenseAttachment);


    }
}
