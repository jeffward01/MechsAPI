﻿using System.Collections.Generic;
using UMPG.USL.Models;
using UMPG.USL.Models.LicenseModel;

namespace UMPG.USL.API.Data.LicenseData
{
    public interface ILicenseRepository
    {
        License Add(License license);

        License Get(int Id);

        List<License> GetAll();

        List<License> GetAll(int startIndex, int endIndex);

        List<License> GetInboxLicenses(int assigneeId);
        List<License> GetProductLicenses(long productId);
        List<License> Search(string query);

        PagedResponse<License> PagedSearch(LicenseRequest request);

        void UpdateLicense(License license);

        List<License> GetByIds(List<int> ids);

        List<int> GetLicenseProductIds(int licenseid);

        // List<LicenseProduct> GetLicenseProducts(int licenseid);

        List<int> GetLicenseRecordingsTrackIds(int licenseproductid);

        License GetLite(int id);

        List<SendLicenseContact> GetSendLicenseContacts(int licenseSentId);

        SendLicenseInfo AddSendLicenseInfo(SendLicenseInfo sendLicenseInfo);

        SendLicenseInfo GetSendLicenseInfo(int licenseId);

        void UpdateSendLicenseInfo(SendLicenseInfo request);

        void UpdateSendLicenseContact(SendLicenseContact sendLicenseContact);

        SendLicenseContact AddSendLicenseContact(SendLicenseContact sendLicenseContact);

        License LicenseNameExists(string licenseName);

        int CountAllLicenses();
        List<int> GetNextLicenseIds(int from, int pageSize);
        //   LicenseProduct GetMechLicenseProduct(int recsProductId);
        List<int> GetAllRelatedLicenseIds(int recProductId);
        License GetLicnese(int id);
    }
}
