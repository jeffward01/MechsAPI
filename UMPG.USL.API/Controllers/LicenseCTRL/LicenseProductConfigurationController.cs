using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using UMPG.USL.API.ActionFilters;
using UMPG.USL.API.Business.Licenses;
using UMPG.USL.Models.LicenseModel;

namespace UMPG.USL.API.Controllers.LicenseCTRL
{
    [RoutePrefix("api/licenseProductConfigurationCTRL")]
    [AuthorizationRequired]
    public class LicenseProductConfigurationController: ApiController
    {
        private readonly ILicenseProductConfigurationManager _licenseProductConfigurationManager;
        public LicenseProductConfigurationController(ILicenseProductConfigurationManager licenseProductConfigurationManager)
        {
            _licenseProductConfigurationManager = licenseProductConfigurationManager;
        }

        [Route("GetLicenseProductConfiguration/{licenseProductId}")]
        [HttpGet]
        public List<LicenseProductConfiguration> GetProducts(int licenseProductId)
        {
            return _licenseProductConfigurationManager.GetLicenseProductConfigurations(licenseProductId);
        }

        [Route("GetLicenseConfigurationList")]
        [HttpPost]
        public List<LicenseProductConfiguration> GetLicenseConfigurationList(List<int> licenseProductIds)
        {
            return _licenseProductConfigurationManager.GetLicenseConfigurationList(licenseProductIds);
        }



        [Route("DeleteLicenseProductConfiguration")]
        [HttpPost]
        public bool DeleteLicenseProductConfiguration(UpdateLicenseProductConfigurationRequest request)
        {
            return _licenseProductConfigurationManager.DeleteLicenseProductConfiguration(request);
        }

       
        [Route("AddLicenseProductConfiguration")]
        [HttpPost]
        public UpdateLicenseProductConfigurationResult AddLicenseProductConfiguration(UpdateLicenseProductConfigurationRequest request)
        {
            return _licenseProductConfigurationManager.AddLicenseProductConfiguration(request);
        }

        [Route("UpdateLicenseProductConfiguration")]
        [HttpPost]
        public List<UpdateLicenseProductConfigurationResult> AddLicenseProductConfiguration(List<UpdateLicenseProductConfigurationRequest> requests)
        {
            return _licenseProductConfigurationManager.UpdateLicenseProductConfiguration(requests);
        }
        [Route("UpdateAllLicensesConfiguration/{startIndex}/{endIndex}")]
        [HttpGet]
        public bool UpdateAllLicensesConfiguration(int startIndex, int endIndex)
        {
            if (endIndex >= startIndex)
            {
                return _licenseProductConfigurationManager.UpdateAllLicensesConfiguration(startIndex, endIndex);
            }

            return false;
        }
        
        /*
                public LicenseProductConfiguration AddLicenseProductConfiguration(UpdateLicenseProductConfigurationRequest request)
        {
            return _licenseProductConfigurationManager.AddLicenseProductConfiguration(request);
        }
        */
        /*
        [Route("AddNewLicenseProductConfiguration")]
        [HttpPost]
        public LicenseProductConfiguration AddNewLicenseProductConfiguration(UpdateLicenseProductConfigurationRequest request)
        {
            return _licenseProductConfigurationManager.AddNewLicenseProductConfiguration(request);
        }
        */
    }
}