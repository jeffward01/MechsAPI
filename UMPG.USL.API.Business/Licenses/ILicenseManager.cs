using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.Models;
using UMPG.USL.Models.LicenseModel;
using UMPG.USL.Models.Recs;
using UMPG.USL.Models.LicenseGenerate;

namespace UMPG.USL.API.Business.Licenses
{


    public interface ILicenseManager
    {
        License Add(License license);

        License Get(int id);

        List<License> GetAll();

        List<License> GetInboxLicenses(int assigneeId);

        List<License> Search(string query);

        PagedResponse<License> PagedSearch(LicenseRequest request);

       
       // List<LicenseProduct> GetLicenseProductsList(int licenseId);

        

        bool UpdateLicense(UpdateLicenseAssigneeRequest license);

        int UploadGeneratedLicensePreview(GenerateLicensePreviewRequest data);
       

        List<License> GetLicensesForProduct(int productId);

        License EditLicense(License license);

        bool EditStatus(License license, bool isAutomaticProcess, DateTime? automaticSignedDate);

        SendLicenseInfo GetSendLicenseInfo(int licenseId);

        bool UpdateSendLicenseInfo(SendLicenseInfo request);

        bool EditStatusLicenseProcessor(int licenseId, DateTime signedDate);

        bool EditLicenseStatusReport(int licenseId);


    }
}
