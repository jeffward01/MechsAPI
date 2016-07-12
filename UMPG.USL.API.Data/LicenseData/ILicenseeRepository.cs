using System.Collections.Generic;
using UMPG.USL.API.Data.ContactData;
using UMPG.USL.Models;
using UMPG.USL.Models.LicenseModel;

namespace UMPG.USL.API.Data.LicenseData
{
    public interface ILicenseeRepository
    {
        Licensee Add(Licensee licensee);

        Licensee Get(int Id);

        List<Licensee> GetAll();

        List<Licensee> Search(string query);
        PagedResponse<Licensee> GetLicensees(LicenseeAdminRequest request);
        Licensee EditLicensee(Licensee request);
    }
}
