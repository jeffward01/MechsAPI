using System.Collections.Generic;
using System.Web.Http;
using Amazon;
using Amazon.S3;
using UMPG.USL.API.Business.Licenses;
using UMPG.USL.Common;
using UMPG.USL.Models;
using UMPG.USL.Models.LicenseModel;
using UMPG.USL.Models.Recs;
using System.Net;
using System.Net.Http;
using System.Web;
using System;
using UMPG.USL.API.Data.LicenseData;
using System.IO;
using System.Web.Http.Tracing;
using UMPG.USL.API.ActionFilters;


namespace UMPG.USL.API.Controllers.LicenseCTRL
{
    using System.Configuration;
    using Amazon.S3.Model;

   // [AuthorizationRequired]
    [RoutePrefix("api/licenseCTRL/licenseAttachments")]
    public class LicenseAttachmentController : ApiController
    {
        private readonly ITraceWriter _tracer;
        private readonly ILicenseAttachmentManager _licenseAttachmentManager;
        public LicenseAttachmentController(ILicenseAttachmentManager licenseAttachmentManager)
        {
            _licenseAttachmentManager = licenseAttachmentManager;
            _tracer = GlobalConfiguration.Configuration.Services.GetTraceWriter();
        }
        //[Authorize]
        [Route("")]
        [HttpGet]
        [ActionName("GetAll")]
        public List<LicenseAttachment> Get()
        {
            return _licenseAttachmentManager.GetAll();
        }

        [Route("GetAllAttachmentsByLicenseId/{licenseid}")]
        [HttpGet]
        public List<LicenseAttachment> GetAllAttachmentsByLicenseId(int licenseid)
        {
            return _licenseAttachmentManager.GetAllAttachmentsByLicenseId(licenseid);
        }

        [Route("DownloadLicenseAttachment/{licenseAttachmentId}")]
        [HttpGet]
        public TemporaryUrlResult GetAttachmentUrl(int licenseAttachmentId)
        {
            var licenseAttachment = _licenseAttachmentManager.Get(licenseAttachmentId);
            HttpResponseMessage response = Request.CreateResponse();
            var amazonHelper = new AmazonS3Helper();
            var temporaryurl = amazonHelper.GetPresignedUrl(licenseAttachment.virtualFilePath,
                licenseAttachment.fileName + licenseAttachment.fileType);

            return new TemporaryUrlResult
            {
                Success = true,
                Url = temporaryurl
            };

        }

        [Route("UpdateLicenseAttachment")]
        [HttpPost]
        public HttpResponseMessage UpdateLicenseAttachment(LicenseAttachment licenseAttachment)
        {
            var result = _licenseAttachmentManager.UpdateLicenseAttachement(licenseAttachment);

            if (result)
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [Route("UploadAttachmentsByLicenseId/{licenseid}/{attachmentTypeId}")]
        [HttpPost]
        public HttpResponseMessage UploadAttachmentsByLicenseId(int licenseid, int attachmentTypeId)
        {
            HttpResponseMessage result = null;

            var amazonHelper = new AmazonS3Helper();
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                var docfiles = new List<LicenseAttachment>();
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    var fileName = postedFile.FileName;
                    var amazonKeyName = amazonHelper.UploadStreamToAmazon(fileName, postedFile.InputStream, licenseid);
                    string fileNameWithOutExtension = Path.GetFileNameWithoutExtension(postedFile.FileName);
                    string fileExtension = Path.GetExtension(postedFile.FileName);
                    var license = new LicenseAttachment()
                    {
                        licenseId = licenseid,
                        fileName = fileNameWithOutExtension,
                        fileType = fileExtension,
                        includeInLicense = false,
                        uploaddedDate = DateTime.Now,
                        AttachmentTypeId = attachmentTypeId,
                        virtualFilePath = amazonKeyName,
                        CreatedBy = Int32.Parse(httpRequest.Form[0])
                    };
                    docfiles.Add(license);
                }


                foreach (LicenseAttachment licenseAttachment in docfiles)
                {
                    _licenseAttachmentManager.AddLicenseAttachment(licenseAttachment);
                }

                result = Request.CreateResponse(HttpStatusCode.Created, docfiles);
            }
            else
            {
                result = Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            return result;
        }

        [Route("RemoveAttachment")]
        [HttpPost]
        public HttpResponseMessage UpdateProductConfigurationsAll(LicenseAttachment licenseAttachment)
        {
            _licenseAttachmentManager.RemoveLicenseAttachment(licenseAttachment);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("RemoveAttachments")]
        [HttpPost]
        public HttpResponseMessage UpdateProductConfigurationsAll(List<LicenseAttachment> licenseAttachments)
        {
            foreach (LicenseAttachment licenseAttachment in licenseAttachments)
            {
                _licenseAttachmentManager.RemoveLicenseAttachment(licenseAttachment);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        //[Authorize]
        [Route("Search")]
        [HttpPost]
        public List<LicenseAttachment> Search([FromBody]string query)
        {

            return _licenseAttachmentManager.Search(query);
        }

        //[Route("PagedSearch")]
        //[HttpPost]
        //public PagedResponse<License> PagedSearch(LicenseRequest request)
        //{

        //    return _licenseeManager.PagedSearch(request);
        //}

        //[HttpPost]
        //[ActionName("Add")]
        //public Licensee Add(Licensee licensee)
        //{
        //    return _licenseeManager.Add(licensee);
        //}
        
        public string GetFileType(string fileType)
        {
            var text = string.Empty;
            switch (fileType)
            {
                case "text/plain":
                    text = "TXT";
                    break;
                default:
                    text = "PDF";
                    break;
            }
            return text;
        }


    }


    #region Helpers




    #endregion
}
