using System.Collections.Generic;
using System.Web.Http;
using UMPG.USL.API.ActionFilters;
using UMPG.USL.API.Business.Licenses;
using UMPG.USL.Models;
using UMPG.USL.Models.LicenseModel;

namespace UMPG.USL.API.Controllers.LicenseCTRL
{
    using System;
    using System.Configuration;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Web;
    using System.Web.UI;

    using UMPG.USL.API.Business.Contacts;
    using UMPG.USL.Common;
    using UMPG.USL.Models.ContactModel;
    using UMPG.USL.Models.LicenseGenerate;
  
    [RoutePrefix("api/licenseCTRL/licenses")]
    public class LicenseController : BaseController
    {
        private readonly ILicenseManager _licenseManager;
        private readonly IContactManager _contactManager;
        private readonly IContactGenerateLicenseQueueManager _contactGenerateLicenseQueueManager;
        private readonly IGenerateLicenseManager _generateLicenseManager;

     
        public LicenseController(ILicenseManager licenseManager, IContactManager contactManager, IContactGenerateLicenseQueueManager contactGenerateLicenseQueueManager, IGenerateLicenseManager generateLicenseManager)
        {
            _licenseManager = licenseManager;
            _contactManager = contactManager;
            _contactGenerateLicenseQueueManager = contactGenerateLicenseQueueManager;
            _generateLicenseManager = generateLicenseManager;
        }
        [AuthorizationRequired]
        [Route("")]
        [HttpGet]
        [ActionName("GetAll")]
        public List<License> Get()
        {
            return _licenseManager.GetAll();
        }

        [AuthorizationRequired]
        [Route("Search")]
        [HttpPost]
        public List<License> Search([FromBody]string query)
        {

            return _licenseManager.Search(query);
        }

        [AuthorizationRequired]
        [Route("PagedSearch")]
        [HttpPost]
        public PagedResponse<License> PagedSearch(LicenseRequest request)
        {

            return _licenseManager.PagedSearch(request);
        }
        [AuthorizationRequired]
        [HttpPost]
        [ActionName("Add")]
        public License Add(License contact)
        {
            return _licenseManager.Add(contact);
        }

        [AuthorizationRequired]
        [Route("UpdateLicense")]
        [HttpPost]
        public bool UpdateLicense(UpdateLicenseAssigneeRequest request)
        {
            return _licenseManager.UpdateLicense(request);
        }

        [AuthorizationRequired]
        [Route("UploadGeneratedLicensePreview")]
        [HttpPost]
        public bool UploadGeneratedLicensePreview(UploadGeneratedLicensePreviewRequest data)
        {
            var amazonHelper = new AmazonS3Helper();
            byte[] info = new UTF8Encoding(true).GetBytes(data.htmlText);
            Stream stream = new MemoryStream(info);
            var amazonKeyName = amazonHelper.UploadStreamToAmazon(data.fileName, stream, data.licenseId);
            data.fileName = amazonKeyName;

            var fromContactEmail = _contactManager.GetContactEmail(data.fromContactId);
            var FromEmail = "";
            if (fromContactEmail != null)
            {
                FromEmail = fromContactEmail.EmailAddress;
            }
            
           
            
            var Subject = data.Subject;
            data.Content = string.IsNullOrEmpty(data.Content) ? ConfigurationManager.AppSettings["Content"] : ConfigurationManager.AppSettings["Content"] + " " + data.Content;
            
            if (FromEmail == "")
            {
                FromEmail = "umpgtest@hotmail.com";
            }
            var ToEmail = string.Empty;
            if (string.IsNullOrEmpty(Subject))
            {
                data.Subject = ConfigurationManager.AppSettings["Subject"];
            }
            
                    SendLicenseInfo licenseInfo = _licenseManager.GetSendLicenseInfo(data.licenseId);
                   
                    GenerateLicensePreviewRequest dataRequest = new GenerateLicensePreviewRequest(data);
                    if (licenseInfo != null && licenseInfo.SendLicenseContactList.Count > 0)
                    {
                        foreach (var sendLicenseContact in licenseInfo.SendLicenseContactList)
                        {
                            if (string.IsNullOrEmpty(sendLicenseContact.EmailAddress))
                            {
                                int contactId = sendLicenseContact.ContactId.HasValue ? sendLicenseContact.ContactId.Value : -1;
                                ContactEmail sendContactEmail = _contactManager.GetContactEmail(contactId);
                                ToEmail = (string.IsNullOrEmpty(ToEmail)) ? sendContactEmail.EmailAddress : string.Format("{0}|{1}", ToEmail, sendContactEmail.EmailAddress);
                                
                            }
                        }
                    }
                    
                    dataRequest.ToEmail = ToEmail;
                    dataRequest.FromEmail = FromEmail;
                    dataRequest.Directory = "licenseDirectoryHereNotNeededAnymore";


                    if (!string.IsNullOrEmpty(dataRequest.ToEmail))
                    {
                        int generateLicenseQueueId = _licenseManager.UploadGeneratedLicensePreview(dataRequest);
                        if (generateLicenseQueueId > 0)
                        {
                            if (licenseInfo.SendLicenseContactList != null)
                            {
                                foreach (var contact in licenseInfo.SendLicenseContactList)
                                {   
                                    
                                    if (!string.IsNullOrEmpty(contact.EmailAddress))
                                    {
                                        ContactGeneratedLicenseQueue contactQueue = new ContactGeneratedLicenseQueue();
                                        contactQueue.EmailAddress = contact.EmailAddress;
                                        contactQueue.CreatedDate = DateTime.Now;
                                        contactQueue.GenerateLicenseQueueId = generateLicenseQueueId;
                                        _contactGenerateLicenseQueueManager.Add(contactQueue);
                                    }
                                }
                            }
                        }
                    }
                
            
            return true;
        }


        [AuthorizationRequired]
        [Route("UpdateGeneratedLicenseStatus")]
        [HttpPost]
        public bool UpdateGeneratedLicenseStatus(LicenseUserAction data )
        {
            _generateLicenseManager.UpdateGenerateLicenseStatus(data);
            return true;
        }

        [AuthorizationRequired]
        [Route("GetInboxLicenses/{assigneeId}")]
        [HttpGet]
        public List<License> GetInboxLicenses(int assigneeId)
        {
            return _licenseManager.GetInboxLicenses(assigneeId);
        }

        [AuthorizationRequired]
        [Route("GetLicenseDetails/{licenseId}")]
        [HttpGet]
        public License GetLicenseDetails(int licenseId)
        {
            var licDetails = _licenseManager.Get(licenseId);

            // check dates as they come back undefined

            //licDetails.ReceivedDate = null;

            return licDetails;
        }

        [AuthorizationRequired]
        [Route("GetProductLicenses/{productId}")]
        [HttpGet]
        public List<License> GetProductLicenses(int productId)
        {
            return _licenseManager.GetLicensesForProduct(productId);
        }

        [AuthorizationRequired]
        [Route("EditLicense")]
        [HttpPost]
        public License UpdateLicense(License request)
        {
            return _licenseManager.EditLicense(request);
        }

        [AuthorizationRequired]
        [Route("CreateLicense")]
        [HttpPost]
        public License CreateLicense(License request)
        {
            return _licenseManager.Add(request);
        }

        [AuthorizationRequired]
        [Route("EditStatus")]
        [HttpPost]
        public bool EditStatus(License request)
        {
            return _licenseManager.EditStatus(request, false, null);
        }

        [AuthorizationRequired]
        [Route("GetSendLicenseInfo/{licenseId}")]
        [HttpGet]
        public SendLicenseInfo GetSendLicenseInfo(int licenseId)
        {
            return _licenseManager.GetSendLicenseInfo(licenseId);
        }

        [AuthorizationRequired]
        [Route("SaveSendLicenseInfo")]
        [HttpPost]
        public bool UpdateSendLicenseInfo(SendLicenseInfo request)
        {
            //return true;
            return _licenseManager.UpdateSendLicenseInfo(request);
        }


        [Route("EditStatusLicenseProcessor/{licenseId}")]
        [HttpPost]
        public bool EditStatusLicenseProcessor(int licenseId)
        {
            StreamReader stream = new StreamReader(HttpContext.Current.Request.InputStream);
            string inputString = stream.ReadToEnd();
            DateTime signedDateTime = inputString.ToDateTime();
            return _licenseManager.EditStatusLicenseProcessor(licenseId, signedDateTime);
        }

        [AuthorizationRequired]
        [Route("EditLicenseStatusReport/{licenseId}")]
        [HttpPost]
        public bool EditLicenseStatusReport(int licenseId)
        {
            return _licenseManager.EditLicenseStatusReport(licenseId);
        }


    }


    #region Helpers



    #endregion
}
