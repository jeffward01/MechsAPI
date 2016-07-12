﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using UMPG.USL.API.Business.Licenses;
using UMPG.USL.Models.LicenseModel;
using UMPG.USL.Models.LicenseTemplateModel;
using UMPG.USL.Models.Recs;
using UMPG.USL.Models.StaticDropdownsData;

namespace UMPG.USL.API.Controllers.LicenseCTRL
{
   [RoutePrefix("api/licenseProductCTRL/licenseproducts")]
    public class LicenseProductController: ApiController
    {
        private readonly ILicenseProductManager _licenseProductManager ;
        public LicenseProductController(ILicenseProductManager licenseProductManager)
        {
            _licenseProductManager = licenseProductManager;
        }

        [Route("GetProducts/{licenseId}")]
        [HttpGet]
        public List<LicenseProduct> GetProducts(int licenseId)
        {
            return _licenseProductManager.GetProducts(licenseId);
        }

        [Route("GetProductsNew/{licenseId}")]
        [HttpGet]
        public List<LicenseProduct> GetProductsNew(int licenseId)
        {
            return _licenseProductManager.GetProductsNew(licenseId);
        }

        [Route("GetLicenseProductOverview/{recProductId}")]
        [HttpGet]
        public LicenseProductOverview GetLicenseProductOverview(int recProductId)
        {
            return _licenseProductManager.GetLicenseProductOverview(recProductId);
        }

        [Route("GetSelectedProduct/{licenseId}/{productId}")]
        [HttpGet]
        public LicenseProduct GetSelectedProduct(int licenseId, int productId)
        {
            return _licenseProductManager.GetSelectedProduct(licenseId, productId);
        }

        [Route("DeleteLicenseProduct/{licenseId}/{productId}")]
        [HttpGet]
        public bool DeleteLicenseProduct(int licenseId, int productId)
        {
            return _licenseProductManager.DeleteLicenseProduct(licenseId, productId);
        }
        
       /// <summary>
       /// will be replaced by the upside down method
       /// </summary>
       /// <param name="licenseproductId"></param>
       /// <returns></returns>
        [Route("GetRecordings/{licenseproductId}")]
        [HttpGet]
        public List<LicenseProductRecording> GetRecordings(int licenseproductId)
        {
            return _licenseProductManager.GetLicenseProductRecordings(licenseproductId);
        }
        

        //[Route("GetLicenseWriterList")]
        //[HttpPost]
        //public List<LicenseProductRecordingWritersDropdown> GetLicenseWriterList(List<int> licenseRecordingIds)
        //{
        //    return _licenseProductManager.GetLicenseWriterList(licenseRecordingIds);
        //}
        //todo: revisit.. have no ideea yet
        //[Route("GetRecordingsList")]
        //[HttpPost]
        //public List<LicenseProductRecording> GetLicenseRecordingsList(List<int> licenseRecordingIds)
        //{
        //    return _licenseProductManager.GetLicenseRecordingsList(licenseRecordingIds);
        //}

        [Route("GetLicenseWritersNo/{licenseRecordingId}")]
        [HttpGet]
        public int GetLicenseProductRecordingWritersNo(int licenseRecordingId)
        {
            return _licenseProductManager.GetLicenseProductRecordingWritersNo(licenseRecordingId);
        }

        [Route("GetLicensePRWriterRates/{LicenseWriterId}")]
        [HttpGet]
        public List<LicenseProductRecordingWriterRate> GetLicensePRWriterRates(int licenseWriterId)
        {
            return _licenseProductManager.GetLicensePRWriterRates(licenseWriterId);
        }

        [Route("GetLicenseProductConfigurations/{licenseId}")]
        [HttpGet]
        public List<LicenseProductConfiguration> GetLicenseProductConfigurations(int licenseId)
        {
            return _licenseProductManager.GetLicenseProductConfigurations(licenseId);
        }


        [Route("UpdateLicenseProductConfigurations")]
        [HttpPost]
        public bool UpdateLicenseProductConfigurations(UpdateLicenseProductConfigurationsStatusRequest request)
        {
            return _licenseProductManager.UpdateLicenseProductConfigurations(request);
        }

        [Route("GetProductConfigurationsAll")]
        [HttpPost]
        public List<ProductConfiguration> GetProductConfigurationsAll(GetProductConfigurationsAllRequest request)
        {
            return _licenseProductManager.GetProductConfigurationsAll(request);
        }

        [Route("UpdateProductConfigurationsAll")]
        [HttpPost]
        public bool UpdateProductConfigurationsAll(List<UpdateProductConfigurationsAllRequest> requests)
        {
            return _licenseProductManager.UpdateProductConfigurationsAll(requests);
        }

        [Route("UpdateLicenseProducts")]
        [HttpPost]
        public bool UpdateLicenseProducts(UpdateLicenseProductsRequest request)
        {
            return _licenseProductManager.UpdateLicenseProducts(request);
        }
        
        [Route("UpdateLicenseProductSchedule")]
        [HttpPost]
        public bool UpdateLicenseProductSchedule(List<LicenseProduct> request)
        {
            _licenseProductManager.UpdateLicenseProductSchedule(request);
            return true;
        }

        [Route("GetLicenseProductConfigurationsAll/{licenseId}")]
        [HttpGet]
        public List<LicenseProductConfigurations> GetLicenseProductConfigurationsAll(int licenseId)
        {
            return _licenseProductManager.GetLicenseProductConfigurationsAll(licenseId);
        }

        //todo: to be deleted...
        //[Route("GetLicenseProductDropDown/{licenseId}")]
        //[HttpGet]
        //public List<LicenseProductDropdown> GetLicenseProductDropDown(int licenseId)
        //{
        //    return _licenseProductManager.GetLicenseProductDropDown(licenseId);
        //}

        //[Route("GetLicenseProductDropDown/{licenseproductId}")]
        //[HttpGet]
        //public List<LicenseProductDropdown> GetLicenseProductConfigurationDropDown(int licenseId)
        //{
        //    return _licenseProductManager.GetLicenseProductDropDown(licenseId);
        //}

        [Route("UpdateLicenseProductConfigurationsAll")]
        [HttpPost]
        public bool UpdateLicenseProductConfigurations(List<UpdateLicenseProductConfigurationsRequest> request)
        {
            return _licenseProductManager.UpdateLicenseProductConfigurationsAll(request);
        }

        /// <summary>
        /// following methods are used for the edit rates modal.
        /// todo:move to a separate controller if needed
        /// </summary>
        /// <param name="licenseid"></param>
        /// <returns></returns>
        [Route("GetAllLicenseRelatedIds/{licenseId}")]
        [HttpGet]
        public GetWritersRatesRequest GetAllLicenseRelatedIds(int licenseid)
        {
            return _licenseProductManager.GetAllLicenseRelatedIds(licenseid);
        }


        [Route("GetAllLicenseRecordingRelatedIds")]
        [HttpPost]
        public GetWritersRatesRequest GetAllLicenseRecordingRelatedIds(List<int> licenseRecordingIds)
        {
            return _licenseProductManager.GetAllLicenseRecordingRelatedIds(licenseRecordingIds);
        }

        [Route("GetLicenseProductRecordingsDropdown/{licenseId}")]
        [HttpGet]
        public List<LicenseProductRecordingsDropdown> GetLicenseProductRecordingsDropdown(int licenseId)
        {
            return _licenseProductManager.GetLicenseProductRecordingsDropdown(licenseId);
        }

        [Route("GetLicenseProductWritersDropdown")]
        [HttpPost]
        public List<LicenseProductRecordingWritersDropdown> GetLicenseProductRecordingsWritersDropdown(GetWritersRatesRequest request)
        {
            return _licenseProductManager.GetLicenseProductRecordingsWritersDropdown(request);
        }

        [Route("EditRates")]
        [HttpPost]
        public bool EditRates(EditRatesSaveRequest request)
        {
            return _licenseProductManager.EditRatesAndWriters(request);
        }

        [Route("EditIndividualRates")]
        [HttpPost]
        public bool EditRates(List<EditRatesSaveRequest> request)
        {
            return _licenseProductManager.EditIndividualWriterRates(request);
        }
        [Route("GetWritersNoForEditRates")]
        [HttpPost]
        public List<int> GetWritersNoForEditRates(GetWritersRatesRequest request)
        {
            var l = _licenseProductManager.GetWritersNoForEditRates(request);
            return l;
        }



        /// <summary>
        /// New method that has data upside down
        /// </summary>
        /// <param name="licenseproductId"></param>
        /// <returns></returns>
        [Route("GetLicenseProductRecordingsV2/{licenseproductId}")]
        [HttpPost]
        public List<WorksRecording> GetLicenseProductRecordingsV2(int licenseproductId, [FromBody] string safeId)
        {
            return _licenseProductManager.GetLicenseProductRecordingsV2(licenseproductId,safeId);
        }
        /// <summary>
        /// New method that has data upside down
        /// </summary>
        /// <param name="licenseRecordingId"></param>
        /// <param name="worksCode"></param>
        /// <returns></returns>
        [Route("GetLicenseWritersV2/{licenseRecordingId}")]
        [HttpPost]
        public List<WorksWriter> GetLicenseWritersV2(int licenseRecordingId, [FromBody] string worksCode)
        {
            return _licenseProductManager.GetLicenseWritersV2(licenseRecordingId, worksCode);
        }

        /// <summary>
        /// Method that gets the license preview. Placed here because the product 
        /// manager has instances of all needed repositories
        /// </summary>
        /// <param name="licenseId"></param>
        /// <returns></returns>
        [Route("GetLicensePreview/{licenseId}")]
        [HttpGet]
        public LicenseTemplate GetLicensePreview(int licenseId)
        {
            return _licenseProductManager.GetLicenseTemplate(licenseId);
        }

        /// <summary>
        /// Method that gets the license preview. Placed here because the product 
        /// manager has instances of all needed repositories
        /// </summary>
        /// <param name="licenseId"></param>
        /// <returns></returns>
        [Route("CloneLicense/{licenseId}/{clonetype}/{contactid}")]
        [HttpGet]
        public CloneLicenseResult CloneLicense(int licenseId, string clonetype, int contactid)
        {
            return _licenseProductManager.CloneLicense(licenseId, clonetype, contactid);

        }
        /// <summary>
        /// Method that gets the license preview. Placed here because the product 
        /// manager has instances of all needed repositories
        /// </summary>
        /// <param name="licenseId"></param>
        /// <returns></returns>
        [Route("GetLicenseWriterRateIdsNotOnHold/{licenseId}")]
        [HttpGet]
        public List<int> GetLicenseWriterRateStatusesWithOutHolds(int licenseId)
        {
            return _licenseProductManager.GetLicenseWriterRateIdsWithOutHolds(licenseId);

        }

        [Route("GetYearsForEditRates")]
        [HttpGet]
        public List<int> GetYearsForEditRates()
        {
            return _licenseProductManager.GetYearsForEditRates();
        }

        [Route("EditWriterConsent")]
        [HttpPost]
        public bool EditWriterConsent(EditWriterConsentSaveRequest request)
        {
            return _licenseProductManager.EditWriterConsent(request);
        }

        [Route("EditWriterIncluded")]
        [HttpPost]
        public bool EditWriterIncluded(EditWriterIncludedSaveRequest request)
        {
            return _licenseProductManager.EditWriterIsIncluded(request);
        }

        [Route("EditPaidQuarter")]
        [HttpPost]
        public bool EditPaidQuarter(EditPaidQuarterSaveRequest request)
        {
            return _licenseProductManager.EditPaidQuarter(request);
        }

   }
}