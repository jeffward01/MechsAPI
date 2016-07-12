using System.Linq.Expressions;
using System.Runtime.InteropServices;
using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.Models.LicenseModel;
using UMPG.USL.Models.Recs;

namespace UMPG.USL.API.Business.Licenses
{
    public class LicenseUpdateIntercepror : IInterceptor
    {
        private readonly ILicenseSolrManager _licenseSolrManager;
        public LicenseUpdateIntercepror(ILicenseSolrManager solrManager)
        {
            _licenseSolrManager = solrManager;
        }


        public void Intercept(IInvocation invocation)
        {
            invocation.Proceed();
            _licenseSolrManager.ClearCache();
            switch (invocation.MethodInvocationTarget.Name)
            {
                case "Add":
                    UpdateLicense(GetAddMethodParameter(invocation));
                    break;
                case "AddLicenseProductConfiguration":
                     UpdateLicense(GetLicenseAddProductConfiguration(invocation));
                    break;
                case "EditLicense":
                    UpdateLicense(GetUpdateLicenseParameter(invocation));
                    break;
                case "UpdateLicense":
                    var ids = GetUpdateAssigneeParameter(invocation);
                    UpdateAssignee(ids);
                    break;
                case "DeleteLicenseProductConfiguration":
                    UpdateLicense(GetLicenseDeleteProductConfiguration(invocation));
                    break;
                case "EditStatus":
                    UpdateLicenseStatus(GetUpdateLicenseParameter(invocation));
                    break;
                case "EditRatesAndWriters":
                    UpdateLicense(GetEditRateLicenseParameter(invocation));
                    break;
                case "EditIndividualWriterRates":
                    UpdateLicense(GetEditIndividualRatesParameter(invocation));
                    break;
                case "DeleteLicenseProduct":
                    UpdateLicense(GetDeleteLicenseProductLicenseId(invocation));
                    UpdateProduct(GetDeleteLicenseProductPId(invocation));
                    break;
                case "SaveProduct":
                    UpdateProduct(GetSaveProductParameter(invocation));
                    break;
                case "SaveProductLink":
                    UpdateProduct(GetAddTracksParameter(invocation));
                    break;
                case "UpdateProductPriority":
                    UpdateProduct(GetPriorityProductIdParameter(invocation));
                    break;
                case "EditLicenseStatusReport":
                    UpdateLicense(GetUpdateLicenseStatusReport(invocation));
                    break;
                case "CloneLicense":
                    UpdateLicense(GetCloneLicenseParameter(invocation));
                    break;
                case "EditWriterConsent":
                    UpdateLicense(GetEditWriterConsentParam(invocation));
                    break;
            }
            
        }

        private int GetEditWriterConsentParam(IInvocation invocation)
        {
            var param = (EditWriterConsentSaveRequest) invocation.Arguments[0];
            return param.LicenseId;
        }

        private int GetUpdateLicenseStatusReport(IInvocation invocation)
        {
            var param = (int) invocation.Arguments[0];
            return param;
        }
        private int GetAddTracksParameter(IInvocation invocation)
        {
            var request = (ProductLink) invocation.Arguments[0];
            return request.productId;
        }

        private int GetSaveProductParameter(IInvocation invocation)
        {
            var result = (AddProductResult) invocation.ReturnValue;
            var request = (ProductHeader) invocation.Arguments[0];
            return request.Id>0 ? (int)request.Id : (int)result.productHeader.Id;
        }
        private int GetDeleteLicenseProductPId(IInvocation invocation)
        {
            return (int)invocation.Arguments[1];
        }
        private int GetDeleteLicenseProductLicenseId(IInvocation invocation)
        {
            return(int) invocation.Arguments[0];
        }
        private int GetEditIndividualRatesParameter(IInvocation invocation)
        {
            var request = (List < EditRatesSaveRequest >) invocation.Arguments[0];
            return request.FirstOrDefault().LicenseId;
        }
        private int GetEditRateLicenseParameter(IInvocation invocation)
        {
            var request = (EditRatesSaveRequest) invocation.Arguments[0];
            return request.LicenseId;
        }
        private List<int> GetUpdateAssigneeParameter(IInvocation invocation)
        {
            var request = (UpdateLicenseAssigneeRequest) invocation.Arguments[0];
            return request.LicenseIds;
        }
        private int GetUpdateLicenseParameter(IInvocation invocation)
        {
            var request = (License) invocation.Arguments[0];
            return request.LicenseId;
        }
        private int GetLicenseAddProductConfiguration(IInvocation invocation)
        {
            var request = (UpdateLicenseProductConfigurationRequest) invocation.Arguments[0];
            return request.licenseId > 0 ? request.licenseId : request.addTolicenseId;
        }
        private int GetAddMethodParameter(IInvocation invocation)
        {
           var returnedLicense = (License)
            invocation.ReturnValue;
            return returnedLicense.LicenseId;
        }

        private int GetLicenseDeleteProductConfiguration(IInvocation invocation)
        {
            var request = (UpdateLicenseProductConfigurationRequest)invocation.Arguments[0];
            return request.licenseId > 0 ? request.licenseId : request.addTolicenseId;
        }

        private int GetCloneLicenseParameter(IInvocation invocation)
        {
            var returnedLicense = (CloneLicenseResult)
             invocation.ReturnValue;
            return returnedLicense.licenseId;
        }

        private int GetPriorityProductIdParameter(IInvocation invocation)
        {
            var productPriorityRequest = (UpdatePriorityRequest) invocation.Arguments[0];
            return productPriorityRequest.id;
        }

        private void UpdateProduct(int productId)
        {
            _licenseSolrManager.UpdateProduct(productId);
        }
        private void UpdateLicense(int licenseId)
        {
            if (licenseId==-1)
            {
                return;
            }
            _licenseSolrManager.UpdateLicense(licenseId);
        }

        private void UpdateLicenseStatus(int licenseId)
        {
            _licenseSolrManager.UpdateLicenseStatus(licenseId);
        }
        private void UpdateAssignee(List<int> licensesId)
        {
            _licenseSolrManager.UpdateLicenseAssignee(licensesId);
        }
    }
}
