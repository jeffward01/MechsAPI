using UMPG.USL.Models.LicenseGenerate;
using UMPG.USL.API.Data.LicenseData;
using System.Collections.Generic;
using UMPG.USL.Models.Enums;
namespace UMPG.USL.API.Business.Licenses
{
    using UMPG.USL.Models.LicenseModel;

    public class GenerateLicenseManager : IGenerateLicenseManager
    {
        private readonly IGenerateLicenseQueueRepository _generateLicenseQueueRepository;
        
        public GenerateLicenseManager(IGenerateLicenseQueueRepository generateLicenseQueueRepository)
        {
            _generateLicenseQueueRepository = generateLicenseQueueRepository;
        }

        public void UpdateGenerateLicenseStatus(LicenseUserAction data)
        {
            List<GenerateLicenseQueue> generateLicenseQueue = this.GetByLicenseId(data.licenseId);
            foreach (var licenseQueue in generateLicenseQueue)
            {
                switch (data.userAction)
                {
                    case 1:
                        licenseQueue.UserAction = (int)UserActionStatus.OnIssueStateUserPressVoidButton;
                        break;
                    case 2:
                        licenseQueue.UserAction = (int)UserActionStatus.OnIssueStateUserPressReverifyButton;
                        break;
                    case 3:
                        licenseQueue.UserAction = (int)UserActionStatus.OnIssueStateUserExecutesTheLicenseManually;
                        break;
                }
                
                this.Update(licenseQueue);
            }

            
        }

        public List<GenerateLicenseQueue> GetByLicenseId(int licenseId)
        {
            return _generateLicenseQueueRepository.GetByLicenseId(licenseId);
        }

        public void Update(GenerateLicenseQueue generateLicenseQueue)
        {
            _generateLicenseQueueRepository.Update(generateLicenseQueue);
        }
    }
}