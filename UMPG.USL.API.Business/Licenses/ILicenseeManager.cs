using System.Collections.Generic;
using UMPG.USL.API.Data.ContactData;
using UMPG.USL.Models;
using UMPG.USL.Models.LicenseModel;

namespace UMPG.USL.API.Business.Licenses
{
    public interface ILicenseeManager
    {
        List<Licensee> GetAll();

        //Licensee Add(Licensee licensee);

        Licensee Get(int id);

        List<Licensee> Search(string query);

        PagedResponse<Licensee> GetLicensees(LicenseeAdminRequest request);

        Licensee AddLicensee(AddLicenseeRequest request);

        Licensee EditLicensee(Licensee request);

        bool DeleteLicensee(Licensee licensee);
    }
}
