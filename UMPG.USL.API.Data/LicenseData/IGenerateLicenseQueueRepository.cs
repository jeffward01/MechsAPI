using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.Models.LicenseGenerate;

namespace UMPG.USL.API.Data.LicenseData
{
    public interface IGenerateLicenseQueueRepository
    {
        IList<GenerateLicenseQueue> GetAllInProcessLicenses();
        GenerateLicenseQueue GetGenerateLicenseQueueById(int id);
        void Delete(GenerateLicenseQueue indexQueueItem);
        IList<GenerateLicenseQueue> GetAllUnProcessLicenses();
        IList<GenerateLicenseQueue> GetAllFailed();
        List<GenerateLicenseQueue> GetByLicenseId(int licenseId);
        void Update(GenerateLicenseQueue generateLicenseQueue);
    }
}
